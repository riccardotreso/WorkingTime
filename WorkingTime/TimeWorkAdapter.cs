using System;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Linq;

namespace WorkingTime
{
    public class TimeWorkAdapter : BaseAdapter<TimeWork>
    {
        System.Collections.Generic.List<TimeWork> items;
        Activity context;
		Action<TimeWork> deleteElement;

		public TimeWorkAdapter(Activity context, System.Collections.Generic.List<TimeWork> items, Action<TimeWork> deleteElement)
        {
			this.items = items;
            this.context = context;
			this.deleteElement = deleteElement;
        }
        public override TimeWork this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.ListWorkTime, null);

			var item = items[position];
			view.FindViewById<TextView>(Resource.Id.txtDateStart).Text = $"E -> {item.WorkingStart.ToString("dd/MM/yyyy HH:mm:ss")}";
			view.FindViewById<TextView>(Resource.Id.txtDateEnd).Text = $"U -> {item.WorkingEnd.ToString("HH:mm:ss")}";

			Button btnDelete = view.FindViewById<Button>(Resource.Id.btnDelete);

			btnDelete.Click += (object sender, System.EventArgs e) =>
			{
				deleteElement(item);
			};

            return view;
        }
       
        
    }
}
