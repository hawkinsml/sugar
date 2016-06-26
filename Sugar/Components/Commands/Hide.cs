using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    class Hide : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Hide());
        }

        public string Name
        {
            get { return "Hide"; }
        }

        public string Help
        {
            get { return "<h3>Hide</h3><p>Hides the command prompt. Same as pressing <kbd>esc</kbd> key.</p>"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public bool Execute(string[] args)
        {
            EventManager.Instance.FireHideEvent();
            return true; // hide command window
        }

    }
}
