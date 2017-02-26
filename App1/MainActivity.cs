using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Views;

namespace App1
{
    [Activity(Label = "ReadWriteNote", MainLauncher = true)]
    public class MainActivity : Activity
    {
        const int PICKFILE_RESULT_CODE = 1;
        public string path = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            Button BtnApri = FindViewById<Button>(Resource.Id.button1);


            BtnApri.Click += (sender, e) =>
            {
                var plotView = new PlotView(this);
                plotView.Model = CreatePlotModel();

                this.AddContentView(plotView,
                    new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            };
        }

        }

    }
}

