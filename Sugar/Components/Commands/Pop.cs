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
    class Pop : BaseCommand
    {

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Pop(commandManager));
        }

        ICommandManager commandManager;

        public Pop(ICommandManager commandManager)
        {
            this.commandManager = commandManager;
            Name = "Pop";
            ParamList = null;
            Help = "<h3>Pop</h3><p>Put item at the top of the stack back onto clipboard Use Push command place the contents of the clipboard onto a stack.</p>";
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
        }

        override public bool Execute(string[] args)
        {
            return commandManager.ExecuteCommand("Push", "pop");
        }

    }
}
