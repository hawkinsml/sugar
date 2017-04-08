using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSScriptLibrary;
using Sugar.Components.Settings;

namespace Sugar.Components.Commands
{
    class RunCmd : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            SettingsModal data = SettingsManager.LoadSettings();
            if (data != null)
            {
                foreach (ExecutableModel exe in data.Executables)
                {
                    commandManager.AddCommandHandler(new RunCmd(exe));
                }
            }
        }


        private ExecutableModel Exe;

        public RunCmd(ExecutableModel exe)
        {
            this.Exe = exe;
            Exe.Path = Exe.Path.Replace("%user%", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            Exe.Path = Exe.Path.Replace("%windir%", Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            Exe.Path = Exe.Path.Replace("%Dropbox%", Environment.GetEnvironmentVariable("Dropbox", EnvironmentVariableTarget.User));
        }

        public string Name
        {
            get { return Exe.Name; }
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

        public string Help
        {
            get { return ""; }
        }

        public bool Execute(string[] args)
        {
            bool retVal = true;
            try
            {
                Process app = new Process();
                if ( Exe.Path.Contains('{'))
                {
                    app.StartInfo.FileName = string.Format(Exe.Path, args);
                }
                else
                {
                    app.StartInfo.FileName = Exe.Path;
                }

                if ( Exe.Arguments.Contains('{'))
                {
                    app.StartInfo.Arguments = string.Format(Exe.Arguments, args);
                }
                else
                {
                    app.StartInfo.Arguments = Exe.Arguments;
                }                
                app.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return retVal; 
        }

    }
}
