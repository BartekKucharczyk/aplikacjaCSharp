using System;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;
using Android.App;
using Android.OS;
using Android.Content.PM;
using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace PracaInzynierska
{
    [Activity(Label = "Charts", ScreenOrientation = ScreenOrientation.Landscape)]
    public class Charts : Activity
    {

        UaTcpSessionChannel sessionN;

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

        private PlotModel CreatePlotModel(double[] yposition)
        {
            var plotModel = new PlotModel { Title = "Velocity plot" };

            double[] nrOfGrid = new double[13];

            for (int i = 0; i < nrOfGrid.Length; i++)
            {
                    nrOfGrid[i] = (yposition.Length / 12) * i;
            }


            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom ,ExtraGridlines = nrOfGrid ,ExtraGridlineColor = OxyColors.Blue,ExtraGridlineThickness = 0.5});
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 1700, Minimum = 0 });

            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Cross,
                LineLegendPosition = LineLegendPosition.Start,
                MarkerSize = 4,
            };

            for (int i = 0; i < yposition.Length; i++)
            {
               series1.Points.Add(new DataPoint(i,yposition[i]));//xposition[i]
            } 

            plotModel.Series.Add(series1);
            plotModel.AxisTierDistance = 10;
            plotModel.IsLegendVisible = true;
           
          
            return plotModel;
        }

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.charst_lay);

            PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);

            Random kp = new Random(1500);
            double[] ss = new double[150];
            for(int i = 0; i<150; i++)
            {
                ss[i] = kp.Next(1500);
            }

            view.Model = CreatePlotModel(ss);
           
          

        }
    }
}