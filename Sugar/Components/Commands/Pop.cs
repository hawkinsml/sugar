﻿using System;
using System.Collections;
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
    class Pop : ICommand
    {
        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Pop(commandManager));
        }

        ICommandManager commandManager;

        public Pop(ICommandManager commandManager)
        {
            this.commandManager = commandManager;
        }

        public string Name
        {
            get { return "Pop"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return "<h3>Pop</h3><p>Put item at the top of the stack back onto clipboard Use Push command place the contents of the clipboard onto a stack.</p>"; }
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

        public bool Execute(string[] args)
        {
            return commandManager.ExecuteCommand("Push", "pop");
        }

    }
}
