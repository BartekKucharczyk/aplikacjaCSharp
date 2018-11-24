using System;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;

namespace PracaInzynierska
{
    [Activity(Label = "Control")]
    public class Control : Activity
    {

        UaTcpSessionChannel sessionN;
        Button jazda, trybReczny, diagnostyka, wykres, wyjscie, ustawienia_para;
        
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


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.control_layout);

            jazda = FindViewById<Button>(Resource.Id.trybyJazdyBtn);
            trybReczny = FindViewById<Button>(Resource.Id.trybRecznyBtn);
            diagnostyka = FindViewById<Button>(Resource.Id.diagnostykaBtn);
            wykres = FindViewById<Button>(Resource.Id.wykresyBtn);
            ustawienia_para = FindViewById<Button>(Resource.Id.ustawieniaBtn);
            wyjscie = FindViewById<Button>(Resource.Id.wyjścieBtn);

            Connect(Intent.GetStringExtra("url"), Intent.GetBooleanExtra("anonimowy", true), Intent.GetStringExtra("login"), Intent.GetStringExtra("haslo"));

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


          


           
        }
    }
}