// Decompiled with JetBrains decompiler
// Type: ZeusAIO.Constants
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace ZeusAIO
{
  internal class Constants
  {
    public static bool Breached = false;
    public static bool Started = false;
    public static string IV = (string) null;
    public static string Key = (string) null;
    public static string ApiUrl = "https://api.auth.gg/csharp/";
    public static bool Initialized = false;
    public static Random random = new Random();

    public static string Token { get; set; }

    public static string Date { get; set; }

    public static string APIENCRYPTKEY { get; set; }

    public static string APIENCRYPTSALT { get; set; }

    public static string RandomString(int length) => new string(((IEnumerable<char>) Enumerable.Select<string, char>((IEnumerable<M0>) Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length), (Func<M0, M1>) (Constants.\u003C\u003Ec.\u003C\u003E9__23_0 ?? (Constants.\u003C\u003Ec.\u003C\u003E9__23_0 = new Func<string, char>((object) Constants.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRandomString\u003Eb__23_0)))))).ToArray<char>());

    public static string HWID() => WindowsIdentity.GetCurrent().User.Value;
  }
}
