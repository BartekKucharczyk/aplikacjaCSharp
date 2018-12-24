using System;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace PracaInzynierska
{
    public class SterowanieFrag : Android.Support.V4.App.Fragment
    {

        public event EventHandler OptionButtonWasClicked;

        Button powerOn, powerOff, home, errorReset,update,updateCyc;
        ToggleButton mAdditive, mVelocity, mAbsolute, stop,cycPos, cycVelo, cycTorq;
        View v;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);   
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            v = inflater.Inflate(Resource.Layout.sterowanieTJFrag, container, false);

            powerOn = v.FindViewById<Button>(Resource.Id.powerBtn);
            powerOff = v.FindViewById<Button>(Resource.Id.powerOffBtn);
            home = v.FindViewById<Button>(Resource.Id.homeBtn);
            update = v.FindViewById<Button>(Resource.Id.updateBtn);
            errorReset = v.FindViewById<Button>(Resource.Id.resetErrBtn);

            stop = v.FindViewById<ToggleButton>(Resource.Id.stopBtn);
            mAdditive = v.FindViewById<ToggleButton>(Resource.Id.mAdditiveBtn);
            mAbsolute = v.FindViewById<ToggleButton>(Resource.Id.mAbsoluteBtn);
            mVelocity = v.FindViewById<ToggleButton>(Resource.Id.mVelocityBtn);
            
            cycPos = v.FindViewById<ToggleButton>(Resource.Id.cyclicPostionTBtn);
            cycVelo = v.FindViewById<ToggleButton>(Resource.Id.cyclicVelocityTBtn);
            cycTorq = v.FindViewById<ToggleButton>(Resource.Id.cyclicTorqueTBtn);
            updateCyc = v.FindViewById<Button>(Resource.Id.updateCyclicSet);

            powerOn.Click += BtnOption_Click;
            powerOff.Click += BtnOption_Click;
            home.Click += Bazowanie_Click;
            update.Click += Update_Click;
            errorReset.Click += Update_Click;

            stop.Click += Stop_Click;
            mAdditive.Click += MAdditive_Click;
            mAbsolute.Click += MAbsolute_Click;
            mVelocity.Click += MVelocity_Click;

            cycPos.Click += CycPos_Click;
            cycVelo.Click += CycVelo_Click;
            cycTorq.Click += CycTorq_Click;
            updateCyc.Click += UpdateCyc_Click;
            return v;
        }

        private void UpdateCyc_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void Update_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void CycTorq_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void CycVelo_Click(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        private void CycPos_Click(object sender, EventArgs e)
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
            OptionButtonWasClicked(sender, e);
        }

    }
}