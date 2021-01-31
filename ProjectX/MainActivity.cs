using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using AndroidX.ViewPager.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Java.Security;
using SQLite;
using System;
using System.IO;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace ProjectX
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        SQLiteConnection db = new SQLiteConnection(Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "UserData.db3"));
        //TextView textView;
        //Button button;
        AddNewDataFrag AddNewDataFrag;

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // connecting to SQLite , should move to splash screen //////////////////////////////////////
            db.CreateTable<DataTypeAlpha>();
            if (db.Table<DataTypeAlpha>().Count() == 0)
            {
                int i = 0;
                int j = 0;
                while (j < 7)
                {
                    while (i < 10)
                    {
                        db.Insert(new DataTypeAlpha("title",$"{j}0{i}","extra info",j));
                        i++;
                    }
                    i = 0;
                    j++;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////

            var viewPager = FindViewById<ViewPager>(Resource.Id.pager);
            MyPager_Adaptor adaptor = new MyPager_Adaptor(SupportFragmentManager, db);
            viewPager.Adapter = adaptor;

            


            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;


        }

        

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            //View view = (View)sender;
            //Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
            //    .SetAction("Action", (View.IOnClickListener)null).Show();


            AddNewDataFrag = new AddNewDataFrag();
            var trans = SupportFragmentManager.BeginTransaction();
            AddNewDataFrag.Show(trans, "Adding New Data");
            AddNewDataFrag.newDataEnteryEvent += AddNewDataFrag_newDataEnteryEvent;




        }

        private void AddNewDataFrag_newDataEnteryEvent(object sender, AddNewDataFrag.NewDataEventHandler e)
        {
            db.Insert(e.DataTypeAlpha);
            AddNewDataFrag.Dismiss();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }
}
