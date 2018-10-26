using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ColossalFramework;
using static ItemClass;
using static TransferManager;

namespace InfiniteGoodsMod {
    public class GoodsTransfer {
        private static readonly Func<BuildingAI, TransferReason> ProcessingMaterialFunc = ai => {
            ProcessingFacilityAI pAi = ai as ProcessingFacilityAI;
            return pAi != null ? pAi.m_inputResource1 : TransferReason.None;
        };

        // Commercial
        public static readonly GoodsTransfer CommercialGoods =
            new GoodsTransfer(Service.Commercial, SubService.None, TransferReason.Goods);

        public static readonly GoodsTransfer CommercialLuxuryProducts =
            new GoodsTransfer(Service.Commercial, SubService.None, TransferReason.LuxuryProducts);

        // Specialized industry
        public static readonly GoodsTransfer SpecializedOil =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialOil, TransferReason.Oil);

        public static readonly GoodsTransfer SpecializedOre =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialOre, TransferReason.Ore);

        public static readonly GoodsTransfer SpecializedGrain =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialFarming, TransferReason.Grain);

        public static readonly GoodsTransfer SpecializedLogs =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialForestry, TransferReason.Logs);

        // Generic industry
        public static readonly GoodsTransfer GenericPetrol =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Petrol);

        public static readonly GoodsTransfer GenericCoal =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Coal);

        public static readonly GoodsTransfer GenericFood =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Food);

        public static readonly GoodsTransfer GenericLumber =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Lumber);

        // Shelters
        public static readonly GoodsTransfer ShelterGoods =
            new GoodsTransfer(Service.Disaster, SubService.None, TransferReason.Goods);

        // Industries DLC raw
        public static readonly GoodsTransfer PlayerIndustryRawOil =
            new GoodsTransfer(Service.PlayerIndustry, SubService.PlayerIndustryOil, ProcessingMaterialFunc);

        public static readonly GoodsTransfer PlayerIndustryRawOre =
            new GoodsTransfer(Service.PlayerIndustry, SubService.PlayerIndustryOre, ProcessingMaterialFunc);

        public static readonly GoodsTransfer PlayerIndustryRawFarming =
            new GoodsTransfer(Service.PlayerIndustry, SubService.PlayerIndustryFarming, ProcessingMaterialFunc);

        public static readonly GoodsTransfer PlayerIndustryRawForestry =
            new GoodsTransfer(Service.PlayerIndustry, SubService.PlayerIndustryForestry, ProcessingMaterialFunc);

        // Industries DLC processed
        public static readonly GoodsTransfer ProcessedAnimalProducts =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.AnimalProducts);

        public static readonly GoodsTransfer ProcessedFlours =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Flours);

        public static readonly GoodsTransfer ProcessedPaper =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Paper);

        public static readonly GoodsTransfer ProcessedPlanedTimber =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.PlanedTimber);

        public static readonly GoodsTransfer ProcessedPetroleum =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Petroleum);

        public static readonly GoodsTransfer ProcessedPlastics =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Plastics);

        public static readonly GoodsTransfer ProcessedGlass =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Glass);

        public static readonly GoodsTransfer ProcessedMetals =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Metals);

        public static readonly GoodsTransfer ProcessedGrain =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Grain);


        public static readonly IList<GoodsTransfer> GoodsTransfers = new ReadOnlyCollection<GoodsTransfer>(
            new List<GoodsTransfer> {
                CommercialGoods, CommercialLuxuryProducts,
                SpecializedOil, SpecializedOre, SpecializedGrain, SpecializedLogs,
                GenericPetrol, GenericCoal, GenericFood, GenericLumber,
                ShelterGoods,
                PlayerIndustryRawOil, PlayerIndustryRawOre, PlayerIndustryRawFarming, PlayerIndustryRawForestry,
                ProcessedAnimalProducts, ProcessedFlours, ProcessedPaper, ProcessedPlanedTimber,
                ProcessedPetroleum, ProcessedPlastics, ProcessedGlass, ProcessedMetals, ProcessedGrain
            });


        /// <summary>
        /// The default amount of goods to transfer each time. 
        /// A relatively arbitrary number.
        /// </summary>
        private const int DefaultTransferAmount = 100000;

        public Service Service { get; }
        public SubService SubService { get; } // None == any
        public TransferReason Material { get; }
        public int TransferAmount { get; }
        public string Id { get; }
        private Func<BuildingAI, TransferReason> MaterialFunc;

        private readonly BuildingManager buildingManager;

        internal GoodsTransfer(Service service, SubService subService, TransferReason material,
            int transferAmount = DefaultTransferAmount) {
            this.Service = service;
            this.SubService = subService;
            this.Material = material;
            this.TransferAmount = transferAmount;
            this.Id = GenerateId();
            this.buildingManager = Singleton<BuildingManager>.instance;
        }

        internal GoodsTransfer(Service service, SubService subService, Func<BuildingAI, TransferReason> materialFunc)
            : this(service, subService, TransferReason.None) {
            this.MaterialFunc = materialFunc;
        }

        public bool Matches(ushort buildingId) {
            Building building = buildingManager.m_buildings.m_buffer[buildingId];
            var info = building.Info;
            if (info == null)
                return false;
            return Service.Equals(info.GetService()) &&
                   (SubService.Equals(SubService.None) || SubService.Equals(info.GetSubService()));
        }

        public void Transfer(ushort buildingId) {
            Building building = buildingManager.m_buildings.m_buffer[buildingId];
            BuildingAI ai = building.Info?.GetAI() as BuildingAI;
            if (ai == null)
                return;

            TransferReason material = Material.Equals(TransferReason.None) ? MaterialFunc.Invoke(ai) : Material;
            if (material.Equals(TransferReason.None))
                return;

            int amount = TransferAmount;
            ai.ModifyMaterialBuffer(buildingId, ref buildingManager.m_buildings.m_buffer[buildingId],
                material, ref amount);

            Utils.Log(
                $"{material} ({TransferAmount - amount}) => \"{building.Info.name}\" {building.Info.GetService()}->{building.Info.GetSubService()} ({ai.GetType()})");
        }

        public void TransferIfMatch(ushort buildingId) {
            if (Matches(buildingId))
                Transfer(buildingId);
        }

        private string GenerateId() {
            switch (Service) {
                case Service.Commercial:
                    return Enum.GetName(typeof(Service), Service) + MaterialToString();
                case Service.Industrial:
                    bool generic = SubService.Equals(SubService.IndustrialGeneric);
                    return (generic ? "Generic" : "Specialized") + MaterialToString();
                case Service.Disaster:
                    return "ShelterGoods";
                case Service.PlayerIndustry:
                    return Enum.GetName(typeof(Service), Service) +
                           (Material.Equals(TransferReason.None)
                               ? Enum.GetName(typeof(SubService), SubService)
                               : MaterialToString());
            }

            throw new Exception("No valid ID for the GoodsTransfer.");
        }

        private string MaterialToString() {
            return Enum.GetName(typeof(TransferReason), Material);
        }
    }
}