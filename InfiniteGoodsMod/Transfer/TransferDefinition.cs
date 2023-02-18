using System;
using ColossalFramework;
using InfiniteGoodsMod.Settings;
using UnityEngine;
using static ItemClass;
using static TransferManager;

namespace InfiniteGoodsMod.Transfer {
    public interface ITransferDefinition {
        SettingId Id { get; }
        TransferReason Material { get; }
        void TransferIfMatch(ushort buildingId, bool debug);
    }

    public class TransferDefinition<TBuildingAI> : ITransferDefinition where TBuildingAI : BuildingAI {
        /// <summary>
        ///     The default amount of goods to transfer each time.
        ///     A relatively arbitrary number.
        /// </summary>
        private const int TransferAmount = 1_000_000;

        /// <summary>
        ///     The Service to match for this transfer to be executed.
        /// </summary>
        public Service Service { get; internal set; } = Service.None;

        /// <summary>
        ///     The SubService to match for this transfer to be executed.
        /// </summary>
        public SubService SubService { get; internal set; } = SubService.None;

        /// <summary>
        ///     If true, the matcher won't check if the building's SubService matches before execution.
        /// </summary>
        public bool AnySubService { get; internal set; } = false;

        /// <summary>
        ///     The TransferReason to execute (the material to transfer).
        /// </summary>
        public TransferReason Material { get; internal set; } = TransferReason.None;

        /// <summary>
        ///     Id of this transfer, must be unique. Used in the settings file so any changes will break old saves.
        /// </summary>
        public SettingId Id { get; internal set; }

        /// <summary>
        ///     A custom transfer condition check for this transfer definition. Defaults to true.
        /// </summary>
        public Func<ushort, TBuildingAI, bool> TransferCondition { get; internal set; } = DefaultTransferCondition;

        private static bool DefaultTransferCondition(ushort buildingId, TBuildingAI ai) => true;

        /// <summary>
        ///     Get the building info for a building with <paramref name="buildingId" />.
        /// </summary>
        /// <param name="buildingId">The building ID.</param>
        /// <returns>The <see cref="BuildingInfo" /> or null if it does not exist.</returns>
        private static BuildingInfo FindBuildingInfo(ushort buildingId) {
            var building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingId];
            return building.Info;
        }

        /// <summary>
        ///     Find the building AI of a <see cref="BuildingInfo" />. Returns null if the AI type does is not <c>TBuildingAI</c>.
        /// </summary>
        /// <param name="buildingInfo">The building to find AI for.</param>
        /// <returns>The AI or null</returns>
        private static TBuildingAI FindBuildingAi(BuildingInfo buildingInfo) {
            return buildingInfo != null ? buildingInfo.GetAI() as TBuildingAI : null;
        }

        private bool BuildingMatchesService(BuildingInfo buildingInfo) =>
            Service.Equals(buildingInfo.GetService())
            && (AnySubService || SubService.Equals(buildingInfo.GetSubService()));

        private void TransferGoods(
            ushort buildingId,
            BuildingInfo info,
            TBuildingAI ai,
            bool debug
        ) {
            var amount = TransferAmount;
            ai.ModifyMaterialBuffer(
                buildingId,
                ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingId],
                Material,
                ref amount
            );

            if (debug) {
                ai.GetMaterialAmount(
                    buildingId,
                    ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingId],
                    Material,
                    out var currentAmount,
                    out var max
                );
                Debug.Log(
                    $"Transferred {amount:N0} ({currentAmount}/{max}) {Material} => \"{info.name}\" ({buildingId}) {info.GetService()}/{info.GetSubService()} ({ai.GetType().Name})"
                );
            }
        }

        public void TransferIfMatch(ushort buildingId, bool debug) {
            var info = FindBuildingInfo(buildingId);

            // Checks if the building service and sub service types match the properties of this transfer type
            if (info == null || !BuildingMatchesService(info)) {
                return;
            }

            // Gets the building AI, will be null if the AI type does not match TBuildingAI
            var ai = FindBuildingAi(info);
            if (ai == null) {
                return;
            }

            if (TransferCondition(buildingId, ai)) {
                TransferGoods(buildingId, info, ai, debug);
            }
        }
    }
}
