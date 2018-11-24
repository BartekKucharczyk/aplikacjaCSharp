using System;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace PracaInzynierska
{
    public class SterowanieFrag : Android.Support.V4.App.Fragment
    {

        public event EventHandler OptionButtonWasClicked;
        
        Button zasilanie,zasilanieOff,bazowanie,stopOn,mAdditive,mVelocity,mAbsolute,wstecz,stopOff;
        View v;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);   
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            v = inflater.Inflate(Resource.Layout.sterowanieTJFrag, container, false);

            zasilanie = v.FindViewById<Button>(Resource.Id.zasilanieBtn);
            bazowanie = v.FindViewById<Button>(Resource.Id.homeBtn);
            stopOn = v.FindViewById<Button>(Resource.Id.stopOnBtn);
            stopOff = v.FindViewById<Button>(Resource.Id.stopOffBtn);
            mAdditive = v.FindViewById<Button>(Resource.Id.mAdditiveBtn);
            mAbsolute = v.FindViewById<Button>(Resource.Id.mAbsoluteBtn);
            mVelocity = v.FindViewById<Button>(Resource.Id.mVelocityBtn);
            zasilanieOff = v.FindViewById<Button>(Resource.Id.zasilanieOffBtn);
            wstecz = v.FindViewById<Button>(Resource.Id.wsteczBtn);

            zasilanie.Click += BtnOption_Click;
            zasilanieOff.Click += BtnOption_Click;
            bazowanie.Click += Bazowanie_Click;
            mAdditive.Click += MAdditive_Click;
            mAbsolute.Click += MAbsolute_Click;
            mVelocity.Click += MVelocity_Click;
            stopOn.Click += Stop_Click;
            stopOff.Click += StopOff_Click;
            wstecz.Click += Wstecz_Click;

            return v;
        }

        private void StopOff_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void Wstecz_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void MVelocity_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void MAbsolute_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void MAdditive_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void Bazowanie_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void BtnOption_Click(object sender, EventArgs e)
        {
            // Fire the event to the MainActivity
            OptionButtonWasClicked(sender, e);
        }



    }
}