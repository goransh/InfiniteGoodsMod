using ICities;
using static TransferManager.TransferReason;

namespace InfiniteGoodsMod {
    internal class SettingsPanel {
        private readonly Settings settings;

        public SettingsPanel() {
            settings = Settings.GetInstance();
        }

        public void CreatePanel(UIHelperBase helper) {
            UIHelperBase commercialGroup = helper.AddGroup("Fill Commercial Buildings");
            AddCheckbox(commercialGroup, Setting.CommercialGoods, "Goods");

            UIHelperBase specializedIndustrialGroup = helper.AddGroup("Fill Specialized Industry (with raw materials)");
            AddCheckbox(specializedIndustrialGroup, Setting.SpecializedOil, "Oil");
            AddCheckbox(specializedIndustrialGroup, Setting.SpecializedCoal, "Coal");
            AddCheckbox(specializedIndustrialGroup, Setting.SpecializedGrain, "Grain");
            AddCheckbox(specializedIndustrialGroup, Setting.SpecializedLogs, "Logs");

            UIHelperBase genericIndustrialGroup = helper.AddGroup("Fill Generic Industry (with processed materials)");
            AddCheckbox(genericIndustrialGroup, Setting.GenericPetrol, "Petrol");
            AddCheckbox(genericIndustrialGroup, Setting.GenericOre, "Ore");
            AddCheckbox(genericIndustrialGroup, Setting.GenericFood, "Food");
            AddCheckbox(genericIndustrialGroup, Setting.GenericLumber, "Lumber");

            UIHelperBase shelterGroup = helper.AddGroup("Fill Shelters");
            AddCheckbox(shelterGroup, Setting.ShelterGoods, "Food");
        }

        private void AddCheckbox(UIHelperBase group, Setting setting, string settingTitle) {
            group.AddCheckbox(settingTitle, settings.Get(setting), value => {
                settings.Set(setting, value);
                settings.SaveSettings();
            });
        }
    }
}