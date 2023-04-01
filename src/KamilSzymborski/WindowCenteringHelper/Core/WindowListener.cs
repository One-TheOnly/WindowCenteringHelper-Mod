// Decompiled with JetBrains decompiler
// Type: KamilSzymborski.WindowCenteringHelper.Core.WindowListener
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using KamilSzymborski.WindowToolkit;
using System;
using System.Collections.Generic;
using System.Threading;

namespace KamilSzymborski.WindowCenteringHelper.Core
{
  internal sealed class WindowListener
  {
    internal const int EXECUTION_INTERVAL = 50;
    private bool _Loop;
    private bool _KeepHistory;
    private Thread _Thread;
    private readonly object _Lock;

    internal WindowListener() => this._Lock = new object();

    internal event WindowListener.WindowCallback NewWindow;

    internal event WindowListener.WindowCallback CurrentWindow;

    internal void Start()
    {
      if (this._Thread != null && this._Thread.IsAlive)
        this._Close();
      this._Init();
    }

    internal void Close()
    {
      if (this._Thread == null || !this._Thread.IsAlive)
        return;
      this._Close();
    }

    internal bool IsAlive()
    {
      lock (this._Lock)
        return this._Thread != null && this._Loop;
    }

    private void _Init()
    {
      this._Loop = true;
      this._Thread = new Thread((ThreadStart) (() =>
      {
        IntPtr num = IntPtr.Zero;
        Queue<IntPtr> numQueue = new Queue<IntPtr>();
        while (true)
        {
          lock (this._Lock)
          {
            if (!this._Loop)
              break;
          }
          IntPtr currentWindow1 = WindowKit.GetCurrentWindow();
          WindowListener.WindowCallback currentWindow2 = this.CurrentWindow;
          if (currentWindow2 != null)
            currentWindow2(currentWindow1);
          lock (this._Lock)
          {
            if (this._KeepHistory)
            {
              if (!numQueue.Contains(currentWindow1))
              {
                numQueue.Enqueue(currentWindow1);
                WindowListener.WindowCallback newWindow = this.NewWindow;
                if (newWindow != null)
                  newWindow(currentWindow1);
              }
            }
            else if (!this._KeepHistory)
            {
              if (num != currentWindow1)
              {
                num = currentWindow1;
                WindowListener.WindowCallback newWindow = this.NewWindow;
                if (newWindow != null)
                  newWindow(currentWindow1);
              }
            }
          }
          Thread.Sleep(50);
        }
      }));
      this._Thread.Start();
    }

    private void _Close()
    {
      lock (this._Lock)
        this._Loop = false;
      do
        ;
      while (this._Thread.IsAlive);
    }

    internal bool KeepHistory
    {
      get
      {
        lock (this._Lock)
          return this._KeepHistory;
      }
      set
      {
        lock (this._Lock)
          this._KeepHistory = value;
      }
    }

    internal delegate void WindowCallback(IntPtr Window);
  }
}
