// Decompiled with JetBrains decompiler
// Type: TunnelBear.CookieDictionary
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Leaf.xNet;
using System.Net;

namespace TunnelBear
{
  internal class CookieDictionary : CookieStorage
  {
    public CookieDictionary(bool isLocked = false, CookieContainer container = null)
      : base(isLocked, container)
    {
    }
  }
}
