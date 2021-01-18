using System;
using System.Collections.Generic;
using Blazor8s.Shared;

namespace Blazor8s.Client {
    public class ClientGameState {
        public Guid Id { get; set; }
        public List<Card> Hand { get; set; } = new();
        public Card SelectedCard { get; set; }
        public bool HasGameStarted {get;set;} = false;
    }
}