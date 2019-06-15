using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sugar.Components.Commands
{
    class Quit : BaseCommand
    {
        public Quit()
        {
            Name = "Quit";
            Help = "<h3>Quit</h3><p>Quits the application</p>";
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            ParamList = null;
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Quit());
        }

        override public bool Execute(string[] args)
        {
            Application.Exit();
            return true; // don't hide command window
        }

    }
}
