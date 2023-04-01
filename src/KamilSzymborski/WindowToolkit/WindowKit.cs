// Decompiled with JetBrains decompiler
// Type: KamilSzymborski.WindowToolkit.WindowKit
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using System;
using System.Text;
using System.Windows.Forms;

namespace KamilSzymborski.WindowToolkit
{
  public static class WindowKit
  {
    public static void ShowNormal(IntPtr WindowID) => Interops.ShowWindow(WindowID, 1);

    public static void ShowMaximized(IntPtr WindowID) => Interops.ShowWindow(WindowID, 3);

    public static void MoveWindow(IntPtr WindowID, int X, int Y, int Width, int Height) => Interops.MoveWindow(WindowID, X, Y, Width, Height, true);

    public static IntPtr GetCurrentWindow() => Interops.GetForegroundWindow();

    public static int GetBorderSize(IntPtr WindowID)
    {
      Interops.Rect rect1 = new Interops.Rect();
      Interops.Rect rect2 = new Interops.Rect();
      Interops.GetWindowRect(WindowID, ref rect1);
      Interops.GetWindowRect(WindowID, ref rect2);
      return Math.Abs(rect2.Right - rect2.Left - rect1.Right - rect1.Left);
    }

    public static string GetWindowClass(IntPtr WindowID)
    {
      StringBuilder lpClassName = new StringBuilder((int) byte.MaxValue);
      Interops.GetClassName(WindowID, lpClassName, lpClassName.Capacity);
      return lpClassName.ToString();
    }

    public static string GetWindowTitle(IntPtr WindowID)
    {
      StringBuilder lpString = new StringBuilder((int) byte.MaxValue);
      Interops.GetWindowText(WindowID, lpString, lpString.Capacity);
      return lpString.ToString();
    }

    public static Screen GetWindowScreen(IntPtr WindowID) => Screen.FromHandle(WindowID);

    public static WindowBox GetWindowBox(IntPtr WindowID)
    {
      Interops.Rect rect = new Interops.Rect();
      Interops.GetWindowRect(WindowID, ref rect);
      return new WindowBox(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
    }

    public static WindowStyles GetWindowStyle(IntPtr WindowID) => (WindowStyles) Interops.GetWindowLong(WindowID, -16);
  }
}
