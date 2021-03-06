﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace InfiniteGoodsMod {
    public static class SettingsFileParser {
        private const string ConfigPath = "InfiniteGoodsConfig.xml";
        private const string RootNodeName = "InfiniteGoods";

        public static HashSet<string> ReadSettings() {
            if (!File.Exists(ConfigPath)) return null;

            XmlDocument doc = new XmlDocument();

            try {
                doc.Load(ConfigPath);
            } catch (XmlException) {
                //TODO error message?
                return null;
            }

            XmlNodeList root = doc.GetElementsByTagName("settings");
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

            if (settingNodes == null || settingNodes.Count == 0) return null;

            var set = new HashSet<string>();

            foreach (XmlNode node in settingNodes) {
                if (bool.Parse(node.InnerText)) set.Add(node.Name);
            }

            return set;
        }

        public static void WriteSettings(HashSet<string> set) {
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
    }
}
