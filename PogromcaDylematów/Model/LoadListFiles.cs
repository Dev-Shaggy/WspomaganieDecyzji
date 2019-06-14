using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PogromcaDylematów.Model
{
    public class LoadListFiles
    {
        public List<string> Data { get; set; }

        public LoadListFiles()
        {
            Data = new List<string>();
            if (!System.IO.Directory.Exists("data"))
            {
                System.IO.Directory.CreateDirectory("data");
            }
            if (!System.IO.Directory.Exists("data/criterias"))
            {
                System.IO.Directory.CreateDirectory("data/criterias");
            }
            if (!System.IO.Directory.Exists("data/alternatives"))
            {
                System.IO.Directory.CreateDirectory("data/alternatives");
            }
        }
        public void AddFile(string name)
        {
            if(!Data.Contains(name))
            Data.Add(name);
        }
        public void DeleteFile(int id)
        {
            System.IO.File.Delete("data/criterias/" + Data[id] + "Criterias.xml");
            System.IO.File.Delete("data/alternatives/" + Data[id] + "Alternatives.xml");
            Data.RemoveAt(id);
            SaveData();
        }
        public void SaveData()
        {
            XmlDocument doc = new XmlDocument();
            string path = "data/config.xml";
            doc.LoadXml("<listfiles></listfiles>");
            XmlElement newElement;

            foreach (string item in Data)
            {
                newElement = doc.CreateElement("item");
                newElement.InnerText = item;
                doc.DocumentElement.AppendChild(newElement);
            }
            doc.Save(path);
        }
        public void LoadData()
        {
            Data.Clear();
            XmlDocument doc = new XmlDocument();
            string path = "data/config.xml";
            if (!File.Exists(path))
            {
                doc.LoadXml("<listfiles></listfiles>");
                doc.Save(path);
            }
            doc = new XmlDocument();
            
            doc.Load(path);
            XmlNodeList list = doc.GetElementsByTagName("item");

            for (int i = 0; i < list.Count; i++)
            {
                Data.Add(list[i].ChildNodes.Item(0).InnerText.Trim());
            }
        }
    }
}
