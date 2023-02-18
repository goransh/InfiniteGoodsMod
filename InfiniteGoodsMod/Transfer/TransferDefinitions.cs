using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InfiniteGoodsMod.Settings;
using static ItemClass;

namespace InfiniteGoodsMod.Transfer {
    public static class TransferDefinitions {
        public static readonly ReadOnlyCollection<ITransferDefinition> All = Initialize();

        private static ReadOnlyCollection<ITransferDefinition> Initialize() {
            var list = new List<ITransferDefinition> {
                // Cargo service point
            };
            list.AddRange(InitializeCommercial());
            var specializedZonedIndustrial = InitializeSpecializedZonedIndustrial();
            list.AddRange(specializedZonedIndustrial);
            var genericZonedIndustrial = InitializeGenericZonedIndustrial();
            list.AddRange(genericZonedIndustrial);
            list.AddRange(InitializeShelters());
            list.AddRange(InitializeIndustriesDlcRaw());
            list.AddRange(InitializeIndustriesDlcProcessed());
            list.AddRange(InitializeSunsetHarborDlcFishingIndustry());
            list.AddRange(InitializePowerPlants());
            list.AddRange(InitializePedestrianAreaServicePoint());
            list.AddRange(InitializeCargoServicePoint(specializedZonedIndustrial));
            list.AddRange(InitializeCargoServicePoint(genericZonedIndustrial));
            return list.AsReadOnly();
        }

        private static ITransferDefinition[] InitializeCommercial() =>
            new ITransferDefinition[] {
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.CommercialGoods,
                    Service = Service.Commercial,
                    Material = TransferManager.TransferReason.Goods,
                    AnySubService = true,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.CommercialLuxuryProducts,
                    Service = Service.Commercial,
                    Material = TransferManager.TransferReason.LuxuryProducts,
                    AnySubService = true,
                },
            };

