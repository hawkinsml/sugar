﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Format : BaseCommand
    {
        public Format()
        {
            Name = "Format";
            ParamList = new string[] { "Format String", "Delimiter (word, tab, line)" };
            ParamDescriptionList = new string[] { "String passed as the Format text to string.Format().", "Split the lines by <b>word</b>, <b>line</b> or <b>tab</b>." };
            ParamRequired = new bool[] { true, false };
            Description = "For each line on the clipboard, splits the line by spaces into a string array and passes these array into string.Format(Format String, words).";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Format());
        }

        override public bool Execute(string[] args)
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

                StringBuilder errorText = new StringBuilder();

                foreach (var line in lines)
                {
                    try
                    {
                        if (delimiter == "word")
                        {
                            string cleanline = line.Trim();
                            string[] words = cleanline.Split(' ');
                            for (int i = 0; i < words.Length; i++)
                            {
                                words[i] = words[i].Trim();
                            }
                            sb.AppendLine(string.Format(formatText, words));
                        }
                        else if (delimiter == "tab")
                        {
                            string[] words = line.Split('\t');
                            for (int i = 0; i < words.Length; i++)
                            {
                                words[i] = words[i].Trim();
                            }
                            sb.AppendLine(string.Format(formatText, words));
                        }
                        else if (delimiter == "line")
                        {
                            sb.AppendLine(string.Format(formatText, line));
                        }
                    }
                    catch( Exception e)
                    {
                        string errorMessage = $"Format failed this line '{line}' for delimiter {delimiter} and format string {formatText} with error '{e.Message}'";
                        errorText.AppendHtmlLine(errorMessage);
                    }
                }

                if (errorText.Length > 0)
                {
                    WebPage.DisplayWebPage("Format Command Error", formatText + "<hr/>" + errorText.ToString());
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
