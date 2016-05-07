using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UPNPLib;

namespace Sugar.Components.Commands
{
    class WeMo : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new WeMo());
        }

        public string Name
        {
            get { return "WeMo"; }
        }

        public string Help
        {
            get { return "<h3>WeMo</h3><p>Turns on/off a WeMo Switch</p>"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public bool Execute(string[] args)
        {
            try
            {
               // GetAllDevices();

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

            }
            catch (Exception ex) { }
            return true; // hide command window
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
