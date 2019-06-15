using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Line : BaseCommand
    {
        public Line()
        {
            Name = "Line";
            ParamList = new string[] { "Match Text" };
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            Help = "<h3>Line</h3>" +
                    "<p>Remove all lines that do not contain the matching text.</p>";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Line());
        }

        override public bool Execute(string[] args)
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
