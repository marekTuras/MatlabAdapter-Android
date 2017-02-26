using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MatlabAdapter_Android
{
    [Activity(Label = "BlockCustomizationSelected")]
    public class SetBlockParamValue : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetBlockParamValue);
            // Create your application here
        }
    }
}