using ICities;
using System;

using static ColossalFramework.Plugins.PluginManager.MessageType;

namespace InfiniteGoodsMod.UI
{
    public class LoadingExtension : LoadingExtensionBase
    {

        public static void Log(String message, ColossalFramework.Plugins.PluginManager.MessageType type = Message)
        {
            DebugOutputPanel.AddMessage(type, "[Infinite Goods] " + message);
        }

        public override void OnLevelLoaded(LoadMode mode) 
        {
            Log("Loaded version 2.1");
        }

        public override void OnLevelUnloading()
        {
            Settings.SaveSettings();
        }


    }
}
