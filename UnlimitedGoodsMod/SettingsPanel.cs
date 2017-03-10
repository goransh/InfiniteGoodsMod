using ICities;

using static TransferManager.TransferReason;

namespace InfiniteGoodsMod
{
    class SettingsPanel
    {
        private Settings settings;

        public SettingsPanel()
        {
            this.settings = Settings.GetInstance();
        }

        public void CreatePanel(UIHelperBase helper)
        {
            UIHelperBase commercialGroup = helper.AddGroup("Fill Commercial Buildings");
            AddCheckbox(commercialGroup, Goods);

            UIHelperBase specializedIndustrialGroup = helper.AddGroup("Fill Specialized Industry (with raw materials)");
            AddCheckbox(specializedIndustrialGroup, Oil);
            AddCheckbox(specializedIndustrialGroup, Ore);
            AddCheckbox(specializedIndustrialGroup, Grain);
            AddCheckbox(specializedIndustrialGroup, Logs);

            UIHelperBase genericIndustrialGroup = helper.AddGroup("Fill Generic Industry (with processed materials)");
            AddCheckbox(genericIndustrialGroup, Petrol);
            AddCheckbox(genericIndustrialGroup, Coal);
            AddCheckbox(genericIndustrialGroup, Food);
            AddCheckbox(genericIndustrialGroup, Lumber);
        }

        private void AddCheckbox(UIHelperBase group, TransferManager.TransferReason reason)
        {
            group.AddCheckbox(reason.ToString(), settings.Get(reason), (bool value) => {
                settings.Set(reason, value);
                settings.SaveSettings();
            });
        }
    }
}
