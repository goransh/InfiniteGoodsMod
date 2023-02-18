using System.Collections.Generic;
using System.Xml;
using InfiniteGoodsMod.Transfer;

namespace InfiniteGoodsMod.Settings {
    public static class LegacySettingsFileReader {
        /// <summary>
        ///     Used for updating settings files created before version 3.0 to the new style.
        /// </summary>
        public static HashSet<SettingId> ReadV2SettingsFile(ref XmlNodeList settingNodes) {
            var set = new HashSet<SettingId>();

            foreach (XmlNode node in settingNodes) {
                if (bool.Parse(node.InnerText)) {
                    switch (node.Name) {
                        case "Goods":
                            set.Add(SettingId.CommercialGoods);
                            break;
                        case "Oil":
                            set.Add(SettingId.SpecializedIndustryOil);
                            break;
                        case "Ore":
                            set.Add(SettingId.SpecializedIndustryOre);
                            break;
                        case "Grain":
                            set.Add(SettingId.SpecializedIndustryGrain);
                            break;
                        case "Logs":
                            set.Add(SettingId.SpecializedIndustryLogs);
                            break;
                        case "Petrol":
                            set.Add(SettingId.GenericIndustryPetrol);
                            break;
                        case "Coal":
                            set.Add(SettingId.GenericIndustryCoal);
                            break;
                        case "Food":
                            set.Add(SettingId.GenericIndustryFood);
                            break;
                        case "Lumber":
                            set.Add(SettingId.GenericIndustryLumber);
                            break;
                    }
                }
            }

            return set;
        }
    }
}
