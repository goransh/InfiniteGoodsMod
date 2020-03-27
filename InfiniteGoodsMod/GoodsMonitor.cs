using ColossalFramework;
using ICities;
using System;
using UnityEngine;

namespace InfiniteGoodsMod {
    public class GoodsMonitor : ThreadingExtensionBase {
        private readonly BuildingManager buildingManager;
        private readonly SimulationManager simulationManager;

        private readonly Settings settings;

        public GoodsMonitor() {
            buildingManager = Singleton<BuildingManager>.instance;
            simulationManager = Singleton<SimulationManager>.instance;
            settings = Settings.GetInstance();
        }

        /// <summary>
        /// Every simulation tick, a portion of the buildings in the game will be filled with resources.
        /// The building type is checked and if the building is a commercial, shelter or industrial building,
        /// the building will be filled with goods (if the setting for the goods type is activated).
        /// </summary>
        public override void OnAfterSimulationTick() {
            for (var buildingId = (ushort) (simulationManager.m_currentTickIndex % 1000);
                buildingId < buildingManager.m_buildings.m_buffer.Length;
                buildingId += 1000) {
                foreach (GoodsTransfer transfer in GoodsTransfer.GoodsTransfers) {
                    if (settings.Get(transfer.Id))
                        transfer.TransferIfMatch(buildingId);
                }
            }
        }
    }
}
