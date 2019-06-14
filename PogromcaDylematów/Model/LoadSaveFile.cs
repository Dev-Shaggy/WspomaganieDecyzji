using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PogromcaDylematów.Model
{
    class LoadSaveFile
    {
        private List<Alternative> AlternativeList;
        private Criterias CriteriaList;
        private List<Parameters> ParametersList;

        public LoadSaveFile()
        {
            AlternativeList = new List<Alternative>();
            CriteriaList = new Criterias();
            ParametersList = new List<Parameters>();
        }
        public void DeleteFile(string filename)
        {
        }
        public void SaveData(string fileName, List<Alternative> alternatives, Criterias criterias)
        {
            string CriteriaFile = "data/criterias/" + fileName + "Criterias.xml";

            XmlDocument docCritery = new XmlDocument();
            XmlElement newElement;

            docCritery.LoadXml("<criterias></criterias>");
            foreach (string item in criterias.CriteriasNames)
            {
                newElement = docCritery.CreateElement("item");
                newElement.InnerText = item;
                docCritery.DocumentElement.AppendChild(newElement);
            }
            docCritery.Save(CriteriaFile);

            string AlternativeFile = "data/alternatives/" + fileName + "Alternatives.xml";

            XmlDocument docAlternative = new XmlDocument();

            XmlNode alternativeNode;
            XmlElement nameElement;

            docAlternative.LoadXml("<alternatives></alternatives>");
            for (int i = 0; i < alternatives.Count; i++)
            {
                alternativeNode = docAlternative.CreateElement("alternative");

                nameElement = docAlternative.CreateElement("name");
                nameElement.InnerText = alternatives[i].Name;
                alternativeNode.AppendChild(nameElement);

                for(int j = 0; j < alternatives[i].parameters.parameters.Count; j++)
                {
                    newElement = docAlternative.CreateElement("item");
                    newElement.InnerText = alternatives[i].parameters.parameters[j];
                    alternativeNode.AppendChild(newElement);
                }
                docAlternative.DocumentElement.AppendChild(alternativeNode);
            }
            docAlternative.Save(AlternativeFile);
        }

        public void LoadData(string fileName)
        {
            AlternativeList.Clear();
            CriteriaList.Clear();
            ParametersList.Clear();

            string pathCritery = "data/criterias/" + fileName + "Criterias.xml";

            XmlDocument openCriteriaFile = new XmlDocument();
            openCriteriaFile.Load(pathCritery);

            XmlNodeList listCriterias = openCriteriaFile.GetElementsByTagName("item");

            for (int i = 0; i <listCriterias.Count; i++)
            {
                CriteriaList.AddCriteria(listCriterias[i].ChildNodes.Item(0).InnerText.Trim());
            }

            string pathAlternative = "data/alternatives/" + fileName + "Alternatives.xml";
            XmlDocument openAlternativeFile = new XmlDocument();
            openAlternativeFile.Load(pathAlternative);

           // XmlNodeList listAlternatives = openAlternativeFile.GetElementsByTagName("alternative");
            XmlNodeList listParameters = openAlternativeFile.GetElementsByTagName("item");
            XmlNodeList listNames = openAlternativeFile.GetElementsByTagName("name");

            List<string> listValues = new List<string>();
            List<string> listnames = new List<string>();


            for (int i = 0; i < listParameters.Count; i++)
            {
                listValues.Add(listParameters[i].ChildNodes.Item(0).InnerText.Trim());
            }
            for (int i = 0; i < listNames.Count; i++)
            {
                listnames.Add(listNames[i].ChildNodes.Item(0).InnerText.Trim());
                ParametersList.Add(new Parameters());
            }
            int valuesCount = listValues.Count;
            for (int i = 0; i < listnames.Count; i++)
            {      
                for (int j = 0; j < valuesCount / listnames.Count; j++)
                {
                    ParametersList[i].AddOnceParameter(listValues[0]);
                    listValues.RemoveAt(0);
                }
            }
            for (int i = 0; i < listnames.Count; i++)
            {
                AlternativeList.Add(new Alternative(listnames[i], ParametersList[i]));
            }
            openAlternativeFile = null;
            openCriteriaFile = null;
            GC.Collect();
        }

        public List<Alternative> getAlternatives()
        {
            return AlternativeList;
        }
        public Criterias getCriterias()
        {
            return CriteriaList;
        }
        public List<Parameters> getParametrs()
        {
            return ParametersList;
        }
    }
}
