using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using MatlabAdapter_Android.Helpers;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace MatlabAdapter_Android
{
    public class MatlabServicesProvider : Activity
    {
        private readonly Config config;
        private bool IsMatlabRunning { get; set; } = false;
        private static HttpClient _client = new HttpClient();
        private SynchronizationContext sc;

        public MatlabServicesProvider()
        {
            config = ConfigHelper.GetConfig();
        }

        public string CallService(string path)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    var matlabOutput =  webClient.DownloadString(new Uri(config.Ip + config.MainResources + path));
                    return matlabOutput;
                }
            }
            catch (Exception e)
            {
//                    Toast.MakeText(this, "Error: " + e, ToastLength.Long).Show(); 
            }
            return "no Output";
        }

        public void DoItAll()
        {
            StartMatlab();
            OpenSimulinkModel("");
            GetModelInfo("");
        }

        public void GetListOfBlocks()
        {
            if (IsMatlabRunning)
            {
                var output = CallService(@"matlab/simulink/getListOfBlock");
                var t = new Regex(@"\'(.*?)\'", RegexOptions.Singleline);
                var listOfBlocks = (from Match match in t.Matches(output) select match.Value).ToList();
            }
        }
        
        public void UploadSimulinkModel(string modelPath) //TODO: Full implemetation
        {
        }

        public  void OpenSimulinkModel(string modelName)
        {
            if (IsMatlabRunning)
                 CallService(config.OpenSimulinkModelPath);
        }

        public void StartMatlab()
        {
            if (!IsMatlabRunning)
            {
                CallService(config.StartMatlabPath);
                IsMatlabRunning = CheckMatlabStatus();
            }
            else
                Toast.MakeText(this, "Matlab is already running", ToastLength.Short).Show();

        }

        public void StopMatlab()
        {
            CallService(config.StopMatlabPath);
            IsMatlabRunning = CheckMatlabStatus();
        }

        public void ChangeParamValue(string modelName, string blockName, string paramName, string paramValue)
        {
             CallService(config.ChangeParamValuePath + "?modelName=" + modelName + "&blockName=" + blockName
                        + "&paramName=" + paramName + "&paramValue=" + paramValue);
        }

        public string GetParamValue(string modelName, string blockName, string paramName)
        {
            var value = CallService(config.GetParamValuePath + "?modelName=" + modelName + "&blockName=" + blockName
                        + "&paramName=" + paramName);

            return value;
        }

        public void GetModelInfo(string modelName)
        {
            var mapOfBlocks = new Dictionary<string, string>();
            if (IsMatlabRunning)
            {
                var matlabOutput = CallService(config.GetModelInfoPath);
                JToken matlabOutputParsed = JObject.Parse(matlabOutput);
                var innerBlocks = matlabOutputParsed.First.First;
                var listOfBlocks = innerBlocks.ToList();
                foreach (var block in listOfBlocks)
                {
                    mapOfBlocks.Add("SimulinkName", block["simulinkName"].ToString());
                }
            }
        }

        public bool CheckMatlabStatus()
        {
            return Convert.ToBoolean(CallService(config.CheckMatlabStatusPath));
        }

        public string GetScopeData()
        {
            return CallService(config.GetScopeData + "?modelName=vrmaglev");
        }
    }
}