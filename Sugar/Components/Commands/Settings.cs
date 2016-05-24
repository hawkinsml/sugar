using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;
using Sugar.Views;

namespace Sugar.Components.Commands
{
    class SettingsCmd : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new SettingsCmd());
        }

        public string Name
        {
            get { return "Settings"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return "<h3>Settings</h3><p>Access the settings screen.</p>"; }
        }

        public bool Execute(string[] args)
        {
            EventManager.Instance.FireHideEvent();
            SettingsForm setting = new SettingsForm();
            setting.ShowDialog();

            return true; // hide command window
        }

    }
}
