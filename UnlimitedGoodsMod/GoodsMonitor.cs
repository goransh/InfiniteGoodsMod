using ColossalFramework;
using ICities;

using static TransferManager.TransferReason;

namespace InfiniteGoodsMod
{
    public class GoodsMonitor : ThreadingExtensionBase
    {
        private readonly BuildingManager buildingManager;
        private readonly SimulationManager simulationManager;

        private Settings settings;

        public GoodsMonitor()
        {
            buildingManager = Singleton<BuildingManager>.instance;
            simulationManager = Singleton<SimulationManager>.instance;
            settings = Settings.getInstance();
        }

        public override void OnAfterSimulationTick()
        {

            Building building;
            BuildingInfo info;
            BuildingAI ai;

            for (var i = (ushort)(simulationManager.m_currentTickIndex % 1000); i < buildingManager.m_buildings.m_buffer.Length; i += 1000)
            {
                building = buildingManager.m_buildings.m_buffer[i];

                info = building.Info;
                if (info == null) continue;

                ai = info.GetAI() as BuildingAI;
                if (ai == null) continue;


                if (ai is CommercialBuildingAI)
                {
                    FillCommercialBuilding(i, (CommercialBuildingAI)ai);
                }
                else if (ai is IndustrialBuildingAI)
                {
                    FillIndustrialBuilding(i, (IndustrialBuildingAI)ai);
                }


            }
        }

        private void FillCommercialBuilding(ushort i, CommercialBuildingAI ai)
        {
            if (settings.get(Goods))
            {
                int goods = 100000;
                ai.ModifyMaterialBuffer(i, ref buildingManager.m_buildings.m_buffer[i], Goods, ref goods);
            }

        }

        private void FillIndustrialBuilding(ushort i, IndustrialBuildingAI ai)
        {
            int value;
            foreach (TransferManager.TransferReason reason in Settings.supportedIndustry)
            {
                if (settings.get(reason))
                {
                    value = 10000;
                    ai.ModifyMaterialBuffer(i, ref buildingManager.m_buildings.m_buffer[i], reason, ref value);
                }
            }
        }

    }
}
