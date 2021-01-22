using System;
using System.Collections.Generic;
using Blazor8s.Shared;
namespace Blazor8s.Server.Hubs
{
    public class GameState
    {
        public Dictionary<Guid, Player> Players { get; set; } = new();
        public bool HasGameStarted { get; set; }
        public Stack<Card> Deck { get; set; } =
            new Stack<Card>(CardUtilities.GetShuffledDeck().Shuffle());

        public Card LastDiscard { get; set; }
    }
}