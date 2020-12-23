// Decompiled with JetBrains decompiler
// Type: ProxyChecker.Variables
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using System.Collections.Generic;

namespace ProxyChecker
{
  public class Variables
  {
    public static string Version = "1.1";
    public static string Permission = "";
    public static string Type = "";
    public static string Folder = "";
    public static int Threads = 250;
    public static int TimeOut = 5000;
    public static int Alive = 0;
    public static int Dead = 0;
    public static List<string> Proxies = new List<string>();
    public static Queue<string> ProxiesQueue = new Queue<string>();
  }
}
