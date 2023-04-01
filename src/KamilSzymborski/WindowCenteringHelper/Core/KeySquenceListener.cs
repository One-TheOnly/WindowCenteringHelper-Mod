// Decompiled with JetBrains decompiler
// Type: KamilSzymborski.WindowCenteringHelper.Core.KeySquenceListener
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using Gma.System.MouseKeyHook;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace KamilSzymborski.WindowCenteringHelper.Core
{
  internal sealed class KeySquenceListener
  {
    internal const int EXECUTION_INTERVAL = 30;
    private bool _Loop;
    private bool _FirstInit;
    private int _SequenceTimeout;
    private int _SequenceLength;
    private Keys _SequenceKey;
    private Thread _Thread;
    private readonly object _Lock;
    private readonly Queue<Keys> _PressedKeys;

    internal KeySquenceListener()
    {
      this._Lock = new object();
      this._FirstInit = true;
      this._PressedKeys = new Queue<Keys>();
    }

    internal event KeySquenceListener.Callback SequenceReleased;

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
      lock (this._Lock)
      {
        if (this._FirstInit)
        {
          Hook.GlobalEvents().KeyUp += (KeyEventHandler) ((Instance, Arguments) =>
          {
            lock (this._Lock)
            {
              if (Arguments.KeyCode != this._SequenceKey)
                return;
              this._PressedKeys.Enqueue(Arguments.KeyCode);
            }
          });
          this._FirstInit = false;
        }
      }
      this._Thread = new Thread((ThreadStart) (() =>
      {
        Stopwatch stopwatch = Stopwatch.StartNew();
        while (true)
        {
          lock (this._Lock)
          {
            if (!this._Loop)
              break;
            if (stopwatch.Elapsed.TotalMilliseconds >= (double) this._SequenceTimeout || this._PressedKeys.Count == 0)
            {
              stopwatch.Restart();
              this._PressedKeys.Clear();
            }
            else if (this._PressedKeys.Count == this._SequenceLength)
            {
              KeySquenceListener.Callback sequenceReleased = this.SequenceReleased;
              if (sequenceReleased != null)
                sequenceReleased();
              stopwatch.Restart();
              this._PressedKeys.Clear();
            }
          }
          Thread.Sleep(30);
        }
      }));
      this._Loop = true;
      this._PressedKeys.Clear();
      this._Thread.Start();
    }

    private void _Close()
    {
      Hook.GlobalEvents().Dispose();
      lock (this._Lock)
        this._Loop = false;
      do
        ;
      while (this._Thread.IsAlive);
    }

    internal Keys SequenceKey
    {
      get
      {
        lock (this._Lock)
          return this._SequenceKey;
      }
      set
      {
        lock (this._Lock)
          this._SequenceKey = value;
      }
    }

    internal uint SequenceTimeout
    {
      get
      {
        lock (this._Lock)
          return (uint) this._SequenceTimeout;
      }
      set
      {
        lock (this._Lock)
          this._SequenceTimeout = (int) value;
      }
    }

    internal uint SequenceLength
    {
      get
      {
        lock (this._Lock)
          return (uint) this._SequenceLength;
      }
      set
      {
        lock (this._Lock)
          this._SequenceLength = (int) value;
      }
    }

    internal delegate void Callback();
  }
}
