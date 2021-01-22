using Blazor8s.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor8s.Client.Pages
{
    public partial class Table : IAsyncDisposable
    {
        List<string> _players = new();
        HubConnection _hubConnection;
        bool _hasJoinedGame = false;
        Card _discardCard;
        int _deckCount = 52;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                  .WithUrl(NavigationManager.ToAbsoluteUri("/gamehub"))
                  .Build();

            _hubConnection.On("JoinedGame", () =>
            {
                _hasJoinedGame = true;
                StateHasChanged();
            });

            _hubConnection.On<string>(nameof(IGameHub.PlayerJoined), PlayerJoined);
            _hubConnection.On<int, Card>(nameof(IGameHub.GameStarted), GameStarted);
            _hubConnection.On<Card>(nameof(IGameHub.DiscardPlayed), DiscardPlayed);
            _hubConnection.On<int>(nameof(IGameHub.UpdateDeckCount), UpdateDeckCount);

            await _hubConnection.StartAsync();

            await _hubConnection.SendAsync("TableJoinGame");

        }

        void GameStarted(int count, Card card)
        {
            _deckCount = count;
            _discardCard = card;
            StateHasChanged();
        }

        void UpdateDeckCount(int count)
        {
            _deckCount = count;
            StateHasChanged();
        }

        void DiscardPlayed(Card card)
        {
            _discardCard = card;
            StateHasChanged();
        }

        void PlayerJoined(string player)
        {
            _players.Add(player);
            StateHasChanged();
        }

        ValueTask IAsyncDisposable.DisposeAsync() => _hubConnection.DisposeAsync();
    }
}
