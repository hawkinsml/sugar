using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Base64 : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Base64());
        }

        public string Name
        {
            get { return "Base64"; }
        }

        public string[] ParamList
        {
            get { return new string[] { "Encode | Decode" }; }
        }

        public string Help
        {
            get
            {
                return "<h3>Base64</h3>" +
                    "<p>Encode or Decode a base64 string</p>";
            }

        
        }

        public bool Execute(string[] args)
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
