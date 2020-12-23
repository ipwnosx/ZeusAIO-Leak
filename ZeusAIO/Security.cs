// Decompiled with JetBrains decompiler
// Type: ZeusAIO.Security
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;

namespace ZeusAIO
{
  internal class Security
  {
    private const string _key = "046EECD33E469E9E1958D6BEEDE0A71843202724A5758BD1723F6C340C5E98EDE06FF5C21B35F359C65B850744729B3AA999B0B6392DA69EDB278EB31DBCE85774";

    public static string Signature(string value)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        return BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", "");
      }
    }

    private static string Session(int length) => new string(((IEnumerable<char>) Enumerable.Select<string, char>((IEnumerable<M0>) Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz", length), (Func<M0, M1>) new Func<string, char>((object) new ZeusAIO.Security.\u003C\u003Ec__DisplayClass1_0()
    {
      random = new Random()
    }, __methodptr(\u003CSession\u003Eb__0)))).ToArray<char>());

    public static string Obfuscate(int length) => new string(((IEnumerable<char>) Enumerable.Select<string, char>((IEnumerable<M0>) Enumerable.Repeat<string>("gd8JQ57nxXzLLMPrLylVhxoGnWGCFjO4knKTfRE6mVvdjug2NF/4aptAsZcdIGbAPmcx0O+ftU/KvMIjcfUnH3j+IMdhAW5OpoX3MrjQdf5AAP97tTB5g1wdDSAqKpq9gw06t3VaqMWZHKtPSuAXy0kkZRsc+DicpcY8E9+vWMHXa3jMdbPx4YES0p66GzhqLd/heA2zMvX8iWv4wK7S3QKIW/a9dD4ALZJpmcr9OOE=", length), (Func<M0, M1>) new Func<string, char>((object) new ZeusAIO.Security.\u003C\u003Ec__DisplayClass2_0()
    {
      random = new Random()
    }, __methodptr(\u003CObfuscate\u003Eb__0)))).ToArray<char>());

    public static void Start()
    {
      string pathRoot = Path.GetPathRoot(Environment.SystemDirectory);
      if (Constants.Started)
      {
        int num = (int) MessageBox.Show("A session has already been started, please end the previous one!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        Process.GetCurrentProcess().Kill();
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(pathRoot + "Windows\\System32\\drivers\\etc\\hosts"))
        {
          if (streamReader.ReadToEnd().Contains("api.auth.gg"))
          {
            Constants.Breached = true;
            int num = (int) MessageBox.Show("DNS redirecting has been detected!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
            Process.GetCurrentProcess().Kill();
          }
        }
        new InfoManager().StartListener();
        Constants.Token = Guid.NewGuid().ToString();
        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ZeusAIO.Security.PinPublicKey);
        Constants.APIENCRYPTKEY = Convert.ToBase64String(Encoding.Default.GetBytes(ZeusAIO.Security.Session(32)));
        Constants.APIENCRYPTSALT = Convert.ToBase64String(Encoding.Default.GetBytes(ZeusAIO.Security.Session(16)));
        Constants.IV = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(16)));
        Constants.Key = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(32)));
        Constants.Started = true;
      }
    }

    public static void End()
    {
      if (!Constants.Started)
      {
        int num = (int) MessageBox.Show("No session has been started, closing for security reasons!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        Process.GetCurrentProcess().Kill();
      }
      else
      {
        Constants.Token = (string) null;
        ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) ((_param1, _param2, _param3, _param4) => true);
        Constants.APIENCRYPTKEY = (string) null;
        Constants.APIENCRYPTSALT = (string) null;
        Constants.IV = (string) null;
        Constants.Key = (string) null;
        Constants.Started = false;
      }
    }

    private static bool PinPublicKey(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
      return certificate != null && certificate.GetPublicKeyString() == "046EECD33E469E9E1958D6BEEDE0A71843202724A5758BD1723F6C340C5E98EDE06FF5C21B35F359C65B850744729B3AA999B0B6392DA69EDB278EB31DBCE85774";
    }

    public static string Integrity(string filename)
    {
      string lowerInvariant;
      using (MD5 md5 = MD5.Create())
      {
        using (FileStream fileStream = System.IO.File.OpenRead(filename))
          lowerInvariant = BitConverter.ToString(md5.ComputeHash((Stream) fileStream)).Replace("-", "").ToLowerInvariant();
      }
      return lowerInvariant;
    }

    public static bool MaliciousCheck(string date)
    {
      TimeSpan timeSpan = DateTime.Parse(date) - DateTime.Now;
      if (Convert.ToInt32(timeSpan.Seconds.ToString().Replace("-", "")) < 5 && Convert.ToInt32(timeSpan.Minutes.ToString().Replace("-", "")) < 1)
        return false;
      Constants.Breached = true;
      return true;
    }
  }
}
