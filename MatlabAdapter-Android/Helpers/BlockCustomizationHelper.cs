using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MatlabAdapter_Android.Helpers
{
    public static class BlockCustomizationHelper
    {
        static Dictionary<string, List<string>> _blocks = new Dictionary<string, List<string>>();
        static List<string> _names = new List<string>();
        private static string _schemaName;

        public static void LoadCustomizationXml()
        {
            string content;
            using (var sr = new StreamReader(Android.App.Application.Context.Assets.Open("customization.xml")))
            {
                content = sr.ReadToEnd();
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);

            XmlNodeList schema = doc.GetElementsByTagName("schema");
            _schemaName = schema[0].Attributes["name"].Value;

            List<List<string>> listOfParameters = new List<List<string>>();

            XmlNodeList nodeList = doc.GetElementsByTagName("block");
            for (int i = 0; i < nodeList.Count; i++)
            {
                _names.Add(nodeList[i].Attributes["name"].Value);
                var list = new List<string>();
                for (int j = 0; j < nodeList[i].ChildNodes.Count; j++)
                {
                    list.Add(nodeList[i].ChildNodes[j].InnerText);
                }
                listOfParameters.Add(list);
                _blocks.Add(_names[i], listOfParameters[i]);
            }
        }

        public static string[] GetListOfBlocks()
        {
            return _names.ToArray();
        }

        public static Dictionary<string, List<string>> GetListOfBlocksForCustomization()
        {
            return _blocks;
        }

        public static string GetSchemaName()
        {
            return _schemaName;
        }

    }
}