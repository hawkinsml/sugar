using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Sort : ICommand
    {
        string description = "Sort the contents of the clipboard by the text of each line.";
        string[] paramList = new string[]  { "Asc | Desc" };
        string[] paramDescriptionList = new string[] { "sort ascending or descending. Defaults to ascending." };
        bool[] paramRequired = { true, false };


        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Sort());
        }

        public string Name
        {
            get { return "Sort"; }
        }

        public string[] ParamList
        {
            get { return paramList; }
        }

        public string[] ParamDescriptionList
        {
            get { return paramDescriptionList; }
        }

        public bool[] ParamRequired
        {
            get { return paramRequired; }
        }

        public string Description
        {
            get { return description; }
        }

        public string Help
        {
            get {               
                return null; 
            }
        }

        public bool Execute(string[] args)
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
