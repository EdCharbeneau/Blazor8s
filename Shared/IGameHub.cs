using System.Threading.Tasks;

namespace Blazor8s.Shared {
    public interface IGameHub {
        Task JoinedGame();
        Task PlayerJoined(string player);
    }
}