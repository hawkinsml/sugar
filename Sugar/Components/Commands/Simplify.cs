using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sugar.Components.Commands
{
    class Simplify : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Simplify());
        }

        public string Name
        {
            get { return "Simplify"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return "<h3>Simplify</h3><p>Converts the contents of the clipboard from formatted text such as HTML or Rich Text into plain text.</p>"; }
        }

        public bool Execute(string[] args)
        {
            string type = "Text";
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                type = args[1].Trim();
            }

            var data = Clipboard.GetDataObject();
            string[] formats = data.GetFormats(true);
            foreach (var format in formats)
            {
                Console.WriteLine(format);
                if (format.Contains(type))
                {
                    type = format;
                    break;
                }
            }


            object text = data.GetData(type);
            if (text != null)
            {
                Clipboard.SetText(text.ToString());
            }

            return true; // hide command window
        }

    }
}
