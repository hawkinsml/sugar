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
    class AddPassword : ICommand
    {
        private String SharedSecret { get; set; }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new AddPassword());
        }

        public string Name
        {
            get { return "Add Password"; }
        }

        public string[] ParamList
        {
            get { return new string[] { "name", "url", "email", "user name", "password" }; }
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
                return "<h3>Add Password</h3>" +
                    "<p>Add new account for pw command.</p>" +
                     "<dl>" +
                    "<dt>name <span class='label label-default'>required</span></dt>" +
                    "<dd></dd>" +
                    "<dt>url <span class='label label-default'>required</span></dt>" +
                    "<dd></dd>" +
                    "<dt>email <span class='label label-default'>required</span></dt>" +
                    "<dd></dd>" +
                    "<dt>user name <span class='label label-default'>required</span></dt>" +
                    "<dd></dd>" +
                    "<dt>password <span class='label label-default'>required</span></dt>" +
                    "<dd></dd>" +
                    "</dl>";
            }
        }

        public bool Execute(string[] args)
        {

            if (string.IsNullOrWhiteSpace(Passwordfile.SharedSecret))
            {
                MessageBox.Show("Password not set. Use 'Open Password' command to set password.");
            }
            else
            {
                if (args.Length > 5)
                {
                    PasswordModel pw = new PasswordModel();
                    pw.Name = args[1];
                    pw.Url = args[2];
                    pw.Email = args[3];
                    pw.UserName = args[4];
                    pw.Password = args[5];

                    PasswordsModal data = Passwordfile.ReadFile();
                    if ( data != null )
                    {
                        if ( data.Passwords != null )
                        {
                            data.Passwords.Add(pw);
                        }
                        Passwordfile.SaveFile(data);
                    }
                }
            }
            return true; // hide command window
        }


 
    }

}
