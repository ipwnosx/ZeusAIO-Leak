// Decompiled with JetBrains decompiler
// Type: ZeusAIO.Comboediter
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace ZeusAIO
{
  internal class Comboediter
  {
    public static void menu()
    {
      System.Console.Clear();
      Write.ascii();
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("1", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Email:Pass to User:Pass\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("2", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Domain Changer - Changes Domains\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("3", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Basic Edits\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("4", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Gaming Edits\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("5", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Numbers to Letters\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("6", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Back\n", Color.White);
      string str = Colorful.Console.ReadLine();
      if (str == "1")
        Comboediter.Emailpasstouserpass();
      else if (str == "2")
        Comboediter.Domainchanger();
      else if (str == "3")
        Comboediter.basicedit();
      else if (str == "4")
        Comboediter.gamingedits();
      else if (str == "5")
        Comboediter.numberstoletters();
      else if (str == "6")
        mainmenu.menu();
      else
        Comboediter.menu();
    }

    public static void Emailpasstouserpass()
    {
      try
      {
        string str = "[Edited] UserPass";
        string[] strArray1 = File.ReadAllLines("Comboediter\\needtobeedited.txt");
        Colorful.Console.WriteLine(">> Reading Combos from [Comboediter\\needtobeedited.txt]", Color.Magenta);
        Thread.Sleep(500);
        while (true)
        {
          try
          {
            using (StreamWriter streamWriter = new StreamWriter("Comboediter/Results/" + Login.date + "/" + str + ".txt"))
            {
              for (int index = 0; index < strArray1.Length; ++index)
              {
                try
                {
                  string[] strArray2 = strArray1[index].Split('@');
                  string[] strArray3 = strArray1[index].Split(':');
                  streamWriter.WriteLine(strArray2[0] + ":" + strArray3[1]);
                }
                catch (Exception ex)
                {
                  ++index;
                }
              }
              break;
            }
          }
          catch
          {
            Directory.CreateDirectory("Comboediter/Results/" + Login.date);
          }
        }
        Colorful.Console.WriteLine(">> Successfully Edited and saved [Comboediter/Results] ", Color.LawnGreen);
        System.Console.WriteLine();
        Colorful.Console.WriteLine(">> Press enter to go back to menu", Color.Magenta);
        System.Console.ReadLine();
        Comboediter.menu();
      }
      catch
      {
        Colorful.Console.WriteLine(">> Error -- Please Put ur combos [Make a folder called called [Comboediter] then make a txt file called [needtobeedited] === paste your combos here]", Color.Red);
        System.Console.WriteLine();
        System.Console.WriteLine(">> Press Enter to go back", (object) Color.LawnGreen);
        System.Console.ReadLine();
        Comboediter.menu();
      }
    }

    public static void Domainchanger()
    {
      string str1 = "[Edited] Domainchanged";
      Colorful.Console.WriteLine(">> Reading Combos from [Comboediter\\needtobeedited.txt]", Color.Magenta);
      Thread.Sleep(500);
      try
      {
        string str2 = File.ReadAllText("Comboediter\\needtobeedited.txt").Replace("@gmail", "@yahoo").Replace("@yahoo", "@protonmail").Replace("@outlook", "@gmail").Replace("@hotmail", "@yahoo");
        while (true)
        {
          try
          {
            using (StreamWriter streamWriter = new StreamWriter("Comboediter/Results/" + Login.date + "/" + str1 + ".txt"))
            {
              streamWriter.WriteLine(str2);
              break;
            }
          }
          catch
          {
            Directory.CreateDirectory("Comboediter/Results/" + Login.date);
          }
        }
        Colorful.Console.WriteLine(">> Successfully Edited and saved [Comboediter/Results] ", Color.LawnGreen);
        System.Console.WriteLine();
        Colorful.Console.WriteLine(">> Press enter to go back to menu", Color.Magenta);
        System.Console.ReadLine();
        Comboediter.menu();
      }
      catch
      {
        Colorful.Console.WriteLine(">> Error -- Please Put ur combos [Make a folder called called [Comboediter] then make a txt file called [needtobeedited] === paste your combos here]", Color.Red);
        System.Console.WriteLine();
        System.Console.WriteLine(">> Press Enter to go back", (object) Color.LawnGreen);
        System.Console.ReadLine();
        Comboediter.menu();
      }
    }

    public static void basicedit()
    {
      string str1 = "[Edited] BasicEdits";
      Colorful.Console.WriteLine(">> Reading Combos from [Comboediter\\needtobeedited.txt]", Color.Magenta);
      Thread.Sleep(500);
      try
      {
        string str2 = File.ReadAllText("Comboediter\\needtobeedited.txt").Replace("@gmail", "@yahoo").Replace("@yahoo", "@protonmail").Replace("@outlook", "@gmail").Replace("@hotmail", "@yahoo").Replace("123", "dwdwd").Replace("2", "wd").Replace("M", "D2").Replace("F", "WW").Replace("E", "@yahoo").Replace("WS", "SSS").Replace("C", "CCC").Replace("X", "WWWW").Replace("DW", "CCC").Replace("23323", "SSS").Replace("dwdwedfw", "RRR").Replace("!!!", "@@@").Replace("123", "dwdwd").Replace("2", "we2ed").Replace("W", "D2e2e2").Replace("FF", "qqW").Replace("E", "@y2e2eahoo").Replace("WS", "S22SS").Replace("C", "CC2e2eC").Replace("X", "@Se2e2eeW");
        while (true)
        {
          try
          {
            using (StreamWriter streamWriter = new StreamWriter("Comboediter/Results/" + Login.date + "/" + str1 + ".txt"))
            {
              streamWriter.WriteLine(str2);
              break;
            }
          }
          catch
          {
            Directory.CreateDirectory("Comboediter/Results/" + Login.date);
          }
        }
        Colorful.Console.WriteLine(">> Successfully Edited and saved [Comboediter/Results] ", Color.LawnGreen);
        System.Console.WriteLine();
        Colorful.Console.WriteLine(">> Press enter to go back to menu", Color.Magenta);
        System.Console.ReadLine();
        Comboediter.menu();
      }
      catch
      {
        Colorful.Console.WriteLine(">> Error -- Please Put ur combos [Make a folder called called [Comboediter] then make a txt file called [needtobeedited] === paste your combos here]", Color.Red);
        System.Console.WriteLine();
        System.Console.WriteLine(">> Press Enter to go back", (object) Color.LawnGreen);
        System.Console.ReadLine();
        Comboediter.menu();
      }
    }

    public static void gamingedits()
    {
      string str1 = "[Edited] GamingEdits";
      Colorful.Console.WriteLine(">> Reading Combos from [Comboediter\\needtobeedited.txt]", Color.Magenta);
      Thread.Sleep(500);
      try
      {
        string str2 = File.ReadAllText("Comboediter\\needtobeedited.txt").Replace("A", "B").Replace("C", "D").Replace("E", "F").Replace("G", "H").Replace("I", "J").Replace("K", "L").Replace("M", "N").Replace("O", "P").Replace("Q", "R").Replace("S", "T").Replace("U", "V").Replace("W", "X").Replace("Y", "Z").Replace("1", "2").Replace("3", "4").Replace("5", "6").Replace("7", "8").Replace("9", "10").Replace("@gmail", "@yahoo").Replace("@yahoo", "@protonmail").Replace("@outlook", "@gmail").Replace("@hotmail", "@yahoo");
        while (true)
        {
          try
          {
            using (StreamWriter streamWriter = new StreamWriter("Comboediter/Results/" + Login.date + "/" + str1 + ".txt"))
            {
              streamWriter.WriteLine(str2);
              break;
            }
          }
          catch
          {
            Directory.CreateDirectory("Comboediter/Results/" + Login.date);
          }
        }
        Colorful.Console.WriteLine(">> Successfully Edited and saved [Comboediter/Results] ", Color.LawnGreen);
        System.Console.WriteLine();
        Colorful.Console.WriteLine(">> Press enter to go back to menu", Color.Magenta);
        System.Console.ReadLine();
        Comboediter.menu();
      }
      catch
      {
        Colorful.Console.WriteLine(">> Error -- Please Put ur combos [Make a folder called called [Comboediter] then make a txt file called [needtobeedited] === paste your combos here]", Color.Red);
        System.Console.WriteLine();
        System.Console.WriteLine(">> Press Enter to go back", (object) Color.LawnGreen);
        System.Console.ReadLine();
        Comboediter.menu();
      }
    }

    public static void numberstoletters()
    {
      string str1 = "[Edited] NumberstoLetters";
      Colorful.Console.WriteLine(">> Reading Combos from [Comboediter\\needtobeedited.txt]", Color.Magenta);
      Thread.Sleep(500);
      try
      {
        string str2 = File.ReadAllText("Comboediter\\needtobeedited.txt").Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D").Replace("5", "E").Replace("6", "F").Replace("7", "G").Replace("8", "H").Replace("9", "I").Replace("10", "J").Replace("11", "K").Replace("12", "L").Replace("13", "M").Replace("14", "N").Replace("15", "O").Replace("16", "P").Replace("17", "Q").Replace("18", "R").Replace("19", "S").Replace("20", "T").Replace("21", "U").Replace("22", "V");
        while (true)
        {
          try
          {
            using (StreamWriter streamWriter = new StreamWriter("Comboediter/Results/" + Login.date + "/" + str1 + ".txt"))
            {
              streamWriter.WriteLine(str2);
              break;
            }
          }
          catch
          {
            Directory.CreateDirectory("Comboediter/Results/" + Login.date);
          }
        }
        Colorful.Console.WriteLine(">> Successfully Edited and saved [Comboediter/Results] ", Color.LawnGreen);
        System.Console.WriteLine();
        Colorful.Console.WriteLine(">> Press enter to go back to menu", Color.Magenta);
        System.Console.ReadLine();
        Comboediter.menu();
      }
      catch
      {
        Colorful.Console.WriteLine(">> Error -- Please Put ur combos [Make a folder called called [Comboediter] then make a txt file called [needtobeedited] === paste your combos here]", Color.Red);
        System.Console.WriteLine();
        System.Console.WriteLine(">> Press Enter to go back", (object) Color.LawnGreen);
        System.Console.ReadLine();
        Comboediter.menu();
      }
    }
  }
}
