using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UnoSignalR.Base.Shared;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UnoSignalR.CrossPlatformClient
{
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<UIMessage> UIMessages { get; }
        
        private HubConnection connection;

        private string clientId;

        public MainPage()
        {
            this.InitializeComponent();
            UIMessages = new ObservableCollection<UIMessage>();
            InitializeHubConnection();

#if __ANDROID__
            clientId = "Android";
#elif NETFX_CORE
            clientId = "UWP";
#elif HAS_UNO_WASM
            clientId = "WASM";
#endif

        }

        private async Task InitializeHubConnection()
        {
            UIMessages.Add(new UIMessage { Message = $"Connecting to the hub..." });

            string address = "http://localhost:25562/unohub";

#if __ANDROID__
            address = "http://10.0.2.2:25562/unohub";
#endif

            connection = new HubConnectionBuilder()
                .WithUrl(address)
                .WithAutomaticReconnect()
                .Build();

            connection.On(nameof(IUnoClient.ReceiveMessage), async (string message) =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.High,
                    (DispatchedHandler)(() => UIMessages.Add(new UIMessage { Message = $"> {message}" })));
            });

            await connection.StartAsync();

            UIMessages.Add(new UIMessage { Message = $"Connected to the hub." });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var message = $"{clientId}: {MessageTextBox.Text}";
            await connection.InvokeAsync(nameof(IUnoHub.PushMessage), message);
        }
    }
}
