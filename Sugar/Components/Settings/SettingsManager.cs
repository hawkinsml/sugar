using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sugar.Helpers;
namespace Sugar.Components.Settings
{
    class SettingsManager
    {


        public static void test()
        {
            SettingsModal data = new SettingsModal();

            CommandModel cmd1 = new CommandModel();
            cmd1.Name = "Test";
            cmd1.ParamList = new string[] { "File Name", "Password" };
            cmd1.Help = "<h3>Test</h3><p>Creates an Excel file with the contents of the clipboard. Has two params.</p><dl><dt>File Name <span class='label label-default'>optional</span></dt><dd>File name to use when creating Excel file. If file name is not provide, a temp file name will be created.</dd><dt>Password <span class='label label-default'>optional</span></dt>  <dd>Encrypt file using this password.</dd></dl>";

            cmd1.SourceCode =
                @"
                using System;
                using System.Collections.Generic;
                using System.Linq;
                using System.Text;
                using System.Threading.Tasks;
                using System.Windows.Forms;
                public class Script
                {
                    public bool Execute(string[] args)
                    {
                        string text = Clipboard.GetText();
                        if ( !string.IsNullOrWhiteSpace( text ) )
                        {
                            Clipboard.SetText(text.ToUpper(), TextDataFormat.Text);
                        }
                        return true;
                    }
                }";

            data.Commands.Add(cmd1);
            SaveSettings(data);
        }

        public static bool SaveSettings(SettingsModal data)
        {
            bool retVal = false;

            string json = JsonConvert.SerializeObject(data, new IsoDateTimeConverter());
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullFileName = string.Format( "{0}\\{1}", path, "sugar.json");

            json.SaveAsTextFile(fullFileName);

            EventManager.Instance.FireSettingsChangedEvent();

            return retVal;
        }

        public static SettingsModal LoadSettings()
        {
            SettingsModal retVal = null;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullFileName = string.Format("{0}\\{1}", path, "sugar.json");

            try
            {   
                String json = "";
                using (StreamReader sr = new StreamReader(fullFileName))
                {
                    json = sr.ReadToEnd();
                }

                retVal = JsonConvert.DeserializeObject<SettingsModal>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return retVal;
        }

        public void CreateFileWatcher(string fileName)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path.GetDirectoryName(fileName);
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            // Only watch text files.
            watcher.Filter = fileName;

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }
    }
}
