using ICities;
using InfiniteGoodsMod.Settings;
using UnityEngine;

namespace InfiniteGoodsMod {
    public class LoadingExtension : LoadingExtensionBase {
        public override void OnLevelLoaded(LoadMode mode) {
            if (GetSettings()[SettingId.Debug]) {
                Debug.Log("Loaded Infinite Goods version " + ModIdentity.Version);
            }
        }

        public override void OnLevelUnloading() => GetSettings().SaveSettings();

        private static Settings.Settings GetSettings() => Settings.Settings.GetInstance();
    }
}
