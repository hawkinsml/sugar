using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    class Show : BaseCommand
    {
        public Show()
        {
            Name = "Show";
            Help = "<h3>Show</h3><p>Display the contents of the clipboard</p>";
            ParamList = null;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Show());
        }

        override public bool Execute(string[] args)
        {
            EventManager.Instance.FireShowEvent();
            return true; // hide command window
        }

    }
}
