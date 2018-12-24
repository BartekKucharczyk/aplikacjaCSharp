using System;
using Android.App;
using Android.OS;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using System.Timers;

namespace PracaInzynierska
{
    [Activity(Label = "Axis 1")]
    public class TrybyJazdyKlasa : AppCompatActivity
    {               
        Timer timer;
        UaTcpSessionChannel sessionN;
        ZarzadzanieOsia zarzadzanieOsia = new ZarzadzanieOsia();
        ConnectionWithServer conn = new ConnectionWithServer();
      
        private ParametryFrag para;
        private InfoFrag info;
        private SterowanieFrag ster;

        private ViewPager viewPager;
        private TabLayout tabLayout;
        
        private void InitFragment()
        {
             ster = new SterowanieFrag();
             info = new InfoFrag();
             para = new ParametryFrag();
        }
    
        public async void Connect(string url, bool anonimowe, string login, string haslo)
        {
            IUserIdentity userIdentity = null;

            if (anonimowe) userIdentity = new AnonymousIdentity();       
            else userIdentity = new UserNameIdentity(login, haslo);
         
            sessionN = conn.GetSesssion(userIdentity, url);

            try
            {
                await sessionN.OpenAsync().ConfigureAwait(true);
            }
            catch (ServiceResultException)
            {
                await sessionN.AbortAsync();
            }
            catch (InvalidOperationException)
            {
                await sessionN.AbortAsync();
            }
        }

        public void SetupViewPager(ViewPager viewPager)
        {
            InitFragment();
            var adapter = new ViewPagerAdapter(SupportFragmentManager);
            
            adapter.addFragment(ster,"Control");
            adapter.addFragment(info, "Information");
            adapter.addFragment(para, "Parameter");

            viewPager.Adapter = adapter;
        }
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tryby_jazdy_layout);
            
            viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewpager);
            SetupViewPager(viewPager);

            tabLayout = FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            ster.OptionButtonWasClicked += MFragMyActivities_OptionButtonWasClicked;
            para.OptionButtonWasClicked += Para_OptionButtonWasClicked;

            Connect(Intent.GetStringExtra("url"), Intent.GetBooleanExtra("anonimowy", true), Intent.GetStringExtra("login"), Intent.GetStringExtra("haslo"));

            timer = new Timer
            {
                Interval = 500,
                Enabled = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Para_OptionButtonWasClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int buttonId = btn.Id;

            switch (buttonId)
            {
                case Resource.Id.setCyclicBtn:
                    Toast.MakeText(this, "Sending params...", ToastLength.Short).Show();
                    zarzadzanieOsia.SendCycParams(sessionN,
                        para.velocityCycIn.Text, para.torqueCycIn.Text,
                        para.postionInSet.Text);
                    break;
                case Resource.Id.btnSetDriveParams:
               Toast.MakeText(this, "Sending params...", ToastLength.Short).Show();
                    zarzadzanieOsia.SendParams(sessionN,
                  para.velocityIn.Text, para.distanceIn.Text,
                  para.positionIn.Text, para.accelerationIn.Text,
                  para.decelerationIn.Text);
                    break;

                default:
                    break;
            }
        }

        private void MFragMyActivities_OptionButtonWasClicked(object sender, object obj)
        {
            try
            {
                Button btn = (Button)sender;
                int buttonId = btn.Id;

                switch (buttonId)
                {
                    case Resource.Id.powerBtn:
                        Toast.MakeText(this, "Power On", ToastLength.Short).Show();
                        zarzadzanieOsia.PowerOn(sessionN);
                        break;
                    case Resource.Id.powerOffBtn:
                        Toast.MakeText(this, "Power Off", ToastLength.Short).Show();
                        zarzadzanieOsia.PowerOff(sessionN);
                        break;
                    case Resource.Id.homeBtn:
                        Toast.MakeText(this, "Homing", ToastLength.Short).Show();
                        zarzadzanieOsia.Home(sessionN);
                        break;
                    case Resource.Id.updateBtn:
                        Toast.MakeText(this, "Update", ToastLength.Short).Show();
                        zarzadzanieOsia.Update(sessionN);
                        break;
                    case Resource.Id.updateCyclicSet:
                        Toast.MakeText(this, "Update", ToastLength.Short).Show();
                        zarzadzanieOsia.UpdateCyclicSet(sessionN);
                        break;
                    case Resource.Id.resetErrBtn:
                        zarzadzanieOsia.ErrorReset(sessionN);
                        break;
                    default:
                        break;
                }
            }
            catch(InvalidCastException){}

            try
            {

                ToggleButton tglBtn = (ToggleButton)sender;
                int tglID = tglBtn.Id;
            switch (tglID)
            {
                case Resource.Id.stopBtn:
                    if (tglBtn.Checked)
                    {
                        zarzadzanieOsia.StopOn(sessionN);
                    }
                    else
                    {
                        zarzadzanieOsia.StopOff(sessionN);
                    }

                    break;
                case Resource.Id.mAbsoluteBtn:
                    if (tglBtn.Checked)
                    {
                        zarzadzanieOsia.MoveAbsolute(sessionN);
                    }
                    else
                    {
                        zarzadzanieOsia.MoveAbsoluteOff(sessionN);
                    }
                    break;

                case Resource.Id.mAdditiveBtn:
                    if (tglBtn.Checked)
                    {
                        zarzadzanieOsia.MoveAdditive(sessionN);
                    }
                    else
                    {
                        zarzadzanieOsia.MoveAdditiveOff(sessionN);
                    }
                    break;

                case Resource.Id.mVelocityBtn:
                    if (tglBtn.Checked)
                    {
                        zarzadzanieOsia.MoveVelocity(sessionN);
                    }
                    else
                    {
                        zarzadzanieOsia.MoveVelocityOff(sessionN);
                    }
                    break;

                case Resource.Id.cyclicPostionTBtn:
                        if (tglBtn.Checked)
                        {
                            zarzadzanieOsia.CyclicPositionSetOn(sessionN);
                        }
                        else
                        {
                            zarzadzanieOsia.CyclicPositionSetOff(sessionN);
                        }
                        break;

                    case Resource.Id.cyclicTorqueTBtn:
                        if (tglBtn.Checked)
                        {
                            zarzadzanieOsia.CyclicTorqueSetOn(sessionN);
                        }
                        else
                        {
                            zarzadzanieOsia.CyclicTorqueSetOff(sessionN);
                        }
                        break;

                       
                    case Resource.Id.cyclicVelocityTBtn:
                        if (tglBtn.Checked)
                        {
                            zarzadzanieOsia.CyclicVelocitySetOn(sessionN);
                        }
                        else
                        {
                            zarzadzanieOsia.CyclicVelocitySetOff(sessionN);
                        }
                        break;


                    default:
                    break;
            }
            }
            catch (InvalidCastException) {  }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sessionN.State.Equals(CommunicationState.Opened))
            {
                if(!zarzadzanieOsia.wczytal) zarzadzanieOsia.ReadStatus(sessionN);
                
                RunOnUiThread(() => {
                    if(zarzadzanieOsia.wczytal)
                    {

                        if (zarzadzanieOsia.stanOsi.Active) info.napedAkt.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.napedAkt.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.PowerSt)
                        {
                            info.powerOn.SetBackgroundColor(Android.Graphics.Color.Green);
                            info.powerOff.SetBackgroundColor(Android.Graphics.Color.LightGray);
                        }
                        else
                        {
                            info.powerOn.SetBackgroundColor(Android.Graphics.Color.LightGray);
                            info.powerOff.SetBackgroundColor(Android.Graphics.Color.Green);
                        }

                        if (zarzadzanieOsia.stanOsi.Home) info.napedZbazowany.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.napedZbazowany.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.InVelocity) info.predZad.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.predZad.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.InPosition) info.pozycjaZad.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.pozycjaZad.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.MoveActive) info.napedWruchu.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.napedWruchu.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.Stopped) info.zatrzymany.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.zatrzymany.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.Error) info.bladNap.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.bladNap.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        // Cyclic Set
                        if (zarzadzanieOsia.stanOsi.ActiveCyc) info.activCyc.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.activCyc.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.CyclicSetActive) info.cyclicSetAcitve.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.cyclicSetAcitve.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.ErrorCyc) info.errorCyc.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.errorCyc.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.CommandBusy) info.commandBusy.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.commandBusy.SetBackgroundColor(Android.Graphics.Color.LightGray);

                        if (zarzadzanieOsia.stanOsi.CommandAborted) info.cyclicAborted.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.cyclicAborted.SetBackgroundColor(Android.Graphics.Color.LightGray);
                        try
                        {
                            info.StatusID.Text = zarzadzanieOsia.stanOsi.Status;
                            info.statusIDCyc.Text = zarzadzanieOsia.stanOsi.StatusIdCyc;
                            para.pozycjaAkt.Text = zarzadzanieOsia.stanOsi.positionAct;
                            para.predkoscAkt.Text = zarzadzanieOsia.stanOsi.velocityAct;

                            para.velocitSet.Text = zarzadzanieOsia.stanOsi.velocitySet;
                            para.distanceSet.Text = zarzadzanieOsia.stanOsi.distanceSet;
                            para.positionSet.Text = zarzadzanieOsia.stanOsi.positionCycSet;
                            para.accelerationSet.Text = zarzadzanieOsia.stanOsi.accelerationSet;
                            para.decelerationSet.Text = zarzadzanieOsia.stanOsi.decelerationSet;

                            para.postionCycSet.Text = zarzadzanieOsia.stanOsi.positionCycSet;
                            para.velocityCycSet.Text = zarzadzanieOsia.stanOsi.velocityCycSet;
                            para.torqueCycSet.Text = zarzadzanieOsia.stanOsi.torqueCycSet;

                        }
                        catch (NullReferenceException)
                        {

                        }
                        zarzadzanieOsia.wczytal = false;
                    }
                } );
                    
            }
        }
    }
}