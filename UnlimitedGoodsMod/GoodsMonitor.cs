using ColossalFramework;
using ICities;

namespace InfiniteGoodsMod
{
    public class GoodsMonitor : ThreadingExtensionBase
    {
        private readonly BuildingManager buildingManager;
        private readonly SimulationManager simulationManager;

        public GoodsMonitor()
        {
            buildingManager = Singleton<BuildingManager>.instance;
            simulationManager = Singleton<SimulationManager>.instance;
        }

        public override void OnAfterSimulationTick()
        {
            PrefabAI ai;
            int amount;
            for (var i = (ushort)(simulationManager.m_currentTickIndex % 1000); i < buildingManager.m_buildings.m_buffer.Length; i += 1000)
            {
                ai = buildingManager.m_buildings.m_buffer[i].Info.GetAI();

                if (ai == null) continue; 

                if (ai is CommercialBuildingAI)
                {
                    amount = 100000;
                    ((CommercialBuildingAI)ai).ModifyMaterialBuffer(i, ref buildingManager.m_buildings.m_buffer[i], TransferManager.TransferReason.Goods, ref amount);
                }
            }
        }
    }
}
