// Decompiled with JetBrains decompiler
// Type: WindowCenteringHelper.Start
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowCenteringHelper
{
  internal static class Start
  {
    [STAThread]
    private static void Main()
    {
      Start.SetProcessDPIAware();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((ApplicationContext) new Icon());
    }

    [DllImport("user32.dll")]
    internal static extern bool SetProcessDPIAware();
  }
}
