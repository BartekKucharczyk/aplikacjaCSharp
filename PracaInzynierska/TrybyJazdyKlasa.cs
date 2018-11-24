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
    [Activity(Label = "Oś 1")]
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
            para = new ParametryFrag();
            info = new InfoFrag();
            ster = new SterowanieFrag();
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
            catch (ServiceResultException ex)
            {
                await sessionN.AbortAsync();
            }
            catch (InvalidOperationException ex)
            {
                await sessionN.AbortAsync();
            }
        }

        public void setupViewPager(ViewPager viewPager)
        {
            InitFragment();
            var adapter = new ViewPagerAdapter(SupportFragmentManager);
            
            adapter.addFragment(info, "Informacje");
            adapter.addFragment(para, "Parametry");    
            adapter.addFragment(ster,"Sterowanie");
            viewPager.Adapter = adapter;
        }
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tryby_jazdy_layout);
            
            viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewpager);
            setupViewPager(viewPager);

            tabLayout = FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            ster.OptionButtonWasClicked += MFragMyActivities_OptionButtonWasClicked;
           
            Connect(Intent.GetStringExtra("url"), Intent.GetBooleanExtra("anonimowy", true), Intent.GetStringExtra("login"), Intent.GetStringExtra("haslo"));
         
            timer = new Timer();
            timer.Interval = 500;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void MFragMyActivities_OptionButtonWasClicked(object sender, object obj)
        {
            Button btn = (Button)sender;
            int buttonId = btn.Id;

            switch (buttonId)
            {
                case Resource.Id.zasilanieBtn:
                    zarzadzanieOsia.PowerOn(sessionN);
                    break;
                case Resource.Id.homeBtn:
                    zarzadzanieOsia.Home(sessionN);
                    break;
                case Resource.Id.stopOnBtn:
                    zarzadzanieOsia.StopOn(sessionN);
                    break;
                case Resource.Id.stopOffBtn:
                    zarzadzanieOsia.StopOff(sessionN);
                    break;
                case Resource.Id.mAbsoluteBtn:
                    zarzadzanieOsia.MoveAbsolute(sessionN);
                    break;
                case Resource.Id.mAdditiveBtn:
                    zarzadzanieOsia.MoveAdditive(sessionN);
                    break;
                case Resource.Id.mVelocityBtn:
                    zarzadzanieOsia.MoveVelocity(sessionN);
                    break;
                case Resource.Id.zasilanieOffBtn:
                    zarzadzanieOsia.PowerOff(sessionN);
                    break;
                case Resource.Id.wsteczBtn:
                    this.Finish();
                    break;
                default:
                    break;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sessionN.State.Equals(CommunicationState.Opened))
            {
                if(!zarzadzanieOsia.wczytal) zarzadzanieOsia.ReadStatus(sessionN);
                
                RunOnUiThread(() => {
                    if(zarzadzanieOsia.wczytal)
                    {
                        info.StatusID.Text = zarzadzanieOsia.stanOsi.Status+"";
                    
                        if (zarzadzanieOsia.stanOsi.Active) info.napedAkt.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.napedAkt.SetBackgroundColor(Android.Graphics.Color.Gray);

                        if (zarzadzanieOsia.stanOsi.PowerSt)
                        {
                            info.powerOn.SetBackgroundColor(Android.Graphics.Color.Green);
                            info.powerOff.SetBackgroundColor(Android.Graphics.Color.Gray);
                        }
                        else
                        {
                            info.powerOn.SetBackgroundColor(Android.Graphics.Color.Gray);
                            info.powerOff.SetBackgroundColor(Android.Graphics.Color.Green);
                        }

                        if (zarzadzanieOsia.stanOsi.Home) info.napedZbazowany.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.napedZbazowany.SetBackgroundColor(Android.Graphics.Color.Gray);

                        if (zarzadzanieOsia.stanOsi.InVelocity) info.predZad.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.predZad.SetBackgroundColor(Android.Graphics.Color.Gray);

                        if (zarzadzanieOsia.stanOsi.InPosition) info.pozycjaZad.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.pozycjaZad.SetBackgroundColor(Android.Graphics.Color.Gray);

                        if (zarzadzanieOsia.stanOsi.MoveActive) info.napedWruchu.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.napedWruchu.SetBackgroundColor(Android.Graphics.Color.Gray);

                        if (zarzadzanieOsia.stanOsi.Stopped) info.zatrzymany.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.zatrzymany.SetBackgroundColor(Android.Graphics.Color.Gray);

                        if (zarzadzanieOsia.stanOsi.Error) info.bladNap.SetBackgroundColor(Android.Graphics.Color.Green);
                        else info.bladNap.SetBackgroundColor(Android.Graphics.Color.Gray);

                        para.pozycjaAkt.Text = zarzadzanieOsia.stanOsi.positionAct + "";
                        para.predkoscAkt.Text = zarzadzanieOsia.stanOsi.velocityAct + "";

                        zarzadzanieOsia.wczytal = false;
                    }
                } );
                    
            }
        }
    }
}