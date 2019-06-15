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
    class SettingsCmd : BaseCommand
    {
        public SettingsCmd()
        {
            Name = "Settings";
            ParamList = null;
            Help = "<h3>Settings</h3><p>Access the settings screen.</p>";
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new SettingsCmd());
        }

        override public bool Execute(string[] args)
        {
            EventManager.Instance.FireHideEvent();
            SettingsForm setting = new SettingsForm();
            setting.ShowDialog();

            return true; // hide command window
        }

    }
}
