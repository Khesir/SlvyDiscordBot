﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using SlvyDiscordBot.External_Classes;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus;

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
            var deck = new Deck();
            deck.Shuffle();

            var playerHand = new Hand();
            var dealerHand = new Hand();
            
            // Deal initial cards
            playerHand.AddCard(deck.DrawCard());
            playerHand.AddCard(deck.DrawCard());

            dealerHand.AddCard(deck.DrawCard());
            dealerHand.AddCard(deck.DrawCard());

            // Display initial hands
            var playerHandMessage = await ctx.RespondAsync($"Your Hand: {playerHand} | Total = {playerHand.GetHandValue()}");
            var dealerHandMessage = await ctx.RespondAsync($"Dealer Hand: {dealerHand.FirstCard() }| Total = {playerHand.GetHandValue()}");

            //Add Reactions for player moves
            var hitEmoji = DiscordEmoji.FromName(ctx.Client, ":punch:",false);
            var standEmoji = DiscordEmoji.FromName(ctx.Client, ":raised_hand:", false);

            await playerHandMessage.CreateReactionAsync(hitEmoji);
            await playerHandMessage.CreateReactionAsync(standEmoji);
            //Event handler for reaction added
            async Task OnReactionAdded(DiscordClient sender,MessageReactionAddEventArgs e)
            {
                if (e.User.Id != ctx.User.Id || e.Message.Id != playerHandMessage.Id)
                    return;
                if (e.Emoji == hitEmoji)
                {
                    //Player draws another card
                    var card = deck.DrawCard();
                    playerHand.AddCard(card);
                    await ctx.RespondAsync($"Player draws: {card}");

                    //Update player hand message
                    await playerHandMessage.ModifyAsync($"Player Hand: {playerHand} | Total {playerHand.GetHandValue()}");
                    
                    // Check if player busts
                    if(playerHand.IsBust())
                    {
                        await ctx.RespondAsync("Player busts! Dealer wins!");
                        ctx.Client.MessageReactionAdded -= OnReactionAdded;
                        return;
                    }
                
                }
                else if(e.Emoji== standEmoji)
                {
                    //Player stands, and the dealer plays
                    await ctx.RespondAsync($"Player stands. Dealer reveals {dealerHand}");

                    await dealerHandMessage.ModifyAsync($"Dealer Hand: {dealerHand}");

                    while(dealerHand.GetHandValue() < 17) 
                    {
                        var card = deck.DrawCard();
                        dealerHand.AddCard(card);
                        await ctx.RespondAsync($"Dealer draws: {card}");

                        //Update dealer hand message after each card drawn.

                        await dealerHandMessage.ModifyAsync($"Dealer Hand: {dealerHand}");
                    }
                    // Determine the winner
                    if(dealerHand.IsBust() || playerHand.GetHandValue() > dealerHand.GetHandValue())
                    {
                        await ctx.RespondAsync("Player wins!");
                    }
                    else if (playerHand.GetHandValue() < dealerHand.GetHandValue())
                    {
                        await ctx.RespondAsync("Dealer wins!");
                    }
                    else
                    {
                        await ctx.RespondAsync("It's a tie!");
                    }

                    ctx.Client.MessageReactionAdded -= OnReactionAdded;
                    return;
                }
                // Remove the user's reaction
                await playerHandMessage.DeleteReactionAsync(e.Emoji, e.User);
            }
            // Subscribe to thhe reaction added event
            ctx.Client.MessageReactionAdded += OnReactionAdded; 
        }
    }
}
