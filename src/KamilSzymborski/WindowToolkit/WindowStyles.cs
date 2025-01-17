﻿// Decompiled with JetBrains decompiler
// Type: KamilSzymborski.WindowToolkit.WindowStyles
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using System;

namespace KamilSzymborski.WindowToolkit
{
  [Flags]
  [Serializable]
  public enum WindowStyles : uint
  {
    OVERLAPPED = 0,
    POPUP = 2147483648, // 0x80000000
    CHILD = 1073741824, // 0x40000000
    MINIMIZE = 536870912, // 0x20000000
    VISIBLE = 268435456, // 0x10000000
    DISABLED = 134217728, // 0x08000000
    CLIPSIBLINGS = 67108864, // 0x04000000
    CLIPCHILDREN = 33554432, // 0x02000000
    MAXIMIZE = 16777216, // 0x01000000
    BORDER = 8388608, // 0x00800000
    DLGFRAME = 4194304, // 0x00400000
    VSCROLL = 2097152, // 0x00200000
    HSCROLL = 1048576, // 0x00100000
    SYSMENU = 524288, // 0x00080000
    THICKFRAME = 262144, // 0x00040000
    GROUP = 131072, // 0x00020000
    TABSTOP = 65536, // 0x00010000
    MINIMIZEBOX = GROUP, // 0x00020000
    MAXIMIZEBOX = TABSTOP, // 0x00010000
  }
}
