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
            var document = LoadSettingsDocument();
            if (document == null) {
                return null;
            }

            var root = document.GetElementsByTagName("settings");
            if (root.Count != 0) {
                // Old settings document
                var nodes = root[0].ChildNodes;
                return LegacySettingsFileReader.ReadV2SettingsFile(ref nodes);
            }

            var rootNode = document.GetElementsByTagName(RootNodeName)[0];
            var settingNodes = FindSettingNodes(rootNode);

            if (settingNodes == null || settingNodes.Count == 0) {
                return null;
            }

            var majorVersion = ParseMajorVersion(rootNode);

            var set = new HashSet<SettingId>();

            if (majorVersion < 6) {
                MigratePreV6WarehouseSettings(set, settingNodes);
            }

            foreach (XmlNode node in settingNodes) {
                if (!bool.TryParse(node.InnerText, out var settingEnabled)) {
                    Debug.LogError($"Failed to parse {node.InnerText} from {node.Name} to a boolean value.");
                    return null;
                }

                if (!settingEnabled) {
                    continue;
                }

                if (TryParseSettingId(node.Name, out var settingId)) {
                    set.Add(settingId);
                }
            }

            return set;
        }

        private static XmlNodeList FindSettingNodes(XmlNode rootNode) =>
            rootNode.ChildNodes
                .Cast<XmlNode>()
                .Where(node => node.Name == "Settings")
                .Select(childNode => childNode.ChildNodes)
                .FirstOrDefault();

        private static XmlDocument LoadSettingsDocument() {
            if (!File.Exists(ConfigPath)) {
                Debug.Log($"No settings file found ({ConfigPath})");
                return null;
            }

            var document = new XmlDocument();

            try {
                document.Load(ConfigPath);
            } catch (Exception exception) {
                Debug.LogError($"Failed to load settings file: {exception.Message}");
                Debug.LogException(exception);
                return null;
            }

            return document;
        }

        private static int? ParseMajorVersion(XmlNode rootNode) {
            var versionValue = rootNode.ChildNodes
                .Cast<XmlNode>()
                .FirstOrDefault(node => node.Name == "Version")
                ?.InnerText;

            if (versionValue == null) {
                return null;
            }

            var dotIndex = versionValue.IndexOf('.');
            if (dotIndex == -1) {
                return null;
            }

            var majorStr = versionValue.Substring(0, dotIndex);

            if (int.TryParse(majorStr, out var majorVersion)) {
                return majorVersion;
            }

            return null;
        }

        /// <summary>
        ///     Prior to version 6.0, the Plopped industry settings would apply to Warehouses (such as Grain Silos) in
        ///     addition to the plopped industry buildings (such as Flour Mills). In version 6.0, these settings were
        ///     split into two separate categories. This migration will make it so that if the user had the Plopped
        ///     industry setting enabled prior to the update, the corresponding Warehouse setting will also be enabled.
        ///     The end result is that the mod behaves exactly as before without any user interaction.
        /// </summary>
        private static void MigratePreV6WarehouseSettings(HashSet<SettingId> set, XmlNodeList settingsNodes) {
            foreach (XmlNode node in settingsNodes) {
                if (!bool.TryParse(node.InnerText, out var settingEnabled) || !settingEnabled) {
                    continue;
                }
                if (TryParseSettingId(node.Name, out var settingId)) {
                    if (settingId == SettingId.PloppedIndustryOil) {
                        set.Add(SettingId.WarehouseOil);
                    }

                    if (settingId == SettingId.PloppedIndustryOre) {
                        set.Add(SettingId.WarehouseOre);
                    }

                    if (settingId == SettingId.PloppedIndustryGrain) {
                        set.Add(SettingId.WarehouseGrain);
                    }

                    if (settingId == SettingId.PloppedIndustryLogs) {
                        set.Add(SettingId.WarehouseLogs);
                    }
                }
            }
        }

        private static bool TryParseSettingId(string name, out SettingId settingId) {
            try {
                settingId = (SettingId)Enum.Parse(typeof(SettingId), name, ignoreCase: true);
                return true;
            } catch (Exception exception) {
                Debug.LogError($"Failed to parse setting id {name}: {exception.Message}");
                settingId = default;
                return false;
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
