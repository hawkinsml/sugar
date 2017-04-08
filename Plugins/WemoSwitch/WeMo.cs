using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WemoSwitch.WeMoService; // https://raw.github.com/sklose/WeMoWsdl/master/BasicService.wsdl
using UPNPLib;
using Sugar;

namespace WemoSwitch.Components.Commands
{
    class WeMo : ICommand
    {
        string ip = "192.168.86.177";
        int port = 49153;

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new WeMo());
        }

        public string Name
        {
            get { return "WeMo"; }
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
            get { return "Turns on/off a WeMo Switch"; }
        }

        public bool Execute(string[] args)
        {
            try
            {
                port = 49153;
                if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
                {
                    port = 49154;
                }

                runGetAllDevices();
                runToggle();
                /*
                bool turnOn = true;
                if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
                {
                    turnOn = !args[1].StartsWith("of", StringComparison.OrdinalIgnoreCase);
                }

                string ip = "192.168.86.177";
                int port = 49154;

                var client = new WeMoService.BasicServicePortTypeClient();
                client.Endpoint.Address = new EndpointAddress(string.Format("http://{0}:{1}/upnp/control/basicevent1", ip, port));

                var state = client.GetBinaryState(new WeMoService.GetBinaryState());
                Console.WriteLine("Switch is current set to: {0}", state.BinaryState);

                if (state.BinaryState == "0")
                {
                    state.BinaryState = "1";
                }
                else
                {
                    state.BinaryState = "0";
                }

                var msg = new WeMoService.SetBinaryState { BinaryState = state.BinaryState };
                client.SetBinaryState(msg);
                 * */

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
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;
                var finder = new UPnPDeviceFinder();
                var foundDevices = new List<UPnPDevice>();

                var deviceType = "upnp:rootdevice";
                var devices = finder.FindByType(deviceType, 1);

                foreach (UPnPDevice upnpDevice in devices)
                {

                    string port = new Uri(upnpDevice.PresentationURL).Port.ToString();
                    string baseUrl = new Uri(upnpDevice.PresentationURL).DnsSafeHost.ToString();

                    Console.WriteLine("Type: " + upnpDevice.Type);
                    Console.WriteLine("Port: " + port);
                    Console.WriteLine("baseUrl: " + baseUrl);

                }

                //                    b.ReportProgress(i * 10);

            });

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

        private void runToggle()
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

        private void doworkNotWorking(object o, DoWorkEventArgs args)
        {
            BackgroundWorker b = o as BackgroundWorker;
            string ip = "192.168.86.177";
            Console.WriteLine(string.Format("Wemo Address: http://{0}:{1}/upnp/control/basicevent1", ip, port));

            WSHttpBinding binding = new WSHttpBinding();
            binding.OpenTimeout = new TimeSpan(0, 0, 20);
            binding.CloseTimeout = new TimeSpan(0, 1, 0);
            binding.SendTimeout = new TimeSpan(0, 1, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 1, 0);

            var client = new WeMoService.BasicServicePortTypeClient(binding, new EndpointAddress(string.Format("http://{0}:{1}/upnp/control/basicevent1", ip, port)));
            GetBinaryStateResponse state = null;
            try
            {
                
                state = client.GetBinaryState(new WeMoService.GetBinaryState());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (port == 49154)
                {
                    port = 49153;
                }
                else
                {
                    port = 49154;
                }
                try
                {
                    Console.WriteLine(string.Format("Wemo Address: http://{0}:{1}/upnp/control/basicevent1", ip, port));
                    client = new WeMoService.BasicServicePortTypeClient(binding, new EndpointAddress(string.Format("http://{0}:{1}/upnp/control/basicevent1", ip, port)));
                    state = client.GetBinaryState(new WeMoService.GetBinaryState());
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
            }

            try
            {
                Console.WriteLine("Switch is current set to: {0}", state.BinaryState);
                if (state != null)
                {
                    if (state.BinaryState == "0")
                    {
                        state.BinaryState = "1";
                    }
                    else
                    {
                        state.BinaryState = "0";
                    }

                    var msg = new WeMoService.SetBinaryState { BinaryState = state.BinaryState };
                    client.SetBinaryState(msg);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

        }

        private void dowork(object o, DoWorkEventArgs args)
         {
             try
             {
                 var client = new WeMoService.BasicServicePortTypeClient();
                 client.Endpoint.Address = new EndpointAddress(string.Format("http://{0}:{1}/upnp/control/basicevent1", ip, port));
 
                 var state = client.GetBinaryState(new WeMoService.GetBinaryState());
                 Console.WriteLine("Switch is current set to: {0}", state.BinaryState);
 
                 if (state.BinaryState == "0")
                 {
                     state.BinaryState = "1";
                 }
                 else
                 {
                     state.BinaryState = "0";
                 }
 
                 var msg = new WeMoService.SetBinaryState { BinaryState = state.BinaryState };
                 client.SetBinaryState(msg);
 
             }
             catch (Exception ex) 
             {
                 WebPage.DisplayWebPage("Wemo Command Error", ex.Message);             
             }
         }


        private void GetAllDevices()
        {
            var finder = new UPnPDeviceFinder();
            var foundDevices = new List<UPnPDevice>();

            var deviceType = "upnp:rootdevice";
            var devices = finder.FindByType(deviceType, 1);

            foreach (UPnPDevice upnpDevice in devices)
            {

                string port = new Uri(upnpDevice.PresentationURL).Port.ToString();
                string baseUrl = new Uri(upnpDevice.PresentationURL).DnsSafeHost.ToString();

                Console.WriteLine("Type: " + upnpDevice.Type);
                Console.WriteLine("Port: " + port);
                Console.WriteLine("baseUrl: " + baseUrl);

            }
        }

    }
}
