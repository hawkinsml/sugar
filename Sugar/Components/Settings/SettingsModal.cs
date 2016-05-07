using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Settings
{
    class SettingsModal
    {
        public SettingsModal()
        {
            Commands = new List<CommandModel>();
            Executables = new List<ExecutableModel>();
        }

        public List<CommandModel> Commands { get; set; }
        public List<ExecutableModel> Executables { get; set; }
    }


    class CommandModel
    {
        public string Name { get; set; }
        public string[] ParamList { get; set; }
        public string Help { get; set; }
        public string SourceCode { get; set; }
    }

    class ExecutableModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
