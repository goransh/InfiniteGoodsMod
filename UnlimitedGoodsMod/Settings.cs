using ColossalFramework.IO;
using ColossalFramework.Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
                set(reason, false);
            }

            set(Goods, true);
        }

        private Settings(Dictionary<string, bool> dictionary)
        {
            this.dictionary = dictionary;
        }

        public static Settings getInstance()
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
                instance = ReadXml();
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

        private static Settings ReadXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

            XmlNodeList nodeList = doc.GetElementsByTagName("settings")[0].ChildNodes;


            Dictionary<string, bool> dict = new Dictionary<string, bool>();

            foreach (XmlNode node in nodeList)
            {
                dict[node.Name] = Boolean.Parse(node.InnerText);
            }
            return new Settings(dict);
        }

        public bool get(TransferManager.TransferReason reason)
        {
            return get(reason.ToString());
        }

        public bool get(string reason)
        {
            if (!dictionary.ContainsKey(reason))
            {
                return false;
            }

            return dictionary[reason];
        }

        public void set(TransferManager.TransferReason reason, bool value)
        {
            set(reason.ToString(), value);
        }


        public void set(string reason, bool value)
        {
            dictionary[reason] = value;
        }
    }
}
