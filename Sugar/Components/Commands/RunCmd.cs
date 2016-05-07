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
        static public void Init()
        {
            

            SettingsModal data = SettingsManager.LoadSettings();

            if (data != null)
            {
                foreach (ExecutableModel exe in data.Executables)
                {
                    CommandManager.AddCommandHandler(new RunCmd(exe));
                }
            }
        }


        private ExecutableModel Exe;

        public RunCmd(ExecutableModel exe)
        {
            this.Exe = exe;
        }

        public string Name
        {
            get { return Exe.Name; }
        }

        public string[] ParamList
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
                app.StartInfo.FileName = Exe.Path;

                //foreach (string arg in args)
                //{
                //}
                //app.StartInfo.Arguments = "";
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
