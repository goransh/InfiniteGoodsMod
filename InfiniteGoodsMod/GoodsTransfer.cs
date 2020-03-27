using System;
using System.Collections.Generic;
using ColossalFramework;
using UnityEngine;
using static ItemClass;
using static TransferManager;

namespace InfiniteGoodsMod {
    public class GoodsTransfer {
        public static readonly IList<GoodsTransfer> GoodsTransfers = new List<GoodsTransfer>();

        // Commercial
        public static readonly GoodsTransfer CommercialGoods = new GoodsTransfer {
            Id = "CommercialGoods",
            Service = Service.Commercial,
            Material = TransferReason.Goods,
            AnySubService = true
        };

        public static readonly GoodsTransfer CommercialLuxuryProducts = new GoodsTransfer {
            Id = "CommercialLuxuryProducts",
            Service = Service.Commercial,
            Material = TransferReason.LuxuryProducts,
            AnySubService = true
        };

        // Specialized industry
        public static readonly GoodsTransfer SpecializedOil = new GoodsTransfer {
            Id = "SpecializedIndustryOil",
            Service = Service.Industrial,
            SubService = SubService.IndustrialOil,
            Material = TransferReason.Oil
        };

        public static readonly GoodsTransfer SpecializedOre = new GoodsTransfer {
            Id = "SpecializedIndustryOre",
            Service = Service.Industrial,
            SubService = SubService.IndustrialOre,
            Material = TransferReason.Ore
        };

        public static readonly GoodsTransfer SpecializedGrain = new GoodsTransfer {
            Id = "SpecializedIndustryGrain",
            Service = Service.Industrial,
            SubService = SubService.IndustrialFarming,
            Material = TransferReason.Grain
        };

        public static readonly GoodsTransfer SpecializedLogs = new GoodsTransfer {
            Id = "SpecializedIndustryLogs",
            Service = Service.Industrial,
            SubService = SubService.IndustrialForestry,
            Material = TransferReason.Logs
        };

        // Generic industry
        public static readonly GoodsTransfer GenericPetrol = new GoodsTransfer {
            Id = "GenericIndustryPetrol",
            Service = Service.Industrial,
            SubService = SubService.IndustrialGeneric,
            Material = TransferReason.Petrol
        };

        public static readonly GoodsTransfer GenericCoal = new GoodsTransfer {
            Id = "GenericIndustryCoal",
            Service = Service.Industrial,
            SubService = SubService.IndustrialGeneric,
            Material = TransferReason.Coal
        };

        public static readonly GoodsTransfer GenericFood = new GoodsTransfer {
            Id = "GenericIndustryFood",
            Service = Service.Industrial,
            SubService = SubService.IndustrialGeneric,
            Material = TransferReason.Food
        };

        public static readonly GoodsTransfer GenericLumber = new GoodsTransfer {
            Id = "GenericIndustryLumber",
            Service = Service.Industrial,
            SubService = SubService.IndustrialGeneric,
            Material = TransferReason.Lumber
        };

        // Shelters
        public static readonly GoodsTransfer ShelterGoods = new GoodsTransfer {
            Id = "ShelterGoods",
            Service = Service.Disaster,
            SubService = SubService.None,
            Material = TransferReason.Goods
        };

        // Industries DLC raw
        public static readonly GoodsTransfer PloppedIndustryRawOil = new GoodsTransfer {
            Id = "PloppedIndustryOil",
            Service = Service.PlayerIndustry,
            SubService = SubService.PlayerIndustryOil,
            Material = TransferReason.Oil
        };

        public static readonly GoodsTransfer PloppedIndustryRawOre = new GoodsTransfer {
            Id = "PloppedIndustryOre",
            Service = Service.PlayerIndustry,
            SubService = SubService.PlayerIndustryOre,
            Material = TransferReason.Ore
        };

        public static readonly GoodsTransfer PloppedIndustryRawFarming = new GoodsTransfer {
            Id = "PloppedIndustryGrain",
            Service = Service.PlayerIndustry,
            SubService = SubService.PlayerIndustryFarming,
            Material = TransferReason.Grain
        };

        public static readonly GoodsTransfer PloppedIndustryRawForestry = new GoodsTransfer {
            Id = "PloppedIndustryLogs",
            Service = Service.PlayerIndustry,
            SubService = SubService.PlayerIndustryForestry,
            Material = TransferReason.Logs
        };

        // Industries DLC processed
        public static readonly GoodsTransfer ProcessedAnimalProducts = new GoodsTransfer {
            Id = "UniqueIndustryAnimalProducts",
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.AnimalProducts
        };

        public static readonly GoodsTransfer ProcessedFlours = new GoodsTransfer {
            Id = "UniqueIndustryFlours",
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Flours
        };

