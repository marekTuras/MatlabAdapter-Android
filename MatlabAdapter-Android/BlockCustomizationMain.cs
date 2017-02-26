using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MatlabAdapter_Android.Helpers;

namespace MatlabAdapter_Android
{
    [Activity(Label = "BlockCustomizationMain")]
    public class BlockCustomizationMain : ListActivity
    {
        private bool _isBlockSelected = false;
        private string _selected;
        private string[] _block;
        private string[] _activities;
        private Dictionary<string, List<string>> _blocks = new Dictionary<string, List<string>>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Blocks);
            _block = BlockCustomizationHelper.GetListOfBlocks();
            _blocks = BlockCustomizationHelper.GetListOfBlocksForCustomization();
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _block);
        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var t = _block[position];
            if (!_isBlockSelected)
            {
                _selected = t;
                ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _blocks[t]);
                _isBlockSelected = true;
            }
            else
            {
                var changeParamValueActivity = new Intent(this, typeof(ChangeParamValue));
                changeParamValueActivity.PutStringArrayListExtra("Block", new string[] { _selected,_blocks[_selected][position] });
                StartActivity(changeParamValueActivity);
//                Android.Widget.Toast.MakeText(this,blocks[selected][position] , Android.Widget.ToastLength.Short).Show(); 
            }



        }      
    }
}