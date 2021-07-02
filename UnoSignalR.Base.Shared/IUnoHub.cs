using System.Threading.Tasks;

namespace UnoSignalR.Base.Shared
{
    public interface IUnoHub
    {
        Task PushMessage(string message);
    }
}
