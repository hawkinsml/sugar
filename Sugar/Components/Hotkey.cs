using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace Sugar
{
    public class Hotkey : IMessageFilter
    {
        #region Interop

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int UnregisterHotKey(IntPtr hWnd, int id);

        private const uint WM_HOTKEY = 0x312;

        private const uint MOD_ALT = 0x1;
        private const uint MOD_CONTROL = 0x2;
        private const uint MOD_SHIFT = 0x4;
        private const uint MOD_WIN = 0x8;

        private const uint ERROR_HOTKEY_ALREADY_REGISTERED = 1409;

        #endregion

        private static int currentID;
        private const int maximumID = 0xBFFF;

        private Keys KeyCode = Keys.None;
        private bool Shift = false;
        private bool Control = false;
        private bool Alt = false;
        private bool Windows = false;

        private int hotKeyid;


        public event HandledEventHandler Pressed;

        public Hotkey(Keys keyCode, bool shift, bool control, bool alt, bool windows)
        {
            // Assign properties
            KeyCode = keyCode;
            Shift = shift;
            Control = control;
            Alt = alt;
            Windows = windows;

            // Register us as a message filter
            Application.AddMessageFilter(this);
        }

        ~Hotkey()
        {
            Unregister();
        }


        public bool Register()
        {
            bool retVal = false;

            // We can't register an empty hotkey
            if (KeyCode != Keys.None)
            {
                // Get an ID for the hotkey and increase current ID
                hotKeyid = Hotkey.currentID;
                Hotkey.currentID = Hotkey.currentID + 1 % Hotkey.maximumID;

                // Translate modifier keys into unmanaged version
                uint modifiers = (this.Alt ? Hotkey.MOD_ALT : 0) | (this.Control ? Hotkey.MOD_CONTROL : 0) | (this.Shift ? Hotkey.MOD_SHIFT : 0) | (this.Windows ? Hotkey.MOD_WIN : 0);

                if (RegisterHotKey((IntPtr)null, hotKeyid, modifiers, KeyCode) != 0)
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public bool Unregister()
        {
            bool retVal = true;
            if (Hotkey.UnregisterHotKey((IntPtr)null, hotKeyid) == 0)
            {
                retVal = false;
            }
            return retVal;
        }


        public bool PreFilterMessage(ref Message message)
        {
            bool retVal = false;

            // Process WM_HOTKEY messages with the ID we registerd
            if (message.Msg == Hotkey.WM_HOTKEY && (message.WParam.ToInt32() == hotKeyid))
            {
                // Fire the event and pass on the event if our handlers didn't handle it
                retVal = OnPressed();
            }

            return retVal;
        }

        private bool OnPressed()
        {
            // Fire the event if we can
            HandledEventArgs handledEventArgs = new HandledEventArgs(false);
            if (Pressed != null)
            {
                Pressed(this, handledEventArgs);
            }

            // Return whether we handled the event or not
            return handledEventArgs.Handled;
        }

        public override string ToString()
        {
            // We can be empty
            if (KeyCode != Keys.None)
            {
                return "(none)";
            }

            // Build key name
            string keyName = Enum.GetName(typeof(Keys), KeyCode); ;
            switch (KeyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    // Strip the first character
                    keyName = keyName.Substring(1);
                    break;
                default:
                    // Leave everything alone
                    break;
            }

            // Build modifiers
            string modifiers = "";
            if (Shift)
            {
                modifiers += "Shift+";
            }
            if (Control)
            {
                modifiers += "Control+";
            }
            if (Alt)
            {
                modifiers += "Alt+";
            }
            if (Windows)
            {
                modifiers += "Windows+";
            }

            // Return result
            return modifiers + keyName;
        }
    }
}
