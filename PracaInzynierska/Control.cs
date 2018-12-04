using System;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;
using System.Timers;
using Android.Content.PM;

namespace PracaInzynierska
{
    [Activity(Label = "Control",ScreenOrientation = ScreenOrientation.Portrait)]
    public class Control : Activity
    {
        Timer timer;
        UaTcpSessionChannel sessionN;
        Button jazda, trybReczny, diagnostyka,  ustawienia_para;//wykres, wyjscie,
        ZarzadzanieSterownikiem zarzadzanie = new ZarzadzanieSterownikiem();
        ImageButton wykres, wyjscie;

        public async void Connect(string url, bool anonimowe, string login, string haslo)
        {
           ConnectionWithServer conn = new ConnectionWithServer();

           IUserIdentity userIdentity = null;

            if (anonimowe)
            {
                userIdentity = new AnonymousIdentity();
            }
            else
            {
                userIdentity = new UserNameIdentity(login, haslo);
            }

            sessionN = conn.GetSesssion(userIdentity, url);

            try
            {
                await sessionN.OpenAsync();
                zarzadzanie.StartStopTime(sessionN, true);
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

        public void TimerInit()
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.control_layout);

            jazda = FindViewById<Button>(Resource.Id.trybyJazdyBtn);
            trybReczny = FindViewById<Button>(Resource.Id.trybRecznyBtn);
            diagnostyka = FindViewById<Button>(Resource.Id.diagnostykaBtn);
            //wykres = FindViewById<Button>(Resource.Id.wykresyBtn);
            ustawienia_para = FindViewById<Button>(Resource.Id.ustawieniaBtn);
            //wyjscie = FindViewById<Button>(Resource.Id.wyjścieBtn);
            wyjscie = FindViewById<ImageButton>(Resource.Id.wyjścieBtn);
            wykres = FindViewById<ImageButton>(Resource.Id.wykresyBtn);

      //      < Button

      //  android: text = "Wyjście"

      //  android: layout_width = "350px"

      //  android: layout_height = "350px"

      //  android: bufferType = "normal"

      //  android: clickable = "true"

      //  android: layout_gravity = "right"

      //  android: layout_marginLeft = "5dp"

      //  android: id = "@+id/wyjścieBtn"
      ///>

             Connect(Intent.GetStringExtra("url"), Intent.GetBooleanExtra("anonimowy", true), Intent.GetStringExtra("login"), Intent.GetStringExtra("haslo"));

            TimerInit();

            jazda.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(TrybyJazdyKlasa));
                intent.PutExtra("url", Intent.GetStringExtra("url"));
                intent.PutExtra("anonimowy", Intent.GetBooleanExtra("anonimowy", true));
                intent.PutExtra("login", Intent.GetStringExtra("login"));
                intent.PutExtra("haslo", Intent.GetStringExtra("haslo"));
                StartActivity(intent);
            };

            diagnostyka.Click += (sender, e) =>
            {
                //timer.Stop();
                Intent intent = new Intent(this, typeof(Diagnosis));
                intent.PutExtra("url", Intent.GetStringExtra("url"));
                intent.PutExtra("anonimowy", Intent.GetBooleanExtra("anonimowy", true));
                intent.PutExtra("login", Intent.GetStringExtra("login"));
                intent.PutExtra("haslo", Intent.GetStringExtra("haslo"));
                StartActivity(intent);
            };

            wyjscie.Click += (sender, e) =>
            {
              
                this.Finish();
            };

            wykres.Click += (sender,e) =>
            {
                timer.Stop();
                Intent intent = new Intent(this, typeof(Charts));
                intent.PutExtra("url", Intent.GetStringExtra("url"));
                intent.PutExtra("anonimowy", Intent.GetBooleanExtra("anonimowy", true));
                intent.PutExtra("login", Intent.GetStringExtra("login"));
                intent.PutExtra("haslo", Intent.GetStringExtra("haslo"));
                StartActivity(intent);
            }; 
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sessionN.State.Equals(CommunicationState.Opened))  zarzadzanie.ReadTimePLC(sessionN);
        }

    }
}