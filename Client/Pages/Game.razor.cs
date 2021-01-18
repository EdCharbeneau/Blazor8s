using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor8s.Client;
using Blazor8s.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Blazor8s.Client.Pages
{

    public partial class Game : IAsyncDisposable
    {
        [Inject] NavigationManager NavigationManager { get; set; }

        string userName;
        bool HasJoined = false;

        bool CanStartGame => HasJoined && !state.HasGameStarted;

        ClientGameState state = new();

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        private HubConnection hubConnection;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/gamehub"))
                .Build();

            hubConnection.On<Guid>(nameof(IGameHub.JoinedGame), JoinedGame);
            hubConnection.On(nameof(IGameHub.GameStarted), GameStarted);
            hubConnection.On<List<Card>>(nameof(IGameHub.AddHand), AddHand);
            hubConnection.On<Card>(nameof(IGameHub.AddCardToHand), AddCardToHand);
            hubConnection.On<Card>(nameof(IGameHub.DiscardPlayed), DiscardPlayed);
            await hubConnection.StartAsync();
        }

        void DiscardPlayed(Card card)
        {
            state.SelectedCard = null;
            var playerCard = state.Hand.Find(c => c.Value == card.Value && c.Suit == card.Suit);
            state.Hand.Remove(playerCard);
            StateHasChanged();
        }

        void HandleSelectedCard(Card card)
        {
            if (state.SelectedCard == card)
            {
                state.SelectedCard = null;
            }
            else
            {
                state.SelectedCard = card;
            }
        }

        void JoinedGame(Guid id)
        {
            state.Id = id;
            HasJoined = true;
            StateHasChanged();
        }

        void AddHand(List<Card> hand)
        {
            state.Hand = hand;
        }

        void AddCardToHand(Card card)
        {
            state.Hand.Add(card);
            StateHasChanged();
        }

        Task PlayCard() => hubConnection.SendAsync("PlayCard",state.Id, state.SelectedCard);

        void GameStarted()
        {
            state.HasGameStarted = true;
            StateHasChanged();
        }

        Task StartGame() => hubConnection.SendAsync("StartGame");

        Task DrawCard() => hubConnection.SendAsync("DrawCard", state.Id);

        Task JoinGame() => hubConnection.SendAsync("PlayerJoinGame", userName);
        public async ValueTask DisposeAsync()
        {
            await hubConnection.DisposeAsync();
        }
    }
}