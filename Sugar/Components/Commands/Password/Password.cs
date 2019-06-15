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
    class Password : BaseCommand
    {

        public Password()
        {
            Name = "pw";
            ParamList = new string[] { "site name" };
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            Help = "<h3>pw (Password)</h3>" +
                    "<p>Places a password on the clipboard for a given account.</p>" +
                     "<dl>" +
                    "<dt>Site or Account <span class='label label-default'>required</span></dt>" +
                    "<dd>url or other text that matches the account for the password.</dd>" +
                    "</dl>";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Password());
        }

        override public bool Execute(string[] args)
        {
            if (string.IsNullOrWhiteSpace(Passwordfile.SharedSecret))
            {
                MessageBox.Show("Password not set. Use 'Open Password' command to set password.");
            }
            else
            {
                if (args.Length > 1)
                {
                    string name = args[1];
                    PasswordsModel data = Passwordfile.ReadFile();
                    if (data != null)
                    {
                        if (data.Passwords != null)
                        {
                            string password = data.Passwords.Where(o => o.Name.ToLower() == name.ToLower()).Select(o => o.Password).FirstOrDefault();
                            if (string.IsNullOrWhiteSpace(password))
                            {
                                MessageBox.Show("Password not found.");
                            }
                            else
                            {
                                Clipboard.SetText(password, TextDataFormat.Text);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password not set. Use 'Open Password' command to set password.");
                    }
                }
            }
            return true; // hide command window
        }
    }

}
