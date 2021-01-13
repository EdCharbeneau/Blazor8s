using Blazor8s.Shared;
using System;
using Xunit;

namespace Blazor8s.Tests
{
    public class CardTests
    {
        [Fact]
        public void CanCreateDeck()
        {
            Game game = new();
            Assert.Equal(52, game.Deck.Count);
        }
    }
}
