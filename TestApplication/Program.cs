using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(Environment.CurrentDirectory + "\\customization.xml");

            Dictionary<string, List<string>> blocks = new Dictionary<string, List<string>>();
            List<List<string>> listOfParameters = new List<List<string>>();

            XmlNodeList schema = doc.GetElementsByTagName("schema");
            var name = schema[0].Attributes["name"].Value;


            XmlNodeList elemList = doc.GetElementsByTagName("block");
            List<string> names = new List<string>();
            for (int i = 0; i < elemList.Count; i++)
            {
                names.Add(elemList[i].Attributes["name"].Value);
                var list = new List<string>();
                for (int j = 0; j < elemList[i].ChildNodes.Count; j++)
                {
                    list.Add(elemList[i].ChildNodes[j].InnerText);
                }
                listOfParameters.Add(list);
                blocks.Add(names[i],listOfParameters[i]);
            }           
        }
    }
}
