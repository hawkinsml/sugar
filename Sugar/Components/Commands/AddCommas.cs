using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    public class AddCommas : BaseCommand
    {
        public AddCommas()
        {
            Name = "Commas";
            ParamList = null;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            Help = "<h3>Commas</h3><p>Replace line feed and carriage return with a comma.</p>";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new AddCommas());
        }

        override public bool Execute(string[] args)
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
