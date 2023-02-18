using ColossalFramework.PlatformServices;
using ICities;
using InfiniteGoodsMod.Settings;

namespace InfiniteGoodsMod.Gui {
    internal class SettingsPanel {
        private readonly Settings.Settings settings;

        public SettingsPanel() {
            settings = Settings.Settings.GetInstance();
        }

        public void CreatePanel(UIHelperBase helper) {
            var commercialGroup = helper.AddGroup("Stock Commercial Buildings with");
            AddCheckbox(commercialGroup, SettingId.CommercialGoods, "Goods");
            if (PlatformService.IsDlcInstalled(SteamHelper.kIndustryDLCAppID)) {
                AddCheckbox(commercialGroup, SettingId.CommercialLuxuryProducts, "Luxury Products");
            }

            var powerPlantGroup = helper.AddGroup("Stock Power Plants with");
            AddCheckbox(powerPlantGroup, SettingId.PowerPlantCoal, "Coal");
            AddCheckbox(powerPlantGroup, SettingId.PowerPlantOil, "Oil");

            var specializedIndustrialGroup = helper.AddGroup("Stock Specialized Industry with raw");
            AddCheckbox(specializedIndustrialGroup, SettingId.SpecializedIndustryOil, "Oil");
            AddCheckbox(specializedIndustrialGroup, SettingId.SpecializedIndustryOre, "Ore");
            AddCheckbox(specializedIndustrialGroup, SettingId.SpecializedIndustryGrain, "Grain");
            AddCheckbox(specializedIndustrialGroup, SettingId.SpecializedIndustryLogs, "Logs");

            var genericIndustrialGroup = helper.AddGroup("Stock Generic Industry with processed");
            AddCheckbox(genericIndustrialGroup, SettingId.GenericIndustryPetrol, "Petrol");
            AddCheckbox(genericIndustrialGroup, SettingId.GenericIndustryCoal, "Coal");
            AddCheckbox(genericIndustrialGroup, SettingId.GenericIndustryFood, "Food");
            AddCheckbox(genericIndustrialGroup, SettingId.GenericIndustryLumber, "Lumber");

            if (PlatformService.IsDlcInstalled(SteamHelper.kIndustryDLCAppID)) {
                var warehouseGroup = helper.AddGroup("Stock Warehouses with");
                AddCheckbox(warehouseGroup, SettingId.WarehouseOil, "Oil");
                AddCheckbox(warehouseGroup, SettingId.WarehouseOre, "Ore");
                AddCheckbox(warehouseGroup, SettingId.WarehouseGrain, "Crops");
                AddCheckbox(warehouseGroup, SettingId.WarehouseLogs, "Logs");
                
                var playerRawIndustryGroup = helper.AddGroup("Stock Plopped Industry with");
                AddCheckbox(playerRawIndustryGroup, SettingId.PloppedIndustryOil, "Oil");
                AddCheckbox(playerRawIndustryGroup, SettingId.PloppedIndustryOre, "Ore");
                AddCheckbox(playerRawIndustryGroup, SettingId.PloppedIndustryGrain, "Crops");
                AddCheckbox(playerRawIndustryGroup, SettingId.PloppedIndustryLogs, "Forest Products");

                var uniqueFactoriesGroup = helper.AddGroup("Stock Unique Factories with");
                AddCheckbox(uniqueFactoriesGroup, SettingId.UniqueIndustryAnimalProducts, "Animal Products");
                AddCheckbox(uniqueFactoriesGroup, SettingId.UniqueIndustryFlours, "Flours");
                AddCheckbox(uniqueFactoriesGroup, SettingId.UniqueIndustryPaper, "Paper");
                AddCheckbox(uniqueFactoriesGroup, SettingId.UniqueIndustryPlanedTimber, "Planed Timber");
                AddCheckbox(uniqueFactoriesGroup, SettingId.UniqueIndustryPetroleum, "Petroleum");
                AddCheckbox(uniqueFactoriesGroup, SettingId.UniqueIndustryPlastics, "Plastics");
                AddCheckbox(uniqueFactoriesGroup, SettingId.UniqueIndustryGlass, "Glass");
                AddCheckbox(uniqueFactoriesGroup, SettingId.UniqueIndustryMetals, "Metals");
                AddCheckbox(uniqueFactoriesGroup, SettingId.UniqueIndustryGrain, "Crops");
            }

            if (PlatformService.IsDlcInstalled(SteamHelper.kUrbanDLCAppID)) {
                var fishingGroup = helper.AddGroup("Stock with Fish");
                AddCheckbox(fishingGroup, SettingId.FishingHarbor, "Fishing Harbors");
                AddCheckbox(fishingGroup, SettingId.FishingFarm, "Fish/Algae/Seaweed Farms");
                AddCheckbox(fishingGroup, SettingId.FishingMarket, "Fish Markets");
                AddCheckbox(fishingGroup, SettingId.FishingProcessing, "Fish Processing Facilities");
            }

            if (PlatformService.IsDlcInstalled(SteamHelper.kNaturalDisastersDLCAppID)) {
                var shelterGroup = helper.AddGroup("Stock Shelters with");
                AddCheckbox(shelterGroup, SettingId.ShelterGoods, "Food");
            }

            if (PlatformService.IsDlcInstalled(SteamHelper.kPlazasAndPromenadesDLCAppID)) {
                var pedestrianAreaServicePointGroup = helper.AddGroup("Stock Pedestrian Area Service Points with");
                AddCheckbox(
                    pedestrianAreaServicePointGroup,
                    SettingId.PedestrianServicePointGoods,
                    "Goods"
                );
                AddCheckbox(
                    pedestrianAreaServicePointGroup,
                    SettingId.PedestrianServicePointLuxuryProducts,
                    "Luxury Products"
                );

                var cargoServicePointGroup = helper.AddGroup("Stock Cargo Service Points with");

                AddCheckbox(cargoServicePointGroup, SettingId.CargoServicePointSpecializedIndustryOil, "Oil (raw)");
                AddCheckbox(cargoServicePointGroup, SettingId.CargoServicePointSpecializedIndustryOre, "Ore (raw)");
                AddCheckbox(cargoServicePointGroup, SettingId.CargoServicePointSpecializedIndustryGrain, "Grain (raw)");
                AddCheckbox(cargoServicePointGroup, SettingId.CargoServicePointSpecializedIndustryLogs, "Logs (raw)");

                AddCheckbox(cargoServicePointGroup, SettingId.CargoServicePointGenericIndustryPetrol, "Petrol (processed)");
                AddCheckbox(cargoServicePointGroup, SettingId.CargoServicePointGenericIndustryCoal, "Coal (processed)");
                AddCheckbox(cargoServicePointGroup, SettingId.CargoServicePointGenericIndustryFood, "Food (processed)");
                AddCheckbox(cargoServicePointGroup, SettingId.CargoServicePointGenericIndustryLumber, "Lumber (processed)");
            }


            var advancedGroup = helper.AddGroup("Advanced");
            AddCheckbox(advancedGroup, SettingId.Debug, "Debug mode");
        }

        private void AddCheckbox(UIHelperBase group, SettingId settingId, string settingTitle) {
            group.AddCheckbox(
                settingTitle,
                settings[settingId],
                value => {
                    settings[settingId] = value;
                    settings.SaveSettings();
                }
            );
        }
    }
}
