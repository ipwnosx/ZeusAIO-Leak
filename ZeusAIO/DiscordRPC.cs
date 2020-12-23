// Decompiled with JetBrains decompiler
// Type: ZeusAIO.DiscordRPC
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using DiscordRPC;

namespace ZeusAIO
{
  internal class DiscordRPC
  {
    public static DiscordRpcClient client;

    public static void Initialize()
    {
      ZeusAIO.DiscordRPC.client = new DiscordRpcClient("757059293248880640");
      ZeusAIO.DiscordRPC.client.Initialize();
      ZeusAIO.DiscordRPC.client.SetPresence(new RichPresence()
      {
        Details = "The Best AIO",
        State = "Invite.gg/zeusaio",
        Timestamps = Timestamps.Now,
        Assets = new Assets()
        {
          LargeImageKey = "zeus_2",
          LargeImageText = "Coded by YoBoi#9999"
        }
      });
    }
  }
}
