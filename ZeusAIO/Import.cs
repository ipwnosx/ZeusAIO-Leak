// Decompiled with JetBrains decompiler
// Type: ZeusAIO.Import
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Colorful;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ZeusAIO
{
  internal class Import
  {
    public static void LoadCombos()
    {
      Console.WriteLine();
      Console.WriteLine(">> Please Load Combos:", Color.White);
      OpenFileDialog openFileDialog = new OpenFileDialog();
      string fileName;
      do
      {
        openFileDialog.Title = "Load Combos";
        openFileDialog.DefaultExt = "txt";
        openFileDialog.Filter = "Text Files|*.txt";
        openFileDialog.RestoreDirectory = true;
        int num = (int) openFileDialog.ShowDialog();
        fileName = openFileDialog.FileName;
      }
      while (!File.Exists(fileName));
      try
      {
        mainmenu.combos = (IEnumerable<string>) File.ReadAllLines(fileName);
      }
      catch
      {
      }
      Console.WriteLine(">> Loading into Memory...", Color.Magenta);
      Thread.Sleep(500);
      mainmenu.comboTotal = mainmenu.combos.Count<string>();
      Console.WriteLine(">> Loaded {0} combos.", Color.White, new object[1]
      {
        (object) mainmenu.comboTotal
      });
      Thread.Sleep(1000);
    }

    public static void LoadProxies()
    {
      Console.WriteLine();
      Console.WriteLine(">> Please Load Proxies:", Color.White);
      OpenFileDialog openFileDialog = new OpenFileDialog();
      string fileName;
      do
      {
        openFileDialog.Title = "Load Proxies";
        openFileDialog.DefaultExt = "txt";
        openFileDialog.Filter = "Text Files|*.txt";
        openFileDialog.RestoreDirectory = true;
        int num = (int) openFileDialog.ShowDialog();
        fileName = openFileDialog.FileName;
      }
      while (!File.Exists(fileName));
      try
      {
        mainmenu.proxies = (IEnumerable<string>) File.ReadAllLines(fileName);
      }
      catch
      {
      }
      mainmenu.proxiesCount = mainmenu.proxies.Count<string>();
      Console.WriteLine(">> Loading into Memory...", Color.Magenta);
      Thread.Sleep(500);
      Console.WriteLine(">> Loaded {0} proxies.", Color.White, new object[1]
      {
        (object) mainmenu.proxiesCount
      });
      if (mainmenu.p1 == "2")
      {
        Console.WriteLine();
        Console.WriteLine(">> Configuration", Color.DarkMagenta);
        Console.WriteLine(" > Proxys Loaded: " + mainmenu.proxiesCount.ToString(), Color.LawnGreen);
        Console.WriteLine(" > Proxys protocol: " + mainmenu.proxyProtocol, Color.LawnGreen);
        Console.WriteLine(" > Combos Loaded: " + mainmenu.comboTotal.ToString(), Color.LawnGreen);
        Console.WriteLine(" > Retries Selected: " + Config.config.Retries.ToString(), Color.LawnGreen);
        Console.WriteLine(" > Threads Selected: " + mainmenu.globalThreads.ToString(), Color.LawnGreen);
        Console.WriteLine();
        Thread.Sleep(20);
      }
      Thread.Sleep(100);
    }
  }
}
