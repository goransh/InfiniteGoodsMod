using ICities;
using UnityEngine;

namespace InfiniteGoodsMod {
    public class LoadingExtension : LoadingExtensionBase {
        public override void OnLevelLoaded(LoadMode mode) {
            if (ModIdentity.DebugMode) {
                Debug.Log("Loaded Infinite Goods version " + ModIdentity.Version);
            }
        }

        public override void OnLevelUnloading() => Settings.GetInstance().SaveSettings();
    }
}
