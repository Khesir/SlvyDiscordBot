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
using SlvyDiscordBot.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.CommandsNext.Attributes;

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
                Token = configJson.DiscordToken,
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
                StringPrefixes = new string[] { configJson.BotPrefix },
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
            Commands.RegisterCommands<Music>();
            Commands.RegisterCommands<ActionCommands>();

            slashCommandsConfig.RegisterCommands<Funslc>();

            Commands.CommandErrored += OnCommandError;
            Client.Ready += OnReady;
            // Config of Lavalink
            var endpoint = new ConnectionEndpoint
            {
                Hostname = "ssl.horizxon.studio",
                Port = 443,
                Secured = true,
            };

            var lavalinkConfig = new LavalinkConfiguration
            {
                Password = "horizxon.studio",
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint,
            };

            var lavalink = Client.UseLavalink();



            // Connect to make the bot online
            
            await Client.ConnectAsync();
            await lavalink.ConnectAsync(lavalinkConfig);
            await Task.Delay(-1);
        }

        private async Task OnReady(DiscordClient sender, ReadyEventArgs args)
        {
            await Client.UpdateStatusAsync(new DiscordActivity(">help ", ActivityType.Playing), UserStatus.DoNotDisturb);
        }

        private async Task OnCommandError(CommandsNextExtension sender, CommandErrorEventArgs args)
        {
            if(args.Exception is ChecksFailedException)
            {
                var castedException = (ChecksFailedException)args.Exception;
                string cooldownTimer = string.Empty;

                foreach(var check in castedException.FailedChecks)
                {
                    var cooldown = (CooldownAttribute)check;
                    TimeSpan timeleft = cooldown.GetRemainingCooldown(args.Context);
                    cooldownTimer = timeleft.ToString(@"hh\:mm\:ss");
                }
                var cooldownMessage = new DiscordEmbedBuilder()
                {
                    Title = "Wait for the Cooldown to End",
                    Description = "Remaining Time: " + cooldownTimer,
                    Color = DiscordColor.Red
                };
                await args.Context.Channel.SendMessageAsync(embed: cooldownMessage);
            }
        }

        private Task OnClientReady (ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
        
    }
}
