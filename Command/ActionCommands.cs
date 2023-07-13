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
            //Set the search query parameters
            string searcQuery = "anime hug";
            int resultLimit = 3;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if(gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and warm hug to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No hug GIF's found at the moment");
            }
            
        }
        [Command("kiss")]
        public async Task Kiss(CommandContext ctx)
        {
            //Set the search query parameters
            string searcQuery = "anime kiss";
            int resultLimit = 10;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if (gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and warm kiss to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No kiss GIF's found at the moment");
            }
        }
        [Command("pat")]
        public async Task pat(CommandContext ctx)
        {
            //Set the search query parameters
            string searcQuery = "anime pat";
            int resultLimit = 10;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if (gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and warm pat to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No pat GIF's found at the moment");
            }
        }
        [Command("slap")]
        public async Task Slap(CommandContext ctx)
        {
            //Set the search query parameters
            string searcQuery = "anime slap";
            int resultLimit = 10;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if (gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and warm slap to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No slap GIF's found at the moment");
            }
        }
        [Command("punch")]
        public async Task Punch(CommandContext ctx)
        {
            //Set the search query parameters
            string searcQuery = "anime punch";
            int resultLimit = 10;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if (gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and warm punch to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No punch GIF's found at the moment");
            }
        }
        [Command("pinch")]
        public async Task Pinch(CommandContext ctx)
        {
            //Set the search query parameters
            string searcQuery = "anime pinch";
            int resultLimit = 10;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if (gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and warm pinch to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No pinch GIF's found at the moment");
            }
        }
        [Command("pout")]
        public async Task Pout(CommandContext ctx)
        {
            //Set the search query parameters
            string searcQuery = "anime pout";
            int resultLimit = 10;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if (gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and warm pout to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No pout GIF's found at the moment");
            }
        }
        [Command("happy")]
        public async Task Happy(CommandContext ctx)
        {
            //Set the search query parameters
            string searcQuery = "anime happy";
            int resultLimit = 10;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if (gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and warm happy to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No happy GIF's found at the moment");
            }
        }
        [Command("sad")]
        public async Task sad(CommandContext ctx)
        {
            //Set the search query parameters
            string searcQuery = "anime sad";
            int resultLimit = 10;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if (gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and warm sad to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No sad GIF's found at the moment");
            }
        }
        [Command("cry")]
        public async Task cry(CommandContext ctx)
        {
            //Set the search query parameters
            string searcQuery = "anime cry";
            int resultLimit = 10;


            var gifData = await tenorApi.SearchGifs(searcQuery, resultLimit);
            Console.WriteLine(gifData?.Results?.Count);

            if (gifData?.Results?.Count > 0)
            {
                // Randomly select a gif from the results
                var random = new Random();
                var randomGif = gifData.Results[random.Next(gifData.Results.Count)];

                //Create the embed with the GIF and caption

                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(randomGif.Media.First().Gif.Url)
                    .WithDescription("Uhh, Nice and cry hug to you!")
                    .WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.RespondAsync("Sorry, No cry GIF's found at the moment");
            }
        }
    }
}
