using System.Collections.Generic;
using Blazor8s.Shared;
using System;
namespace Blazor8s.Server.Hubs
{
    public class GameState 
    {
        public List<Player> Players { get; set; } = new();
        public bool HasGameStarted { get; set; }
        public Stack<Card> Deck { get; set; } =
                    new Stack<Card>(CardUtilities.GetShuffledDeck().Shuffle());

        public Card LastDiscard { get; set; }
    }

    public class Player
    {
        public Guid Id { get; } = Guid.NewGuid();
        public List<Card> Hand { get; set; } = new();
        public string Name { get; set; }
    }
}