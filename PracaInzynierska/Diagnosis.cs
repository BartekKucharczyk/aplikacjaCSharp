using System;
using System.Collections.Generic;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;

namespace PracaInzynierska
{
    [Activity(Label = "Diagnosis")]
    public class Diagnosis : Activity
    {
        public List<OnelogClass> logList;

        UaTcpSessionChannel sessionN;
        ZarzadzanieLoggerem zarzadzanieLoggerem = new ZarzadzanieLoggerem();

        ListView myList;
        EditText nameOfLogbook,numberOfLog;
        Button readLogs;

        ProgressBar connectingProgresBar;
        Dialog dialog;

        Timer timer;
        bool readBook = false;
        bool sendAck;

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
            catch (ServiceResultException)
            {
                await sessionN.AbortAsync();
            }
            catch (InvalidOperationException)
            {
                await sessionN.AbortAsync();
            }
        }

        public void TimerInit()
        {
            timer = new Timer();
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public void ShowDialogErrorBookName()
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Incorrect parameter");
            alert.SetMessage("The logbook with that name is not exist.");
            alert.SetPositiveButton("Ok, I understand", (senderAlert, args) => {
                
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private void ConnectingProgress()
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View progressDialogBox = layoutInflater.Inflate(Resource.Layout.progres_dialog_box, null);
            connectingProgresBar = progressDialogBox.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            TextView text = progressDialogBox.FindViewById<TextView>(Resource.Id.connectingOrLoading);
            text.Text = "Loading, please wait...";
            Android.App.AlertDialog.Builder alertDialogBuilder = new Android.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetView(progressDialogBox);
            alertDialogBuilder.SetCancelable(false);
            //alertDialogBuilder.SetNegativeButton("Cancel", (senderAlert, args) => {
            //    readLogs.Enabled = true;
            //    dialog.Dismiss();

            //});
            connectingProgresBar.Max = 100;
            connectingProgresBar.Progress = 0;

            dialog = alertDialogBuilder.Create();
            
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.diagnosis);

            readLogs = FindViewById<Button>(Resource.Id.readLogBtn);
            nameOfLogbook = FindViewById<EditText>(Resource.Id.nameOfLoogbook);
            numberOfLog = FindViewById<EditText>(Resource.Id.numberOfLogToRead);
            TimerInit();
            Connect(Intent.GetStringExtra("url"), Intent.GetBooleanExtra("anonimowy", true), Intent.GetStringExtra("login"), Intent.GetStringExtra("haslo"));
          
            myList = FindViewById<ListView>(Resource.Id.loggerList);

            ConnectingProgress();

            readLogs.Click += (sender, e) =>
            {
                if (nameOfLogbook.Text.Equals(""))
                {
                    Toast.MakeText(this, "Logbook can not be empty.", ToastLength.Short).Show();
                    return;
                }

                if(numberOfLog.Text.ToString().Length == 0)
                {
                    Toast.MakeText(this, "Number of log can not be empty.", ToastLength.Short).Show();
                    return;
                }

                if (numberOfLog.Text.ToString().Length == 1)
                {
                    if (numberOfLog.Text.ToString().Equals("."))
                    {
                        Toast.MakeText(this, "Detected dot. Please remove dot from " + 1 + " position.", ToastLength.Short).Show();
                        return;
                    }
                }
                else if(numberOfLog.Text.ToString().Length == 2)
                {
                    if (numberOfLog.Text.ToString()[0].Equals('.'))
                    {
                        Toast.MakeText(this, "Detected dot. Please remove dot from 1 position.", ToastLength.Short).Show();
                        return;
                    }
                    if (numberOfLog.Text.ToString()[1].Equals('.'))
                    {
                        Toast.MakeText(this, "Detected dot. Please remove dot from 2 position.", ToastLength.Short).Show();
                        return;
                    }
                }
                if(numberOfLog.Text.ToString().Equals("0"))
                {
                    Toast.MakeText(this, "Detected zero. Please change the value.", ToastLength.Short).Show();
                    return;
                }

                connectingProgresBar.Progress = 0;

                dialog.Show();
                sendAck = false;
                readLogs.Enabled = false;
                readBook = true;
            };
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (dialog.IsShowing)
            {
                RunOnUiThread(() =>
                {
                    connectingProgresBar.IncrementProgressBy(1);
                });
            }

            if (sessionN.State.Equals(CommunicationState.Opened))
            {
                zarzadzanieLoggerem.ReadEndStateCmd(sessionN);
              
            if (readBook)
                {
                    zarzadzanieLoggerem.SendNumberOfLogToRead(sessionN,Int16.Parse(numberOfLog.Text));
                    zarzadzanieLoggerem.SendNameOfLogBook(sessionN, nameOfLogbook.Text);
                    zarzadzanieLoggerem.StartReadLog(sessionN);
                    readBook = false;
                }


            if (zarzadzanieLoggerem.endStateCmd.Equals("CMD_DONE_ALL: GET PREVIOUS RECORD") || zarzadzanieLoggerem.endStateCmd.Equals("CMD_OK: READING_ALL_LOG_DONE") )
                {
                   zarzadzanieLoggerem.ReadLoggerLists(sessionN);
                   zarzadzanieLoggerem.Acknowledge(sessionN);
                }
            

            if (zarzadzanieLoggerem.endStateCmd.Equals("CMD_ERROR: Logbook entry doesnt exist") && !sendAck)
            {
                readLogs.Enabled = true;
                RunOnUiThread(() =>
                {
                   dialog.Dismiss();
                   ShowDialogErrorBookName();
                });

                zarzadzanieLoggerem.Acknowledge(sessionN);
                sendAck = true;
            }

            if (zarzadzanieLoggerem.getReadInfo())

            {
                logList = new List<OnelogClass>();

                
                for (int i = 0; i < zarzadzanieLoggerem.logDesc.Length-1; i++)
                {
                    logList.Add(new OnelogClass()
                    {
                        IdLog = "(" + (i + 1).ToString() + ")",
                        ErrorId = zarzadzanieLoggerem.logList[i],
                        Description = zarzadzanieLoggerem.logDesc[i],
                        Date = zarzadzanieLoggerem.logTime[i]
                      
                    });
                   
                }
               
                RunOnUiThread(() =>
                {
                    dialog.Dismiss();
                    myList.Adapter = new MyCustomListAdapter(logList);
                    readLogs.Enabled = true;
                    
                });
              }
                zarzadzanieLoggerem.setReadInfo(false);


            }
            

        }

    }
}