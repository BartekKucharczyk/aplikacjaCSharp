using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
        View headreList;
        Button readLogs;
        Timer timer;
        bool readBook = false;

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

        public void TimerInit()
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public void ShowDialogErrorBookName()
        {
            
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
            headreList = View.Inflate(this, Resource.Layout.headerLogRow, null);
            myList.AddHeaderView(headreList);

            readLogs.Click += (sender, e) =>
            {

                if (!nameOfLogbook.Text.Equals(""))
                {
                   readLogs.Enabled = false;
                   readBook = true;
                }
            };

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
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


            if (zarzadzanieLoggerem.endStateCmd.Equals("CMD_DONE_ALL: GET PREVIOUS RECORD") || zarzadzanieLoggerem.endStateCmd.Equals("CMD_OK: READING_ALL_LOG_DONE"))
                {
                   zarzadzanieLoggerem.ReadLoggerLists(sessionN);
                   zarzadzanieLoggerem.Acknowledge(sessionN);
                }
            }

            if (zarzadzanieLoggerem.endStateCmd.Equals("CMD_ERROR: Logbook entry doesnt exist"))
            {
                readLogs.Enabled = true;
                zarzadzanieLoggerem.Acknowledge(sessionN);
                ShowDialogErrorBookName();
            }

            if(zarzadzanieLoggerem.getReadInfo())
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
                       // Severity = zarzadzanieLoggerem.logSeverity[i],
                    });
                   
                }
                RunOnUiThread(() =>
                {
                    myList.Adapter = new MyCustomListAdapter(logList);
                    readLogs.Enabled = true;
                });
                
                zarzadzanieLoggerem.setReadInfo(false);
            }
        }

    }
}