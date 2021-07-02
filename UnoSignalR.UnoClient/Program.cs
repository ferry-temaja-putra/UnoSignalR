using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnoSignalR.Base.Shared;

namespace UnoSignalR.UnoClient
{
    class Program
    {
        static HubConnection connection;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Creating connection to the hub...");

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:25562/unohub")
                .WithAutomaticReconnect()
                .Build();

            connection.On<string>(nameof(IUnoClient.ReceiveMessage), (string message) =>
            {
                Console.WriteLine($"Server pushed: {message}");
            });

            await connection.StartAsync();
            Console.WriteLine("Connected to the hub.");

            while (true)
            {
                Console.WriteLine("Enter message to be pushed to the hub...");
                var message = Console.ReadLine();

                await connection.InvokeAsync(nameof(IUnoHub.PushMessage), message);
            }
        }
    }
}
