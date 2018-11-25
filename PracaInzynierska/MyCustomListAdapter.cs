using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using static Android.Support.V7.Widget.RecyclerView;

namespace PracaInzynierska
{
    public class MyCustomListAdapter : BaseAdapter<OnelogClass>
    {
        List<OnelogClass> log;

        public MyCustomListAdapter(List<OnelogClass> log)
        {
            this.log = log;
        }

        public override OnelogClass this[int position]
        {
            get
            {
                return log[position];
            }
        }

        public override int Count
        {
            get
            {
                return log.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.logRow, parent, false);

                var id = view.FindViewById<TextView>(Resource.Id.idTextView);
                var errorId = view.FindViewById<TextView>(Resource.Id.errorIdTextView);
                var description = view.FindViewById<TextView>(Resource.Id.descTxtView);
                var dataAndTim = view.FindViewById<TextView>(Resource.Id.dateTxtView);

                view.Tag = new ViewHolder() { Id = id, ErrorId = errorId, Description = description,Data = dataAndTim };
            }

            var holder = (ViewHolder)view.Tag;


            //switch (log[position].Severity)
            //{
            //    case "S":
            //        holder.ErrorId.SetTextColor(Android.Graphics.Color.Blue);
            //        break;
            //    case "W":
            //        holder.ErrorId.SetTextColor(Android.Graphics.Color.Orange);
            //        break;
            //    case "I":
            //        holder.ErrorId.SetTextColor(Android.Graphics.Color.Black);
            //        break;
            //    case "E":
            //        holder.ErrorId.SetTextColor(Android.Graphics.Color.Red);
            //        break;
            //    default:
            //        holder.ErrorId.SetTextColor(Android.Graphics.Color.Black);
            //        break;
            //}


            holder.Id.Text = log[position].IdLog;
            holder.ErrorId.Text = log[position].ErrorId;
            holder.Description.Text = log[position].Description;
            holder.Data.Text = log[position].Date;

            return view;

        }

    }
}