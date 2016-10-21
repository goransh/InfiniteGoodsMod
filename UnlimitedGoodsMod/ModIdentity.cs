using ICities;

namespace InfiniteGoodsMod
{
    public class ModIdentity : IUserMod
    {
        public const System.UInt64 workshop_id = 725555912;

        public string Name => "Infinite Goods";

        public string Description => "Commercial buildings have infinite goods. Version 2.0";


        public void OnSettingsUI(UIHelperBase helper)
        {
            SettingsPanel sp = new SettingsPanel(Settings.getInstance());
            sp.createPanel(helper);
        }

        
    }
}
