﻿using System;
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
    public class InfoFrag : Android.Support.V4.App.Fragment
    {

        public Button napedAkt,powerOn,powerOff,napedZbazowany,pozycjaZad,predZad,bladNap,resetOnN,napedWruchu,zatrzymany;
        public EditText StatusID;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); 
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           
            var v = inflater.Inflate(Resource.Layout.infoTJFrag, container, false);

            napedAkt = v.FindViewById<Button>(Resource.Id.napedActBtn);
            powerOn = v.FindViewById<Button>(Resource.Id.ispowerOn);
            powerOff = v.FindViewById<Button>(Resource.Id.ispowerOff);
            napedZbazowany = v.FindViewById<Button>(Resource.Id.isHomed);
            pozycjaZad = v.FindViewById<Button>(Resource.Id.pozZadOs);
            predZad = v.FindViewById<Button>(Resource.Id.preZadOs);
            bladNap = v.FindViewById<Button>(Resource.Id.napInError);
            resetOnN = v.FindViewById<Button>(Resource.Id.resetON);
            StatusID = v.FindViewById<EditText>(Resource.Id.statusIDEdxt);
            napedWruchu = v.FindViewById<Button>(Resource.Id.moveActive);
            zatrzymany = v.FindViewById<Button>(Resource.Id.stoppedN);
            return v;
        }
    }
}