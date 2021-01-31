using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using System;
using System.Collections.Generic;

namespace ProjectX
{

    public class AddNewDataFrag : DialogFragment
    {


        // making an event handler for new data entery //
        public event EventHandler<NewDataEventHandler> newDataEnteryEvent;
        public class NewDataEventHandler : EventArgs
        {
            public DataTypeAlpha DataTypeAlpha { get; set; }
        }
        // end od event handler


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            

        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.AddNewDataLayout, container, false);
            var weekDays = new WeekDays();
            List<KeyValuePair<string, int>> keyValuePairs = weekDays.getWeekDaysListValue();
            var date = DateTime.Now;
            var today = date.DayOfWeek;
            int todayNum = (int)(((int)today==6) ? 0 : today+1); //setting today as custom format

            //Console.WriteLine(today); //debug

            var title = view.FindViewById<TextInputLayout>(Resource.Id.TitleEdtTxt);
            var room = view.FindViewById<TextInputLayout>(Resource.Id.ClassNumberEdtTxt);
            var extraInfo = view.FindViewById<TextInputLayout>(Resource.Id.ExtraInfoEdtTxt);
            int dayNumber = todayNum;
            var spinnerDay = view.FindViewById<Spinner>(Resource.Id.spinner1);


            var picker1 = view.FindViewById<NumberPicker>(Resource.Id.startTimepicker);
            var picker2 = view.FindViewById<NumberPicker>(Resource.Id.endTimePicker);
            picker1.MaxValue = 23;picker1.MinValue = 0; picker1.Value = date.Hour;
            picker2.MaxValue = 23; picker2.MinValue = 0; picker2.Value = date.Hour+2;
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////// continue here : setting time  selector 

            var adaptorDay = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, weekDays.getDayOfWeek());

            adaptorDay.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerDay.Adapter = adaptorDay;
            spinnerDay.SetSelection(todayNum);

            spinnerDay.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var tmpOjct = new DataTypeAlpha(title.EditText.Text, room.EditText.Text, extraInfo.EditText.Text, dayNumber);

            void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
            {
                //Spinner spinner = (Spinner)sender;
                //string toast = string.Format("Name of day is {0} and value is {1}",              //for debug
                //    spinner.GetItemAtPosition(e.Position), keyValuePairs[e.Position].Value);
                //Toast.MakeText(Context, toast, ToastLength.Long).Show();
                dayNumber = keyValuePairs[e.Position].Value;
                tmpOjct = new DataTypeAlpha(title.EditText.Text, room.EditText.Text, extraInfo.EditText.Text, dayNumber);
            }

            

            var saveBtn = view.FindViewById<Button>(Resource.Id.SaveBtn);
            saveBtn.Click += (sender , e ) => {
                //////////////// invoking an event to pass data ///////////////

                newDataEnteryEvent?.Invoke(this, new NewDataEventHandler { DataTypeAlpha = tmpOjct });
               
            };// end of saveBtn.click

            return view;
        }

        
    }
}