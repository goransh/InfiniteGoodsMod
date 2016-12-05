using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void createPanel(UIHelperBase helper)
        {
            UIHelperBase commercialGroup = helper.AddGroup("Commercial Buildings");
            commercialGroup.AddCheckbox("Goods", settings.get(Goods), GoodsCheck);

            UIHelperBase specializedIndustrialGroup = helper.AddGroup("Specialized Industry (Raw materials)");
            specializedIndustrialGroup.AddCheckbox("Oil", settings.get(Oil), OilCheck);
            specializedIndustrialGroup.AddCheckbox("Ore", settings.get(Ore), OreCheck);
            specializedIndustrialGroup.AddCheckbox("Grain", settings.get(Grain), GrainCheck);
            specializedIndustrialGroup.AddCheckbox("Logs", settings.get(Logs), LogsCheck);

            UIHelperBase genericIndustrialGroup = helper.AddGroup("Generic Industry (Processed materials)");
            genericIndustrialGroup.AddCheckbox("Petrol", settings.get(Petrol), PetrolCheck);
            genericIndustrialGroup.AddCheckbox("Coal", settings.get(Coal), CoalCheck);
            genericIndustrialGroup.AddCheckbox("Food", settings.get(Food), FoodCheck);
            genericIndustrialGroup.AddCheckbox("Lumber", settings.get(Lumber), LumberCheck);
        }

        private void SetSetting(TransferManager.TransferReason reason, bool value)
        {
            settings.set(reason, value);
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
