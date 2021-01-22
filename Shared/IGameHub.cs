using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor8s.Shared
{
    public interface IGameHub
    {
        Task JoinedGame(Guid playerId);
        Task PlayerJoined(string player);
        Task GameStarted(/*game id*/);
        Task GameStarted(int deckCount, Card discardCard);
        Task AddHand(ICollection<Card> hand);
        Task AddCardToHand(Card card);
        Task DiscardPlayed(Card card);
        Task UpdateDeckCount(int deckCount);
    }
}