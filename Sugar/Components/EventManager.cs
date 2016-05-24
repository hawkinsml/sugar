using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components
{
    public delegate void HideEventHandler(Object sender, EventArgs e);
    public delegate void ShowEventHandler(Object sender, EventArgs e);
    public delegate void MoveEventHandler(Object sender, EventArgs e);
    public delegate void SettingsChangedHandler(Object sender, EventArgs e);

    class EventManager
    {
        public event HideEventHandler HideEvent;
        public event ShowEventHandler ShowEvent;
        public event MoveEventHandler MoveEvent;
        public event SettingsChangedHandler SettingsChangedEvent;

        static private EventManager eventManager = new EventManager();


        static public EventManager Instance { get {return eventManager; }}


        public void FireHideEvent()
        {
            HideEventHandler handler = HideEvent;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        public void FireShowEvent()
        {
            ShowEventHandler handler = ShowEvent;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        public void FireMoveEvent()
        {
            MoveEventHandler handler = MoveEvent;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        public void FireSettingsChangedEvent()
        {
            SettingsChangedHandler handler = SettingsChangedEvent;
            if (handler != null)
            {
                handler(this, null);
            }
        }
    }
}
