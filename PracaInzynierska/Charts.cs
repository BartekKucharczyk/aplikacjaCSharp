using System;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;
using Android.App;
using Android.OS;
using Android.Content.PM;
using OxyPlot.Xamarin.Android;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Timers;
using Android.Content;
using Android.Preferences;
using OxyPlot;
using System.IO;
using Android;

namespace PracaInzynierska
{
    [Activity(Label = "Charts", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Charts : Activity
    {
        ZarzadzanieCharts zarzadzanie = new ZarzadzanieCharts();
        UaTcpSessionChannel sessionN;
        TextView para1;
        Button load, change1,scores,refresh;
        PlotView view1;
        List<string> titleList;
        List<string> paramsCode;
        Timer timer;
        string path1;
        

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
            timer.Interval = 10;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            if (zarzadzanie.error)
            {
                RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Error! Bad path model", ToastLength.Short).Show();
                });
                timer.Stop();
                zarzadzanie.error = false;
;            }

            if (zarzadzanie.done)
            {
                RunOnUiThread(() =>
                {
                    view1.Model = zarzadzanie.CreatePlotModel(para1.Text);
                    scores.Enabled = true;
                });
                timer.Stop();
            }
        }

        private void ConfScreen()
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View progressDialogBox = layoutInflater.Inflate(Resource.Layout.configuratorChart, null);

            EditText nameNew  = progressDialogBox.FindViewById<EditText>(Resource.Id.titleParamsEdtxt);
            EditText pathNew = progressDialogBox.FindViewById<EditText>(Resource.Id.pathParamsEdtx);

            Spinner title = progressDialogBox.FindViewById<Spinner>(Resource.Id.spinnerParTitle);
            Spinner path = progressDialogBox.FindViewById<Spinner>(Resource.Id.spinnerPath);
           
            title.ItemSelected += (sender, e) =>
            {
               path.SetSelection(title.SelectedItemPosition);
            };

            TextView add = progressDialogBox.FindViewById<TextView>(Resource.Id.addBtnChart);

            var titleAdapter = new ArrayAdapter<string>(
                  this, Android.Resource.Layout.SimpleSpinnerItem, titleList);
            var paramsAdapter = new ArrayAdapter<string>(
                 this, Android.Resource.Layout.SimpleSpinnerItem, paramsCode);

            titleAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            paramsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            
            title.Adapter = titleAdapter;
            path.Adapter = paramsAdapter;
            path.Enabled = false;
            title.SetSelection(1);
            path.SetSelection(1);

            Android.App.AlertDialog.Builder alertDialogBuilder = new Android.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetView(progressDialogBox);
            alertDialogBuilder.SetCancelable(false);
            alertDialogBuilder.SetTitle("Parameter");
            alertDialogBuilder.SetPositiveButton("Save", (sender, args) =>
             {
                     para1.Text = title.SelectedItem.ToString();
                     path1 = path.SelectedItem.ToString();
             });

            alertDialogBuilder.SetNegativeButton("Cancel", (senderAlert, args) => {
                

            });

            add.Click += (sender, e) =>
            {
                if(nameNew.Text.ToString().Equals("") || pathNew.Text.ToString().Equals("") )
                {
                    Toast.MakeText(this, "Parameters can not be empty!", ToastLength.Short);
                    return;
                }
            
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                string pathToSave = "";
                string nameToSave = "";
                for(int i =0;i < titleList.Count; i++)
                {
                    nameToSave = nameToSave + titleList[i]+";";
                    pathToSave = pathToSave + paramsCode[i] + "$";
                }
                nameToSave = nameToSave + nameNew.Text + ";";
                pathToSave = pathToSave + pathNew.Text + "$";

                editor.PutString("path_to_chart", pathToSave);
                editor.PutString("name_to_chart", nameToSave);
                editor.Apply();
                Toast.MakeText(this, "Saved!", ToastLength.Short).Show();
       
            };

