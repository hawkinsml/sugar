using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Line : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Line());
        }

        public string Name
        {
            get { return "Line"; }
        }

        public string[] ParamList
        {
            get { return new string[] { "Match Text" }; }
        }

        public string Help
        {
            get
            {
                return "<h3>Line</h3>" +
                    "<p>Remove all lines that do not contain the matching text.</p>";
            }

        
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            string matchText = "";
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                matchText = args[1];
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                StringBuilder sb = new StringBuilder();

                List<string> lines = text.SplitLines();

                foreach (var line in lines)
                {
                    try
                    {
                        if (line.Contains(matchText))
                        {
                            sb.AppendLine(line);
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
