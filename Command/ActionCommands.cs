using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using SlvyDiscordBot.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot.Command
{
    public class ActionCommands : BaseCommandModule
    {
        private readonly TenorApi tenorApi;

        public ActionCommands()
        {
            var configJson = File.ReadAllText("config.json");
            var config = JsonConvert.DeserializeObject<ConfigJSON>(configJson);

            tenorApi = new TenorApi(config.TenorApiToken);
        }

        [Command("hug")]
        public async Task Hug(CommandContext ctx)
        {
            string gifUrl = "https://tenor.com/view/hug-gif-25588769";

            string caption = "hug itself";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
        [Command("kiss")]
        public async Task Kiss(CommandContext ctx)
        {
            string gifUrl = "";

            string caption = "";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
        [Command("pat")]
        public async Task pat(CommandContext ctx)
        {
            string gifUrl = "";

            string caption = "";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
        [Command("slap")]
        public async Task Slap(CommandContext ctx)
        {
            string gifUrl = "";

            string caption = "";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
        [Command("punch")]
        public async Task Punch(CommandContext ctx)
        {
            string gifUrl = "";

            string caption = "";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
        [Command("pinch")]
        public async Task Pinch(CommandContext ctx)
        {
            string gifUrl = "";

            string caption = "";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
        [Command("pout")]
        public async Task Pout(CommandContext ctx)
        {
            string gifUrl = "";

            string caption = "";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
        [Command("happy")]
        public async Task Happy(CommandContext ctx)
        {
            string gifUrl = "";

            string caption = "";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
        [Command("sad")]
        public async Task sad(CommandContext ctx)
        {
            string gifUrl = "";

            string caption = "";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
        [Command("cry")]
        public async Task cry(CommandContext ctx)
        {
            string gifUrl = "";

            string caption = "";

            var embed = new DiscordEmbedBuilder()
                .WithImageUrl(gifUrl)
                .WithDescription(caption)
                .WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed);
        }
    }
}
