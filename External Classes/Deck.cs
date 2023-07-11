using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot.External_Classes
{
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = GenerateDeck();
        }

        public void Shuffle()
        {
            var rng = new Random();
            cards = cards.OrderBy(c => rng.Next()).ToList();
        }
        public Card DrawCard()
        {
            var card = cards.First();
            cards.RemoveAt(0);
            return card;
        }

        private List<Card> GenerateDeck()
        {
            var suits = new string[] { "Hearts", "Diamonds", "Clubs", "Spades"};
            var ranks = new string[]
            {
                "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"
            };

            var deck = new List<Card>();
            foreach(var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    deck.Add(new Card { Suit = suit, Rank = rank });
                }
            } 
            return deck;
        }
    }
}
