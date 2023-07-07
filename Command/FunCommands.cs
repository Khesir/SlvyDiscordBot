using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot.Command
{
    public class FunCommands : BaseCommandModule
    {
        [Command("Hi")]
        public async Task GreetCommand(CommandContext ctx)
        {
            //Place where we put our commands
            await ctx.Channel.SendMessageAsync("Hello");
        }

        [Command("embedmessage1")]
        public async Task SendEmbedMessage(CommandContext ctx)
        {
            var embedMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("This is a Title")
                .WithDescription("This is the Description")
                
                );
            await ctx.Channel.SendMessageAsync(embedMessage);
        }

        [Command("poll")]
        public async Task PollCommand(CommandContext ctx, int TimeLimit, string Option1, string Option2, string Option3, string Option4, params string[] Question)
        {
            var interactvity = ctx.Client.GetInteractivity();
            TimeSpan timer = TimeSpan.FromSeconds(TimeLimit);
            DiscordEmoji[] optionEmojis = { DiscordEmoji.FromName(ctx.Client, ":one:", false),
                                            DiscordEmoji.FromName(ctx.Client, ":two:", false),
                                            DiscordEmoji.FromName(ctx.Client, ":three:", false),
                                            DiscordEmoji.FromName(ctx.Client, ":four:", false),};
            string optionString = optionEmojis[0] + " : " + Option1 + "\n" +
                optionEmojis[1] + " : " + Option2 + "\n" +
                optionEmojis[2] + " : " + Option3 + "\n" +
                optionEmojis[3] + " : " + Option4;

            var pollMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle(string.Join(" ", Question))
                .WithDescription(optionString));



            var putReactOn = await ctx.Channel.SendMessageAsync(pollMessage);
        
            foreach(var emoji in optionEmojis)
            {
                await putReactOn.CreateReactionAsync(emoji);
            }

            var result = await interactvity.CollectReactionsAsync(putReactOn, timer);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            foreach(var emoji in result)
            {
                if(emoji.Emoji == optionEmojis[0])
                {
                    count1++;
                } 
                if (emoji.Emoji == optionEmojis[1])
                {
                    count2++;
                }
                if (emoji.Emoji == optionEmojis[2])
                {
                    count3++;
                }
                if (emoji.Emoji == optionEmojis[3])
                {
                    count4++;
                }
            }
            int totalVotes = count1 + count2 + count3 + count4;

            string ResultString = optionEmojis[0] + " : " + count1 + " Votes \n" +
               optionEmojis[1] + " : " + count2 + " Votes \n" +
               optionEmojis[2] + " : " + count3 + " Votes \n" +
               optionEmojis[3] + " : " + count4 + " Votes \n\n" + 
               "The total number of vote is " + totalVotes;

            var resultsMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Green)
                .WithDescription(ResultString)
                );
            await ctx.Channel.SendMessageAsync(resultsMessage);
        }
    }
}
