using ColossalFramework.PlatformServices;
using ICities;
using InfiniteGoodsMod.Settings;
using InfiniteGoodsMod.Transfer;

namespace InfiniteGoodsMod.Gui {
    internal class SettingsPanel {
        private readonly Settings.Settings settings;

        public SettingsPanel() {
            settings = Settings.Settings.GetInstance();
        }

        public void CreatePanel(UIHelperBase helper) {
            var commercialGroup = helper.AddGroup("Stock Commercial Buildings with");
            AddCheckbox(commercialGroup, TransferDefinitions.CommercialGoods, "Goods");
            if (PlatformService.IsDlcInstalled(SteamHelper.kIndustryDLCAppID)) {
                AddCheckbox(commercialGroup, TransferDefinitions.CommercialLuxuryProducts, "Luxury Products");
            }

            var specializedIndustrialGroup = helper.AddGroup("Stock Specialized Industry with raw");
            AddCheckbox(specializedIndustrialGroup, TransferDefinitions.SpecializedOil, "Oil");
            AddCheckbox(specializedIndustrialGroup, TransferDefinitions.SpecializedOre, "Ore");
            AddCheckbox(specializedIndustrialGroup, TransferDefinitions.SpecializedGrain, "Grain");
            AddCheckbox(specializedIndustrialGroup, TransferDefinitions.SpecializedLogs, "Logs");

            var genericIndustrialGroup = helper.AddGroup("Stock Generic Industry with processed");
            AddCheckbox(genericIndustrialGroup, TransferDefinitions.GenericPetrol, "Petrol");
            AddCheckbox(genericIndustrialGroup, TransferDefinitions.GenericCoal, "Coal");
            AddCheckbox(genericIndustrialGroup, TransferDefinitions.GenericFood, "Food");
            AddCheckbox(genericIndustrialGroup, TransferDefinitions.GenericLumber, "Lumber");

            if (PlatformService.IsDlcInstalled(SteamHelper.kIndustryDLCAppID)) {
                var playerRawIndustryGroup = helper.AddGroup("Stock Plopped Industry with");
                AddCheckbox(playerRawIndustryGroup, TransferDefinitions.PloppedIndustryRawOil, "Oil");
                AddCheckbox(playerRawIndustryGroup, TransferDefinitions.PloppedIndustryRawOre, "Ore");
                AddCheckbox(playerRawIndustryGroup, TransferDefinitions.PloppedIndustryRawFarming, "Crops");
                AddCheckbox(playerRawIndustryGroup, TransferDefinitions.PloppedIndustryRawForestry, "Forestry");

                var uniqueFactoriesGroup = helper.AddGroup("Stock Unique Factories with");
                AddCheckbox(uniqueFactoriesGroup, TransferDefinitions.ProcessedAnimalProducts, "Animal Products");
                AddCheckbox(uniqueFactoriesGroup, TransferDefinitions.ProcessedFlours, "Flours");
                AddCheckbox(uniqueFactoriesGroup, TransferDefinitions.ProcessedPaper, "Paper");
                AddCheckbox(uniqueFactoriesGroup, TransferDefinitions.ProcessedPlanedTimber, "Planed Timber");
                AddCheckbox(uniqueFactoriesGroup, TransferDefinitions.ProcessedPetroleum, "Petroleum");
                AddCheckbox(uniqueFactoriesGroup, TransferDefinitions.ProcessedPlastics, "Plastics");
                AddCheckbox(uniqueFactoriesGroup, TransferDefinitions.ProcessedGlass, "Glass");
                AddCheckbox(uniqueFactoriesGroup, TransferDefinitions.ProcessedMetals, "Metals");
                AddCheckbox(uniqueFactoriesGroup, TransferDefinitions.ProcessedCrops, "Crops");
            }

            if (PlatformService.IsDlcInstalled(SteamHelper.kUrbanDLCAppID)) {
                var fishingGroup = helper.AddGroup("Stock with Fish");
                AddCheckbox(fishingGroup, TransferDefinitions.FishingHarbor, "Fishing Harbors");
                AddCheckbox(fishingGroup, TransferDefinitions.FishingFarm, "Fish/Algae/Seaweed Farms");
                AddCheckbox(fishingGroup, TransferDefinitions.FishingMarket, "Fish Markets");
                AddCheckbox(fishingGroup, TransferDefinitions.FishingProcessing, "Fish Processing Facilities");
            }

            if (PlatformService.IsDlcInstalled(SteamHelper.kNaturalDisastersDLCAppID)) {
                var shelterGroup = helper.AddGroup("Stock Shelters with");
                AddCheckbox(shelterGroup, TransferDefinitions.ShelterGoods, "Food");
            }

            var advancedGroup = helper.AddGroup("Advanced");
            AddCheckbox(advancedGroup, SettingId.Debug, "Debug mode");
        }

        private void AddCheckbox(UIHelperBase group, ITransferDefinition transfer, string settingTitle) {
            AddCheckbox(group, transfer.Id, settingTitle);
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
