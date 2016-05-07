using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Dump : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Dump());
        }

        public string Name
        {
            get { return "Dump"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return "<h3>Dump</h3><p>Displays a test file with the full details and contents of the clipboard.</p>"; }
        }

        public bool Execute(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            var data = Clipboard.GetDataObject();
            string[] formats = data.GetFormats(true);
            foreach (var format in formats)
            {
                Console.WriteLine(format);
                sb.AppendLine(format);
                sb.AppendLine();
                object text = data.GetData(format);
                if (text != null)
                {
                    sb.AppendLine(text.ToString());
                    sb.AppendLine();
                    sb.AppendLine();
                }
            }

            string fileName = sb.ToString().SaveAsTextFile();

            ProcessStartInfo pi = new ProcessStartInfo(fileName);
            pi.Arguments = Path.GetFileName(fileName);
            pi.UseShellExecute = true;
            pi.WorkingDirectory = Path.GetDirectoryName(fileName);
            pi.FileName = fileName;
            pi.Verb = "EDIT";

            System.Diagnostics.Process.Start(pi);

            return true; // hide command window
        }

    }
}
