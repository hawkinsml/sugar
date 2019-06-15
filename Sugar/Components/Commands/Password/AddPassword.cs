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
    class AddPassword : BaseCommand
    {
        private String SharedSecret { get; set; }

        public AddPassword()
        {
            Name = "Add Password";
            ParamList = new string[] { "name", "url", "email", "user name", "password" };
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            Help = "<h3>Add Password</h3>" +
                    "<p>Add new account for pw command.</p>" +
                     "<dl>" +
                    "<dt>name <span class='label label-default'>required</span></dt>" +
                    "<dd></dd>" +
                    "<dt>password <span class='label label-default'>required</span></dt>" +
                    "<dd></dd>" +
                    "<dt>user name <span class='label label-default'>option</span></dt>" +
                    "<dd></dd>" +
                    "<dt>url <span class='label label-default'>option</span></dt>" +
                    "<dd></dd>" +
                    "<dt>email <span class='label label-default'>option</span></dt>" +
                    "<dd></dd>" +
                    "</dl>";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new AddPassword());
        }

        override public bool Execute(string[] args)
        {
            if (string.IsNullOrWhiteSpace(Passwordfile.SharedSecret))
            {
                MessageBox.Show("Password not set. Use 'Open Password' command to set password.");
            }
            else
            {
                if (args.Length > 2)
                {
                    PasswordModel pw = new PasswordModel();
                    pw.Name = args[1];
                    pw.Password = args[2];
                    if (args.Length > 3)
                    {
                        pw.UserName = args[3];
                        if (args.Length > 4)
                        {
                            pw.Url = args[4];
                            if (args.Length > 5)
                            {
                                pw.Email = args[5];
                            }
                        }
                    }

                    PasswordsModel data = Passwordfile.ReadFile();
                    if (data == null)
                    {
                        data = new PasswordsModel();
                    }
                    if ( data.Passwords != null )
                    {
                        data.Passwords.Add(pw);
                    }
                    Passwordfile.SaveFile(data);
                }
            }
            return true; // hide command window
        }
    }
}
