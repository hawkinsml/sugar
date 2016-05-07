using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    class Show : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Show());
        }

        public string Name
        {
            get { return "Show"; }
        }

        public string Help
        {
            get { return "<h3>Show</h3><p>Display the contents of the clipboard</p>"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public bool Execute(string[] args)
        {
            EventManager.Instance.FireShowEvent();
            return true; // hide command window
        }

    }
}
