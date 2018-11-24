using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Workstation.ServiceModel.Ua.Channels;
using Workstation.ServiceModel.Ua;
using Android.Content;
using System;

namespace PracaInzynierska
{
    [Activity(Label = "BR Drive Diagnostic", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        // "@string/app_name"
        RadioButton anonimowy, uzytkownik;
        Button polacz;
        EditText ipAdress, portEd, endpoinUrlET, login, haslo;

        UaTcpSessionChannel session;
        ConnectionWithServer connection = new ConnectionWithServer();

        public void InitScreen()
        {
            polacz = FindViewById<Button>(Resource.Id.polaczBtn);
          
            ipAdress = FindViewById<EditText>(Resource.Id.ipAdresEB);
            portEd = FindViewById<EditText>(Resource.Id.portEB);
            endpoinUrlET = FindViewById<EditText>(Resource.Id.endPointURLEB);
            login = FindViewById<EditText>(Resource.Id.loginEB);
            haslo = FindViewById<EditText>(Resource.Id.hasloEB);

            anonimowy = FindViewById<RadioButton>(Resource.Id.anonimowyRB);
            uzytkownik = FindViewById<RadioButton>(Resource.Id.uzytkownikRB);
        }

        public void TimeoutError()
        {
            login.Text = "ERROR";
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            InitScreen();

            polacz.Click +=  (sender, e) =>
            {
                polacz.Enabled = false;
               
                var discoveryUrl = endpoinUrlET.Text + "://" + ipAdress.Text + ":" + portEd.Text;
                IUserIdentity userIdentity = null;
                if (anonimowy.Checked)
                {
                    userIdentity = new AnonymousIdentity();
                }
                else
                {
                    userIdentity = new UserNameIdentity(login.Text, haslo.Text);
                }

                try
                {
                    session = connection.GetSesssion(userIdentity, discoveryUrl);
                    Connect();
                    
                       Intent intent = new Intent(this, typeof(Control));
                       intent.PutExtra("url", discoveryUrl);
                       intent.PutExtra("anonimowy", anonimowy.Checked);
                       intent.PutExtra("login", login.Text);
                       intent.PutExtra("haslo", haslo.Text);
                       polacz.Enabled = true;
                       StartActivity(intent);
                             
                }
                
                catch (ServiceResultException ex)
                {
                    polacz.Enabled = true;
                    Disconnect();
                }
                catch (TimeoutException ex)
                {
                    polacz.Enabled = true;
                    TimeoutError();                 
                }
                catch (Exception ex)
                {
                    polacz.Enabled = true;
                    Disconnect();
                }
            };

            anonimowy.Click += (sender, e) =>
            {
                login.Enabled = false;
                haslo.Enabled = false;         
            };

            uzytkownik.Click += (sender, e) =>
            {
                login.Enabled = true;
                haslo.Enabled = true;
            };
        }

        public async void Connect()
        {
           await session.OpenAsync().ConfigureAwait(true);
        }
        public async void Disconnect()
        {
            await session.CloseAsync().ConfigureAwait(true);
        }

    }
}