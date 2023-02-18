using System.Collections.Generic;
using InfiniteGoodsMod.Settings;
using static ItemClass;

namespace InfiniteGoodsMod.Transfer {
    public static class TransferDefinitions {
        public static readonly IList<ITransferDefinition> All = new List<ITransferDefinition>();

        // Commercial
        public static readonly TransferDefinition<BuildingAI> CommercialGoods = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.CommercialGoods,
                Service = Service.Commercial,
                Material = TransferManager.TransferReason.Goods,
                AnySubService = true,
            }
        );

        public static readonly TransferDefinition<BuildingAI> CommercialLuxuryProducts = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.CommercialLuxuryProducts,
                Service = Service.Commercial,
                Material = TransferManager.TransferReason.LuxuryProducts,
                AnySubService = true,
            }
        );

        // Specialized industry
        public static readonly TransferDefinition<BuildingAI> SpecializedOil = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.SpecializedIndustryOil,
                Service = Service.Industrial,
                SubService = SubService.IndustrialOil,
                Material = TransferManager.TransferReason.Oil,
            }
        );

        public static readonly TransferDefinition<BuildingAI> SpecializedOre = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.SpecializedIndustryOre,
                Service = Service.Industrial,
                SubService = SubService.IndustrialOre,
                Material = TransferManager.TransferReason.Ore,
            }
        );

        public static readonly TransferDefinition<BuildingAI> SpecializedGrain = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.SpecializedIndustryGrain,
                Service = Service.Industrial,
                SubService = SubService.IndustrialFarming,
                Material = TransferManager.TransferReason.Grain,
            }
        );

        public static readonly TransferDefinition<BuildingAI> SpecializedLogs = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.SpecializedIndustryLogs,
                Service = Service.Industrial,
                SubService = SubService.IndustrialForestry,
                Material = TransferManager.TransferReason.Logs,
            }
        );

        // Generic industry
        public static readonly TransferDefinition<BuildingAI> GenericPetrol = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.GenericIndustryPetrol,
                Service = Service.Industrial,
                SubService = SubService.IndustrialGeneric,
                Material = TransferManager.TransferReason.Petrol,
            }
        );

        public static readonly TransferDefinition<BuildingAI> GenericCoal = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.GenericIndustryCoal,
                Service = Service.Industrial,
                SubService = SubService.IndustrialGeneric,
                Material = TransferManager.TransferReason.Coal,
            }
        );

        public static readonly TransferDefinition<BuildingAI> GenericFood = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.GenericIndustryFood,
                Service = Service.Industrial,
                SubService = SubService.IndustrialGeneric,
                Material = TransferManager.TransferReason.Food,
            }
        );

        public static readonly TransferDefinition<BuildingAI> GenericLumber = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.GenericIndustryLumber,
                Service = Service.Industrial,
                SubService = SubService.IndustrialGeneric,
                Material = TransferManager.TransferReason.Lumber,
            }
        );

        // Shelters
        public static readonly TransferDefinition<BuildingAI> ShelterGoods = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.ShelterGoods,
                Service = Service.Disaster,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Goods,
            }
        );

        // Industries DLC raw
        public static readonly TransferDefinition<BuildingAI> PloppedIndustryRawOil = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.PloppedIndustryOil,
                Service = Service.PlayerIndustry,
                SubService = SubService.PlayerIndustryOil,
                Material = TransferManager.TransferReason.Oil,
            }
        );

        public static readonly TransferDefinition<BuildingAI> PloppedIndustryRawOre = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.PloppedIndustryOre,
                Service = Service.PlayerIndustry,
                SubService = SubService.PlayerIndustryOre,
                Material = TransferManager.TransferReason.Ore,
            }
        );

        public static readonly TransferDefinition<BuildingAI> PloppedIndustryRawFarming = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.PloppedIndustryGrain,
                Service = Service.PlayerIndustry,
                SubService = SubService.PlayerIndustryFarming,
                Material = TransferManager.TransferReason.Grain,
            }
        );

        public static readonly TransferDefinition<BuildingAI> PloppedIndustryRawForestry = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.PloppedIndustryLogs,
                Service = Service.PlayerIndustry,
                SubService = SubService.PlayerIndustryForestry,
                Material = TransferManager.TransferReason.Logs,
            }
        );

        // Industries DLC processed
        public static readonly TransferDefinition<BuildingAI> ProcessedAnimalProducts = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.UniqueIndustryAnimalProducts,
                Service = Service.PlayerIndustry,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.AnimalProducts,
            }
        );

        public static readonly TransferDefinition<BuildingAI> ProcessedFlours = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.UniqueIndustryFlours,
                Service = Service.PlayerIndustry,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Flours,
            }
        );

        public static readonly TransferDefinition<BuildingAI> ProcessedPaper = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.UniqueIndustryPaper,
                Service = Service.PlayerIndustry,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Paper,
            }
        );

        public static readonly TransferDefinition<BuildingAI> ProcessedPlanedTimber = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.UniqueIndustryPlanedTimber,
                Service = Service.PlayerIndustry,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.PlanedTimber,
            }
        );

        public static readonly TransferDefinition<BuildingAI> ProcessedPetroleum = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.UniqueIndustryPetroleum,
                Service = Service.PlayerIndustry,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Petroleum,
            }
        );

        public static readonly TransferDefinition<BuildingAI> ProcessedPlastics = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.UniqueIndustryPlastics,
                Service = Service.PlayerIndustry,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Plastics,
            }
        );

        public static readonly TransferDefinition<BuildingAI> ProcessedGlass = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.UniqueIndustryGlass,
                Service = Service.PlayerIndustry,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Glass,
            }
        );

        public static readonly TransferDefinition<BuildingAI> ProcessedMetals = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.UniqueIndustryMetals,
                Service = Service.PlayerIndustry,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Metals,
            }
        );

        public static readonly TransferDefinition<BuildingAI> ProcessedCrops = CreateAndAdd(
            new TransferDefinition<BuildingAI> {
                Id = SettingId.UniqueIndustryGrain,
                Service = Service.PlayerIndustry,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Grain,
            }
        );

        // Sunset Harbor DLC Fishing Industry
        public static readonly TransferDefinition<FishingHarborAI> FishingHarbor = CreateAndAdd(
            new TransferDefinition<FishingHarborAI> {
                Id = SettingId.FishingHarbor,
                Service = Service.Fishing,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Fish,
            }
        );

        public static readonly TransferDefinition<FishFarmAI> FishingFarm = CreateAndAdd(
            new TransferDefinition<FishFarmAI> {
                Id = SettingId.FishingFarm,
                Service = Service.Fishing,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Fish,
            }
        );

        public static readonly TransferDefinition<MarketAI> FishingMarket = CreateAndAdd(
            new TransferDefinition<MarketAI> {
                Id = SettingId.FishingMarket,
                Service = Service.Fishing,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Fish,
            }
        );

        public static readonly TransferDefinition<ProcessingFacilityAI> FishingProcessing =
            CreateAndAdd(
                new TransferDefinition<ProcessingFacilityAI> {
                    Id = SettingId.FishingProcessing,
                    Service = Service.Fishing,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.Fish,
                }
            );

        private static TransferDefinition<TBuildingAI> CreateAndAdd<TBuildingAI>(
            TransferDefinition<TBuildingAI> definition
        ) where TBuildingAI : BuildingAI {
            All.Add(definition);
            return definition;
        }
    }
}
