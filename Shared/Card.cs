using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor8s.Shared
{
    public class Card
    {
        public Suits Suit { get; set; }
        public CardValue Value { get; set; }
        bool IsFaceDown { get; set; } = true;

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }

    public class Game
    {
        public Game()
        {
            Deck = new List<Card>();
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    Deck.Add(new Card
                    {
                        Suit = suit,
                        Value = value
                    });
                }
            }
        }

        public List<Card> Deck { get; set; }
    }
}
