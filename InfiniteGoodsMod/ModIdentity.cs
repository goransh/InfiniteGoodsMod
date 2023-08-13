using ICities;
using InfiniteGoodsMod.Gui;

namespace InfiniteGoodsMod {
    public class ModIdentity : IUserMod {
        public const ulong WorkshopId = 725555912;
        public const string Version = "6.1";

        public string Name => "Infinite Goods";

        public string Description => "Remove the need for industry (v" + Version + ")";

        public void OnSettingsUI(UIHelperBase helper) {
            var settingsPanel = new SettingsPanel();
            settingsPanel.CreatePanel(helper);
        }
    }
}
