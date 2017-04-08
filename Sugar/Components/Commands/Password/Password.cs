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
    class Password : ICommand
    {

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Password());
        }

        public string Name
        {
            get { return "pw"; }
        }

        public string[] ParamList
        {
            get { return new string[] { "site name" }; }
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
                return "<h3>pw (Password)</h3>" +
                    "<p>Places a password on the clipboard for a given account.</p>" +
                     "<dl>" +
                    "<dt>Site or Account <span class='label label-default'>required</span></dt>" +
                    "<dd>url or other text that matches the account for the password.</dd>" +
                    "</dl>";
            }
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();

            bool xml = true;
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                xml = !args[1].StartsWith("j", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    string check = text.TrimStart();
                    if (check.Length > 0 && check[0] == '<')
                    {
                        xml = true;
                    }
                    else
                    {
                        xml = false;
                    }
                }
            }


            return true; // hide command window
        }


 
    }

}
