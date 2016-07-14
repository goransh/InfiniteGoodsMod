using ICities;

namespace InfiniteGoodsMod.UI
{
    public class LoadingExtension : LoadingExtensionBase
    {

        public override void OnLevelLoaded(LoadMode mode) 
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "[Infinite Goods] Loaded");
        }

    }
}
