using ICities;

namespace InfiniteGoodsMod
{
    public class ModIdentity : IUserMod
    {
        public const System.UInt64 workshop_id = 725555912;

        public string Name => "Infinite Goods";

        public string Description => "Remove the need for industry. Version 2.1";


        public void OnSettingsUI(UIHelperBase helper)
        {
            SettingsPanel sp = new SettingsPanel(Settings.GetInstance());
            sp.CreatePanel(helper);
        }

        
    }
}
