using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Sugar.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace Sugar.Components.Commands
{
    class OpenPassword : ICommand
    {
        private String SharedSecret { get; set; }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new OpenPassword());
        }

        public string Name
        {
            get { return "Open Password"; }
        }

        public string[] ParamList
        {
            get { return new string[] { "password" }; }
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
            get { return null; }
        }

        public string Help
        {
            get
            {
                return "<h3>Open Password</h3>" +
                    "<p>Set password so sugar can open your password file.</p>" +
                     "<dl>" +
                    "<dt>password <span class='label label-default'>required</span></dt>" +
                    "<dd></dd>" +
                    "</dl>";
            }
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (args.Length > 1 )
            {
                Passwordfile.SharedSecret = args[1];
            }
            return true; // hide command window
        }


 
    }

}
