using ColossalFramework.Plugins;
using ICities;

namespace InfiniteGoodsMod {
    public class LoadingExtension : LoadingExtensionBase {
        public static void Log(string message, PluginManager.MessageType type = PluginManager.MessageType.Message) {
            DebugOutputPanel.AddMessage(type, "[Infinite Goods] " + message);
        }

        public override void OnLevelLoaded(LoadMode mode) {
            Log("Loaded version " + ModIdentity.Version);
        }

        public override void OnLevelUnloading() {
            Settings.GetInstance().SaveSettings();
        }
    }
}