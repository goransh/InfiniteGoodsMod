using ColossalFramework;
using ICities;
using InfiniteGoodsMod.Settings;

namespace InfiniteGoodsMod {
    public class GoodsMonitor : ThreadingExtensionBase {
        private readonly BuildingManager buildingManager;
        private readonly SimulationManager simulationManager;

        private readonly Settings.Settings settings;

        public GoodsMonitor() {
            buildingManager = Singleton<BuildingManager>.instance;
            simulationManager = Singleton<SimulationManager>.instance;
            settings = Settings.Settings.GetInstance();
        }

        /// <summary>
        ///     Every simulation tick, a portion of the buildings in the game will be filled with resources.
        ///     The building type is checked and if the building is a commercial, shelter or industrial building,
        ///     the building will be filled with goods (if the setting for the goods type is activated).
        /// </summary>
        public override void OnAfterSimulationTick() {
            var buildingId = (ushort)(simulationManager.m_currentTickIndex % 1000);
            var bufferSize = buildingManager.m_buildings.m_buffer.Length;

            var debug = settings[SettingId.Debug];

            for (; buildingId < bufferSize; buildingId += 1000) {
                foreach (var transfer in GoodsTransfer.GoodsTransfers) {
                    if (settings[transfer.Id]) {
                        transfer.TransferIfMatch(buildingId, debug);
                    }
                }
            }
        }
    }
}
