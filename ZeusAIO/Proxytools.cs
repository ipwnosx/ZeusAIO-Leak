// Decompiled with JetBrains decompiler
// Type: ZeusAIO.Proxytools
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ZeusAIO
{
  internal class Proxytools
  {
    public static void menu()
    {
      System.Console.Clear();
      Write.ascii();
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("1", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Proxy Scraper\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("2", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Back\n", Color.White);
      string str = System.Console.ReadLine();
      if (str == "1")
        Proxytools.proxyscraper();
      else if (str == "2")
        mainmenu.menu();
      else
        Proxytools.menu();
    }

    public static void proxyscraper()
    {
      try
      {
        IEnumerable<string> list = (IEnumerable<string>) System.IO.File.ReadLines("Sources.txt").ToList<string>();
        ThreadPool.SetMinThreads(50, 50);
        ThreadPool.SetMaxThreads(50, 50);
        Parallel.ForEach<string>((IEnumerable<M0>) list, (Action<M0>) (currentUrl =>
        {
          try
          {
            foreach (object match in Regex.Matches(new WebClient().DownloadString(currentUrl), "\\b(\\d{1,3}\\.){3}\\d{1,3}\\:\\d{1,8}\\b", RegexOptions.Singleline))
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              Proxytools.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new Proxytools.\u003C\u003Ec__DisplayClass1_0();
              // ISSUE: reference to a compiler-generated field
              cDisplayClass10.line = (Match) match;
              ++Proxytools.Variables.total;
              // ISSUE: method pointer
              if (!Enumerable.Any<string>((IEnumerable<M0>) Proxytools.Variables.Proxies, (Func<M0, bool>) new Func<string, bool>((object) cDisplayClass10, __methodptr(\u003Cproxyscraper\u003Eb__1))))
              {
                ++Proxytools.Variables.unique;
                // ISSUE: reference to a compiler-generated field
                Proxytools.Variables.Proxies.Add(cDisplayClass10.line.Groups[0].Value);
                // ISSUE: reference to a compiler-generated field
                Colorful.Console.WriteLine(cDisplayClass10.line.Groups[0].Value, Color.Green);
                while (true)
                {
                  try
                  {
                    using (StreamWriter streamWriter = new StreamWriter("Proxies/Scraped/" + Login.date + "/Scraped.txt", true))
                    {
                      using (List<string>.Enumerator enumerator = Proxytools.Variables.Proxies.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          string current = enumerator.Current;
                          streamWriter.WriteLine(current);
                        }
                        break;
                      }
                    }
                  }
                  catch
                  {
                    Directory.CreateDirectory("Proxies/Scraped/" + Login.date);
                  }
                }
              }
            }
            Colorful.Console.Title = string.Format("ZeusAIO [v0.02] - YoBoi#9999 | Total Scraped: {1} | Duplicates removed: {2}", (object) Proxytools.Variables.Version, (object) Proxytools.Variables.total, (object) (Proxytools.Variables.total - Proxytools.Variables.unique));
          }
          catch (Exception ex)
          {
          }
          Colorful.Console.Title = string.Format("ZeusAIO [v0.02] - YoBoi#9999 | Total Scraped: {1} | Duplicates removed: {2}", (object) Proxytools.Variables.Version, (object) Proxytools.Variables.total, (object) (Proxytools.Variables.total - Proxytools.Variables.unique));
        }));
        System.Console.WriteLine();
        Colorful.Console.WriteLine("Scraped: " + Proxytools.Variables.total.ToString() + " | Done and Saved! | Press Enter to go back", Color.Magenta);
        System.Console.ReadLine();
        Proxytools.menu();
      }
      catch
      {
        Colorful.Console.WriteLine("Sources.txt not Found...", Color.Red);
        Thread.Sleep(700);
        Proxytools.menu();
      }
    }

    public class Variables
    {
      public static string Version = "0.02";
      public static int total = 0;
      public static int unique = 0;
      public static List<string> Proxies = new List<string>();
      public static Queue<string> ProxiesQueue = new Queue<string>();
    }
  }
}
