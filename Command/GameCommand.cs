using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using SlvyDiscordBot.External_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot.Command
{
    public class GameCommand : BaseCommandModule
    {
        [Command("drawcard")]
        public async Task SimpleCardGame(CommandContext ctx)
        {

            // Fix some bugs here
            var UserCard = new Cardbuilder();
            var BotCard = new Cardbuilder();

            var userCardMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("YourCard")
                .WithDescription("You drew a: " + UserCard.SelectedCard)
                );
            await ctx.Channel.SendMessageAsync(userCardMessage);

            var botCardMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder().WithColor(DiscordColor.Azure).WithTitle("BotCard")
                .WithDescription("The bot drew a: " + BotCard.SelectedCard)
                );
            await ctx.Channel.SendMessageAsync(botCardMessage);

            if(UserCard.SelectedNumber > BotCard.SelectedNumber) 
            {
                // The user wins
                var winningMesssage = new DiscordEmbedBuilder()
                {
                    Title = "You win the game!",
                    Color = DiscordColor.Green,
                };
                await ctx.Channel.SendMessageAsync(embed: winningMesssage);
            }
            else 
            {
                //The bot wins
                var losingMesssage = new DiscordEmbedBuilder()
                {
                    Title = "You lost the game!",
                    Color = DiscordColor.Green,
                };
                await ctx.Channel.SendMessageAsync(embed: losingMesssage);
            }
        }
    }
}
