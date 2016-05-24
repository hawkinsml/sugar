﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CSScriptLibrary;
using Microsoft.Win32;
using Sugar.Components;
using Sugar.Components.Commands;
using Sugar.Helpers;
using Sugar.Views;

namespace Sugar
{
    public partial class CommandForm : Form
    {
        ClipboardViewForm clipboardPreview = new ClipboardViewForm();
        CommandManager cmdHandler = new CommandManager();
        WebForm webForm = new WebForm();

        //SuggestedForm suggestedList = new SuggestedForm();

        List<ICommand> suggestedList = null;
        int suggestIndex = 0;

        // The path to the key where Windows looks for startup applications
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        bool autoHide = true;
        string autoCommand = "";
        string suggestedCommand = "";

        [DllImport("User32.dll")]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("User32.dll")]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool LockWorkStation();

        private bool settingClipboardViewer = true;
        private IntPtr _nextClipboardViewer;		// The next clipboard in the Windows clipboard chain

        public String CommandText
        {
            get { return commandTextBox.Text;  }
            set { 
                    commandTextBox.Text = value;
                    commandTextBox.SelectionStart = commandTextBox.Text.Length;
                }
        }


        private void SetForStartUp()
        {
            bool set = false;
            // Check to see the current state (running at startup or not)
            if (rkApp.GetValue(Application.ProductName) == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                set = false;
            }
            else
            {
                // The value exists, the application is set to run at startup
                set = true;
            }

            if (!set)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue(Application.ProductName, Application.ExecutablePath);
            }
            else
            {
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue(Application.ProductName, false);
            }
        }
    

        /// <summary>
        /// Handles the clipboard calls from the operating system and forwards them
        /// to our methods when appropriate.
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0308: // WM_DRAWCLIPBOARD
                    {
                        if (!settingClipboardViewer)
                        {
                            OnClipboardChanged();
                        }
                        SendMessage(_nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        break;
                    }
                case 0x030D: // WM_CHANGECBCHAIN
                    {
                        if (m.WParam == _nextClipboardViewer)
                        {
                            _nextClipboardViewer = m.LParam;
                        }
                        else
                        {
                            SendMessage(_nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        }
                        break;
                    }
                case 0x112: // WM_SYSCOMMAND
                    {
                        if (m.WParam == (IntPtr)0xF020) //SC_MINIMIZE
                        {
                            HideCommandWindow();
                            this.notifyIcon.Visible = true;
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    }
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        public static bool SetStyle(Control c, ControlStyles Style, bool value)
        {
            bool retval = false;
            Type typeTB = typeof(Control);
            System.Reflection.MethodInfo misSetStyle = typeTB.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (misSetStyle != null && c != null) { misSetStyle.Invoke(c, new object[] { Style, value }); retval = true; }
            return retval;
        }

        public CommandForm()
        {
            InitializeComponent();
            SetForStartUp();

            CommandManager.InitCommands();

            EventManager.Instance.HideEvent += Instance_hideEvent;
            EventManager.Instance.ShowEvent += Instance_ShowEvent;
            EventManager.Instance.MoveEvent += Instance_MoveEvent;
            EventManager.Instance.SettingsChangedEvent += Instance_SettingsChangedEvent;
            notifyIcon.Text = Application.ProductName + " - MLH Software";
            //this.BackColor = Color.White;
            //this.TransparencyKey = Color.White;
        }

        void Instance_SettingsChangedEvent(object sender, EventArgs e)
        {
            CommandManager.InitCommands();
        }

        void Instance_MoveEvent(object sender, EventArgs e)
        {
            int height = SystemInformation.VirtualScreen.Height;
            int y = this.Location.Y;
            if (y == 0)
            {
                y = (int)((height - this.Height) / 2);
            }
            else
            {
                y = 0;
            }
            this.Location = new Point(this.Location.X, y);
        }

        void Instance_ShowEvent(object sender, EventArgs e)
        {
            autoHide = clipboardPreview.ToggleForm();
        }

        private void OnClipboardChanged()
        {
            clipboardPreview.UpdateDisplay();
        }

        void Instance_hideEvent(object sender, EventArgs e)
        {
            CommandText = "";
            if (autoHide)
            {
                HideCommandWindow();
            }
        }

        private void HideCommandWindow()
        {
            clipboardPreview.HideForm();
            Hide();
        }

        private void ShowCommandWindow()
        {
            Visible = true;
            TopMost = true;
            TopLevel = true;

            if (!autoHide)
            {
                clipboardPreview.ShowForm();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cef.Initialize();
            this.TopMost = true;
            this.Visible = false;
            this.WindowState = FormWindowState.Normal;

            HotKeysManager.Instance.SetHotKeys(this);
            // Sign up for clipboard change notifications from the operating system.
            _nextClipboardViewer = SetClipboardViewer(Handle);
            settingClipboardViewer = false;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
            // Remove ourselves from OS clipboard notifications
            ChangeClipboardChain(Handle, _nextClipboardViewer);
            HotKeysManager.Instance.ReleaseHotKeys();
        }

        private void ShowBalloonTip(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                text = "<empty>";
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine(text);
            sb.AppendLine("");
            notifyIcon.BalloonTipText = sb.ToString();
            this.notifyIcon.BalloonTipTitle = "Clipboard";
            //this.notifyIcon.Icon = new Icon("icon.ico");
            this.notifyIcon.Visible = true;
            this.notifyIcon.ShowBalloonTip(3);
        }


        internal void HotKeyPressed()
        {
            ShowCommandWindow();

        }

        internal void AutoHotKeyPressed()
        {
            if (!string.IsNullOrWhiteSpace(autoCommand))
            {
                // Use SendKeys to Paste
                //SendKeys.Send("^C");

                CommandText = autoCommand;
                ProcessCommand();

                // Use SendKeys to Paste
                SendKeys.Send("^V");
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowCommandWindow();
        }

        private void CommandForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                // 'Steal' the focus.
                this.Activate();
                CommandText = "";
                commandTextBox.Focus();
            }
            Console.WriteLine("CommandForm_VisibleChanged");
        }

        private void ProcessCommand()
        {
            List<string> command = new List<string>();
            List<string> tmp = CommandText.Split('▶').ToList<string>();
            foreach (var item in tmp)
            {
                command.Add(item.Trim());
            }

            bool foundCommand = CommandManager.ExecuteCommand(command.FirstOrDefault(), command.ToArray());
            if (!foundCommand)
            {
                CommandManager.ExecuteCommand(suggestedCommand, command.ToArray());
            }
            commandTextBox.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                if (CommandText.Length < suggestedCommand.Length)
                {
                    CommandText = suggestedCommand;
                }
                else
                {
                    CommandText = CommandText + " ▶ ";
                }
            }
            else if (keyData == Keys.PageDown)
            {
                webForm.Show();
            }
            else if (keyData == Keys.PageUp)
            {
                webForm.Hide();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CommandForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                HideCommandWindow();
            }
        }

        private void commandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessCommand();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                CommandText = "";
            }
            else if (e.KeyCode == Keys.Escape)
            {
                HideCommandWindow();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (suggestIndex < suggestedList.Count - 1)
                {
                    suggestIndex++;
                    suggestedCommand = suggestedList[suggestIndex].Name;
                    suggestedLabel.Text = suggestedCommand;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (suggestIndex > 0)
                {
                    suggestIndex--;
                    suggestedCommand = suggestedList[suggestIndex].Name;
                    suggestedLabel.Text = suggestedCommand;
                }
            }
        }

        private void commandTextBox_TextChanged(object sender, EventArgs e)
        {
            string[] command = CommandText.Split('▶');
            if (command.Length > 0)
            {
                suggestIndex = 0;
                suggestedList = CommandManager.Search(command[0].Trim());
                if (suggestedList.Count > 0)
                {
                    int paramIndex = command.Length - 2;

                    if (paramIndex >= 0 && suggestedList[0].ParamList != null && suggestedList[0].ParamList.Length > paramIndex)
                    {
                        suggestedCommand = suggestedList[0].ParamList[paramIndex];
                    }
                    else
                    {
                        suggestedCommand = suggestedList[0].Name;
                    }
                    //suggestedList.SetSuggestions(list);
                    //suggestedList.Location = new Point(this.Location.X + 17, this.Location.Y + 100);
                    //suggestedList.Show();
                }
                else
                {
                    suggestedCommand = "";
                    //suggestedList.Hide();
                }
            }
            suggestedLabel.Text = suggestedCommand;
        }
    }
}
