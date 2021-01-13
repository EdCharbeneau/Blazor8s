using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor8s.Shared
{
    public static class CardUtilities
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        /// </summary>

        public static List<T> Shuffle<T>(this IList<T> list)
        {
            List<T> newList = new List<T>(list);
            Random rng = new();
            int n = newList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = newList[k];
                newList[k] = newList[n];
                newList[n] = value;
            }
            return newList;
        }

        public static List<Card> CreateDeck()
        {
            List<Card> deck = new();
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    deck.Add(new(value, suit));
                }
            }
            return deck;
        }

    }
}
