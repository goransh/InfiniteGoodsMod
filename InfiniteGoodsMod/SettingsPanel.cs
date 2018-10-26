using ICities;

namespace InfiniteGoodsMod {
    internal class SettingsPanel {
        private readonly Settings settings;

        public SettingsPanel() {
            settings = Settings.GetInstance();
        }

        public void CreatePanel(UIHelperBase helper) {
            UIHelperBase commercialGroup = helper.AddGroup("Fill Commercial Buildings with");
            AddCheckbox(commercialGroup, GoodsTransfer.CommercialGoods, "Goods");
            AddCheckbox(commercialGroup, GoodsTransfer.CommercialLuxuryProducts, "Luxury Products");

            UIHelperBase specializedIndustrialGroup = helper.AddGroup("Fill Specialized Industry with raw");
            AddCheckbox(specializedIndustrialGroup, GoodsTransfer.SpecializedOil, "Oil");
            AddCheckbox(specializedIndustrialGroup, GoodsTransfer.SpecializedOre, "Ore");
            AddCheckbox(specializedIndustrialGroup, GoodsTransfer.SpecializedGrain, "Grain");
            AddCheckbox(specializedIndustrialGroup, GoodsTransfer.SpecializedLogs, "Logs");

            UIHelperBase genericIndustrialGroup = helper.AddGroup("Fill Generic Industry with processed");
            AddCheckbox(genericIndustrialGroup, GoodsTransfer.GenericPetrol, "Petrol");
            AddCheckbox(genericIndustrialGroup, GoodsTransfer.GenericCoal, "Coal");
            AddCheckbox(genericIndustrialGroup, GoodsTransfer.GenericFood, "Food");
            AddCheckbox(genericIndustrialGroup, GoodsTransfer.GenericLumber, "Lumber");

            UIHelperBase playerRawIndustryGroup = helper.AddGroup("Fill Player Industry with");
            AddCheckbox(playerRawIndustryGroup, GoodsTransfer.PlayerIndustryRawOil, "Oil");
            AddCheckbox(playerRawIndustryGroup, GoodsTransfer.PlayerIndustryRawOre, "Ore");
            AddCheckbox(playerRawIndustryGroup, GoodsTransfer.PlayerIndustryRawFarming, "Farming");
            AddCheckbox(playerRawIndustryGroup, GoodsTransfer.PlayerIndustryRawForestry, "Forestry");

            UIHelperBase uniqueFactoriesGroup = helper.AddGroup("Fill Unique Factories with");
            AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedAnimalProducts, "Animal Products");
            AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedFlours, "Flours");
            AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedPaper, "Paper");
            AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedPlanedTimber, "Planed Timber");
            AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedPetroleum, "Petroleum");
            AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedPlastics, "Plastics");
            AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedGlass, "Glass");
            AddCheckbox(uniqueFactoriesGroup, GoodsTransfer.ProcessedMetals, "Metals");

            UIHelperBase shelterGroup = helper.AddGroup("Fill Shelters with");
            AddCheckbox(shelterGroup, GoodsTransfer.ShelterGoods, "Food");
        }

        private void AddCheckbox(UIHelperBase group, GoodsTransfer transfer, string settingTitle) {
            group.AddCheckbox(settingTitle, settings.Get(transfer.Id), value => {
                settings.Set(transfer.Id, value);
                settings.SaveSettings();
            });
        }
    }
}