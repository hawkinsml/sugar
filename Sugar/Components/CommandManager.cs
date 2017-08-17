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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sugar.Components.Commands;
using Sugar.Components.Settings;
using Sugar.Helpers;

namespace Sugar
{
    public class CommandManager : ICommandManager
    {
        static public List<ICommand> CommandHandlers = new List<ICommand>();

        public void InitCommands()
        {
            CommandHandlers.Clear();
            LoadCommands();
            CommandHandlers = CommandHandlers.OrderBy(o => o.Name).ToList();
        }

        private void LoadCommands()
        {
            LoadPluginAssemblies();
            var asseblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            //IEnumerable<Type> commandClasses = asseblies.SelectMany(a => a.GetTypes()).Where(o => o.GetInterfaces().Contains(typeof(Sugar.ICommand)));
            IEnumerable<Type> commandClasses = asseblies.SelectMany(a => a.GetTypes()).Where(t => typeof(Sugar.ICommand).IsAssignableFrom(t));
            InitCommands(commandClasses);
        }

        private void InitCommands(IEnumerable<Type> commandClasses)
        {
            object[] parameters = new object[1];
            parameters[0] = this; // pass ICommandManager to Init methods
            foreach (Type taskHandler in commandClasses)
            {
                MethodInfo method = taskHandler.GetMethod("Init", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                if (method != null)
                {
                    method.Invoke(null, parameters);
                }
            }
        }

        private void LoadPluginAssemblies()
        {
            string pluginPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            if ( !Directory.Exists(pluginPath) )
            {
                Directory.CreateDirectory(pluginPath);
            }


            foreach (string dll in Directory.GetFiles(pluginPath, "*.dll", SearchOption.AllDirectories))
            {
                try
                {                    
                    Assembly loadedAssembly = Assembly.LoadFile(dll);
                }
                catch (FileLoadException loadEx)
                { } // The Assembly has already been loaded.
                catch (BadImageFormatException imgEx)
                { } // If a BadImageFormatException exception is thrown, the file is not an assembly.

            }
        }

        public void AddCommandHandler(ICommand handler)
        {
            CommandHandlers.Add(handler);
        }

        public bool ExecuteCommand(string commandName, string param)
        {
            List<string> args = new List<string>();
            args.Add(commandName);
            args.Add(param);
            return ExecuteCommand(commandName, args.ToArray());
        }

        public bool ExecuteCommand(string commandName, string[] args)
        {
            bool foundCommand = false;
            foreach (var handler in CommandHandlers)
            {
                if (string.Compare(handler.Name, commandName, true) == 0)
                {
                    foundCommand = true;
                    if (handler.Execute(args))
                    {
                        EventManager.Instance.FireHideEvent();
                    }
                    break;
                }
            }
            return foundCommand;
        }

        public List<ICommand> Search(string text)
        {
            List<ICommand> retVal = new List<ICommand>();

            if (string.IsNullOrWhiteSpace(text) == false)
            {
                foreach (var handler in CommandHandlers)
                {
                    if (handler.Name.ToLower().StartsWith(text.ToLower()) == true)
                    {
                        retVal.Add(handler);
                    }
                }
            }
            return retVal;
        }

        public CommandHistoryModel SaveCommandHistory(string command)
        {
            CommandHistoryModel history = LoadCommandHistory();

            if (history == null)
            {
                history = new CommandHistoryModel();
                history.CommandHistory = new List<string>();
            }

            if (!string.IsNullOrEmpty(command) && !history.CommandHistory.Contains(command))
            {
                history.CommandHistory.Add(command);
            }

            if (history.CommandHistory.Count >= 10)
            {
                history.CommandHistory = history.CommandHistory.Skip(Math.Max(0, history.CommandHistory.Count() - 10)).ToList();
            }

            SaveCommandHistory(history);
            return history;
        }

        private void SaveCommandHistory(CommandHistoryModel data)
        {
            string json = JsonConvert.SerializeObject(data, new IsoDateTimeConverter());
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullFileName = string.Format( "{0}\\{1}", path, "sugarHistory.json");

            json.SaveAsTextFile(fullFileName);
        }

        public CommandHistoryModel LoadCommandHistory()
        {
            CommandHistoryModel retVal = null;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullFileName = string.Format("{0}\\{1}", path, "sugarHistory.json");

            try
            {   
                String json = "";
                using (StreamReader sr = new StreamReader(fullFileName))
                {
                    json = sr.ReadToEnd();
                }

                retVal = JsonConvert.DeserializeObject<CommandHistoryModel>(json);
            }
            catch (Exception e)
            {
                retVal = new CommandHistoryModel();
                retVal.CommandHistory = new List<string>();

                SaveCommandHistory(retVal);
            }

            return retVal;
        }


    }
}
