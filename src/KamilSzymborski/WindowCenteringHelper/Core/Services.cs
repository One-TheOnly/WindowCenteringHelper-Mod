// Decompiled with JetBrains decompiler
// Type: KamilSzymborski.WindowCenteringHelper.Core.Services
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using System;

namespace KamilSzymborski.WindowCenteringHelper.Core
{
  [Flags]
  [Serializable]
  public enum Services
  {
    None = 0,
    OnKeySequence = 1,
    Automatically = 2,
  }
}
