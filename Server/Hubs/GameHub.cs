using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Blazor8s.Shared;

namespace Blazor8s.Server.Hubs
{
    public class GameHub : Hub<IGameHub>
    {
        private GameState _state;

        public GameHub(GameState state)
        {
            _state = state;
        }
        public async Task PlayerJoinGame(string player)
        {
            _state.Players.Add(new Player { Name = player } );
            await Clients.Groups("table").PlayerJoined(player);
            await Clients.Caller.JoinedGame();
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task TableJoinGame()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"table");
            await Clients.Caller.JoinedGame();
            //var players = _state.Players;
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}