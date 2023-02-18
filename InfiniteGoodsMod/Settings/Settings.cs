using System.Collections.Generic;

namespace InfiniteGoodsMod.Settings {
    public class Settings {
        private static Settings Instance;

        private readonly HashSet<SettingId> _enabledSettings;

        /// <summary>
        ///     Get or set enabled/disabled state of a setting.
        /// </summary>
        /// <param name="settingId">
        ///     The setting ID (property name).
        /// </param>
        public bool this[SettingId settingId] {
            get => _enabledSettings.Contains(settingId);
            set {
                if (value) {
                    _enabledSettings.Add(settingId);
                } else {
                    _enabledSettings.Remove(settingId);
                }
            }
        }

        private Settings(HashSet<SettingId> enabledSettings) {
            _enabledSettings = enabledSettings;
        }

        public void SetAll(bool state) {
            if (state) {
                foreach (var settingId in SettingIdExtensions.Values) {
                    if (settingId != SettingId.Debug) {
                        _enabledSettings.Add(settingId);
                    }
                }
            } else {
                ClearSettings();
            }
        }

        private void ClearSettings() {
            _enabledSettings.RemoveWhere(id => id != SettingId.Debug);
        }

        public void RestoreDefault() {
            _enabledSettings.Clear(); // This also disables debug mode
            foreach (var settingId in GetDefaultSettings()) {
                _enabledSettings.Add(settingId);
            }
        }

        public static Settings GetInstance() => Instance ?? LoadSettings();

        private static Settings LoadSettings() => Instance = new Settings(ReadSettingsOrDefault());

        public void SaveSettings() {
            SettingsFileParser.WriteSettings(_enabledSettings);
        }

        private static HashSet<SettingId> ReadSettingsOrDefault()
            => SettingsFileParser.ReadSettings() ?? GetDefaultSettings();

        private static HashSet<SettingId> GetDefaultSettings() => new HashSet<SettingId> { SettingId.CommercialGoods };
    }
}
