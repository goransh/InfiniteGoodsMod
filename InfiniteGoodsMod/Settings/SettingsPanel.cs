using ColossalFramework.PlatformServices;
using ICities;

namespace InfiniteGoodsMod {
    internal class SettingsPanel {
        private readonly Settings settings;

        public SettingsPanel() {
            settings = Settings.GetInstance();
        }

        public void CreatePanel(UIHelperBase helper) {
            var commercialGroup = helper.AddGroup("Stock Commercial Buildings with");
            AddCheckbox(commercialGroup, GoodsTransfer.CommercialGoods, "Goods");
            if (PlatformService.IsDlcInstalled(SteamHelper.kIndustryDLCAppID)) {
                AddCheckbox(commercialGroup, GoodsTransfer.CommercialLuxuryProducts, "Luxury Products");
            }

            var specializedIndustrialGroup = helper.AddGroup("Stock Specialized Industry with raw");
            AddCheckbox(specializedIndustrialGroup, GoodsTransfer.SpecializedOil, "Oil");
            AddCheckbox(specializedIndustrialGroup, GoodsTransfer.SpecializedOre, "Ore");
            AddCheckbox(specializedIndustrialGroup, GoodsTransfer.SpecializedGrain, "Grain");
            AddCheckbox(specializedIndustrialGroup, GoodsTransfer.SpecializedLogs, "Logs");

            var genericIndustrialGroup = helper.AddGroup("Stock Generic Industry with processed");
            AddCheckbox(genericIndustrialGroup, GoodsTransfer.GenericPetrol, "Petrol");
            AddCheckbox(genericIndustrialGroup, GoodsTransfer.GenericCoal, "Coal");
            AddCheckbox(genericIndustrialGroup, GoodsTransfer.GenericFood, "Food");
            AddCheckbox(genericIndustrialGroup, GoodsTransfer.GenericLumber, "Lumber");

            if (PlatformService.IsDlcInstalled(SteamHelper.kIndustryDLCAppID)) {
                var playerRawIndustryGroup = helper.AddGroup("Stock Plopped Industry with");
                AddCheckbox(playerRawIndustryGroup, GoodsTransfer.PloppedIndustryRawOil, "Oil");
                AddCheckbox(playerRawIndustryGroup, GoodsTransfer.PloppedIndustryRawOre, "Ore");
                AddCheckbox(playerRawIndustryGroup, GoodsTransfer.PloppedIndustryRawFarming, "Crops");
                AddCheckbox(playerRawIndustryGroup, GoodsTransfer.PloppedIndustryRawForestry, "Forestry");

                var uniqueFactoriesGroup = helper.AddGroup("Stock Unique Factories with");
                AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedAnimalProducts, "Animal Products");
                AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedFlours, "Flours");
                AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedPaper, "Paper");
                AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedPlanedTimber, "Planed Timber");
                AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedPetroleum, "Petroleum");
                AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedPlastics, "Plastics");
                AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedGlass, "Glass");
                AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedMetals, "Metals");
                AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedCrops, "Crops");
            }

            if (PlatformService.IsDlcInstalled(SteamHelper.kUrbanDLCAppID)) {
                var fishingGroup = helper.AddGroup("Stock with Fish");
                AddCheckbox(fishingGroup, GoodsTransfer.FishingHarbor, "Fishing Harbors");
                AddCheckbox(fishingGroup, GoodsTransfer.FishingFarm, "Fish/Algae/Seaweed Farms");
                AddCheckbox(fishingGroup, GoodsTransfer.FishingMarket, "Fish Markets");
                AddCheckbox(fishingGroup, GoodsTransfer.FishingProcessing, "Fish Processing Facilities");
            }

            if (PlatformService.IsDlcInstalled(SteamHelper.kNaturalDisastersDLCAppID)) {
                var shelterGroup = helper.AddGroup("Stock Shelters with");
                AddCheckbox(shelterGroup, GoodsTransfer.ShelterGoods, "Food");
            }
        }

        private void AddCheckbox(UIHelperBase group, GoodsTransfer transfer, string settingTitle) {
            group.AddCheckbox(
                settingTitle,
                settings.Get(transfer.Id),
                value => {
                    settings.Set(transfer.Id, value);
                    settings.SaveSettings();
                }
            );
        }
    }
}
