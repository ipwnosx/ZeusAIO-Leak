// Decompiled with JetBrains decompiler
// Type: ZeusAIO.InfoManager
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

namespace ZeusAIO
{
  internal class InfoManager
  {
    private System.Threading.Timer timer;
    private string lastGateway;

    public InfoManager() => this.lastGateway = this.GetGatewayMAC();

    public void StartListener() => this.timer = new System.Threading.Timer((TimerCallback) (_ => this.OnCallBack()), (object) null, 5000, -1);

    private void OnCallBack()
    {
      this.timer.Dispose();
      if (!(this.GetGatewayMAC() == this.lastGateway))
      {
        Constants.Breached = true;
        int num = (int) MessageBox.Show("ARP Cache poisoning has been detected!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
        Process.GetCurrentProcess().Kill();
      }
      else
        this.lastGateway = this.GetGatewayMAC();
      this.timer = new System.Threading.Timer((TimerCallback) (_ => this.OnCallBack()), (object) null, 5000, -1);
    }

    public static IPAddress GetDefaultGateway() => ((IEnumerable<IPAddress>) Enumerable.Where<IPAddress>((IEnumerable<M0>) Enumerable.Select<GatewayIPAddressInformation, IPAddress>((IEnumerable<M0>) Enumerable.SelectMany<NetworkInterface, GatewayIPAddressInformation>(Enumerable.Where<NetworkInterface>(Enumerable.Where<NetworkInterface>((IEnumerable<M0>) NetworkInterface.GetAllNetworkInterfaces(), (Func<M0, bool>) (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_0 = new Func<NetworkInterface, bool>((object) InfoManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetDefaultGateway\u003Eb__5_0))))), (Func<M0, bool>) (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_1 ?? (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_1 = new Func<NetworkInterface, bool>((object) InfoManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetDefaultGateway\u003Eb__5_1))))), (Func<M0, IEnumerable<M1>>) (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_2 ?? (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_2 = new Func<NetworkInterface, IEnumerable<GatewayIPAddressInformation>>((object) InfoManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetDefaultGateway\u003Eb__5_2))))), (Func<M0, M1>) (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_3 ?? (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_3 = new Func<GatewayIPAddressInformation, IPAddress>((object) InfoManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetDefaultGateway\u003Eb__5_3))))), (Func<M0, bool>) (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_4 ?? (InfoManager.\u003C\u003Ec.\u003C\u003E9__5_4 = new Func<IPAddress, bool>((object) InfoManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetDefaultGateway\u003Eb__5_4)))))).FirstOrDefault<IPAddress>();

    private string GetArpTable()
    {
      string pathRoot = Path.GetPathRoot(Environment.SystemDirectory);
      using (Process process = Process.Start(new ProcessStartInfo()
      {
        FileName = pathRoot + "Windows\\System32\\arp.exe",
        Arguments = "-a",
        UseShellExecute = false,
        RedirectStandardOutput = true
      }))
      {
        using (StreamReader standardOutput = process.StandardOutput)
          return standardOutput.ReadToEnd();
      }
    }

    private string GetGatewayMAC() => new Regex(string.Format("({0} [\\W]*) ([a-z0-9-]*)", (object) InfoManager.GetDefaultGateway().ToString())).Match(this.GetArpTable()).Groups[2].ToString();
  }
}
