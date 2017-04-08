using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    class Move : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Move());
        }

        public string Name
        {
            get { return "Move"; }
        }

        public string Help
        {
            get { return "<h3>Move</h3><p>Moves the command prompt on the screen.</p>"; }
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
            get { return null; }
        }

        public bool Execute(string[] args)
        {
            EventManager.Instance.FireMoveEvent();
            return false; // don't hide command window
        }

    }
}
