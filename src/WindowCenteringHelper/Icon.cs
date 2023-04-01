// Decompiled with JetBrains decompiler
// Type: WindowCenteringHelper.Icon
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using KamilSzymborski.WindowCenteringHelper.Core;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace WindowCenteringHelper
{
  internal sealed class Icon : ApplicationContext
  {
    internal Icon() => this._Init();

    private void _Init()
    {
      CenteringHelper Core = new CenteringHelper();
      NotifyIcon Icon = new NotifyIcon();
      Config Cfg = (Config) null;
      try
      {
        using (FileStream fileStream = File.OpenRead(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WindowCenteringHelper.ini")))
          Cfg = (Config) new BinaryFormatter().Deserialize((Stream) fileStream);
      }
      catch
      {
        Cfg = new Config();
        Cfg.ActiveServices = Services.OnKeySequence | Services.Automatically;
        Cfg.CustomWindowWidth = false;
        Cfg.CustomWindowHeight = false;
        Cfg.WindowWidth = 88;
        Cfg.WindowHeight = 88;
        Cfg.ForceToResizeWindow = false;
        Cfg.OnlyNewWindow = true;
        Cfg.OnlyOnPrimaryScreen = false;
        Cfg.HelpMeDecide = true;
        Cfg.Key = Keys.LShiftKey.ToString();
        Cfg.Times = 3;
        Cfg.Timeout = 1050;
        Cfg.FirstRun = true;
      }
      finally
      {
        Core.ActiveServices = Cfg.ActiveServices;
        Core.CustomWindowWidth = Cfg.CustomWindowWidth;
        Core.CustomWindowHeight = Cfg.CustomWindowHeight;
        Core.WindowWidth = (uint) Cfg.WindowWidth;
        Core.WindowHeight = (uint) Cfg.WindowHeight;
        Core.ForceToResizeWindow = Cfg.ForceToResizeWindow;
        Core.OnlyNewWindow = Cfg.OnlyNewWindow;
        Core.HelpMeDecide = Cfg.HelpMeDecide;
        Core.SequenceKey = (Keys) Enum.Parse(typeof (Keys), Cfg.Key);
        Core.SequenceLength = (uint) Cfg.Times;
        Core.SequenceTimeout = (uint) Cfg.Timeout;
        Core.OnlyOnPrimaryScreen = Cfg.OnlyOnPrimaryScreen;
      }
      UI UI = new UI(Core, Cfg);
      Bitmap icon = Images.Icon;
      this.RepaintIcon(icon);
      Icon.Icon = System.Drawing.Icon.FromHandle(icon.GetHicon());
      Icon.MouseClick += (MouseEventHandler) ((Instance, Arguments) =>
      {
        if (Arguments.Button != MouseButtons.Left)
          return;
        UI.Show();
        UI.Activate();
      });

        Icon.ContextMenu = new ContextMenu(new MenuItem[] { new MenuItem("退出", (a, b) => { Application.Exit(); }) });

      UI.FormClosed += (FormClosedEventHandler) ((Instance, Arguments) =>
      {
        try
        {
          Config config = new Config();
          config.ActiveServices = Core.ActiveServices;
          config.CustomWindowWidth = Core.CustomWindowWidth;
          config.CustomWindowHeight = Core.CustomWindowHeight;
          config.WindowWidth = (int) Core.WindowWidth;
          config.WindowHeight = (int) Core.WindowHeight;
          config.ForceToResizeWindow = Core.ForceToResizeWindow;
          config.OnlyNewWindow = Core.OnlyNewWindow;
          config.HelpMeDecide = Core.HelpMeDecide;
          config.Key = Core.SequenceKey.ToString();
          config.Times = (int) Core.SequenceLength;
          config.Timeout = (int) Core.SequenceTimeout;
          config.FirstRun = false;
          config.OnlyOnPrimaryScreen = Core.OnlyOnPrimaryScreen;
          using (FileStream fileStream = File.Open(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WindowCenteringHelper.ini"), FileMode.Create))
            new BinaryFormatter().Serialize((Stream) fileStream, (object) config);
        }
        catch
        {
        }
        Icon.Visible = false;
        Application.Exit();
      });
      Icon.Visible = true;
      if (!Cfg.FirstRun)
        return;
      Cfg.FirstRun = false;
      Icon.ShowBalloonTip(5000, "First Run", string.Format("Press {0} x [{1}] to center a window", (object) Cfg.Times, (object) Cfg.Key), ToolTipIcon.None);
    }

    private void RepaintIcon(Bitmap image)
    {
      System.Drawing.Color color;
      try
      {
        if (!((string) Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion").GetValue("ProductName")).Contains("Windows 10"))
          return;
        string name = "ImmersiveApplicationBackground";
        int colorFromColorSetEx = (int) Icon.GetImmersiveColorFromColorSetEx(Icon.GetImmersiveUserColorSetPreference(false, false), Icon.GetImmersiveColorTypeFromName(name), false, 0U);
        byte[] numArray = new byte[4]
        {
          (byte) ((4278190080L & (long) colorFromColorSetEx) >> 24),
          (byte) ((16711680 & colorFromColorSetEx) >> 16),
          (byte) ((65280 & colorFromColorSetEx) >> 8),
          (byte) ((int) byte.MaxValue & colorFromColorSetEx)
        };
        int num = (int) numArray[3] << 16 | (int) numArray[2] << 8 | (int) numArray[1];
        color = System.Drawing.Color.FromArgb(num >> 16 & (int) byte.MaxValue, num >> 8 & (int) byte.MaxValue, num & (int) byte.MaxValue);
      }
      catch
      {
        return;
      }
      color = color.R >= (byte) 60 || color.G >= (byte) 60 || color.B >= (byte) 60 ? System.Drawing.Color.Black : System.Drawing.Color.White;
      for (int y = 0; y < image.Height; ++y)
      {
        for (int x = 0; x < image.Width; ++x)
        {
          if (image.GetPixel(x, y).A == byte.MaxValue)
          {
            image.SetPixel(x, y, color);
            image.SetPixel(x - 1, y, color);
          }
        }
      }
    }

    [DllImport("uxtheme.dll", EntryPoint = "#94", CharSet = CharSet.Unicode)]
    private static extern int GetImmersiveColorSetCount();

    [DllImport("uxtheme.dll", EntryPoint = "#95", CharSet = CharSet.Unicode)]
    private static extern uint GetImmersiveColorFromColorSetEx(
      uint dwImmersiveColorSet,
      uint dwImmersiveColorType,
      bool bIgnoreHighContrast,
      uint dwHighContrastCacheMode);

    [DllImport("uxtheme.dll", EntryPoint = "#96", CharSet = CharSet.Unicode)]
    private static extern uint GetImmersiveColorTypeFromName(string name);

    [DllImport("uxtheme.dll", EntryPoint = "#98", CharSet = CharSet.Unicode)]
    private static extern uint GetImmersiveUserColorSetPreference(
      bool bForceCheckRegistry,
      bool bSkipCheckOnFail);

    [DllImport("uxtheme.dll", EntryPoint = "#100", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetImmersiveColorNamedTypeByIndex(uint dwIndex);
  }
}
