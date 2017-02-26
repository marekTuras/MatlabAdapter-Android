using Android.App;
using Android.OS;
using Android.Widget;
using MatlabAdapter_Android.Helpers;

namespace MatlabAdapter_Android
{
    [Activity(Label = "Change Parameter Value")]
    public class ChangeParamValue : Activity
    {
        private EditText newParamValue { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetBlockParamValue);
            var servicesProvider = new MatlabServicesProvider();
            var editText = FindViewById<EditText>(Resource.Id.changeParamValueEditText);

            var value = Intent.GetStringArrayListExtra("Block");
            FindViewById<TextView>(Resource.Id.textView1).Text = "Change value of " + value[0] + " -> " + value[1];
            editText.Text =
                 servicesProvider.GetParamValue(BlockCustomizationHelper.GetSchemaName(), value[0], value[1]);
            FindViewById<Button>(Resource.Id.submitButton).Click += delegate
            {
//                servicesProvider.ChangeParamValue("model", value[0], value[1], 1);
                Android.Widget.Toast.MakeText(this, value[0] + " -> " + value[1] + " = " + FindViewById<EditText>(Resource.Id.changeParamValueEditText).Text, Android.Widget.ToastLength.Short).Show();
                servicesProvider.ChangeParamValue(BlockCustomizationHelper.GetSchemaName(), value[0], value[1], editText.Text);
                StartActivity(typeof(MainActivity));
            };


            // Create your application here
        }

}
}