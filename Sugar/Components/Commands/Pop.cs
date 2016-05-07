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
    class Pop : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Pop());
        }

        public string Name
        {
            get { return "Pop"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return "<h3>Pop</h3><p>Put item at the top of the stack back onto clipboard Use Push command place the contents of the clipboard onto a stack.</p>"; }
        }

        public bool Execute(string[] args)
        {
            return CommandManager.ExecuteCommand("Push", "pop");
        }

    }
}
