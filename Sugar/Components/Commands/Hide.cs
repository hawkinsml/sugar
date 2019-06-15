using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    class Hide : BaseCommand
    {
        public Hide()
        {
            Name = "Hide";
            Help = "<h3>Hide</h3><p>Hides the command prompt. Same as pressing <kbd>esc</kbd> key.</p>";
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            ParamList = null;
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Hide());
        }

        override public bool Execute(string[] args)
        {
            EventManager.Instance.FireHideEvent();
            return true; // hide command window
        }

    }
}
