using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if ( args.Length > 0 && !string.IsNullOrWhiteSpace( args[0] ) )
            {
                text = args[0];
            }

            if ( !string.IsNullOrWhiteSpace( text ) )
            {
                Clipboard.SetText(text.ToUpper(), TextDataFormat.Text);
            }
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

        public void Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                text = args[0];
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                Clipboard.SetText(text.ToLower(), TextDataFormat.Text);
            }
        }
    }
}
