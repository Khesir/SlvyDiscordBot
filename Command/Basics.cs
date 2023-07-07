using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot.Command
{
    public class Basics : BaseCommandModule
    {

        // 
        
        [Command("help")]
        //[Cooldown(5, 10, CooldownBucketType.User)]
        public async Task helpFunction(CommandContext ctx)
        {
            var helpMessage = new DiscordMessageBuilder().AddEmbed(new DiscordEmbedBuilder()
                
                .WithTitle("Slvy's Command List")
                
                .WithColor(DiscordColor.White)
                .WithDescription("Slash Commands are applicable to all commands. To find specific command guide write `>help <command>`" +
                ", this applies to `/help` command. To find more info about the commands [click here](https://www.youtube.com/watch?v=dQw4w9WgXcQ)." +
                "\n\n`*still in development most of the command does not work.*`")
                .AddField("Basic Commands","`help` `pool` `profile`", false)
                .AddField("Fun Commands", "`drawcard` `blackjack` `animerate` `froghops`", false)
                .AddField("Action Commands", "`hug` `kiss` `pat` `slap` `punch` `pinch` `pout` `happy` `sad` `cry`", false)
                .AddField("Game Commands", "`still in development`", false)
                .AddField("Image Commands", "`search` `meme` `anime` `nsfw`", false)
                .AddField("Modeation", "`mute` `unmute` `ban` `unban`", false)
                .AddField("Music", "`play` `pause` `stop` `current` `skip` `queue`", false)
                .AddField("More Commands to Add", "soon to be developed: \n "
                + "- Twitter api (notification) \n" +
                "- Youtube Api (notification) \n" +
                "- Facebook Api (notification) \n"+
                "- Chat gpt query \n"+
                "- Image Search and Google Search\n"+
                "- Rpg game system\n"+
                "- Leveling system\n"+
                "- interactivty Buttons\n" +
                "- Moderation", false)
                .WithFooter("Arigato mastah! " + DiscordEmoji.FromName(ctx.Client, ":heart:", false))
                .WithThumbnail(ctx.Client.CurrentUser.AvatarUrl)
                );

            await ctx.Channel.SendMessageAsync(helpMessage);

        }


        [Command("play")]
        public async Task Playmusic(CommandContext ctx,[RemainingText] string query)
        {
            // Checker for if the bot has already joined the call or not
            // Check if the user is in the voice channel
            var userVC = ctx.Member?.VoiceState.Channel;
            var lavalinkInstance = ctx.Client.GetLavalink();


            //Pre-execution Checks
            if(ctx.Member.VoiceState == null || userVC == null)
            {
                await ctx.Channel.SendMessageAsync("Please enter a vc");
                return;
            }
            if(!lavalinkInstance.ConnectedNodes.Any())
            {
                await ctx.Channel.SendMessageAsync("Connection is not Established.");
                return;
            }
            if(userVC.Type != DSharpPlus.ChannelType.Voice)
            {
                await ctx.Channel.SendMessageAsync("Please Enter a valid VC");
                return;
            }

            // Connecting to the VC and playing music
            var lavaNode = lavalinkInstance.ConnectedNodes.Values.FirstOrDefault();
            // Makes the th
            await lavaNode.ConnectAsync(userVC);

            var con = lavaNode.GetGuildConnection(ctx.Member.VoiceState.Guild);
            if(con == null)
            {
                await ctx.Channel.SendMessageAsync("Failed to connect");
                return;
            }

            //This is the load result
            var searchQuery = await lavaNode.Rest.GetTracksAsync(query); 
            if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches || searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed)
            {
                await ctx.Channel.SendMessageAsync($"No tracks found");
                return;
            }

            // Playing the music
            var musicTrack = searchQuery.Tracks.First();
            await con.PlayAsync(musicTrack);

            //Embed message
            string musicDescription = $"Now Playing: {musicTrack.Title} \n" +
                                      $"Author: {musicTrack.Author} \n" +
                                      $"URL: {musicTrack.Uri}";
            var nowPlayingEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Purple,
                Title = $"Successfully joined channel {userVC.Name} : and playing music",
                Description = musicDescription
            };
            await ctx.Channel.SendMessageAsync(embed: nowPlayingEmbed);
            //await Task.Delay(searchQuery.Tracks.First());
        }
        [Command("pause")]
        public async Task PauseMusic(CommandContext ctx)
        {
            var userVC = ctx.Member.VoiceState.Channel;
            var lavalinkInstance = ctx.Client.GetLavalink();


            //Pre-execution Checks
            if (ctx.Member.VoiceState == null || userVC == null)
            {
                await ctx.Channel.SendMessageAsync("Please enter a vc");
                return;
            }
            if (!lavalinkInstance.ConnectedNodes.Any())
            {
                await ctx.Channel.SendMessageAsync("Connection is not Established.");
                return;
            }
            if (userVC.Type != DSharpPlus.ChannelType.Voice)
            {
                await ctx.Channel.SendMessageAsync("Please Enter a valid VC");
                return;
            }

            var node = lavalinkInstance.ConnectedNodes.Values.First();
            var con = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if( con == null )
            {
                await ctx.Channel.SendMessageAsync("Failed to connect");
                return;
            }

            if(con.CurrentState.CurrentTrack == null)
            {
                await ctx.Channel.SendMessageAsync("No tracks are playing");
                return;
            }

            await con.PauseAsync();

            var pauseEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Aquamarine,
                Title = "Track Paused"
            };

            await ctx.Channel.SendMessageAsync(embed: pauseEmbed);
        }
        [Command("resume")]
        public async Task ResumeMusic(CommandContext ctx)
        {
            var userVC = ctx.Member.VoiceState.Channel;
            var lavalinkInstance = ctx.Client.GetLavalink();


            //Pre-execution Checks
            if (ctx.Member.VoiceState == null || userVC == null)
            {
                await ctx.Channel.SendMessageAsync("Please enter a vc");
                return;
            }
            if (!lavalinkInstance.ConnectedNodes.Any())
            {
                await ctx.Channel.SendMessageAsync("Connection is not Established.");
                return;
            }
            if (userVC.Type != DSharpPlus.ChannelType.Voice)
            {
                await ctx.Channel.SendMessageAsync("Please Enter a valid VC");
                return;
            }

            var node = lavalinkInstance.ConnectedNodes.Values.First();
            var con = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (con == null)
            {
                await ctx.Channel.SendMessageAsync("Failed to connect");
                return;
            }

            if (con.CurrentState.CurrentTrack == null)
            {
                await ctx.Channel.SendMessageAsync("No tracks are playing");
                return;
            }

            await con.ResumeAsync();

            var resumeEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Green,
                Title = "Track Resumed"
            };

            await ctx.Channel.SendMessageAsync(embed: resumeEmbed);
        }
        [Command("stop")]
        public async Task stopMusic(CommandContext ctx)
        {
            var userVC = ctx.Member.VoiceState.Channel;
            var lavalinkInstance = ctx.Client.GetLavalink();


            //Pre-execution Checks
            if (ctx.Member.VoiceState == null || userVC == null)
            {
                await ctx.Channel.SendMessageAsync("Please enter a vc");
                return;
            }
            if (!lavalinkInstance.ConnectedNodes.Any())
            {
                await ctx.Channel.SendMessageAsync("Connection is not Established.");
                return;
            }
            if (userVC.Type != DSharpPlus.ChannelType.Voice)
            {
                await ctx.Channel.SendMessageAsync("Please Enter a valid VC");
                return;
            }

            var node = lavalinkInstance.ConnectedNodes.Values.First();
            var con = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (con == null)
            {
                await ctx.Channel.SendMessageAsync("Failed to connect");
                return;
            }

            if (con.CurrentState.CurrentTrack == null)
            {
                await ctx.Channel.SendMessageAsync("No tracks are playing");
                return;
            }

            await con.StopAsync();
            await con.DisconnectAsync();

            var stopEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Aquamarine,
                Title = "Stopped the Track",
                Description = "Successfully disconnected from the VC"
            };

            await ctx.Channel.SendMessageAsync(embed: stopEmbed);
        }
    }
}
