using System;
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
using CSScriptLibrary;
using Sugar.Components.Commands;
using Sugar.Helpers;

namespace Sugar
{
    public partial class CommandForm : Form
    {
        ClipboardViewForm clipboardPreview = new ClipboardViewForm();
        CommandManager cmdHandler = new CommandManager();

        bool showCLipboard = true;
        string autoCommand = "";

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

        public String ClipboardText
        {
            get
            {
                string text = "";
                if (System.Windows.Forms.Clipboard.GetDataObject().GetDataPresent(DataFormats.UnicodeText))
                {
                    text = System.Windows.Forms.Clipboard.GetDataObject().GetData(DataFormats.UnicodeText).ToString();
                }
                return text;
            }
            set
            {
                try
                {
                    System.Windows.Forms.Clipboard.SetDataObject(value, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Writting to Clipboard:" + Environment.NewLine + ex.ToString());
                }
            }
        }

        public String CommandText
        {
            get { return commandTextBox.Text;  }
            set { commandTextBox.Text = value;  }
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
                            this.Hide();
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

            notifyIcon.Text = Application.ProductName + " - MLH Software";
            //this.BackColor = Color.White;
            //this.TransparencyKey = Color.White;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.WindowState = FormWindowState.Normal;

            HotKeysManager.Instance.SetHotKeys(this);
            // Sign up for clipboard change notifications from the operating system.
            _nextClipboardViewer = SetClipboardViewer(Handle);
            settingClipboardViewer = false;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Remove ourselves from OS clipboard notifications
            ChangeClipboardChain(Handle, _nextClipboardViewer);
            HotKeysManager.Instance.ReleaseHotKeys();
        }


        private void OnClipboardChanged()
        {
            if (showCLipboard == true)
            {
                ShowClipboard();
            }
        }

        private void ShowClipboard()
        {
            if (Clipboard.ContainsText())
            {
                ShowBalloonTip(Clipboard.GetText());
            }
            else
            {
                clipboardPreview.ShowForm();
            }
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
            Visible = true;
            TopMost = true;
            TopLevel = true;
            //OnClipboardChanged();
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
            this.Show();
        }

        private void CommandForm_VisibleChanged(object sender, EventArgs e)
        {
            CommandText = "";
            commandTextBox.Focus();
            Console.WriteLine("CommandForm_VisibleChanged");
        }




        private void ProcessCommand()
        {
            bool handled = CommandManager.ExecuteCommand(CommandText);
            if (handled)
            {
                CommandText = "";
                this.Hide();
            }
            commandTextBox.Focus();
        }

        private void CommandForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
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
            else if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void commandTextBox_TextChanged(object sender, EventArgs e)
        {
            List<string> list = CommandManager.Search(CommandText);

            if (list.Count > 0)
            {
                tbShadow.Text = list[0];
            }
            else
            {
                tbShadow.Text = "";
            }
        }

    }
}
