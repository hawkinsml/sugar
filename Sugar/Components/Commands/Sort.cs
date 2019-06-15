using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Sort : BaseCommand
    {
        public Sort()
        {
            Name = "Sort";
            ParamList = new string[] { "Asc | Desc" };
            ParamDescriptionList = new string[] { "sort ascending or descending. Defaults to ascending." };
            ParamRequired = new bool[] { true, false };
            Description = "Sort the contents of the clipboard by the text of each line.";
            Help = null;

        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Sort());
        }

        override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            bool ascending = true;

            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                string sortOrder = args[1].Trim();
                if ( sortOrder.StartsWith("d", StringComparison.CurrentCultureIgnoreCase) == true )
                {
                    ascending = false;
                }
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                StringBuilder sb = new StringBuilder();

                List<string> lines = text.SplitLines();

                if (ascending)
                {
                    lines = lines.OrderBy(o => o).ToList();
                }
                else
                {
                    lines = lines.OrderByDescending(o => o).ToList();
                }

                foreach (var line in lines)
                {
                    sb.AppendLine(line);
                }

                try
                {
                    Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
                }
                catch (Exception) { }
            }
            return true; // hide command window
        }
    }
}
