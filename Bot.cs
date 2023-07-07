using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using Newtonsoft.Json;
using SlvyDiscordBot.Command;
using SlvyDiscordBot.Slash_Commands;
//using SlvyDiscordBot.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;

namespace SlvyDiscordBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            // Setting out and putting a config.json
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);


            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });
            //Client.Ready += OnReady;
            // Commands Configuration
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            // Register commands
            Commands = Client.UseCommandsNext(commandsConfig);
            var slashCommandsConfig = Client.UseSlashCommands();


            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<GameCommand>();
            Commands.RegisterCommands<Basics>();

            slashCommandsConfig.RegisterCommands<Funslc>();
            // Config of Lavalink
            var endpoint = new ConnectionEndpoint
            {
                Hostname = "lavalink.devamop.in",
                Port = 443,
                Secured = true,
            };

            var lavalinkConfig = new LavalinkConfiguration
            {
                Password = "DevamOP",
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint,
            };

            var lavalink = Client.UseLavalink();



            // Connect to make the bot online
            
            await Client.ConnectAsync();
            await lavalink.ConnectAsync(lavalinkConfig);
            await Task.Delay(-1);
        }
        private async Task OnReady(ReadyEventArgs e)
        {
            await Client.UpdateStatusAsync(new DiscordActivity(">help ", ActivityType.Playing), UserStatus.DoNotDisturb);
        }
        
    }
}
