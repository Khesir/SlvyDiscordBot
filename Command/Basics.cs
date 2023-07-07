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
        
        public async Task helpFunction(CommandContext ctx)
        {
            var helpEmbed = new DiscordEmbedBuilder()
            {
                Title = "Help",
                Description = "This is the list of commands available to SlvyBot",
                Color = DiscordColor.Blue,
                
            };
            helpEmbed.AddField("Field Title", "Just Random shit").WithAuthor(ctx.Client.CurrentUser.Username).WithImageUrl("https://images.search.yahoo.com/search/images;_ylt=AwrOsjYStKNk9IIo0x.JzbkF;_ylu=c2VjA3NlYXJjaARzbGsDYnV0dG9u;_ylc=X1MDOTYwNjI4NTcEX3IDMgRmcgNtY2FmZWUEZnIyA3A6cyx2OmksbTpzYi10b3AEZ3ByaWQDBG5fcnNsdAMwBG5fc3VnZwMwBG9yaWdpbgNpbWFnZXMuc2VhcmNoLnlhaG9vLmNvbQRwb3MDMARwcXN0cgMEcHFzdHJsAzAEcXN0cmwDOQRxdWVyeQNmYXRlJTIwZmFjZQR0X3N0bXADMTY4ODQ1MDA3OA--?p=fate+face&fr=mcafee&fr2=p%3As%2Cv%3Ai%2Cm%3Asb-top&ei=UTF-8&x=wrt&type=E210US91215G0#id=5&iurl=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F45%2F88%2Fb2%2F4588b2cbf6e03d133ddbc540661c1203.jpg&action=click");

            await ctx.Channel.SendMessageAsync(embed: helpEmbed);

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
