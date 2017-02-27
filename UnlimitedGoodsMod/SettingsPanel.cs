using ICities;

using static TransferManager.TransferReason;

namespace InfiniteGoodsMod
{
    class SettingsPanel
    {

        private Settings settings;

        public SettingsPanel(Settings settings)
        {
            this.settings = settings;
        }

        public void CreatePanel(UIHelperBase helper)
        {
            UIHelperBase commercialGroup = helper.AddGroup("Fill Commercial Buildings");
            commercialGroup.AddCheckbox("Goods", settings.Get(Goods), GoodsCheck);

            UIHelperBase specializedIndustrialGroup = helper.AddGroup("Fill Specialized Industry (with raw materials)");
            specializedIndustrialGroup.AddCheckbox("Oil", settings.Get(Oil), OilCheck);
            specializedIndustrialGroup.AddCheckbox("Ore", settings.Get(Ore), OreCheck);
            specializedIndustrialGroup.AddCheckbox("Grain", settings.Get(Grain), GrainCheck);
            specializedIndustrialGroup.AddCheckbox("Logs", settings.Get(Logs), LogsCheck);

            UIHelperBase genericIndustrialGroup = helper.AddGroup("Fill Generic Industry (with processed materials)");
            genericIndustrialGroup.AddCheckbox("Petrol", settings.Get(Petrol), PetrolCheck);
            genericIndustrialGroup.AddCheckbox("Coal", settings.Get(Coal), CoalCheck);
            genericIndustrialGroup.AddCheckbox("Food", settings.Get(Food), FoodCheck);
            genericIndustrialGroup.AddCheckbox("Lumber", settings.Get(Lumber), LumberCheck);
        }

        private void SetSetting(TransferManager.TransferReason reason, bool value)
        {
            settings.Set(reason, value);
            Settings.SaveSettings();
        }

        private void FoodCheck(bool value)
        {
            SetSetting(Food, value);
        }

        private void CoalCheck(bool value)
        {
            SetSetting(Coal, value);
        }

        private void OilCheck(bool value)
        {
            SetSetting(Oil, value);
        }

        private void LumberCheck(bool value)
        {
            SetSetting(Lumber, value);
        }

        private void LogsCheck(bool value)
        {
            SetSetting(Logs, value);
        }

        private void GrainCheck(bool value)
        {
            SetSetting(Grain, value);
        }

        private void OreCheck(bool value)
        {
            SetSetting(Ore, value);
        }

        private void GoodsCheck(bool value)
        {
            SetSetting(Goods, value);
        }

        private void PetrolCheck(bool value)
        {
            SetSetting(Petrol, value);
        }
    }
}
