using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using static TransferManager.TransferReason;

namespace InfiniteGoodsMod
{
    public class Settings
    {
        public static readonly TransferManager.TransferReason[] supportedIndustry = 
            { Oil, Coal, Grain, Logs, Petrol, Ore, Food, Lumber};

        private static Settings instance;

        private Dictionary<string, bool> dictionary;

        private Settings(Dictionary<string, bool> dictionary)
        {
            this.dictionary = dictionary;
        }

        public static Settings GetInstance()
        {
            if(instance == null)
            {
                LoadSettings();
            }
            return instance;
        }

        private static void LoadSettings()
        {
            instance = new Settings(XMLUtil.ReadXml());
        }

        public void SaveSettings()
        {
            if(instance == null)
                LoadSettings();
            else
                XMLUtil.WriteXml(dictionary);
        }

        public bool Get(TransferManager.TransferReason reason)
        {
            return Get(reason.ToString());
        }

        public bool Get(string reason)
        {
            if (!dictionary.ContainsKey(reason))
            {
                return false;
            }

            return dictionary[reason];
        }

        public void Set(TransferManager.TransferReason reason, bool value)
        {
            Set(reason.ToString(), value);
        }

        public void Set(string reason, bool value)
        {
            dictionary[reason] = value;
        }

        public static Dictionary<string, bool> GenerateDefaultSettings()
        {
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();

            foreach (TransferManager.TransferReason reason in supportedIndustry)
            {
                dictionary[reason.ToString()] = false;
            }

            dictionary[Goods.ToString()] = true;
            return dictionary;
        }
    }

    static class XMLUtil
    {
        private const string configPath = "InfiniteGoodsConfig.xml";
        private const string rootNodeName = "settings";

        private static void checkFileExists()
        {
            if (!File.Exists(configPath))
            {
                WriteXml(Settings.GenerateDefaultSettings());
            }
        }

        public static void WriteXml(Dictionary<string, bool> dictionary)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            XmlElement rootNode = (XmlElement)doc.AppendChild(doc.CreateElement(rootNodeName));
            rootNode.SetAttribute("version", ModIdentity.Version);

            foreach (KeyValuePair<string, bool> entry in dictionary)
            {
                XmlElement setting = (XmlElement)rootNode.AppendChild(doc.CreateElement(entry.Key));
                setting.InnerText = entry.Value.ToString();
            }
            doc.Save(configPath);

        }

        public static Dictionary<string, bool> ReadXml()
        {
            checkFileExists();

            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

            XmlNodeList nodeList = doc.GetElementsByTagName(rootNodeName)[0].ChildNodes;


            Dictionary<string, bool> dict = new Dictionary<string, bool>();

            foreach (XmlNode node in nodeList)
            {
                dict[node.Name] = Boolean.Parse(node.InnerText);
            }
            return dict;
        }
    }
}