            Dialog dialog;
            dialog = alertDialogBuilder.Create();
            dialog.Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.charst_lay);

            view1 = FindViewById<PlotView>(Resource.Id.plot_view1);
            change1 = FindViewById<Button>(Resource.Id.changeBtn1);
            para1 = FindViewById<TextView>(Resource.Id.parameterTxt1);
            load = FindViewById<Button>(Resource.Id.loadChartBtn);
            scores = FindViewById<Button>(Resource.Id.tableChartBtn);
            refresh = FindViewById<Button>(Resource.Id.refreshBtnCh);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            string[] nameS = prefs.GetString("name_to_chart", "").Split(';');
            string[] pathS = prefs.GetString("path_to_chart", "").Split('$');
            if (!nameS[0].Equals(""))
            {
                titleList = new List<string>();
                paramsCode = new List<string>();
                for(int i =0;i< nameS.Length-1; i++)
                {
                    titleList.Add(nameS[i]);
                    paramsCode.Add(pathS[i]);
                }
            }
            else
            {
            titleList = zarzadzanie.ListOfName();
            paramsCode = zarzadzanie.ListOfPath();
            }

            Connect(Intent.GetStringExtra("url"), Intent.GetBooleanExtra("anonimowy", true), Intent.GetStringExtra("login"), Intent.GetStringExtra("haslo"));

            change1.Click += Change1_Click;
            load.Click += Load_Click;
            scores.Click += Scores_Click;
            refresh.Click += Refresh_Click;
            TimerInit();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            view1.Invalidate();
            
        }

        private void Scores_Click(object sender, EventArgs e)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View scoresTable = layoutInflater.Inflate(Resource.Layout.chartTable, null);
            ListView scoresList = scoresTable.FindViewById<ListView>(Resource.Id.listSummit);
            List<scoreChart> listScoreChart = new List<scoreChart>();

            for(int i = 0; i < zarzadzanie.yposition.Length; i++)
            {
                listScoreChart.Add(new scoreChart()
                {
                    ID = ""+(i+1),
                    Value = zarzadzanie.yposition[i]+"",
                    Time = zarzadzanie.xposition[i]+""
                });
            }

            scoresList.Adapter = new MyCustomScoreAdapter(listScoreChart);
            Android.App.AlertDialog.Builder alertDialogBuilder = new Android.App.AlertDialog.Builder(this);
            alertDialogBuilder.SetView(scoresTable);
            alertDialogBuilder.SetCancelable(false);
                   
            alertDialogBuilder.SetNegativeButton("Cancel", (senderAlert, args) => {


            });
            //alertDialogBuilder.SetPositiveButton("Export", (senderAlert, args) =>
            //{
            //    if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) != Permission.Granted
            //        && PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted)
            //    {
            //        var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
            //        RequestPermissions(permissions, 1);
            //    }

            //    Stream s = File.Create(Android.OS.Environment.DirectoryDownloads+"/HelloWorld.pdf");
            //    var doc = new PortableDocument();
            //    doc.Title = "Hello world";
            //    doc.Author = "objo";
            //    doc.AddPage(PageSize.A4);
            //    doc.SetFont("Arial", 96);
            //    doc.DrawText(50, 400, "Hello world!");
            //    doc.Save(s);
            //});

            Dialog dialog;
            dialog = alertDialogBuilder.Create();
            dialog.Show();
        }

        private void Load_Click(object sender, EventArgs e)
        {
            if (sessionN.State.Equals(CommunicationState.Opened))
            {
                
               if(para1.Text.Equals("empty"))
                {
                    Toast.MakeText(this, "Parameter can not be empty", ToastLength.Short).Show();
                    return;
                }
                Toast.MakeText(this, "Loading...", ToastLength.Short).Show();
                scores.Enabled = false;
                zarzadzanie.ReadHistoricalDataDef(sessionN, path1);
                timer.Start();
                
               
            }
        }

        private void Change1_Click(object sender, EventArgs e)
        {
            ConfScreen();
        }

    }
}