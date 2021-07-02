using System.Threading.Tasks;

namespace UnoSignalR.Base.Shared
{
    public interface IUnoClient
    {
        Task ReceiveMessage(string message);
    }
}
