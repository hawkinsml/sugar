﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Edit : BaseCommand
    {
        public Edit()
        {
            Name = "Edit";
            ParamList = null;
            Help = "<h3>Edit</h3><p>Saves the contents of the clipboard as a temp file (.bmp for images and .txt for text) and opens the file with the defualt editor for the content type.</p>";
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Edit());
        }

        override public bool Execute(string[] args)
        {
            if (Clipboard.ContainsImage())
            {
                Image clipboardImage = Clipboard.GetImage();
                string fileName = Path.GetTempFileName();
                fileName = Path.ChangeExtension(fileName, ".bmp");
                clipboardImage.Save(fileName);

                ProcessStartInfo pi = new ProcessStartInfo(fileName);
                pi.Arguments = Path.GetFileName(fileName);
                pi.UseShellExecute = true;
                pi.WorkingDirectory = Path.GetDirectoryName(fileName);
                pi.FileName = fileName;
                pi.Verb = "EDIT";

                System.Diagnostics.Process.Start(pi);
            }
            else if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();
                string fileName = text.SaveAsTextFile();

                ProcessStartInfo pi = new ProcessStartInfo(fileName);
                pi.Arguments = Path.GetFileName(fileName);
                pi.UseShellExecute = true;
                pi.WorkingDirectory = Path.GetDirectoryName(fileName);
                pi.FileName = fileName;
                pi.Verb = "EDIT";

                System.Diagnostics.Process.Start(pi);
            }

            return true; // hide command window
        }

    }
}
