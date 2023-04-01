// Decompiled with JetBrains decompiler
// Type: KamilSzymborski.WindowCenteringHelper.Core.CenteringHelper
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using KamilSzymborski.WindowToolkit;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KamilSzymborski.WindowCenteringHelper.Core
{
    public class CenteringHelper
    {
        internal const int SHORT_EXECUTION_INTERVAL = 30;
        internal const int STANDARD_EXECUTION_INTERVAL = 300;
        private bool _HelpMeDecide;
        private uint _WindowWidth;
        private uint _WindowHeight;
        private bool _CustomWindowWidth;
        private bool _CustomWindowHeight;
        private bool _ForceToResizeWindow;
        private bool _OnlyOnPrimaryScreen;
        private Services _ActiveServices;
        private readonly object _Lock;
        private readonly WindowListener _WindowListener;
        private readonly KeySquenceListener _KeySequenceListener;

        private readonly static string logPath = Path.Combine(Application.StartupPath, "WindowTitle.log");


        public CenteringHelper()
        {
            this._WindowListener = new WindowListener();
            this._KeySequenceListener = new KeySquenceListener();
            this._Lock = new object();
            this._Init();

            using (File.Create(logPath))
            {

            };
        }

        public void Close() => this._Close();

        private void _Close()
        {
            if (this._WindowListener.IsAlive())
                this._WindowListener.Close();
            if (!this._KeySequenceListener.IsAlive())
                return;
            this._KeySequenceListener.Close();
        }

        private void _Init()
        {
            Action<IntPtr> CenterWindow = (Action<IntPtr>)(WindowID =>
           {
               WindowStyles windowStyle = WindowKit.GetWindowStyle(WindowID);
               if (this._CustomWindowWidth && this._CustomWindowHeight && windowStyle.HasFlag((Enum)WindowStyles.MAXIMIZE))
                   WindowKit.ShowNormal(WindowID);
               WindowBox windowBox = WindowKit.GetWindowBox(WindowID);
               Screen windowScreen = WindowKit.GetWindowScreen(WindowID);
               if (this._OnlyOnPrimaryScreen && !windowScreen.Primary)
                   return;
               WindowKit.GetBorderSize(WindowID);
               Rectangle workingArea = windowScreen.WorkingArea;
               int width = workingArea.Width;
               workingArea = windowScreen.WorkingArea;
               int height = workingArea.Height;
               int Width = windowBox.Width;
               int Height = windowBox.Height;
               bool flag = windowStyle.HasFlag((Enum)WindowStyles.THICKFRAME) && windowStyle.HasFlag((Enum)WindowStyles.TABSTOP) && !windowStyle.HasFlag((Enum)WindowStyles.MAXIMIZE);
               if (this._CustomWindowWidth && flag)
                   Width = Helpers.PercentOf(width, (int)this._WindowWidth);
               if (this._CustomWindowWidth && !flag && this._ForceToResizeWindow)
                   Width = Helpers.PercentOf(width, (int)this._WindowWidth);
               if (this._CustomWindowHeight && flag)
                   Height = Helpers.PercentOf(height, (int)this._WindowHeight);
               if (this._CustomWindowHeight && !flag && this._ForceToResizeWindow)
                   Height = Helpers.PercentOf(height, (int)this._WindowHeight);
               int X = width / 2 - Width / 2;
               int Y = height / 2 - Height / 2;
               WindowKit.MoveWindow(WindowID, X, Y, Width, Height);
           });

            Action<IntPtr> CenterWindowIM = (Action<IntPtr>)(WindowID =>
           {
               WindowStyles windowStyle = WindowKit.GetWindowStyle(WindowID);
               if (this._CustomWindowWidth && this._CustomWindowHeight && windowStyle.HasFlag((Enum)WindowStyles.MAXIMIZE))
                   WindowKit.ShowNormal(WindowID);
               WindowBox windowBox = WindowKit.GetWindowBox(WindowID);
               Screen windowScreen = WindowKit.GetWindowScreen(WindowID);
               if (this._OnlyOnPrimaryScreen && !windowScreen.Primary)
                   return;
               WindowKit.GetBorderSize(WindowID);
               Rectangle workingArea = windowScreen.WorkingArea;
               int width = workingArea.Width;
               workingArea = windowScreen.WorkingArea;
               int height = workingArea.Height;
               int Width = windowBox.Width;
               int Height = windowBox.Height;
               bool flag = windowStyle.HasFlag((Enum)WindowStyles.THICKFRAME) && windowStyle.HasFlag((Enum)WindowStyles.TABSTOP) && !windowStyle.HasFlag((Enum)WindowStyles.MAXIMIZE);
               if (this._CustomWindowWidth && flag)
                   Width = Helpers.PercentOf(width, (int)75);
               if (this._CustomWindowWidth && !flag && this._ForceToResizeWindow)
                   Width = Helpers.PercentOf(width, (int)75);
               if (this._CustomWindowHeight && flag)
                   Height = Helpers.PercentOf(height, (int)75);
               if (this._CustomWindowHeight && !flag && this._ForceToResizeWindow)
                   Height = Helpers.PercentOf(height, (int)75);
               int X = width / 2 - Width / 2;
               int Y = height / 2 - Height / 2;
               WindowKit.MoveWindow(WindowID, X, Y, Width, Height);
           });

            Action<IntPtr, int?> CenterWindowWays = (Action<IntPtr, int?>)((WindowID, withper) =>
              {
                  WindowStyles windowStyle = WindowKit.GetWindowStyle(WindowID);
                  if (this._CustomWindowWidth && this._CustomWindowHeight && windowStyle.HasFlag((Enum)WindowStyles.MAXIMIZE))
                      WindowKit.ShowNormal(WindowID);
                  WindowBox windowBox = WindowKit.GetWindowBox(WindowID);
                  Screen windowScreen = WindowKit.GetWindowScreen(WindowID);
                  if (this._OnlyOnPrimaryScreen && !windowScreen.Primary)
                      return;
                  WindowKit.GetBorderSize(WindowID);
                  Rectangle workingArea = windowScreen.WorkingArea;
                  int width = workingArea.Width;
                  workingArea = windowScreen.WorkingArea;
                  int height = workingArea.Height;
                  int Width = windowBox.Width;
                  int Height = windowBox.Height;
                  bool flag = windowStyle.HasFlag((Enum)WindowStyles.THICKFRAME) && windowStyle.HasFlag((Enum)WindowStyles.TABSTOP) && !windowStyle.HasFlag((Enum)WindowStyles.MAXIMIZE);
                  if (this._CustomWindowWidth && flag)
                      Width = Helpers.PercentOf(width, withper ?? (int)this._WindowWidth);
                  if (this._CustomWindowWidth && !flag && this._ForceToResizeWindow)
                      Width = Helpers.PercentOf(width, withper ?? (int)this._WindowWidth);
                  if (this._CustomWindowHeight && flag)
                      Height = Helpers.PercentOf(height, withper ?? (int)this._WindowHeight);
                  if (this._CustomWindowHeight && !flag && this._ForceToResizeWindow)
                      Height = Helpers.PercentOf(height, withper ?? (int)this._WindowHeight);

                  ConfigurationManager.RefreshSection("appSettings");
                  var left = int.Parse(ConfigurationManager.AppSettings["left"]);
                  var top = int.Parse(ConfigurationManager.AppSettings["top"]);

                  int X = width / 2 - Width / 2 + left;
                  int Y = height / 2 - Height / 2 + top;
                  WindowKit.MoveWindow(WindowID, X, Y, Width, Height);
              });

            this._WindowListener.NewWindow += (WindowListener.WindowCallback)(WindowID =>
           {
               lock (this._Lock)
               {
                   string windowClass = WindowKit.GetWindowClass(WindowID);
                   if (windowClass.Contains("CoreWindow") || windowClass.Contains("WorkerW") || (windowClass.Contains("Flyout") || windowClass.Contains("DV2ControlHost")) || (windowClass.Contains("NotifyIcon") || windowClass.Contains("NativeHWNDHost") || (windowClass.Contains("Popup") || windowClass.Contains("Progman"))))
                       return;
                   if (this._HelpMeDecide)
                   {
                       WindowStyles windowStyle = WindowKit.GetWindowStyle(WindowID);
                       if (windowStyle.HasFlag((Enum)WindowStyles.MAXIMIZE) || windowStyle.HasFlag((Enum)WindowStyles.POPUP) && (!windowStyle.HasFlag((Enum)WindowStyles.SYSMENU) || !windowStyle.HasFlag((Enum)WindowStyles.BORDER)))
                           return;
                   }

                   CenterWays(WindowID, CenterWindowWays);



                   //if (windowTitle.Contains("微信") ||
                   //    windowTitle.ToLower().Contains("cmd") ||
                   //    windowTitle.ToLower().Contains("powershell"))



               }
           });
            this._KeySequenceListener.SequenceReleased += (KeySquenceListener.Callback)(() =>
           {
               lock (this._Lock)
                   //CenterWindow(WindowKit.GetCurrentWindow());
                   CenterWays(WindowKit.GetCurrentWindow(), CenterWindowWays);

           });
        }

        private static void CenterWays(IntPtr WindowID, Action<IntPtr, int?> CenterWindow)
        {
            string windowTitle = WindowKit.GetWindowTitle(WindowID);

            Console.WriteLine(windowTitle);

            if (windowTitle.Trim().Length == 0)
            {
                return;
            }

            OneLog(windowTitle);

            var excludeCfgPath = Path.Combine(Application.StartupPath, "config.exclude.cfg");

            var excludeCfg = File.ReadAllText(excludeCfgPath);

            if (excludeCfg.Split('|').Contains(windowTitle))
            {
                Console.WriteLine($"exclude:{windowTitle}");
                return;
            }

            var miniCfgPath = Path.Combine(Application.StartupPath, "config.mini.cfg");

            var minicfg = File.ReadAllText(miniCfgPath);

            var smallCfgPath = Path.Combine(Application.StartupPath, "config.small.cfg");

            var smallcfg = File.ReadAllText(smallCfgPath);

            if (smallcfg.Split('|').Any(x => windowTitle.IndexOf(x, StringComparison.OrdinalIgnoreCase) != -1))
            {
                Console.WriteLine($"small:{windowTitle}");
                CenterWindow(WindowID, 75);
                OneLog($"small:{windowTitle}");
            }
            else if (minicfg.Split('|').Any(x => windowTitle.IndexOf(x, StringComparison.OrdinalIgnoreCase) != -1))
            {
                Console.WriteLine($"mini:{windowTitle}");
                CenterWindow(WindowID, 50);
                OneLog($"mini:{windowTitle}");
            }
            else
            {
                CenterWindow(WindowID, null);
                OneLog($"normal:{windowTitle}");
            }
        }

        private static void OneLog(string log)
        {
            using (var file = File.AppendText(logPath))
            {
                file.WriteLine($"{DateTime.Now} {log}");
            }
        }

        private void _Init(Services Services)
        {
            if (Services.HasFlag((Enum)Services.OnKeySequence) && !this._KeySequenceListener.IsAlive())
                this._KeySequenceListener.Start();
            if (!Services.HasFlag((Enum)Services.OnKeySequence) && this._KeySequenceListener.IsAlive())
                this._KeySequenceListener.Close();
            if (Services.HasFlag((Enum)Services.Automatically) && !this._WindowListener.IsAlive())
                this._WindowListener.Start();
            if (Services.HasFlag((Enum)Services.Automatically) || !this._WindowListener.IsAlive())
                return;
            this._WindowListener.Close();
        }

        public uint WindowWidth
        {
            get
            {
                lock (this._Lock)
                    return this._WindowWidth;
            }
            set
            {
                lock (this._Lock)
                    this._WindowWidth = value;
            }
        }

        public uint WindowHeight
        {
            get
            {
                lock (this._Lock)
                    return this._WindowHeight;
            }
            set
            {
                lock (this._Lock)
                    this._WindowHeight = value;
            }
        }

        public bool HelpMeDecide
        {
            get
            {
                lock (this._Lock)
                    return this._HelpMeDecide;
            }
            set
            {
                lock (this._Lock)
                    this._HelpMeDecide = value;
            }
        }

        public bool OnlyNewWindow
        {
            get => this._WindowListener.KeepHistory;
            set => this._WindowListener.KeepHistory = value;
        }

        public bool CustomWindowWidth
        {
            get
            {
                lock (this._Lock)
                    return this._CustomWindowWidth;
            }
            set
            {
                lock (this._Lock)
                    this._CustomWindowWidth = value;
            }
        }

        public bool CustomWindowHeight
        {
            get
            {
                lock (this._Lock)
                    return this._CustomWindowHeight;
            }
            set
            {
                lock (this._Lock)
                    this._CustomWindowHeight = value;
            }
        }

        public bool ForceToResizeWindow
        {
            get
            {
                lock (this._Lock)
                    return this._ForceToResizeWindow;
            }
            set
            {
                lock (this._Lock)
                    this._ForceToResizeWindow = value;
            }
        }

        public Keys SequenceKey
        {
            get => this._KeySequenceListener.SequenceKey;
            set => this._KeySequenceListener.SequenceKey = value;
        }

        public uint SequenceLength
        {
            get => this._KeySequenceListener.SequenceLength;
            set => this._KeySequenceListener.SequenceLength = value;
        }

        public uint SequenceTimeout
        {
            get => this._KeySequenceListener.SequenceTimeout;
            set => this._KeySequenceListener.SequenceTimeout = value;
        }

        public bool OnlyOnPrimaryScreen
        {
            get
            {
                lock (this._Lock)
                    return this._OnlyOnPrimaryScreen;
            }
            set
            {
                lock (this._Lock)
                    this._OnlyOnPrimaryScreen = value;
            }
        }

        public Services ActiveServices
        {
            get => this._ActiveServices;
            set
            {
                this._ActiveServices = value;
                this._Init(value);
            }
        }
    }
}
