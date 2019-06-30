using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Url : BaseCommand
    {
        public Url()
        {
            Name = "Url";
            ParamList = new string[] { "Encode | Decode | All" };
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            Help = "<h3>Url</h3>" +
                   "<p>Encode or Decode a Url string</p>" +
                   "<p>All will encode normal as well as encode the follow: $ & + , / : ; = ? @</p>";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Url());
        }

        override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            bool decode = false;
            bool all = false;
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                if (args[1].StartsWith("d", StringComparison.OrdinalIgnoreCase))
                {
                    decode = true;
                }
                else if (args[1].StartsWith("a", StringComparison.OrdinalIgnoreCase))
                {
                    decode = false;
                    all = true;
                }


            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                string newText = "";
                if (decode)
                {
                    newText = Uri.UnescapeDataString(text);
                }
                else
                {
                    newText = Uri.EscapeUriString(text);
                    if (all)
                    {
                        newText = newText.Replace("$", "%24");
                        newText = newText.Replace("&", "%26");
                        newText = newText.Replace("+", "%2B");
                        newText = newText.Replace(",", "%2C");
                        newText = newText.Replace("/", "%2F");
                        newText = newText.Replace(":", "%3A");
                        newText = newText.Replace(";", "%3B");
                        newText = newText.Replace("=", "%3D");
                        newText = newText.Replace("?", "%3F");
                        newText = newText.Replace("@", "%40");
                    }
                }

                try
                {
                    Clipboard.SetText(newText, TextDataFormat.Text);
                }
                catch (Exception) { }
            }
            return true; // hide command window
        }
    }
}
