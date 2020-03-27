using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace InfiniteGoodsMod {
    public class ModIdentity : IUserMod {
        public const ulong WorkshopId = 725555912;
        public const string Version = "4.0";
        public const bool DebugMode = false;

        public string Name => "Infinite Goods";

        public string Description => "Remove the need for industry (v" + Version + ")";


        public void OnSettingsUI(UIHelperBase helper) {
            SettingsPanel sp = new SettingsPanel();
            sp.CreatePanel(helper);
        }
    }
}
