// Decompiled with JetBrains decompiler
// Type: ZeusAIO.App
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using System.Collections.Generic;

namespace ZeusAIO
{
  internal class App
  {
    public static string Error = (string) null;
    public static Dictionary<string, string> Variables = new Dictionary<string, string>();

    public static string GrabVariable(string name)
    {
      try
      {
        if (User.ID != null || User.HWID != null || User.IP != null || !Constants.Breached)
          return App.Variables[name];
        Constants.Breached = true;
        return "User is not logged in, possible breach detected!";
      }
      catch
      {
        return "N/A";
      }
    }
  }
}
