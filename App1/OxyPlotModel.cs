using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxyPlotExample
{
    public class OxyPlotModel
    {
        public OxyPlotModel()
        {
            PlotModel temp = new PlotModel();

            temp.Title = "OxyPlotText";

            temp.LegendPosition = LegendPosition.LeftTop;

            var linearAxis1 = new LinearAxis();
            linearAxis1.Title = "Testdata1";
            temp.Axes.Add(linearAxis1);

            var linearAxis2 = new LinearAxis();
            linearAxis2.Title = "Testdata2";
            linearAxis2.Position = AxisPosition.Bottom;
            temp.Axes.Add(linearAxis2);

            var lineSeries = new LineSeries();

            List<Tuple<int,int>> dataPointsX = new List<Tuple<int,int>>();
            for (int i = 0; i < 10; i++)
            {
                dataPointsX.Add(new Tuple<int,int>(i,i));
            }

            foreach (var item in dataPointsX)
            {
                DataPoint point = new DataPoint(item.Item1,item.Item2);

                lineSeries.Points.Add(point);
            }

            temp.Series.Add(lineSeries);

            OxyModel = temp;
        }

        public PlotModel OxyModel { get; set; }
    }
}
