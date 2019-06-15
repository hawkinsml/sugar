using System;
using System.Collections.Generic;
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
    class ScriptCmd : BaseCommand
    {
        static public void Init(ICommandManager commandManager)
        {
            SettingsModal data = SettingsManager.LoadSettings();

            if (data != null)
            {
                foreach (CommandModel cmd in data.Commands)
                {
                    commandManager.AddCommandHandler(new ScriptCmd(cmd));
                }
            }
        }


        private CommandModel Cmd;

        public ScriptCmd(CommandModel cmd)
        {
            this.Cmd = cmd;
            Name = Cmd.Name;
            ParamList = Cmd.ParamList;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
            Help = Cmd.Help;
        }

        override public bool Execute(string[] args)
        {
            bool retVal = true;
            try
            {
                CSScript.Evaluator.ReferenceAssembliesFromCode(Cmd.SourceCode);
                //CSScript.Evaluator.ReferenceAssembly(Assembly.GetAssembly(typeof(System.Windows.Forms.Clipboard)));
                dynamic script = CSScript.Evaluator.LoadCode(Cmd.SourceCode);

                string clipboardText = Clipboard.GetText();
                string resultText = clipboardText;
                retVal = script.Execute(args, clipboardText, out resultText);
                if (!string.IsNullOrWhiteSpace(resultText))
                {
                    Clipboard.SetText(resultText, TextDataFormat.Text);
                }
                else
                {
                    Clipboard.Clear();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return retVal; 
        }

    }
}
