using System.Collections.Generic;
using System.Xml;

namespace InfiniteGoodsMod {
    public static class LegacySettingsFileReader {
        /// <summary>
        ///     Used for updating settings files created before version 3.0 to the new style.
        /// </summary>
        public static HashSet<string> ReadV2SettingsFile(ref XmlNodeList settingNodes) {
            var set = new HashSet<string>();

            foreach (XmlNode node in settingNodes) {
                if (bool.Parse(node.InnerText)) {
                    switch (node.Name) {
                        case "Goods":
                            set.Add(GoodsTransfer.CommercialGoods.Id);
                            break;
                        case "Oil":
                            set.Add(GoodsTransfer.SpecializedOil.Id);
                            break;
                        case "Ore":
                            set.Add(GoodsTransfer.SpecializedOre.Id);
                            break;
                        case "Grain":
                            set.Add(GoodsTransfer.SpecializedGrain.Id);
                            break;
                        case "Logs":
                            set.Add(GoodsTransfer.SpecializedLogs.Id);
                            break;
                        case "Petrol":
                            set.Add(GoodsTransfer.GenericPetrol.Id);
                            break;
                        case "Coal":
                            set.Add(GoodsTransfer.GenericCoal.Id);
                            break;
                        case "Food":
                            set.Add(GoodsTransfer.GenericFood.Id);
                            break;
                        case "Lumber":
                            set.Add(GoodsTransfer.GenericLumber.Id);
                            break;
                    }
                }
            }

            return set;
        }
    }
}
