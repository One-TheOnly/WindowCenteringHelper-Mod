// Decompiled with JetBrains decompiler
// Type: KamilSzymborski.WindowToolkit.WindowBox
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using System;

namespace KamilSzymborski.WindowToolkit
{
  [Serializable]
  public sealed class WindowBox
  {
    private readonly int _X;
    private readonly int _Y;
    private readonly int _Width;
    private readonly int _Height;

    internal WindowBox(int X, int Y, int Width, int Height)
    {
      this._X = X;
      this._Y = Y;
      this._Width = Width;
      this._Height = Height;
    }

    public int X => this._X;

    public int Y => this._Y;

    public int Width => this._Width;

    public int Height => this._Height;
  }
}
