using Blazor8s.Shared;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blazor8s.Tests
{
    public class CardTests
    {
        [Fact]
        public void CanGetShuffledDeck()
        {
            List<Card> deck = CardUtilities.GetShuffledDeck();
            Assert.Equal(52, deck.Distinct().Count());
        }

    }
}
