using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot.Command
{
    public class ImageCommands : BaseCommandModule
    {
        private readonly HttpClient httpClient;
        public ImageCommands() 
        { 
            this.httpClient = new HttpClient();
        }

        [Command("meme")]
        public async Task Meme(CommandContext ctx)
        {
           
            string apiUrl = "https://www.reddit.com/r/memes/random.json?limit=1";
            
            try
            {

                httpClient.DefaultRequestHeaders.Add("User-Agent", "DiscordBot");

                //Send the HTtP request to Reddit API
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                //Parse the JSON response
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<RedditPost[]>(jsonResponse);
                
                if(posts?.Length > 0)
                {
                    //Get the URL of the meme image
                    var memeUrl = posts[0].Data.Url;

                    var embed = new DiscordEmbedBuilder()
                        .WithImageUrl(memeUrl)
                        .WithColor(DiscordColor.Green);

                    await ctx.RespondAsync(embed: embed);
                }
                else
                {
                    await ctx.RespondAsync("Sorry, couldn't fetch a meme at the moment");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error fetching meme: {ex.Message}");
                await ctx.RespondAsync("Oops! Something went wrong while fetching the meme");
            }


        }
        public class RedditPost
        {
            public RedditPostData Data { get; set; }
        }
        public class RedditPostData
        {
            public string Url { get; set; }
        }
    }
}
