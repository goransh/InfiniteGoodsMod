using ColossalFramework.IO;
using ColossalFramework.Steamworks;
using ICities;
using System;
using UnityEngine;

namespace InfiniteGoodsMod.UI
{
    public class LoadingExtension : LoadingExtensionBase
    {

        public static void Log(String message)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, message);
        }

        public override void OnLevelLoaded(LoadMode mode) 
        {
            Log("[Infinite Goods] Loaded version 2.0");
        }

        public override void OnLevelUnloading()
        {
            Settings.SaveSettings();
        }


    }
}
