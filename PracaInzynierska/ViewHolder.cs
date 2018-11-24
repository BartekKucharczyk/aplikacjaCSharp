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

namespace PracaInzynierska
{
    public class ViewHolder: Java.Lang.Object
    {
        public TextView Id { get; set; }
        public TextView ErrorId { get; set; }
        public TextView Description { get; set; }
        public TextView Data { get; set; }


    }
}