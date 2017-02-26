using System.IO;
using System.Xml;
using Android.App;

namespace MatlabAdapter_Android.Helpers
{
    public class Config
    {
        public string Ip { get; set; }
        public string MainResources { get; set; }
        public string GetModelInfoPath { get; set; }
        public string CheckMatlabStatusPath { get; set; }
        public string StopMatlabPath { get; set; }
        public string StartMatlabPath { get; set; }
        public string OpenSimulinkModelPath { get; set; }
        public string ChangeParamValuePath { get; set; }
        public string GetParamValuePath { get; set; }
        public string GetScopeData { get; set; }

    }

    public static class ConfigHelper
    {
        private static Config _config;

        public static void LoadXmlData()
        {
            string content;
            using (var sr = new StreamReader(Application.Context.Assets.Open("config.xml")))
            {
                content = sr.ReadToEnd();
            }

            var doc = new XmlDocument();
            doc.LoadXml(content);

            var node = doc.DocumentElement.SelectSingleNode("/application/config");
        
            _config = new Config
            {
                Ip = node["ip"].InnerText,
                MainResources = node["mainResources"].InnerText,
                GetModelInfoPath = node["getModelInfoPath"].InnerText,
                CheckMatlabStatusPath = node["checkMatlabStatusPath"].InnerText,
                StopMatlabPath = node["stopMatlabPath"].InnerText,
                StartMatlabPath = node["startMatlabPath"].InnerText,
                OpenSimulinkModelPath = node["openSimulinkModelPath"].InnerText,
                ChangeParamValuePath = node["changeParamValuePath"].InnerText,
                GetParamValuePath = node["getParamValuePath"].InnerText,
                GetScopeData = node["getScopeData"].InnerText

            };
        }

        public static Config GetConfig()
        {
            return _config;
        }
    }
}