        private static ITransferDefinition[] InitializeSpecializedZonedIndustrial() =>
            new ITransferDefinition[] {
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.SpecializedIndustryOil,
                    Service = Service.Industrial,
                    SubService = SubService.IndustrialOil,
                    Material = TransferManager.TransferReason.Oil,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.SpecializedIndustryOre,
                    Service = Service.Industrial,
                    SubService = SubService.IndustrialOre,
                    Material = TransferManager.TransferReason.Ore,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.SpecializedIndustryGrain,
                    Service = Service.Industrial,
                    SubService = SubService.IndustrialFarming,
                    Material = TransferManager.TransferReason.Grain,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.SpecializedIndustryLogs,
                    Service = Service.Industrial,
                    SubService = SubService.IndustrialForestry,
                    Material = TransferManager.TransferReason.Logs,
                },
            };

        private static ITransferDefinition[] InitializeGenericZonedIndustrial() => new ITransferDefinition[] {
            new TransferDefinition<BuildingAI> {
                Id = SettingId.GenericIndustryPetrol,
                Service = Service.Industrial,
                SubService = SubService.IndustrialGeneric,
                Material = TransferManager.TransferReason.Petrol,
            },
            new TransferDefinition<BuildingAI> {
                Id = SettingId.GenericIndustryCoal,
                Service = Service.Industrial,
                SubService = SubService.IndustrialGeneric,
                Material = TransferManager.TransferReason.Coal,
            },
            new TransferDefinition<BuildingAI> {
                Id = SettingId.GenericIndustryFood,
                Service = Service.Industrial,
                SubService = SubService.IndustrialGeneric,
                Material = TransferManager.TransferReason.Food,
            },
            new TransferDefinition<BuildingAI> {
                Id = SettingId.GenericIndustryLumber,
                Service = Service.Industrial,
                SubService = SubService.IndustrialGeneric,
                Material = TransferManager.TransferReason.Lumber,
            },
        };

        private static ITransferDefinition[] InitializeShelters() => new[] {
            new TransferDefinition<BuildingAI> {
                Id = SettingId.ShelterGoods,
                Service = Service.Disaster,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Goods,
            },
        };

        private static ITransferDefinition[] InitializeIndustriesDlcRaw() => new ITransferDefinition[] {
            new TransferDefinition<BuildingAI> {
                Id = SettingId.PloppedIndustryOil,
                Service = Service.PlayerIndustry,
                SubService = SubService.PlayerIndustryOil,
                Material = TransferManager.TransferReason.Oil,
            },
            new TransferDefinition<BuildingAI> {
                Id = SettingId.PloppedIndustryOre,
                Service = Service.PlayerIndustry,
                SubService = SubService.PlayerIndustryOre,
                Material = TransferManager.TransferReason.Ore,
            },
            new TransferDefinition<BuildingAI> {
                Id = SettingId.PloppedIndustryGrain,
                Service = Service.PlayerIndustry,
                SubService = SubService.PlayerIndustryFarming,
                Material = TransferManager.TransferReason.Grain,
            },
            new TransferDefinition<BuildingAI> {
                Id = SettingId.PloppedIndustryLogs,
                Service = Service.PlayerIndustry,
                SubService = SubService.PlayerIndustryForestry,
                Material = TransferManager.TransferReason.Logs,
            },
        };

        private static ITransferDefinition[] InitializeIndustriesDlcProcessed() => new ITransferDefinition[] {
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.UniqueIndustryAnimalProducts,
                    Service = Service.PlayerIndustry,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.AnimalProducts,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.UniqueIndustryFlours,
                    Service = Service.PlayerIndustry,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.Flours,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.UniqueIndustryPaper,
                    Service = Service.PlayerIndustry,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.Paper,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.UniqueIndustryPlanedTimber,
                    Service = Service.PlayerIndustry,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.PlanedTimber,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.UniqueIndustryPetroleum,
                    Service = Service.PlayerIndustry,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.Petroleum,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.UniqueIndustryPlastics,
                    Service = Service.PlayerIndustry,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.Plastics,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.UniqueIndustryGlass,
                    Service = Service.PlayerIndustry,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.Glass,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.UniqueIndustryMetals,
                    Service = Service.PlayerIndustry,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.Metals,
                },
                new TransferDefinition<BuildingAI> {
                    Id = SettingId.UniqueIndustryGrain,
                    Service = Service.PlayerIndustry,
                    SubService = SubService.None,
                    Material = TransferManager.TransferReason.Grain,
                },
        };

        private static ITransferDefinition[] InitializeSunsetHarborDlcFishingIndustry() => new ITransferDefinition[] {
            new TransferDefinition<FishingHarborAI> {
                Id = SettingId.FishingHarbor,
                Service = Service.Fishing,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Fish,
            },
            new TransferDefinition<FishFarmAI> {
                Id = SettingId.FishingFarm,
                Service = Service.Fishing,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Fish,
            },
            new TransferDefinition<MarketAI> {
                Id = SettingId.FishingMarket,
                Service = Service.Fishing,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Fish,
            },
            new TransferDefinition<ProcessingFacilityAI> {
                Id = SettingId.FishingProcessing,
                Service = Service.Fishing,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Fish,
            },
        };

        private static ITransferDefinition[] InitializePowerPlants() => new ITransferDefinition[] {
            new TransferDefinition<PowerPlantAI> {
                Id = SettingId.PowerPlantCoal,
                Service = Service.Electricity,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Coal,
                TransferCondition = ai => ai.m_resourceType == TransferManager.TransferReason.Coal,
            },
            new TransferDefinition<PowerPlantAI> {
                Id = SettingId.PowerPlantOil,
                Service = Service.Electricity,
                SubService = SubService.None,
                Material = TransferManager.TransferReason.Petrol,
                TransferCondition = ai => ai.m_resourceType == TransferManager.TransferReason.Petrol,
            },
        };

        private static ITransferDefinition[] InitializePedestrianAreaServicePoint() => new[] {
            new TransferDefinition<ServicePointAI> {
                Id = SettingId.PedestrianServicePointGoods,
                Service = Service.ServicePoint,
                Material = TransferManager.TransferReason.Goods,
                TransferCondition = ai => (int)ai.m_deliveryCategories == -1,
            },
            new TransferDefinition<ServicePointAI> {
                Id = SettingId.PedestrianServicePointLuxuryProducts,
                Service = Service.ServicePoint,
                Material = TransferManager.TransferReason.LuxuryProducts,
                TransferCondition = ai => (int)ai.m_deliveryCategories == -1,
            },
        };

        private static ITransferDefinition[] InitializeCargoServicePoint(ITransferDefinition[] source) {
            var settingIdOffset = (int)SettingId.CargoServicePointSpecializedIndustryOil
                                  - (int)SettingId.SpecializedIndustryOil;
            return source.Select(
                def => new TransferDefinition<ServicePointAI> {
                    Id = def.Id + settingIdOffset,
                    Service = Service.ServicePoint,
                    Material = def.Material,
                    TransferCondition = BuildingAcceptsCargo,
                } as ITransferDefinition
            ).ToArray();
        }
        
        private static bool BuildingAcceptsCargo(ServicePointAI ai) =>
            (ai.m_deliveryCategories & DistrictPark.DeliveryCategories.Cargo)
            != DistrictPark.DeliveryCategories.None;
    }
}
