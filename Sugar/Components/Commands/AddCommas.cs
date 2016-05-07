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
            CommandManager.AddCommandHandler(new AddCommas());
        }

        public string Name
        {
            get { return "Commas"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return "<h3>Commas</h3><p>Replace line feed and carriage return with a comma.</p>"; }
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
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

            return true; // hide command window
        }
    }
}
