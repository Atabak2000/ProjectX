using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.OS;
using Android.Util;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using SQLite;

namespace ProjectX
{
    [Obsolete]
    public class MyPager_Fragment : Fragment
    {
        public MyPager_Fragment() { }

        public static MyPager_Fragment newInstance(DataTypeAlpha[] datas)
        {
            var fragment = new MyPager_Fragment();
            Bundle bundle = new Bundle();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataTypeAlpha[]));
            StringWriter stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, datas);
            bundle.PutString("datas",stringWriter.ToString());
            fragment.Arguments = bundle;
            return fragment;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.MyRecyclerViewLayout, container,false);
            var myRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView1); 
            // var toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);  // getting title of day in toolbar

            string str = Arguments.GetString("datas"); // getting data which piped here
            TextReader reader = new StringReader(str); // using as stream for xmlSerializer
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataTypeAlpha[]));
            DataTypeAlpha[] dataArr = (DataTypeAlpha[])xmlSerializer.Deserialize(reader); // data array type: dataTypeAlpha[]
            var myRecycleAdapter = new MyRecyclerViewAdapter(dataArr);
            var myrecycleViewLayoutManager = new LinearLayoutManager(Context);
            myRecyclerView.SetLayoutManager(myrecycleViewLayoutManager);
            myRecyclerView.SetAdapter(myRecycleAdapter);
            
            
            return view;
        }
    }

    public class MyPager_Adaptor : FragmentPagerAdapter
    {
        SQLiteConnection SQLite;
        WeekDays week = new WeekDays();

        [Obsolete]
        public MyPager_Adaptor(FragmentManager fm, SQLiteConnection sb) :base(fm)
        {
            SQLite = sb;
        }
        public override int Count => new WeekDays().getNumberOfDays;

        [Obsolete]
        public override Fragment GetItem(int position)
        {
            DataTypeAlpha[] list = SQLite.Query<DataTypeAlpha>("SELECT * FROM Items WHERE daysOfWeek = ?", position).ToArray();
            //foreach (DataTypeAlpha data in list)
            //{
            //    data.print();
            //}
            return MyPager_Fragment.newInstance(list);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(week.getDayOfWeek()[position]);


            
        }
    }
}