using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Components.Settings;

namespace Sugar.Components.Commands
{
    class Clear : BaseCommand
    {
        public Clear()
        {
            Name = "Clear";
            ParamList = null;
            Help = "<h3>Clear</h3><p>Clears the contents of the clipbaord</p>";
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Clear());
        }

        override public bool Execute(string[] args)
        {
            Clipboard.Clear();
            return true; // hide command window
        }

    }
}