        public static readonly GoodsTransfer ProcessedPaper = new GoodsTransfer {
            Id = "UniqueIndustryPaper",
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Paper
        };

        public static readonly GoodsTransfer ProcessedPlanedTimber = new GoodsTransfer {
            Id = "UniqueIndustryPlanedTimber",
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.PlanedTimber
        };

        public static readonly GoodsTransfer ProcessedPetroleum = new GoodsTransfer {
            Id = "UniqueIndustryPetroleum",
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Petroleum
        };

        public static readonly GoodsTransfer ProcessedPlastics = new GoodsTransfer {
            Id = "UniqueIndustryPlastics",
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Plastics
        };

        public static readonly GoodsTransfer ProcessedGlass = new GoodsTransfer {
            Id = "UniqueIndustryGlass",
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Glass
        };

        public static readonly GoodsTransfer ProcessedMetals = new GoodsTransfer {
            Id = "UniqueIndustryMetals",
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Metals
        };

        public static readonly GoodsTransfer ProcessedCrops = new GoodsTransfer {
            Id = "UniqueIndustryGrain",
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Grain
        };

        // Sunset Harbor DLC Fishing Industry
        public static readonly GoodsTransfer FishingHarbor = new GoodsTransfer {
            Id = "FishingHarbor",
            Service = Service.Fishing,
            SubService = SubService.None,
            Material = TransferReason.Fish,
            AiType = typeof(FishingHarborAI)
        };

        public static readonly GoodsTransfer FishingFarm = new GoodsTransfer {
            Id = "FishingFarm",
            Service = Service.Fishing,
            SubService = SubService.None,
            Material = TransferReason.Fish,
            AiType = typeof(FishFarmAI)
        };

        public static readonly GoodsTransfer FishingMarket = new GoodsTransfer {
            Id = "FishingMarket",
            Service = Service.Fishing,
            SubService = SubService.None,
            Material = TransferReason.Fish,
            AiType = typeof(MarketAI)
        };

        public static readonly GoodsTransfer FishingProcessing = new GoodsTransfer {
            Id = "FishingProcessing",
            Service = Service.Fishing,
            SubService = SubService.None,
            Material = TransferReason.Fish,
            AiType = typeof(ProcessingFacilityAI)
        };

        private static readonly BuildingManager BuildingManager = Singleton<BuildingManager>.instance;

        /// <summary>
        /// The default amount of goods to transfer each time.
        /// A relatively arbitrary number.
        /// </summary>
        private const int TransferAmount = 100000;

        /// <summary>
        /// The Service to match for this transfer to be executed.
        /// </summary>
        private Service Service { set; get; } = Service.None;
        /// <summary>
        /// The SubService to match for this transfer to be executed.
        /// </summary>
        private SubService SubService { set; get; } = SubService.None;
        /// <summary>
        /// If true, the matcher won't check if the building's SubService matches before execution.
        /// </summary>
        private bool AnySubService { set; get; } = false;
        /// <summary>
        /// (Optional) The type of the building's AI to match for this transfer to be executed. null means any.
        /// </summary>
        private Type AiType { set; get; } = null;
        /// <summary>
        /// The TransferReason to execute (the material to transfer).
        /// </summary>
        private TransferReason Material { set; get; } = TransferReason.None;
        /// <summary>
        /// Id of this transfer, must be unique. Used in the settings file so any changes will break old saves.
        /// </summary>
        public string Id { private set; get; }

        private GoodsTransfer() {
            GoodsTransfers.Add(this);
        }

        public bool Matches(ushort buildingId) {
            Building building = BuildingManager.m_buildings.m_buffer[buildingId];
            var info = building.Info;
            if (info == null) return false;

            if (AiType != null && !AiType.IsInstanceOfType(info.GetAI())) return false;

            return Service.Equals(info.GetService()) &&
                   (AnySubService || SubService.Equals(info.GetSubService()));
        }

        public void Transfer(ushort buildingId) {
            var building = BuildingManager.m_buildings.m_buffer[buildingId];

            var info = building.Info;
            if (info == null) return;

            var ai = info.GetAI() as BuildingAI;
            if (ai == null) return;

            int amount = TransferAmount;
            ai.ModifyMaterialBuffer(
                buildingId,
                ref BuildingManager.m_buildings.m_buffer[buildingId],
                Material,
                ref amount
            );

            if (ModIdentity.DebugMode) {
                Debug.Log(
                    $"{Material} ({amount}) => \"{building.Info.name}\" {building.Info.GetService()}->{building.Info.GetSubService()} ({ai.GetType()})");
            }
        }

        public void TransferIfMatch(ushort buildingId) {
            if (Matches(buildingId)) Transfer(buildingId);
        }
    }
}
