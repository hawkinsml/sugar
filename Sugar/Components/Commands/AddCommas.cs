using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    public class AddCommas : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new MakeUpper());
        }

        public string Name
        {
            get { return "Commas"; }
        }

        public void Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                text = args[0];
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                StringBuilder sb = new StringBuilder();

                List<string> lines = text.SplitLines();

                foreach (var line in lines)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(line.Trim());
                }

                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            }
        }
    }
}
