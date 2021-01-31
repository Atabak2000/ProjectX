using Android.Content;
using Android.Icu.Lang;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace ProjectX
{
    class MyRecyclerViewAdapter : RecyclerView.Adapter
    {
        DataTypeAlpha[] dataTypeAlphas;
        public MyRecyclerViewAdapter (DataTypeAlpha[] dataTypeAlphas)
        {
            this.dataTypeAlphas = dataTypeAlphas;
        }
        public override int ItemCount => dataTypeAlphas.Length;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as MyRecycleViewViewHolder;
            vh.titleTxtVw.Text = dataTypeAlphas[position].getSubjectTitle();
            vh.classTxtVw.Text = dataTypeAlphas[position].getSubjectClass();
            vh.extraTxtVw.Text = dataTypeAlphas[position].getSubjectInfo();

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.MyCardLayout, parent, false);
            MyRecycleViewViewHolder vh = new MyRecycleViewViewHolder(itemView);
            return vh;
        }
    }


    public class MyRecycleViewViewHolder : RecyclerView.ViewHolder
    {
        public TextView titleTxtVw { get; set; }
        public TextView classTxtVw { get; set; }
        public TextView extraTxtVw { get; set; }
        public MyRecycleViewViewHolder(View view) : base(view)
        {
            titleTxtVw = view.FindViewById<TextView>(Resource.Id.TitleTextViewCard);
            classTxtVw = view.FindViewById<TextView>(Resource.Id.ClassTextViewCard);
            extraTxtVw = view.FindViewById<TextView>(Resource.Id.ExtraInfoTextViewCard);
        }

    }
}