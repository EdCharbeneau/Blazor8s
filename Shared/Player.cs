using System.Collections.Generic;
using System;

namespace Blazor8s.Shared
{
    public record Player
    {
        public Guid Id { get; } = Guid.NewGuid();
        public HashSet<Card> Hand { get; init; } = new();
        public string Name { get; init; }
    }
}