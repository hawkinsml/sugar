using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    public class Hex : BaseCommand
    {
        public Hex()
        {
            Name = "Hex";
            ParamList = new string[] { "decimal value(s)" };
            ParamDescriptionList = new string[] { "decimal value(s) to convert. If not provided, then contents of clipboard is convert." };
            ParamRequired = new bool[] { false };
            Description = "Convert a decimal number (or string of numbers) into hexadecimal values.";
            Help = "<h3>Hex</h3><p>Convert a decimal number (or string of numbers) into hexadecimal values.</p>";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Hex());
        }

        override public bool Execute(string[] args)
        {
            string retVal = "";
            string text = Clipboard.GetText();
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                text = args[1];
            }


            if ( text.Contains(','))
            {
                var values = text.Split(',');
                foreach(var value in values)
                {
                    int i = 0;
                    if (int.TryParse(value, out i))
                    {
                        retVal += i.ToString("X");
                    }
                }
            }
            else if (text.Contains(' '))
            {
                var values = text.Split(' ');
                foreach (var value in values)
                {
                    int i = 0;
                    if (int.TryParse(value, out i))
                    {
                        retVal += i.ToString("X");
                    }
                }
            }
            else
            {
                int i = 0;
                if (int.TryParse(text, out i))
                {
                    text = i.ToString("X");
                }
            }

            if (!string.IsNullOrWhiteSpace(retVal))
            {
                Clipboard.SetText(retVal.ToUpper(), TextDataFormat.Text);
            }

            return true; // hide command window
        }
    }
}
