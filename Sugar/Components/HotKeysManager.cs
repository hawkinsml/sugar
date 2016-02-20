using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Sugar
{
  class HotKeysManager
  {
    // singleton object
    private static HotKeysManager instance = null;

    public static HotKeysManager Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new HotKeysManager();
        }
        return instance;
      }
    }

    private List<Hotkey> m_hotKeys;

    private HotKeysManager()
    {
      m_hotKeys = new List<Hotkey>();
    }

    public void SetHotKeys( CommandForm mainWindow )
    {
        ReleaseHotKeys();
        Hotkey hotKey = new Hotkey();
        hotKey.KeyCode = Keys.Space;
        hotKey.Control = true;
        hotKey.Pressed += delegate { mainWindow.HotKeyPressed(); };
        if (!hotKey.GetCanRegister(mainWindow))
        {
            Console.WriteLine("Whoops, looks like attempts to register will fail or throw an exception, show an error/visual user feedback");
        }
        else
        {
            hotKey.Register(mainWindow);
        }
        m_hotKeys.Add(hotKey);

        Hotkey hotKey2 = new Hotkey();
        hotKey2.KeyCode = Keys.Space;
        hotKey2.Control = true;
        hotKey2.Alt = true;
        hotKey2.Pressed += delegate { mainWindow.AutoHotKeyPressed(); };
        if (!hotKey2.GetCanRegister(mainWindow))
        {
            Console.WriteLine("Whoops, looks like attempts to register will fail or throw an exception, show an error/visual user feedback");
        }
        else
        {
            hotKey2.Register(mainWindow);
        }
        m_hotKeys.Add(hotKey2);
    }

    public void ReleaseHotKeys()
    {
      int count = m_hotKeys.Count;
      for (int i = 0; i < count; i++)
      {
        try
        {
          if (m_hotKeys[i].Registered)
          {
            m_hotKeys[i].Unregister();
          }
        }
        catch (Exception e)
        {
          Console.WriteLine("releaseHotKeys Exception: " + e.ToString());
        }
      }
      m_hotKeys.Clear();
    }
  }
}
