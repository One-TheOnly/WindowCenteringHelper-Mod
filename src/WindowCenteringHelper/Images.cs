// Decompiled with JetBrains decompiler
// Type: WindowCenteringHelper.Images
// Assembly: WindowCenteringHelper, Version=1.2.11.0, Culture=neutral, PublicKeyToken=null
// MVID: 439D2995-1A6B-4F70-A86B-40450458C382
// Assembly location: D:\OneDrive - 薅羊毛大学\下载\WindowCenteringHelper_new\Release\WindowCenteringHelper.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WindowCenteringHelper
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Images
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Images()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (Images.resourceMan == null)
          Images.resourceMan = new ResourceManager("WindowCenteringHelper.Images", typeof (Images).Assembly);
        return Images.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Images.resourceCulture;
      set => Images.resourceCulture = value;
    }

    internal static Bitmap Icon => (Bitmap) Images.ResourceManager.GetObject(nameof (Icon), Images.resourceCulture);
  }
}
