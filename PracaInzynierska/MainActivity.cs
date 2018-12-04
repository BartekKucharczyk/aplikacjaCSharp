using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Workstation.ServiceModel.Ua.Channels;
using Workstation.ServiceModel.Ua;
using Android.Content;
using System.Timers;
using Android.Preferences;

namespace PracaInzynierska
{
    [Activity(Label = "Drive - PLC Diagnostic", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        RadioButton anonimowy, uzytkownik;
        Button polacz;
        EditText ipAdress, portEd, endpoinUrlET, login, haslo;
        Timer timer;
        CheckBox remember;
        bool tryConnect = false;

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

            remember = FindViewById<CheckBox>(Resource.Id.rememberChB);

            anonimowy = FindViewById<RadioButton>(Resource.Id.anonimowyRB);
            uzytkownik = FindViewById<RadioButton>(Resource.Id.uzytkownikRB);
        }
        public void TimerInit()
        {
            timer = new Timer();
            timer.Interval = 50;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
        }

        public void InitValue()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ipAdress.Text = prefs.GetString("ip_server_OPC", "");
            portEd.Text = prefs.GetString("port_server_OPC", "");
            endpoinUrlET.Text = prefs.GetString("endpointURL_server_OPC","");
        }
        
        public void ErrorAlert()
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Incorrect parameter");
            alert.SetMessage("One of the objects may be incorrect. Correct this and try to connect again.");
            alert.SetPositiveButton("Ok, I understand", (senderAlert, args) => {
                //Toast.MakeText(this, "Deleted!", ToastLength.Short).Show();
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
             
            InitScreen();
            InitValue();
            TimerInit();

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

                session = connection.GetSesssion(userIdentity, discoveryUrl);
             
                tryConnect = true;
                timer.Start();
                
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

        public void Connected()
        {
                Intent intent = new Intent(this, typeof(Control));
                intent.PutExtra("url", endpoinUrlET.Text + "://" + ipAdress.Text + ":" + portEd.Text);
                intent.PutExtra("anonimowy", anonimowy.Checked);
                intent.PutExtra("login", login.Text);
                intent.PutExtra("haslo", haslo.Text);
                polacz.Enabled = true;
                StartActivity(intent);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (tryConnect)
            {
                session.OpenAsync();
                tryConnect = false;
            }

            if (session.State.ToString().Equals("Opened"))
            {
                timer.Stop();

                if(remember.Checked)
                {
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("ip_server_OPC", ipAdress.Text.ToString());
                    editor.PutString("port_server_OPC", portEd.Text.ToString());
                    editor.PutString("endpointURL_server_OPC", endpoinUrlET.Text.ToString());
                    editor.Apply();
                }

                RunOnUiThread(() =>
                {
                    Connected();
                });
            }
            else if(session.State.ToString().Equals("Faulted"))
            {
                timer.Stop();
                RunOnUiThread(() =>
                {
                    polacz.Enabled = true;
                    ErrorAlert();
                });
            }


        }

    }
}