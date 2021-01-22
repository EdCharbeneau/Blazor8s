using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor8s.Client.Pages
{
    public partial class Index : IAsyncDisposable
    {
        private HubConnection _hubConnection;
        private List<string> _messages = new();
        private string _userInput;
        private string _messageInput;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/chathub"))
                .Build();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = $"{user}: {message}";
                _messages.Add(encodedMsg);
                StateHasChanged();
            });

            await _hubConnection.StartAsync();
        }

        Task Send() =>
            _hubConnection.SendAsync("SendMessage", _userInput, _messageInput);

        Task Echo() =>
            _hubConnection.SendAsync("EchoMessage", _userInput, _messageInput);

        public bool IsConnected =>
            _hubConnection.State == HubConnectionState.Connected;

        ValueTask IAsyncDisposable.DisposeAsync() => _hubConnection.DisposeAsync();
    }
}
