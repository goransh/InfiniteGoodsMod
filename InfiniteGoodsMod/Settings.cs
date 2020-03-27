using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace InfiniteGoodsMod {
    public class Settings {
        private static Settings Instance;

        private readonly HashSet<string> activeTransfers;

        private Settings(HashSet<string> activeTransfers) {
            this.activeTransfers = activeTransfers;
        }

        public static Settings GetInstance() {
            if (Instance == null) {
                LoadSettings();
            }

            return Instance;
        }

        private static void LoadSettings() {
            Instance = new Settings(XmlUtil.ReadXml());
        }

        public void SaveSettings() {
            if (Instance == null)
                LoadSettings(); // save default settings
            else
                XmlUtil.WriteXml(activeTransfers);
        }

        public bool Get(string setting) {
            return activeTransfers.Contains(setting);
        }

        public void Set(string setting, bool active) {
            if (active)
                activeTransfers.Add(setting);
            else
                activeTransfers.Remove(setting);
        }

        public static HashSet<string> GenerateDefaultSettings() {
            return new HashSet<string> {GoodsTransfer.CommercialGoods.Id};
        }
    }

    internal static class XmlUtil {
        private const string ConfigPath = "InfiniteGoodsConfig.xml";
        private const string RootNodeName = "InfiniteGoods";

        private static void CheckFileExists() {
            if (!File.Exists(ConfigPath)) {
                WriteXml(Settings.GenerateDefaultSettings());
            }
        }

        public static void WriteXml(HashSet<string> set) {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            XmlElement rootNode = (XmlElement) doc.AppendChild(doc.CreateElement(RootNodeName));
            XmlNode versionNode = rootNode.AppendChild(doc.CreateElement("Version"));
            versionNode.InnerText = ModIdentity.Version;

            XmlNode settingsNode = rootNode.AppendChild(doc.CreateElement("Settings"));

            foreach (var transfer in GoodsTransfer.GoodsTransfers) {
                XmlNode setting = settingsNode.AppendChild(doc.CreateElement(transfer.Id));
                setting.InnerText = set.Contains(transfer.Id).ToString();
            }

            doc.Save(ConfigPath);
        }

        public static HashSet<string> ReadXml() {
            CheckFileExists();

            XmlDocument doc = new XmlDocument();

            try {
                doc.Load(ConfigPath);
            } catch (XmlException) {
                var defaultSettings = Settings.GenerateDefaultSettings();
                WriteXml(defaultSettings);
                //TODO error message?
                return defaultSettings;
            }

            XmlNodeList root = doc.GetElementsByTagName("settings");
            if (root.Count != 0) {
                // old settings document
                var nodes = root[0].ChildNodes;
                return LoadV2SettingsFile(ref nodes);
            }

            var rootNode = doc.GetElementsByTagName(RootNodeName)[0];
            XmlNodeList settingNodes = null;

            foreach (XmlNode childNode in rootNode.ChildNodes) {
                if (childNode.Name == "Settings") {
                    settingNodes = childNode.ChildNodes;
                    break;
                }
            }

            if (settingNodes == null || settingNodes.Count == 0) {
                WriteXml(Settings.GenerateDefaultSettings());
                return ReadXml();
            }

            var set = new HashSet<string>();

            foreach (XmlNode node in settingNodes) {
                if (bool.Parse(node.InnerText))
                    set.Add(node.Name);
            }

            return set;
        }

        /// <summary>
        /// Used for updating settings files created before version 3.0 to the new style.
        /// </summary>
        private static HashSet<string> LoadV2SettingsFile(ref XmlNodeList settingNodes) {
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

            WriteXml(set);

            return set;
        }
    }
}
