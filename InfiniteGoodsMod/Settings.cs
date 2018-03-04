using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace InfiniteGoodsMod {

    public enum Setting {
        CommercialGoods,
        SpecializedOil,
        SpecializedOre,
        SpecializedGrain,
        SpecializedLogs,
        GenericPetrol,
        GenericCoal,
        GenericFood,
        GenericLumber,
        ShelterGoods
    }

    public class Settings {
        private static Settings instance;

        private readonly Dictionary<Setting, bool> dictionary;

        private Settings(Dictionary<Setting, bool> dictionary) {
            this.dictionary = dictionary;
        }

        public static Settings GetInstance() {
            if (instance == null) {
                LoadSettings();
            }

            return instance;
        }

        private static void LoadSettings() {
            instance = new Settings(XMLUtil.ReadXml());
        }

        public void SaveSettings() {
            if (instance == null)
                LoadSettings();
            else
                XMLUtil.WriteXml(dictionary);
        }

        public bool Get(Setting setting) {
            return dictionary.ContainsKey(setting) && dictionary[setting];
        }

        public void Set(Setting setting, bool value) {
            dictionary[setting] = value;
        }

        public static Dictionary<Setting, bool> GenerateDefaultSettings() {
            var dictionary = new Dictionary<Setting, bool>();

            foreach (Setting setting in Enum.GetValues(typeof(Setting))) {
                dictionary[setting] = false;
            }

            dictionary[Setting.CommercialGoods] = true;
            return dictionary;
        }
    }

    internal static class XMLUtil {
        private const string configPath = "InfiniteGoodsConfig.xml";
        private const string rootNodeName = "InfiniteGoods";

        private static void checkFileExists() {
            if (!File.Exists(configPath)) {
                WriteXml(Settings.GenerateDefaultSettings());
            }
        }

        public static void WriteXml(Dictionary<Setting, bool> dictionary) {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            XmlElement rootNode = (XmlElement) doc.AppendChild(doc.CreateElement(rootNodeName));
            XmlNode versionNode = rootNode.AppendChild(doc.CreateElement("Version"));
            versionNode.InnerText = ModIdentity.Version;

            XmlNode settingsNode = rootNode.AppendChild(doc.CreateElement("Settings"));

            foreach (var entry in dictionary) {
                XmlNode setting = settingsNode.AppendChild(doc.CreateElement(entry.Key.ToString()));
                setting.InnerText = entry.Value.ToString();
            }

            doc.Save(configPath);
        }

        public static Dictionary<Setting, bool> ReadXml() {
            checkFileExists();

            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

            XmlNodeList root = doc.GetElementsByTagName("settings");
            if (root.Count != 0) {
                // old settings document
                var nodes = root[0].ChildNodes;
                return LoadV2SettingsFile(ref nodes);
            }

            var rootNode = doc.GetElementsByTagName(rootNodeName)[0];
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

            var dict = new Dictionary<Setting, bool>();

            foreach (XmlNode node in settingNodes) {
                dict[(Setting) Enum.Parse(typeof(Setting), node.Name)] = bool.Parse(node.InnerText);
            }

            return dict;
        }

        /// <summary>
        /// Used for updating settings files created before version 3.0 to the new style.
        /// </summary>
        private static Dictionary<Setting, bool> LoadV2SettingsFile(ref XmlNodeList settingNodes) {
            var dict = new Dictionary<Setting, bool>();

            foreach (XmlNode node in settingNodes) {
                bool parsed = bool.Parse(node.InnerText);
                switch (node.Name) {
                    case "Goods":
                        dict[Setting.CommercialGoods] = parsed;
                        break;
                    case "Oil":
                        dict[Setting.SpecializedOil] = parsed;
                        break;
                    case "Ore":
                        dict[Setting.SpecializedOre] = parsed;
                        break;
                    case "Grain":
                        dict[Setting.SpecializedGrain] = parsed;
                        break;
                    case "Logs":
                        dict[Setting.SpecializedLogs] = parsed;
                        break;
                    case "Petrol":
                        dict[Setting.GenericPetrol] = parsed;
                        break;
                    case "Coal":
                        dict[Setting.GenericCoal] = parsed;
                        break;
                    case "Food":
                        dict[Setting.GenericFood] = parsed;
                        break;
                    case "Lumber":
                        dict[Setting.GenericLumber] = parsed;
                        break;
                }
            }

            dict[Setting.ShelterGoods] = false;

            WriteXml(dict);

            return dict;
        }
    }
}