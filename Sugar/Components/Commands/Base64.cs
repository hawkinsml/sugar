using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Base64 : BaseCommand
    {
        public Base64()
        {
            Name = "Base64";
            ParamList = new string[] { "Encode | Decode" };
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            Help = "<h3>Base64</h3>" +
                    "<p>Encode or Decode a base64 string</p>";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Base64());
        }

        override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            bool decode = false;
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                decode = args[1].StartsWith("d", StringComparison.OrdinalIgnoreCase);
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                string newText = "";
                if (decode)
                {
                    byte[] data = Convert.FromBase64String(text);
                    newText = ASCIIEncoding.ASCII.GetString(data);
                }
                else
                {
                    newText = Convert.ToBase64String(new ASCIIEncoding().GetBytes(text));
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
