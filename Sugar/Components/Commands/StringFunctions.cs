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
        static public void Init()
        {
            CommandManager.AddCommandHandler(new MakeUpper());
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
            get { return "<h3>Upper</h3><p>Converts the contents of the clipboard to all uppercase.</p>"; }
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
        static public void Init()
        {
            CommandManager.AddCommandHandler(new MakeLower());
        }

        public string Name
        {
            get { return "Lower"; }
        }

        public string Help
        {
            get { return "<h3>Lower</h3><p>Converts the contents of the clipboard to all lowercase.</p>"; }
        }

        public string[] ParamList
        {
            get { return null; }
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
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Trim());
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
            get { return "<h3>Trim</h3><p>Trims spaces from the begining and ending of each line on the clipboard.</p>"; }
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
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Strip());
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
            get { return "<h3>Strip</h3><p>Rempove carriage returns from text.</p>"; }
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
