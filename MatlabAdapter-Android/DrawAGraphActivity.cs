using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace MatlabAdapter_Android
{
    [Activity(Label = "DrawAGraphActivity")]
    public class DrawAGraphActivity : Activity
    {

        
        private MatlabServicesProvider servicesProvider;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Graph);
            PlotView plotView = FindViewById<PlotView>(Resource.Id.plotView1);
            plotView.SetBackgroundColor(new Color(Color.White));

//            var param = plotView.LayoutParameters;
//            param.Height = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 250, Resources.DisplayMetrics);
//            plotView.LayoutParameters = param;

            var modelName = "vrmaglev";
            servicesProvider = new MatlabServicesProvider();

            var serviceOutput = servicesProvider.GetScopeData();

            JObject jsonObject = JObject.Parse(serviceOutput);

            if (jsonObject[modelName + "/Scope"]["1.0"] != null)
            {
                var scopeOneOutput = jsonObject[modelName + "/Scope"]["1.0"];
                plotView.Model = CreatePlotModel(scopeOneOutput[0].ToList(), scopeOneOutput[1].ToList());
            }

            if (jsonObject[modelName + "/Scope"]["2.0"] != null)
            {
            }           
        }


        private PlotModel CreatePlotModel(List<JToken> x, List<JToken> y)
        {

            var metrics = Resources.DisplayMetrics;

            var height = metrics.HeightPixels;
            var width = metrics.WidthPixels;


            var plotModel = new PlotModel {Title = "Graph (*1000)"};
            List<DataPoint> listOfDataPoints = new List<DataPoint>();

            plotModel.Axes.Add(new LinearAxis {Position = AxisPosition.Bottom});
            plotModel.Axes.Add(new LinearAxis {Position = AxisPosition.Left, Maximum = 1000, Minimum = 0});

            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 0.1,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.Green
            };

            for (var i = 0; i < x.Count; i++)
            {
                listOfDataPoints.Add(new DataPoint(((float)x[i])*1000, ((float)y[i])*1000));
            }

            foreach (var item in listOfDataPoints)
            {
                series1.Points.Add(item);
            }

            plotModel.Series.Add(series1);

            return plotModel;
        }
    }
}