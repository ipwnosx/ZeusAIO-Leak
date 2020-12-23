// Decompiled with JetBrains decompiler
// Type: ZeusAIO.Config
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Colorful;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Threading;

namespace ZeusAIO
{
  internal class Config
  {
    public static Config.configObject config = new Config.configObject();

    public static void printLogo()
    {
      Console.Clear();
      Write.ascii();
      Console.WriteLine("");
      Console.Write("[>>] ", Color.LawnGreen);
    }

    public static Config.configObject renewconfig(bool AskToSave)
    {
      Config.printLogo();
      Console.WriteLine("Please Enter you Discord ID [Than You can do [/s] for Stats on Discord]");
      Config.config.DiscordID = Console.ReadLine();
      Config.printLogo();
      Console.WriteLine("Choose your Zeus experience | [1] for CUI | [2] for LOG");
      Config.config.LogorCui = Console.ReadLine();
      Config.printLogo();
      Console.WriteLine("Please Enter your Desired refresh rate [For CUI Mode/ how fast the menu Refreshes] [1000 Recomeneded]");
      Config.config.RefreshRate = int.Parse(Console.ReadLine());
      Config.printLogo();
      Console.WriteLine("Please Enter your Desired Retries Amount [More =  Slower but no Skips]  - [2 Recomeneded]");
      Config.config.Retries = int.Parse(Console.ReadLine());
      Config.printLogo();
      File.WriteAllText("config.json", JsonConvert.SerializeObject((object) Config.config));
      Console.WriteLine("Config saved!", Color.LawnGreen);
      Thread.Sleep(300);
      mainmenu.ChooseUI();
      return Config.config;
    }

    public class configObject
    {
      public string LogorCui { get; set; }

      public string DiscordID { get; set; }

      public int RefreshRate { get; set; }

      public int Retries { get; set; }
    }
  }
}
