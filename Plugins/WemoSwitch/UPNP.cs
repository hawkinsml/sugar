using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UPNPLib;
using Sugar;
using System.IO;
using WemoSwitch.Helpers;

namespace WemoSwitch.Components.Commands
{
    class UPNP : ICommand
    {

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new UPNP());
        }

        public string Name
        {
            get { return "UPnP"; }
        }

        public string Help
        {
            get { return null; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string[] ParamDescriptionList
        {
            get { return null; }
        }

        public bool[] ParamRequired
        {
            get { return null; }
        }

        public string Description
        {
            get { return "Use Universal Plug and Play to discover other UPnP devices on the network."; }
        }

        public bool Execute(string[] args)
        {
            try
            {
                runGetAllDevices();
            }
            catch (Exception ex) { }
            return true; // hide command window
        }


        private void runGetAllDevices()
        {
            BackgroundWorker bw = new BackgroundWorker();

            // this allows our worker to report progress during work
            bw.WorkerReportsProgress = true;

            // what to do in the background thread
            bw.DoWork += new DoWorkEventHandler(dowork);

            // what to do when progress changed (update the progress bar for example)
            bw.ProgressChanged += new ProgressChangedEventHandler(
            delegate(object o, ProgressChangedEventArgs args)
            {
                Console.WriteLine(string.Format("{0}% Completed", args.ProgressPercentage));
            });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                Console.WriteLine("Finished!");
            });

            bw.RunWorkerAsync();
        }

        private void dowork(object o, DoWorkEventArgs args)
        {
            BackgroundWorker b = o as BackgroundWorker;

            StringBuilder sb = new StringBuilder();
            var finder = new UPnPDeviceFinder();
            var foundDevices = new List<UPnPDevice>();

            var deviceType = "upnp:rootdevice";
            var devices = finder.FindByType(deviceType, 1);

            foreach (UPnPDevice upnpDevice in devices)
            {


                sb.AppendHtmlLine("FriendlyName: " + upnpDevice.FriendlyName);
                sb.AppendHtmlLine("Description: " + upnpDevice.Description);
                sb.AppendHtmlLine("PresentationURL: " + upnpDevice.PresentationURL);


                string port = new Uri(upnpDevice.PresentationURL).Port.ToString();
                string baseUrl = new Uri(upnpDevice.PresentationURL).DnsSafeHost.ToString();

                sb.AppendHtmlLine("Type: " + upnpDevice.Type);
                sb.AppendHtmlLine("Port: " + port);
                sb.AppendHtmlLine("baseUrl: " + baseUrl);

                sb.AppendLine("<hr/>");
            }

            WebPage.DisplayWebPage("Universal Plug and Play devices", sb.ToString());
        }
    }
}
