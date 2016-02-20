using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    class Hide : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Hide());
        }

        public string Name
        {
            get { return "Hide"; }
        }

        public void Execute(string[] args)
        {
        }

    }
}
