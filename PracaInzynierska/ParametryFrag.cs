using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace PracaInzynierska
{
    public class ParametryFrag : Android.Support.V4.App.Fragment
    {

        public EditText pozycjaAkt, predkoscAkt;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           
            var v = inflater.Inflate(Resource.Layout.ParamTJFrag, container, false);
            pozycjaAkt = v.FindViewById<EditText>(Resource.Id.pozActBox);
            predkoscAkt = v.FindViewById<EditText>(Resource.Id.predActBox);

            return v;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}