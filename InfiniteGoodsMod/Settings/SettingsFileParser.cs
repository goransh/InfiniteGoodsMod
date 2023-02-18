using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

namespace InfiniteGoodsMod.Settings {
    public static class SettingsFileParser {
        private const string ConfigPath = "InfiniteGoodsConfig.xml";
        private const string RootNodeName = "InfiniteGoods";

        public static HashSet<SettingId> ReadSettings() {
            if (!File.Exists(ConfigPath)) {
                Debug.Log($"No settings file found ({ConfigPath})");
                return null;
            }

            var doc = new XmlDocument();

            try {
                doc.Load(ConfigPath);
            } catch (Exception exception) {
                Debug.LogError($"Failed to load settings file: {exception.Message}");
                Debug.LogException(exception);
                return null;
            }

            var root = doc.GetElementsByTagName("settings");
            if (root.Count != 0) {
                // old settings document
                var nodes = root[0].ChildNodes;
                return LegacySettingsFileReader.ReadV2SettingsFile(ref nodes);
            }

            var rootNode = doc.GetElementsByTagName(RootNodeName)[0];
            var settingNodes =
                rootNode.ChildNodes
                    .Cast<XmlNode>()
                    .Where(node => node.Name == "Settings")
                    .Select(childNode => childNode.ChildNodes)
                    .FirstOrDefault();

            if (settingNodes == null || settingNodes.Count == 0) {
                return null;
            }

            var set = new HashSet<SettingId>();

            foreach (XmlNode node in settingNodes) {
                if (bool.Parse(node.InnerText)) {
                    var settingId = ParseSettingId(node.Name);
                    set.Add(settingId);
                }
            }

            return set;
        }

        private static SettingId ParseSettingId(string name) {
            try {
                return (SettingId)Enum.Parse(typeof(SettingId), name, ignoreCase: true);
            } catch (Exception exception) {
                Debug.LogError($"Failed to parse setting id {name}: {exception.Message}");
                Debug.LogException(exception);
                throw;
            }
        }

        public static void WriteSettings(HashSet<SettingId> set) {
            var doc = new XmlDocument();

            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            var root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            var rootNode = (XmlElement)doc.AppendChild(doc.CreateElement(RootNodeName));
            var versionNode = rootNode.AppendChild(doc.CreateElement("Version"));
            versionNode.InnerText = ModIdentity.Version;

            var settingsNode = rootNode.AppendChild(doc.CreateElement("Settings"));

            foreach (var settingId in SettingIdExtensions.Values) {
                var settingIdString = settingId.ToStringOptimized();
                var setting = settingsNode.AppendChild(doc.CreateElement(settingIdString));
                setting.InnerText = set.Contains(settingId).ToString();
            }

            doc.Save(ConfigPath);
        }
    }
}
