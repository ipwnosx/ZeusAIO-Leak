// Decompiled with JetBrains decompiler
// Type: ZeusAIO.DiscordBot
// Assembly: ZeusAIO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70786947-2129-410F-AE9A-C082629DAC36
// Assembly location: C:\Users\ofekt\Desktop\סורס\ZeusAIO.exe

using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZeusAIO
{
  internal class DiscordBot
  {
    private static List<string> pickedModulesNames1 = new List<string>();
    private DiscordSocketClient _client;

    public async Task MainAsync()
    {
      this._client = new DiscordSocketClient();
      // ISSUE: method pointer
      this._client.MessageReceived += new Func<SocketMessage, Task>((object) null, __methodptr(CommandHandler));
      string token = "NzU5NDk3NDA5MjU2MjI2ODY4.X2-XFQ.uRUmG2yKSdDSVeUb-ZOKj1FtZVE";
      await this._client.LoginAsync(Discord.TokenType.Bot, token);
      await this._client.StartAsync();
      await Task.Delay(-1);
      token = (string) null;
    }

    private static Task CommandHandler(SocketMessage message)
    {
      if (!message.Content.StartsWith("/") || message.Author.IsBot)
        return Task.get_CompletedTask();
      int num = !message.Content.Contains(" ") ? message.Content.Length : message.Content.IndexOf(' ');
      string lower = message.Content.Substring(1, num - 1).ToLower();
      if (lower.Equals("s") || lower.Equals("stats"))
      {
        string str = message.Author.Id.ToString();
        if (Config.config.DiscordID == str)
        {
          EmbedBuilder embedBuilder = new EmbedBuilder();
          embedBuilder.WithTitle("ZeusAIO Stats for " + Login.username);
          embedBuilder.AddField("Hits", (object) mainmenu.hits, true);
          embedBuilder.AddField("Frees", (object) mainmenu.frees, true);
          embedBuilder.AddField("Bads", (object) (mainmenu.checks - mainmenu.hits - mainmenu.frees), true);
          embedBuilder.AddField("Checked", (object) (mainmenu.checks.ToString() + "/" + mainmenu.comboTotal.ToString()), true);
          embedBuilder.AddField("Error", (object) mainmenu.errors, true);
          embedBuilder.AddField("Cpm", (object) (mainmenu.cpm * 60), true);
          embedBuilder.AddField("Modules Selected", (object) string.Join(", ", (IEnumerable<string>) mainmenu.pickedModulesNames), true);
          embedBuilder.WithThumbnailUrl("http://zeusaio.xyz");
          embedBuilder.WithColor(Color.Green);
          message.Channel.SendMessageAsync("", false, embedBuilder.Build(), (RequestOptions) null);
        }
      }
      else if (lower.Equals("exit"))
      {
        string str = message.Author.Id.ToString();
        if (Config.config.DiscordID == str)
        {
          EmbedBuilder embedBuilder = new EmbedBuilder();
          embedBuilder.WithTitle("ZeusAIO for " + Login.username);
          embedBuilder.AddField("Success", (object) "ZeusAIO is now closed...", true);
          embedBuilder.WithThumbnailUrl("http://zeusaio.xyz");
          embedBuilder.WithColor(Color.Green);
          message.Channel.SendMessageAsync("", false, embedBuilder.Build(), (RequestOptions) null);
          Thread.Sleep(50);
          Environment.Exit(0);
        }
      }
      return Task.get_CompletedTask();
    }
  }
}
