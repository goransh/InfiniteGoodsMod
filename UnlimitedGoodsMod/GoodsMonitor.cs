using ColossalFramework;
using ICities;
using static InfiniteGoodsMod.Setting;
using static TransferManager.TransferReason;

namespace InfiniteGoodsMod {
    public class GoodsMonitor : ThreadingExtensionBase {
        private readonly BuildingManager buildingManager;
        private readonly SimulationManager simulationManager;

        private static readonly Setting[] industrySettings = {
            SpecializedOil,
            SpecializedCoal,
            SpecializedGrain,
            SpecializedLogs,
            GenericPetrol,
            GenericOre,
            GenericFood,
            GenericLumber
        };

        private static readonly TransferManager.TransferReason[] industryTransferReasons = {
            Oil, Coal, Grain, Logs, Petrol, Ore, Food, Lumber
        };

        /// <summary>
        /// The default amount of goods to transfer each time. 
        /// A relatively arbitrary number.
        /// </summary>
        private const int DefaultTransferAmount = 100000;

        private readonly Settings settings;

        /// <summary>
        /// Modifying materials (adding goods to a building) is done by passing this int as a reference.
        /// The actual transferred amount is then subtracted from this variable, thus this variable must 
        /// reset to the DefaultTransferAmount before each call or the mod will stop working after a while.
        /// </summary>
        private int transferAmount = DefaultTransferAmount;

        public GoodsMonitor() {
            buildingManager = Singleton<BuildingManager>.instance;
            simulationManager = Singleton<SimulationManager>.instance;
            settings = Settings.GetInstance();
        }

        /// <summary>
        /// Every simulaion tick, a portion of the buildings in the game will be filled with resources.
        /// The building type is checked and if the building is a commercial or industrial buidling, 
        /// the building will be filled with goods (if the setting for the goods type is activated).
        /// </summary>
        public override void OnAfterSimulationTick() {
            for (var buildingId = (ushort) (simulationManager.m_currentTickIndex % 1000);
                buildingId < buildingManager.m_buildings.m_buffer.Length;
                buildingId += 1000) {
                var building = buildingManager.m_buildings.m_buffer[buildingId];

                var info = building.Info;
                if (info == null) continue;

                BuildingAI ai = info.GetAI() as BuildingAI;
                if (ai == null) continue;

                if (settings.Get(Setting.CommercialGoods) && ai is CommercialBuildingAI) {
                    //fill commercial builing with goods
                    FillBuilding(ref buildingId, ai, TransferManager.TransferReason.Goods);
                }else if (settings.Get(Setting.ShelterGoods) && ai is ShelterAI) {
                    //fill shleters with goods
                    FillBuilding(ref buildingId, ai, TransferManager.TransferReason.Goods);
                }
                else if (ai is IndustrialBuildingAI) {
                    //fill industrial building with (all) industrial materials
                    FillIndustrialBuilding(ref buildingId, ai);
                }
            }
        }

        /// <summary>
        /// Fill a building with goods.
        /// </summary>
        /// <param name="buildingId">The id of the building</param>
        /// <param name="buildingAi">The ai of the building</param>
        /// <param name="goodsType">The type of goods to transfer</param>
        private void FillBuilding(ref ushort buildingId, BuildingAI buildingAi,
            TransferManager.TransferReason goodsType) {
            transferAmount = DefaultTransferAmount;
            buildingAi.ModifyMaterialBuffer(buildingId, ref buildingManager.m_buildings.m_buffer[buildingId], goodsType,
                ref transferAmount);
        }

        /// <summary>
        /// Fill an industrial building with industrial materials (depending on the settings).
        /// </summary>
        /// <param name="buildingId">The id of the building</param>
        /// <param name="buildingAi">The ai of the building</param>
        private void FillIndustrialBuilding(ref ushort buildingId, BuildingAI buildingAi) {
            for (int i = 0; i < industrySettings.Length; ++i) {
                if (settings.Get(industrySettings[i]))
                    FillBuilding(ref buildingId, buildingAi, industryTransferReasons[i]);
            }
        }
    }
}