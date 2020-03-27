using System.Collections.Generic;

namespace InfiniteGoodsMod {
    public class Settings {
        private static Settings Instance;

        private readonly HashSet<string> activeTransfers;

        private Settings(HashSet<string> activeTransfers) {
            this.activeTransfers = activeTransfers;
        }

        public static Settings GetInstance() => Instance ?? LoadSettings();

        private static Settings LoadSettings() => Instance = new Settings(ReadSettingsOrDefault());

        public void SaveSettings() {
            var settings = activeTransfers;
            if (Instance == null) settings = ReadSettingsOrDefault();
            SettingsFileParser.WriteSettings(settings);
        }

        public bool Get(string setting) => activeTransfers.Contains(setting);

        public void Set(string setting, bool active) {
            if (active) activeTransfers.Add(setting);
            else activeTransfers.Remove(setting);
        }

        private static HashSet<string> ReadSettingsOrDefault()
            => SettingsFileParser.ReadSettings() ?? new HashSet<string> {GoodsTransfer.CommercialGoods.Id};
    }
}
