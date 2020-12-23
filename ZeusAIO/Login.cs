// Decompiled with JetBrains decompiler
// Type: ZeusAIO.Login
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ZeusAIO
{
  internal class Login
  {
    public static string date = DateTime.Now.ToString("MM-dd-yyyy H.mm");
    public static string username = "";

    [STAThread]
    public static void Main()
    {
      mainmenu.pickedModulesNames.Add("Idle");
      DiscordRPC.Initialize();
      Colorful.Console.Title = "ZeusAIO [v0.02] - YoBoi#9999";
      Colorful.Console.WriteLine("Please read this, this program is purely a demonstration of the use/flaws of the Modules API \r\nUse this solely for educational purposes, credential stuffing is forbidden !");
      Colorful.Console.WriteLine("");
      Colorful.Console.WriteLine("The author of this tool can't be held responsible for any misuse of this program.\r\nEnd - User License Agreement(EULA) of ZeusAIO");
      Colorful.Console.WriteLine("");
      Colorful.Console.WriteLine("This End-User License Agreement (EULA) is a legal agreement between you and ZeusAIO\r\n\r\nThis EULA agreement governs your acquisition and use of our YoBoi.Dev software(ZeusAIO) directly from YoBoi.Dev or indirectly through a YoBoi.Dev authorized reseller or distributor(a Reseller)\r\n\r\nPlease read this EULA agreement carefully before clicking Agree. It provides a license to use the YoBoi.Dev software [ZeusAIO] and contains warranty information and liability disclaimers\r\n\r\nBy clicking <accept> or installing and/or using the YoBoi.Dev software [ZeusAIO], you are confirming your acceptance of the Software and agreeing to become bound by the terms of this EULA agreement\r\n\r\nIf you are entering into this EULA agreement on behalf of a company or other legal entity, you represent that you have the authority to bind such entity and its affiliates to these terms and conditions. If you do not have such authority or if you do not agree with the terms and conditions of this EULA agreement, do not install or use the Software, and you must not accept this EULA agreement.\r\n\r\nThis EULA agreement shall apply only to the Software supplied by YoBoi.Dev herewith regardless of whether other software is referred to or described herein. The terms also apply to any ZeusAIO [Software] updates, supplements, Internet-based services, and support services for the Software, unless other terms accompany those items on delivery. If so, those terms apply. License Grant\r\n\r\nYou are not permitted to:\r\n\r\n Edit, alter, modify, adapt, translate or otherwise change the whole or any part of the Software nor permit the whole or any part of the Software to be combined with or become incorporated in any other software, nor decompile, disassemble or reverse engineer the Software or attempt to do any such thingsReproduce, copy, distribute, resell or otherwise use the Software for any commercial purposeAllow any third party to use the Software on behalf of or for the benefit of any third partyUse the Software [ZeusAIO] in any way which breaches any applicable local, national or international law use the Software for any purpose that YoBoi.Dev considers is a breach of this EULA agreement\r\n\r\nThis software [ZeusAIO] is for educational purposes only and shall never be used for any illegal activity (such as credential stuffing.)\r\n\r\nIntellectual Property and Ownership\r\n\r\nYoBoi.Dev shall at all times retain ownership of the Software [ZeusAIO] as originally downloaded by you and all subsequent downloads of the Software by you. The Software (and the copyright, and other intellectual property rights of whatever nature in the Software, including any modifications made thereto) are and shall remain the property of YoBoi.Dev\r\n\r\nYoBoi.Dev reserves the right to grant licences to use the Software to third parties.\r\n\r\nTermination\r\n\r\nThis EULA agreement is effective from the date you first use the Software and shall continue until terminated. You may terminate it at any time upon written notice to YoBoi.Dev\r\n\r\nIt will also terminate immediately if you fail to comply with any term of this EULA agreement. Upon such termination, the licenses granted by this EULA agreement will immediately terminate and you agree to stop all access and use of the Software. The provisions that by their nature continue and survive will survive any termination of this EULA agreement. Governing Law\r\n\r\nThis EULA agreement, and any dispute arising out of or in connection with this EULA agreement, shall be governed by and construed in accordance with the laws of Canada.\r\n\r\nChange\r\n\r\nThis EULA is subject to change at any time, to continue using YoBoi.Dev Software [ZeusAIO] you will need to agree on each versions of the EULA\r\n\r\nTHIS SOFTWARE IS PROVIDED AS IS AND ANY EXPRESSED OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE REGENTS OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.\r\n\r\nBY CLICKING OKAY YOU AGREE TO RESPECT THESE RULES.\r\n\r\nAccount Checker Modules IS RESTRICTED TO YOUR Website You Have Permissions For, Cracking Accounts From Websites You Dont Have Permission From is OBVIOUSLY FORBIDDEN By pressing [ENTER] you agree not to use this program for the reasons specified earlier.");
      string str = Colorful.Console.ReadLine();
      if (str.Equals("X", StringComparison.OrdinalIgnoreCase) || str.Equals("yes", StringComparison.OrdinalIgnoreCase))
        Environment.Exit(0);
      else
        Login.check();
    }

    public static void BotShits()
    {
      try
      {
        TaskAwaiter awaiter = new DiscordBot().MainAsync().GetAwaiter();
        ((TaskAwaiter) ref awaiter).GetResult();
      }
      catch
      {
        Colorful.Console.Clear();
        Colorful.Console.WriteLine("Bot Could not be Enabled... [Retrying]", Color.Red);
        Thread.Sleep(2000);
        Colorful.Console.Clear();
        mainmenu.menu();
      }
    }

    public static void check()
    {
      Colorful.Console.Clear();
      OnProgramStart.Initialize("ZeusAIO", "204352", "QkuTu35JjWIDWREQnOUsafYggD8zImsmygi", "2.0");
      if (File.Exists("LoginDetails.xml"))
      {
        foreach (string readAllLine in File.ReadAllLines("LoginDetails.xml"))
        {
          char[] chArray = new char[1]{ ':' };
          string[] strArray = readAllLine.Split(chArray);
          Login.username = strArray[0];
          if (API.Login(strArray[0], strArray[1]))
          {
            Colorful.Console.Clear();
            Write.ascii();
            Colorful.Console.WriteLine(">> Trying to auto login from login.xml", Color.WhiteSmoke);
            Thread.Sleep(500);
            Colorful.Console.WriteLine();
            Colorful.Console.Write(">> Welcome Back ", Color.WhiteSmoke);
            Colorful.Console.Write(strArray[0], Color.Red);
            Thread.Sleep(2000);
            mainmenu.menu();
          }
          else
            Login.check();
        }
      }
      Write.ascii();
      Colorful.Console.WriteLine();
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("1", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Login\n", Color.White);
      Colorful.Console.Write("[", Color.Lavender);
      Colorful.Console.Write("2", Color.LawnGreen);
      Colorful.Console.Write("]", Color.Lavender);
      Colorful.Console.Write(" Register\n", Color.White);
      Colorful.Console.WriteLine();
      while (!Colorful.Console.KeyAvailable)
      {
        ConsoleKeyInfo consoleKeyInfo = Colorful.Console.ReadKey();
        if (consoleKeyInfo.Key == ConsoleKey.D1)
        {
          Colorful.Console.Clear();
          Write.ascii();
          Colorful.Console.WriteLine();
          Colorful.Console.WriteLine("Please enter your username :");
          string username = Colorful.Console.ReadLine();
          Colorful.Console.WriteLine();
          Colorful.Console.WriteLine("Please enter your password :");
          string password = Colorful.Console.ReadLine();
          try
          {
            if (API.Login(username, password))
            {
              Colorful.Console.WriteLine();
              Colorful.Console.WriteLine("Contacting ZeusAIO.xyz Servers to authenticate...", Color.DarkMagenta);
              Thread.Sleep(5000);
              Colorful.Console.WriteLine();
              Colorful.Console.Write("Welcome Back ", Color.Green);
              Colorful.Console.Write(username, Color.Red);
              Thread.Sleep(700);
              using (StreamWriter streamWriter = new StreamWriter("LoginDetails.xml", true))
                streamWriter.WriteLine(username + ":" + password);
              Colorful.Console.Clear();
              mainmenu.menu();
            }
            else
            {
              Colorful.Console.Clear();
              Environment.Exit(0);
            }
          }
          catch
          {
            Thread.Sleep(5000);
            Colorful.Console.WriteLine();
            Thread.Sleep(5000);
            Colorful.Console.Clear();
            Login.check();
          }
        }
        else if (consoleKeyInfo.Key == ConsoleKey.D2)
        {
          Colorful.Console.Clear();
          Write.ascii();
          Colorful.Console.WriteLine();
          Colorful.Console.WriteLine(" Username: ");
          string username = Colorful.Console.ReadLine();
          Colorful.Console.WriteLine();
          Colorful.Console.WriteLine(" Password: ");
          string password = Colorful.Console.ReadLine();
          Colorful.Console.WriteLine();
          Colorful.Console.WriteLine(" Email: ");
          string email = Colorful.Console.ReadLine();
          Colorful.Console.WriteLine();
          Colorful.Console.WriteLine(" License: ");
          string license = Colorful.Console.ReadLine();
          try
          {
            if (API.Register(username, password, email, license))
            {
              Colorful.Console.WriteLine();
              Colorful.Console.WriteLine("Contacting ZeusAIO.xyz Servers to authenticate...", Color.DarkMagenta);
              Thread.Sleep(5000);
              Colorful.Console.WriteLine();
              Colorful.Console.WriteLine("SuccessFully Registered " + username, Color.Green);
              Thread.Sleep(2000);
              Colorful.Console.Clear();
              Login.check();
            }
          }
          catch
          {
            Colorful.Console.Write("Wrong License!", Color.DarkRed);
            Thread.Sleep(5000);
            Colorful.Console.Clear();
            Login.check();
          }
        }
        else
        {
          Colorful.Console.Clear();
          Login.check();
        }
      }
    }
  }
}
