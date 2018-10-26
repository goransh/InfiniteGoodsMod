using ICities;

namespace InfiniteGoodsMod {
    public class LoadingExtension : LoadingExtensionBase {

        public override void OnLevelLoaded(LoadMode mode) {
            Utils.Log("Loaded version " + ModIdentity.Version);
        }

        public override void OnLevelUnloading() => Settings.GetInstance().SaveSettings();
    }
}