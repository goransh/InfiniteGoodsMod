using ICities;

namespace UnlimitedGoodsMod.UI
{
    public class LoadingExtension : LoadingExtensionBase
    {

        public override void OnLevelLoaded(LoadMode mode) 
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "[Unlimited Goods] Loaded");
        }

    }
}
