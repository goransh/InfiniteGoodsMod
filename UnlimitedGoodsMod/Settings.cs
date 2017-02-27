using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using static TransferManager.TransferReason;

namespace InfiniteGoodsMod
{
    public class Settings
    {
        private static readonly string configPath = "InfiniteGoodsConfig.xml";

        public static readonly TransferManager.TransferReason[] supportedIndustry = 
            { Oil, Coal, Grain, Logs, Petrol, Ore, Food, Lumber};

        private static Settings instance;

        private Dictionary<string, bool> dictionary;

        private Settings() {
            dictionary = new Dictionary<string, bool>();

            foreach(TransferManager.TransferReason reason in supportedIndustry)
            {
                Set(reason, false);
            }

            Set(Goods, true);
        }

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
            if (!File.Exists(configPath))
            {
                instance = new Settings();
                instance.WriteXml();
            }
            else
            {
                ReadXml();
                instance.WriteXml();
            }
        }

        public static void SaveSettings()
        {
            if(instance == null)
            {
                LoadSettings();
            }
            else
            {
                instance.WriteXml();
            }
        }

        public void WriteXml()
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            XmlElement rootNode = (XmlElement)doc.AppendChild(doc.CreateElement("settings"));

            foreach (KeyValuePair<string, bool> entry in dictionary)
            {
                XmlElement setting = (XmlElement)rootNode.AppendChild(doc.CreateElement(entry.Key));
                setting.InnerText = entry.Value.ToString();
            }
            doc.Save(configPath);

        }

        private static void ReadXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

            XmlNodeList nodeList = doc.GetElementsByTagName("settings")[0].ChildNodes;


            Dictionary<string, bool> dict = new Dictionary<string, bool>();

            foreach (XmlNode node in nodeList)
            {
                dict[node.Name] = Boolean.Parse(node.InnerText);
            }
            instance = new Settings(dict);
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
    }
}
