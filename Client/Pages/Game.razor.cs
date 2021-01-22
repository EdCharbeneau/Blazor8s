using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor8s.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Blazor8s.Client.Pages
{

    public partial class Game : IAsyncDisposable
    {
        string _userName;
        bool _hasJoined = false;
        bool CanStartGame => _hasJoined && !_state.HasGameStarted;
        readonly ClientGameState _state = new();
        HubConnection _hubConnection;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/gamehub"))
                .Build();

            _hubConnection.On<Guid>(nameof(IGameHub.JoinedGame), JoinedGame);
            _hubConnection.On(nameof(IGameHub.GameStarted), GameStarted);
            _hubConnection.On<HashSet<Card>>(nameof(IGameHub.AddHand), AddHand);
            _hubConnection.On<Card>(nameof(IGameHub.AddCardToHand), AddCardToHand);
            _hubConnection.On<Card>(nameof(IGameHub.DiscardPlayed), DiscardPlayed);

            await _hubConnection.StartAsync();
        }

        void DiscardPlayed(Card card)
        {
            _state.SelectedCard = null;
            _state.Hand.Remove(card);

            StateHasChanged();
        }

        void HandleSelectedCard(Card card) =>
            _state.SelectedCard = _state.SelectedCard == card ? null : card;

        void JoinedGame(Guid id)
        {
            _state.Id = id;
            _hasJoined = true;
            StateHasChanged();
        }

        void AddHand(HashSet<Card> hand) => _state.Hand = hand;

        void AddCardToHand(Card card)
        {
            _state.Hand.Add(card);
            StateHasChanged();
        }

        Task PlayCard() => 
            _hubConnection.SendAsync(
                nameof(PlayCard),_state.Id, _state.SelectedCard);

        void GameStarted()
        {
            _state.HasGameStarted = true;
            StateHasChanged();
        }

        Task StartGame() => _hubConnection.SendAsync(nameof(StartGame));

        Task DrawCard() => _hubConnection.SendAsync(nameof(DrawCard), _state.Id);

        Task JoinGame() => _hubConnection.SendAsync("PlayerJoinGame", _userName);

        public async ValueTask DisposeAsync()
        {
            await _hubConnection.DisposeAsync();
        }
    }
}