using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sugar.Components.Commands
{
    class Quit : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Quit());
        }

        public string Name
        {
            get { return "Quit"; }
        }

        public string Help
        {
            get { return "<h3>Quit</h3><p>Quits the application</p>"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public bool Execute(string[] args)
        {
            Application.Exit();
            return true; // don't hide command window
        }

    }
}
