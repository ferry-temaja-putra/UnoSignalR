using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using UnoSignalR.Base.Shared;

namespace UnoSignalR.ServerApp.Hubs
{
    public class UnoHub : Hub<IUnoClient>, IUnoHub
    {
        public async Task PushMessage(string message)
        {
            await Clients.All.ReceiveMessage($"{message} <{DateTime.Now.ToLongTimeString()}>");
        }

    }
}
