using System.IO;
using Microsoft.Extensions.Logging;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;

namespace PracaInzynierska
{
    class ConnectionWithServer
    {
        UaTcpSessionChannel sessionN;

        LoggerFactory loggerFactory = new LoggerFactory();

        static double sessionTimout = 120000;


        public UaTcpSessionChannelOptions optionChannel = new UaTcpSessionChannelOptions
        {
            TimeoutHint = 30000,
            DiagnosticsHint = 30000,
            SessionTimeout = sessionTimout
        };

        ApplicationDescription appDescription = new ApplicationDescription()
        {
            ApplicationName = "MyHomework",
            ApplicationUri = $"urn:{System.Net.Dns.GetHostName()}:MyHomework",
            ApplicationType = ApplicationType.Client,
        };

        DirectoryStore certificateStore = new DirectoryStore(
             Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Worksta", "pkfi"));


        public UaTcpSessionChannel GetSesssion(IUserIdentity userIdentity,string url)
        {

           sessionN = new UaTcpSessionChannel(
              appDescription,
              certificateStore,
              userIdentity,
              url,
              loggerFactory: loggerFactory,
              options: optionChannel);

            return sessionN;
        }
       

        public void SetTimeout(double timeout)
        {
            sessionTimout = timeout;
        }
        
        public UaTcpSessionChannelOptions GetOptionChannel()
        {
            return optionChannel;
        }

        public ApplicationDescription GetAppDesc()
        {
            return appDescription;

        }

        public DirectoryStore GetCertyficateStore()
        {
            return certificateStore;
        }

    }
}