using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot.Slash_Commands
{
    public class Funslc : ApplicationCommandModule
    {
        [SlashCommand("test", "This is the first slash Command")]
        public async Task TestSlashCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Starting Slash Command ...."));

            var embedMessage = new DiscordEmbedBuilder()
            {
                Title = "test",
                Description = "test description",
            };
            await ctx.Channel.SendMessageAsync(embed: embedMessage);
        }
    }
}
