using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Format : ICommand
    {
        string description = "For each line on the clipboard, splits the line by spaces into a string array and passes these array into string.Format(Format String, words).";
        string[] paramList = new string[]  { "Format String", "Delimiter" };
        string[] paramDescriptionList = new string[] { "String passed as the Format text to string.Format().", "Split the lines by <b>word</b>, <b>line</b> or <b>tab</b>." };
        bool[] paramRequired = { true, false };


        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Format());
        }

        public string Name
        {
            get { return "Format"; }
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
            string formatText = "";
            string delimiter = "word";
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                formatText = args[1];
            }

            if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            {
                if (string.Compare(args[2].Trim(), "line", true) == 0)
                {
                    delimiter = "line";
                }
                else if (string.Compare(args[2].Trim(), "tab", true) == 0)
                {
                    delimiter = "tab";
                }

            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                StringBuilder sb = new StringBuilder();

                List<string> lines = text.SplitLines();

                foreach (var line in lines)
                {
                    try
                    {
                        if (delimiter == "word")
                        {
                            string[] words = line.Trim().Split(' ');
                            sb.AppendLine(string.Format(formatText, words));
                        }
                        else if (delimiter == "tab")
                        {
                            string[] words = line.Trim().Split('\t');
                            sb.AppendLine(string.Format(formatText, words));
                        }
                        else if (delimiter == "line")
                        {
                            sb.AppendLine(string.Format(formatText, line));
                        }
                    }
                    catch( Exception) {}

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
