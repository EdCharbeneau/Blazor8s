using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor8s.Shared
{
    public static class CardUtilities
    {
        static readonly Random _random = new((int)DateTime.Now.Ticks);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        /// </summary>
        public static List<T> Shuffle<T>(this IList<T> source)
        {
            List<T> result = new(source);
            var count = result.Count;

            while (count > 1)
            {
                -- count;
                var k = _random.Next(count + 1);
                var value = result[k];
                result[k] = result[count];
                result[count] = value;
            }

            return result;
        }

        public static List<Card> GetShuffledDeck() =>
            Enum.GetValues(typeof(CardSuit))
                .Cast<CardSuit>()
                .Select(suit =>
                    (Suit: suit, Values: Enum.GetValues(typeof(CardValue)).Cast<CardValue>()))
                .SelectMany(pair => pair.Values, (pair, value) => new Card(value, pair.Suit))
                .ToList()
                .Shuffle();
    }
}
