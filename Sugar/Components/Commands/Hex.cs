using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    public class Hex : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Hex());
        }

        public string Name
        {
            get { return "Hex"; }
        }

        public string[] ParamList
        {
            get { return new string[] { "decimal value(s)" }; }
        }

        public string[] ParamDescriptionList
        {
            get { return new string[] { "decimal value(s) to convert. If not provided, then contents of clipboard is convert." }; }
        }

        public bool[] ParamRequired
        {
            get { return new bool[] {false}; }
        }

        public string Description
        {
            get { return "Convert a decimal number (or string of numbers) into hexadecimal values.";; }
        }


        public string Help
        {
            get { return null; }
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                text = args[1];
            }

            int i = 0;
            if (int.TryParse(text, out i))
            {
                text = i.ToString("X");
                if ( !string.IsNullOrWhiteSpace( text ) )
                {
                    Clipboard.SetText(text.ToUpper(), TextDataFormat.Text);
                }
            }
            return true; // hide command window
        }
    }
}
