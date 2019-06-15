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
    class OpenPassword : BaseCommand
    {
        private String SharedSecret { get; set; }

        public OpenPassword()
        {
            Name = "Open Password";
            ParamList = new string[] { "password" };
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            Help = "<h3>Open Password</h3>" +
                    "<p>Set password so sugar can open your password file.</p>" +
                     "<dl>" +
                    "<dt>password <span class='label label-default'>required</span></dt>" +
                    "<dd></dd>" +
                    "</dl>";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new OpenPassword());
        }

        override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (args.Length > 1 )
            {
                Passwordfile.SharedSecret = args[1];
            }
            else
            {
                Passwordfile.SharedSecret = text;
            }
            return true; // hide command window
        }
    }
}
