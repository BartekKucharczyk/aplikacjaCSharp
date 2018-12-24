using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace PracaInzynierska
{
    public class ParametryFrag : Android.Support.V4.App.Fragment
    {

        public event EventHandler OptionButtonWasClicked;

        public EditText pozycjaAkt, predkoscAkt;
        public EditText velocitSet, distanceSet, positionSet, accelerationSet, decelerationSet;
        public EditText velocityIn, distanceIn, positionIn, accelerationIn, decelerationIn;
        public EditText postionCycSet, postionInSet, velocityCycSet,velocityCycIn, torqueCycSet,torqueCycIn;
        Button setParams, setCyc;
        View v;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        private void BtnOption(object sender, EventArgs e)
        {
            OptionButtonWasClicked(sender, e);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           
            v = inflater.Inflate(Resource.Layout.ParamTJFrag, container, false);

            pozycjaAkt = v.FindViewById<EditText>(Resource.Id.pozActBox);
            predkoscAkt = v.FindViewById<EditText>(Resource.Id.predActBox);

            velocitSet = v.FindViewById<EditText>(Resource.Id.velocitySetEdt);
            velocityIn = v.FindViewById<EditText>(Resource.Id.velocityInDEdt);

            distanceSet = v.FindViewById<EditText>(Resource.Id.distanceSetEdt);
            distanceIn = v.FindViewById<EditText>(Resource.Id.distanceInEdt);

            positionSet = v.FindViewById<EditText>(Resource.Id.positionSetEdt);
            positionIn = v.FindViewById<EditText>(Resource.Id.positionInEdt);

            accelerationSet = v.FindViewById<EditText>(Resource.Id.accelerationSetEdt);
            accelerationIn = v.FindViewById<EditText>(Resource.Id.accelerationInEdt);

            decelerationSet = v.FindViewById<EditText>(Resource.Id.decelerationSetEdt);
            decelerationIn = v.FindViewById<EditText>(Resource.Id.decelerationInEdt);
    
            postionCycSet = v.FindViewById<EditText>(Resource.Id.positionSetCycEdt);
            postionInSet = v.FindViewById<EditText>(Resource.Id.positionInCycEdt);

            torqueCycSet = v.FindViewById<EditText>(Resource.Id.torqueSetCycEdT);
            torqueCycIn = v.FindViewById<EditText>(Resource.Id.torqueInCycEdt);

            velocityCycSet = v.FindViewById<EditText>(Resource.Id.velocitySetCycEdt);
            velocityCycIn = v.FindViewById<EditText>(Resource.Id.velocityInCycEdt);

            setParams = v.FindViewById<Button>(Resource.Id.btnSetDriveParams);
            setCyc = v.FindViewById<Button>(Resource.Id.setCyclicBtn);

            setParams.Click += BtnOption;
            setCyc.Click += BtnOption;

            return v;
        }
    }
}