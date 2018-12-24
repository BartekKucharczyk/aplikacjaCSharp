using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Workstation.ServiceModel.Ua.Channels;
using Workstation.ServiceModel.Ua;
using Android.Content;
using System.Timers;
using Android.Preferences;
using Android.Views;

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
        ProgressBar connectingProgresBar;
        Dialog dialog;
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
            timer.Interval = 100;
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
                 });

            Dialog dialogs = alert.Create();
            dialogs.Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
             
            InitScreen();
            ConnectingProgress();
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
                connectingProgresBar.Progress = 0;
                dialog.Show();
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
            Toast.MakeText(this, "Connected", ToastLength.Short).Show();
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

            if (dialog.IsShowing)
            {
                RunOnUiThread(() =>
                {
                    connectingProgresBar.IncrementProgressBy(1);

                    if(connectingProgresBar.Progress == 120)
                    {
                          dialog.Dismiss();
                          polacz.Enabled = true;
                          ErrorAlert();
                         timer.Stop();
                    }
                });
            }

            if (session.State.ToString().Equals("Opened"))
            {
                

                if(remember.Checked)
                {
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("ip_server_OPC", ipAdress.Text.ToString());
                    editor.PutString("port_server_OPC", portEd.Text.ToString());
                    editor.PutString("endpointURL_server_OPC", endpoinUrlET.Text.ToString());
                    editor.Apply();
                }
                 timer.Stop();
                RunOnUiThread(() =>
                {
                    dialog.Dismiss();
                    Connected();
                });

               
            }
            else if(session.State.ToString().Equals("Faulted"))
            {
                
                RunOnUiThread(() =>
                {
                    dialog.Dismiss();
                    polacz.Enabled = true;
                    ErrorAlert();
                });
                timer.Stop();
            }
        }

        private void ConnectingProgress()
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View progressDialogBox = layoutInflater.Inflate(Resource.Layout.progres_dialog_box, null);
            connectingProgresBar = progressDialogBox.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            Android.App.AlertDialog.Builder alertDialogBuilder = new Android.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetView(progressDialogBox);
            alertDialogBuilder.SetCancelable(false);
            alertDialogBuilder.SetNegativeButton("Cancel", (senderAlert, args) => {
                timer.Stop();
                polacz.Enabled = true;
                dialog.Dismiss();

            });
            connectingProgresBar.Max = 120;
            connectingProgresBar.Progress = 0;
            
            dialog = alertDialogBuilder.Create();
            
            
        }
    }
}