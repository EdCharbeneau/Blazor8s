using System.Collections.Generic;
using Blazor8s.Shared;
using System;
namespace Blazor8s.Server.Hubs
{
    public class GameState
    {
        public List<Player> Players { get; set; } = new();
        public bool HasGameStarted { get; set; }
        public List<Card> Deck { get; set; } = CardUtilities.CreateDeck();
    }

    public class Player
    {
        public Guid Id { get; } = Guid.NewGuid();
        public List<Card> Hand { get; set; } = new();
        public string Name { get; set; }
    }
}