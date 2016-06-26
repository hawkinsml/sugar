using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Components.Settings;

namespace Sugar.Components.Commands
{
    class Clear : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Clear());
        }

        public string Name
        {
            get { return "Clear"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return "<h3>Clear</h3><p>Clears the contents of the clipbaord</p>"; }
        }

        public bool Execute(string[] args)
        {
            Clipboard.Clear();
            return true; // hide command window
        }

    }
}
