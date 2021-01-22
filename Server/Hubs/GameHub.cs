using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Blazor8s.Shared;

namespace Blazor8s.Server.Hubs
{
    public class GameHub : Hub<IGameHub>
    {
        private readonly GameState _state;

        public GameHub(GameState state) => _state = state;

        public async Task PlayerJoinGame(string name)
        {
            var player = new Player { Name = name };
            _state.Players.Add(player);

            await Groups.AddToGroupAsync(Context.ConnectionId, player.Id.ToString());
            await Clients.Groups("table").PlayerJoined(name);
            await Clients.Caller.JoinedGame(player.Id);
        }

        public async Task TableJoinGame()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"table");
            await Clients.Caller.JoinedGame(Guid.NewGuid());
            //var players = _state.Players;
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task StartGame()
        {
            // Deal Players
            foreach (var player in _state.Players)
            {
                for (int i = 0; i < 5; i++)
                {
                    player.Hand.Add(_state.Deck.Pop());
                }
            }

            await Task.WhenAll(
                _state.Players.Select(player => Clients.Group(player.Id.ToString())
                .AddHand(player.Hand))
                );

            _state.HasGameStarted = true;
            await Clients.All.GameStarted();

            _state.LastDiscard = _state.Deck.Pop();
            await Clients.Group("table").GameStarted(_state.Deck.Count, _state.LastDiscard);
        }
        
        public async Task DrawCard(Guid id)
        {
            var player = _state.Players.Find(p => p.Id == id);
            var newCard = _state.Deck.Pop();
            player.Hand.Add(newCard);

            await Clients.Group(id.ToString()).AddCardToHand(newCard);
            await Clients.Group("table").UpdateDeckCount(_state.Deck.Count);
        }

        public async Task PlayCard(Guid id, Card card)
        {
            // remove card from hand
            var player = _state.Players.Find(p => p.Id == id);
            var playerCard = player.Hand.Find(c => c.Value == card.Value && c.Suit == card.Suit);
            player.Hand.Remove(playerCard);

            // add to discard
            _state.LastDiscard = card;

            // notify success
            await Clients.Group("table").DiscardPlayed(card);
            await Clients.Caller.DiscardPlayed(card);
        }
    }
}