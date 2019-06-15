using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    class Move : BaseCommand
    {
        public Move()
        {
            Name = "Move";
            Help = "<h3>Move</h3><p>Moves the command prompt on the screen.</p>";
            ParamList = null;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Move());
        }

        override public bool Execute(string[] args)
        {
            EventManager.Instance.FireMoveEvent();
            return false; // don't hide command window
        }
    }
}
