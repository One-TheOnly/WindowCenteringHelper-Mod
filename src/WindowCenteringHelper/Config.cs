// Decompiled with JetBrains decompiler
// Type: WindowCenteringHelper.Config
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using KamilSzymborski.WindowCenteringHelper.Core;
using System;

namespace WindowCenteringHelper
{
  [Serializable]
  internal sealed class Config
  {
    internal Services ActiveServices { get; set; }

    internal bool CustomWindowWidth { get; set; }

    internal bool CustomWindowHeight { get; set; }

    internal int WindowWidth { get; set; }

    internal int WindowHeight { get; set; }

    internal bool ForceToResizeWindow { get; set; }

    internal bool HelpMeDecide { get; set; }

    internal bool OnlyNewWindow { get; set; }

    internal bool OnlyOnPrimaryScreen { get; set; }

    internal string Key { get; set; }

    internal int Times { get; set; }

    internal int Timeout { get; set; }

    internal bool FirstRun { get; set; }
  }
}
