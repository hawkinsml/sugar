using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    public class MakeUpper : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new MakeUpper());
        }

        public string Name
        {
            get { return "Upper"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return null; }
        }

        public string[] ParamDescriptionList
        {
            get { return null; }
        }

        public bool[] ParamRequired
        {
            get { return null; }
        }

        public string Description
        {
            get { return "Converts the contents of the clipboard to all uppercase."; }
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if ( !string.IsNullOrWhiteSpace( text ) )
            {
                Clipboard.SetText(text.ToUpper(), TextDataFormat.Text);
            }
            return true; // hide command window
        }
    }

    public class MakeLower : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new MakeLower());
        }

        public string Name
        {
            get { return "Lower"; }
        }

        public string Help
        {
            get { return null; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string[] ParamDescriptionList
        {
            get { return null; }
        }

        public bool[] ParamRequired
        {
            get { return null; }
        }

        public string Description
        {
            get { return "Converts the contents of the clipboard to all lowercase."; }
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (!string.IsNullOrWhiteSpace(text))
            {
                Clipboard.SetText(text.ToLower(), TextDataFormat.Text);
            }
            return true; // hide command window
        }
    }

    public class Trim : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Trim());
        }

        public string Name
        {
            get { return "Trim"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return null; }
        }

        public string[] ParamDescriptionList
        {
            get { return null; }
        }

        public bool[] ParamRequired
        {
            get { return null; }
        }

        public string Description
        {
            get { return "Trims spaces from the begining and ending of each line on the clipboard."; }
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
                    sb.AppendLine(line.Trim());
                }

                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            }
            return true; // hide command window
        }
    }


    public class Strip : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Strip());
        }

        public string Name
        {
            get { return "Strip"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return null; }
        }

        public string[] ParamDescriptionList
        {
            get { return null; }
        }

        public bool[] ParamRequired
        {
            get { return null; }
        }

        public string Description
        {
            get { return "Rempove carriage returns from text."; }
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
                    sb.Append(line.Trim());
                    sb.Append(" ");
                }

                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            }
            return true; // hide command window
        }
    }
    
}
