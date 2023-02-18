using System;
using System.Collections.Generic;
using ColossalFramework;
using InfiniteGoodsMod.Settings;
using UnityEngine;
using static ItemClass;
using static TransferManager;

namespace InfiniteGoodsMod {
    public class GoodsTransfer {
        /// <summary>
        ///     The default amount of goods to transfer each time.
        ///     A relatively arbitrary number.
        /// </summary>
        private const int TransferAmount = 100000;

        public static readonly IList<GoodsTransfer> GoodsTransfers = new List<GoodsTransfer>();

        // Commercial
        public static readonly GoodsTransfer CommercialGoods = new GoodsTransfer {
            Id = SettingId.CommercialGoods,
            Service = Service.Commercial,
            Material = TransferReason.Goods,
            AnySubService = true,
        };

        public static readonly GoodsTransfer CommercialLuxuryProducts = new GoodsTransfer {
            Id = SettingId.CommercialLuxuryProducts,
            Service = Service.Commercial,
            Material = TransferReason.LuxuryProducts,
            AnySubService = true,
        };

        // Specialized industry
        public static readonly GoodsTransfer SpecializedOil = new GoodsTransfer {
            Id = SettingId.SpecializedIndustryOil,
            Service = Service.Industrial,
            SubService = SubService.IndustrialOil,
            Material = TransferReason.Oil,
        };

        public static readonly GoodsTransfer SpecializedOre = new GoodsTransfer {
            Id = SettingId.SpecializedIndustryOre,
            Service = Service.Industrial,
            SubService = SubService.IndustrialOre,
            Material = TransferReason.Ore,
        };

        public static readonly GoodsTransfer SpecializedGrain = new GoodsTransfer {
            Id = SettingId.SpecializedIndustryGrain,
            Service = Service.Industrial,
            SubService = SubService.IndustrialFarming,
            Material = TransferReason.Grain,
        };

        public static readonly GoodsTransfer SpecializedLogs = new GoodsTransfer {
            Id = SettingId.SpecializedIndustryLogs,
            Service = Service.Industrial,
            SubService = SubService.IndustrialForestry,
            Material = TransferReason.Logs,
        };

        // Generic industry
        public static readonly GoodsTransfer GenericPetrol = new GoodsTransfer {
            Id = SettingId.GenericIndustryPetrol,
            Service = Service.Industrial,
            SubService = SubService.IndustrialGeneric,
            Material = TransferReason.Petrol,
        };

        public static readonly GoodsTransfer GenericCoal = new GoodsTransfer {
            Id = SettingId.GenericIndustryCoal,
            Service = Service.Industrial,
            SubService = SubService.IndustrialGeneric,
            Material = TransferReason.Coal,
        };

        public static readonly GoodsTransfer GenericFood = new GoodsTransfer {
            Id = SettingId.GenericIndustryFood,
            Service = Service.Industrial,
            SubService = SubService.IndustrialGeneric,
            Material = TransferReason.Food,
        };

        public static readonly GoodsTransfer GenericLumber = new GoodsTransfer {
            Id = SettingId.GenericIndustryLumber,
            Service = Service.Industrial,
            SubService = SubService.IndustrialGeneric,
            Material = TransferReason.Lumber,
        };

        // Shelters
        public static readonly GoodsTransfer ShelterGoods = new GoodsTransfer {
            Id = SettingId.ShelterGoods,
            Service = Service.Disaster,
            SubService = SubService.None,
            Material = TransferReason.Goods,
        };

        // Industries DLC raw
        public static readonly GoodsTransfer PloppedIndustryRawOil = new GoodsTransfer {
            Id = SettingId.PloppedIndustryOil,
            Service = Service.PlayerIndustry,
            SubService = SubService.PlayerIndustryOil,
            Material = TransferReason.Oil,
        };

        public static readonly GoodsTransfer PloppedIndustryRawOre = new GoodsTransfer {
            Id = SettingId.PloppedIndustryOre,
            Service = Service.PlayerIndustry,
            SubService = SubService.PlayerIndustryOre,
            Material = TransferReason.Ore,
        };

        public static readonly GoodsTransfer PloppedIndustryRawFarming = new GoodsTransfer {
            Id = SettingId.PloppedIndustryGrain,
            Service = Service.PlayerIndustry,
            SubService = SubService.PlayerIndustryFarming,
            Material = TransferReason.Grain,
        };

        public static readonly GoodsTransfer PloppedIndustryRawForestry = new GoodsTransfer {
            Id = SettingId.PloppedIndustryLogs,
            Service = Service.PlayerIndustry,
            SubService = SubService.PlayerIndustryForestry,
            Material = TransferReason.Logs,
        };

        // Industries DLC processed
        public static readonly GoodsTransfer ProcessedAnimalProducts = new GoodsTransfer {
            Id = SettingId.UniqueIndustryAnimalProducts,
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.AnimalProducts,
        };

