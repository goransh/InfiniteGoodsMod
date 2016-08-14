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
            BuildingInfo info;
            int goods;

            for (var i = (ushort)(simulationManager.m_currentTickIndex % 1000); i < buildingManager.m_buildings.m_buffer.Length; i += 1000)
            {
                info = buildingManager.m_buildings.m_buffer[i].Info;
                if (info == null) continue;
                
                ai = info.GetAI();
                if (ai == null) continue;

                if (ai is CommercialBuildingAI)
                {
                    goods = 100000;
                    ((CommercialBuildingAI)ai).ModifyMaterialBuffer(i, ref buildingManager.m_buildings.m_buffer[i], TransferManager.TransferReason.Goods, ref goods);

                }
            }
        }
    }
}
