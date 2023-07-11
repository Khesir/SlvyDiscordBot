using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot.External_Classes
{
    public class Hand
    {
        private List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }
        public void AddCard(Card card)
        {
            cards.Add(card); 
        }
        public int GetHandValue()
        {
            int value = 0;
            int numAces = 0;
            
            foreach(var card in cards)
            {
                if (card.Rank == "Ace")
                {
                    value += 11;
                    numAces++;
                }
                else if(card.Rank == "King" || card.Rank == "Queen" || card.Rank == "Jack")
                {
                    value += 10;
                }
                else
                {
                    // All number cards
                    value += int.Parse(card.Rank);
                }
            }
            while (value > 21 && numAces > 0)
            {
                value -= 10;
                numAces--;
            }
            return value;
        }
        public bool HasBlackjack()
        {
            return cards.Count == 2 && GetHandValue() == 21;
        }

        public bool IsBust()
        {
            return GetHandValue() > 21;
        }
        public Card FirstCard()
        {
            return cards.First();
        }
        public override string ToString()
        {
            return string.Join(", ", cards);
        }
    }
}
