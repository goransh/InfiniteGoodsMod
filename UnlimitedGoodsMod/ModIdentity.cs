﻿using ICities;

namespace InfiniteGoodsMod
{
    public class ModIdentity : IUserMod
    {
        public const System.UInt64 WorkshopId = 725555912;
        public const string Version = "2.2";

        public string Name => "Infinite Goods";

        public string Description => "Remove the need for industry. (v" + Version + ")";


        public void OnSettingsUI(UIHelperBase helper)
        {
            SettingsPanel sp = new SettingsPanel();
            sp.CreatePanel(helper);
            
        }

        
    }
}
