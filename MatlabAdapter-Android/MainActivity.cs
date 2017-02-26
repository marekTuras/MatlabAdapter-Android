using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using MatlabAdapter_Android.Helpers;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace MatlabAdapter_Android
{
    [Activity(Label = "Matlab Adapter", MainLauncher = true)]
    public class MainActivity :  Activity
    {
        #region Basic Declaration
        public Button DrawAGraphButton { get; set; }
        public Button CheckMatlabStatusButton { get; set; }
        public Button StopMatlabButton { get; set; }
        public Button CustomizeSchemaButton { get; set; }
        public TextView ErrorText { get; set; }
        private WebView localWebView { get; set; }
        const int PICKFILE_RESULT_CODE = 1;
        public string path = "";
        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainMenu);

            //Inicializing of static class
            BlockCustomizationHelper.LoadCustomizationXml();
            ConfigHelper.LoadXmlData();

            var servicesProvider = new MatlabServicesProvider();



            #region Basic Inicialization
            DrawAGraphButton = FindViewById<Button>(Resource.Id.drawGraphButton);
            CheckMatlabStatusButton = FindViewById<Button>(Resource.Id.matlabStatusButton);
//            StopMatlabButton = FindViewById<Button>(Resource.Id.stopMatlabButton);
            ErrorText = FindViewById<TextView>(Resource.Id.errorText);
            CustomizeSchemaButton = FindViewById<Button>(Resource.Id.customizeSchemaButton);
            localWebView = FindViewById<WebView>(Resource.Id.uploadFileView);
            #endregion

            DrawAGraphButton.Click +=  delegate
            {
                StartActivity(typeof(DrawAGraphActivity));
//                servicesProvider.StartMatlab();
            };

            CheckMatlabStatusButton.Click += delegate
            {
                ErrorText.Text = servicesProvider.CheckMatlabStatus() ? "ide" : "nejde";
                //                Intent intent = new Intent(Intent.ActionGetContent);
                //                intent.SetType(".txt -> text/plain");
                //                StartActivityForResult(intent, PICKFILE_RESULT_CODE);
                //                ErrorText.Text = (servicesProvider.CheckMatlabStatus() == true)
                //                    ? "Matlab is running"
                //                    : "Matlab is not running";
            };




            CustomizeSchemaButton.Click += delegate
            {
                StartActivity(typeof(BlockCustomizationMain));
            };
        }

    }
}

