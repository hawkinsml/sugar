using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Push : BaseCommand
    {
        public Push()
        {
            Name = "Push";
            ParamList = null;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            Help = "<h3>Push</h3><p>Push the contents of the clipboard onto a stack. Use Pop command to put item at the top of the stack back onto clipboard</p>";
        }

        private Stack<string> textStack = new Stack<string>();

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Push());
        }

        override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            bool push = true;
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                push = false;
            }

            if (push)
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    textStack.Push(text);
                    Clipboard.Clear();
                }
            }
            else
            {
                Clipboard.Clear();
                if (textStack.Count > 0)
                {
                    text = textStack.Pop();
                    try
                    {
                        Clipboard.SetText(text, TextDataFormat.Text);
                    }
                    catch (Exception) { }
                }
            }
            return true; // hide command window
        }

    }
}
