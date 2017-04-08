﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Save : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Save());
        }

        public string Name
        {
            get { return "Save"; }
        }

        public string[] ParamList
        {
            get { return new string[] { "File Name" }; } 
        }

        public string[] ParamDescriptionList
        {
            get { return new string[] { "File name to use when creating Excel file. If file name is not provide, a temp file name will be created." }; }
        }

        public bool[] ParamRequired
        {
            get { return new bool[] {false}; }
        }

        public string Description
        {
            get { return "Saves the contents of the clipboard to a file"; }
        }

        public string Help
        {
            get { return null; }
        }

        public bool Execute(string[] args)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                fileName = args[1].Trim();
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullFileName = string.Format( "{0}\\{1}", path, fileName);

            if (Clipboard.ContainsImage())
            {
                Image clipboardImage = Clipboard.GetImage();
                fullFileName = Path.ChangeExtension(fullFileName, ".bmp");
                clipboardImage.Save(fullFileName);
            }
            else if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();
                fullFileName = Path.ChangeExtension(fullFileName, ".txt");
                text.SaveAsTextFile(fullFileName);
            }
            System.Diagnostics.Process.Start(path);


            return true; // hide command window
        }

    }
}
