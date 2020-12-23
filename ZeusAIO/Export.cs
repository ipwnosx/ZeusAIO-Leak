// Decompiled with JetBrains decompiler
// Type: ZeusAIO.Export
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using System;
using System.IO;
using System.Threading;

namespace ZeusAIO
{
  internal class Export
  {
    private static object resultLock = new object();

    public static void Initialize()
    {
      Directory.CreateDirectory("Results");
      Directory.CreateDirectory("Results/" + Login.date);
    }

    public static void AsResult(string fileName, string content)
    {
      object resultLock = Export.resultLock;
      bool flag = false;
      try
      {
        Monitor.Enter(resultLock, ref flag);
        File.AppendAllText("Results/" + Login.date + fileName + ".txt", content + Environment.NewLine);
      }
      finally
      {
        if (flag)
          Monitor.Exit(resultLock);
      }
    }
  }
}
