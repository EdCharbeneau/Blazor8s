using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalRApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message) =>
            Clients.All.SendAsync("ReceiveMessage", user, message);

        public Task EchoMessage(string user, string message) =>
            Clients.Caller.SendAsync("ReceiveMessage", user, message);
    }
}