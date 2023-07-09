using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
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
                .WithFooter("Greetings mastah! " + DiscordEmoji.FromName(ctx.Client, ":heart:", false))
                .WithThumbnail(ctx.Client.CurrentUser.AvatarUrl)
                );

            await ctx.Channel.SendMessageAsync(helpMessage);

        }

        [Command("profile")]
        public async Task showProfile(CommandContext ctx)
        {

            var displayName = ctx.Member.DisplayName;
            var joinDate = ctx.Member.JoinedAt.ToString("MM/dd/yyyy");
            var roles = string.Join(" ", ctx.Member.Roles.Select(r => r.Mention));

            Console.WriteLine("entered");
            var profileEmbed = new DiscordMessageBuilder().AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("Profile")
                .WithThumbnail(ctx.Member.AvatarUrl)
                .WithColor(DiscordColor.White)
                .AddField("Name", displayName, true)
                .AddField("Date Joined", joinDate, true)
                .AddField("Roles", roles, false)
                .WithFooter("test"+ DiscordEmoji.FromName(ctx.Client, ":heart:",false))
                
                );

            await ctx.Channel.SendMessageAsync(profileEmbed);
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

            foreach (var emoji in optionEmojis)
            {
                await putReactOn.CreateReactionAsync(emoji);
            }

            var result = await interactvity.CollectReactionsAsync(putReactOn, timer);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            foreach (var emoji in result)
            {
                if (emoji.Emoji == optionEmojis[0])
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