        public static readonly GoodsTransfer ProcessedFlours = new GoodsTransfer {
            Id = SettingId.UniqueIndustryFlours,
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Flours,
        };

        public static readonly GoodsTransfer ProcessedPaper = new GoodsTransfer {
            Id = SettingId.UniqueIndustryPaper,
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Paper,
        };

        public static readonly GoodsTransfer ProcessedPlanedTimber = new GoodsTransfer {
            Id = SettingId.UniqueIndustryPlanedTimber,
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.PlanedTimber,
        };

        public static readonly GoodsTransfer ProcessedPetroleum = new GoodsTransfer {
            Id = SettingId.UniqueIndustryPetroleum,
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Petroleum,
        };

        public static readonly GoodsTransfer ProcessedPlastics = new GoodsTransfer {
            Id = SettingId.UniqueIndustryPlastics,
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Plastics,
        };

        public static readonly GoodsTransfer ProcessedGlass = new GoodsTransfer {
            Id = SettingId.UniqueIndustryGlass,
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Glass,
        };

        public static readonly GoodsTransfer ProcessedMetals = new GoodsTransfer {
            Id = SettingId.UniqueIndustryMetals,
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Metals,
        };

        public static readonly GoodsTransfer ProcessedCrops = new GoodsTransfer {
            Id = SettingId.UniqueIndustryGrain,
            Service = Service.PlayerIndustry,
            SubService = SubService.None,
            Material = TransferReason.Grain,
        };

        // Sunset Harbor DLC Fishing Industry
        public static readonly GoodsTransfer FishingHarbor = new GoodsTransfer {
            Id = SettingId.FishingHarbor,
            Service = Service.Fishing,
            SubService = SubService.None,
            Material = TransferReason.Fish,
            AiType = typeof(FishingHarborAI),
        };

        public static readonly GoodsTransfer FishingFarm = new GoodsTransfer {
            Id = SettingId.FishingFarm,
            Service = Service.Fishing,
            SubService = SubService.None,
            Material = TransferReason.Fish,
            AiType = typeof(FishFarmAI),
        };

        public static readonly GoodsTransfer FishingMarket = new GoodsTransfer {
            Id = SettingId.FishingMarket,
            Service = Service.Fishing,
            SubService = SubService.None,
            Material = TransferReason.Fish,
            AiType = typeof(MarketAI),
        };

        public static readonly GoodsTransfer FishingProcessing = new GoodsTransfer {
            Id = SettingId.FishingProcessing,
            Service = Service.Fishing,
            SubService = SubService.None,
            Material = TransferReason.Fish,
            AiType = typeof(ProcessingFacilityAI),
        };

        private static readonly BuildingManager BuildingManager = Singleton<BuildingManager>.instance;

        /// <summary>
        ///     The Service to match for this transfer to be executed.
        /// </summary>
        private Service Service { set; get; } = Service.None;

        /// <summary>
        ///     The SubService to match for this transfer to be executed.
        /// </summary>
        private SubService SubService { set; get; } = SubService.None;

        /// <summary>
        ///     If true, the matcher won't check if the building's SubService matches before execution.
        /// </summary>
        private bool AnySubService { set; get; } = false;

        /// <summary>
        ///     (Optional) The type of the building's AI to match for this transfer to be executed. null means any.
        /// </summary>
        private Type AiType { set; get; } = null;

        /// <summary>
        ///     The TransferReason to execute (the material to transfer).
        /// </summary>
        private TransferReason Material { set; get; } = TransferReason.None;

        /// <summary>
        ///     Id of this transfer, must be unique. Used in the settings file so any changes will break old saves.
        /// </summary>
        public SettingId Id { private set; get; }

        private GoodsTransfer() {
            GoodsTransfers.Add(this);
        }

        public bool Matches(ushort buildingId) {
            var building = BuildingManager.m_buildings.m_buffer[buildingId];
            var info = building.Info;
            if (info == null) {
                return false;
            }

            if (AiType != null && !AiType.IsInstanceOfType(info.GetAI())) {
                return false;
            }

            return Service.Equals(info.GetService()) && (AnySubService || SubService.Equals(info.GetSubService()));
        }

        public void Transfer(ushort buildingId, bool debug) {
            var building = BuildingManager.m_buildings.m_buffer[buildingId];

            var info = building.Info;
            if (info == null) {
                return;
            }

            var ai = info.GetAI() as BuildingAI;
            if (ai == null) {
                return;
            }

            var amount = TransferAmount;
            ai.ModifyMaterialBuffer(
                buildingId,
                ref BuildingManager.m_buildings.m_buffer[buildingId],
                Material,
                ref amount
            );

            if (debug) {
                Debug.Log(
                    $"{Material} ({amount}) => \"{building.Info.name}\" {building.Info.GetService()}->{building.Info.GetSubService()} ({ai.GetType()})"
                );
            }
        }

        public void TransferIfMatch(ushort buildingId, bool debug) {
            if (Matches(buildingId)) {
                Transfer(buildingId, debug);
            }
        }
    }
}
