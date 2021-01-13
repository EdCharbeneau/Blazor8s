using Blazor8s.Shared;
using System;
using System.Collections.Generic;
using Xunit;

namespace Blazor8s.Tests
{
    public class CardTests
    {
        [Fact]
        public void CanCreateDeck()
        {
            List<Card> deck = CardUtilities.CreateDeck();
            Assert.Equal(52, deck.Count);
        }

    }
}
