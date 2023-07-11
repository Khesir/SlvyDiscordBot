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

        [Command("blackjack")]
        public async Task playBlackJack(CommandContext ctx)
        {
            var interactvity = ctx.Client.GetInteractivity();
            DiscordEmoji[] optionEmojis = { DiscordEmoji.FromName(ctx.Client, ":arrow_down_small", false),
                                            DiscordEmoji.FromName(ctx.Client, ":arrow_up_small:", false),};
            
            var gameMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle("Black jack")
                .WithDescription("blackjack game embed placeholder"));

            var putReactOn = await ctx.Channel.SendMessageAsync(gameMessage);

            foreach (var emoji in optionEmojis)
            {
                await putReactOn.CreateReactionAsync(emoji);
            }
            //get the reaction in order to update
            //Figure something out that is when on click, update the embeed
            var result = await interactvity.CollectReactionsAsync(putReactOn);
            
            // update embed program and proceed the game
        }
    }
}
