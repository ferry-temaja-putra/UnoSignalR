using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.AspNetCore.SignalR.Client;
using UnoSignalR.Base.Shared;
using Windows.UI.Core;

namespace UnoSignalR.CrossPlatformClient
{
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<UIMessage> UIMessages { get; }
        
        private HubConnection connection;

        public MainPage()
        {
            this.InitializeComponent();
            UIMessages = new ObservableCollection<UIMessage>();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            UIMessages.Add(new UIMessage { Message = $"Connecting to the hub..." });

            // http://localhost:25562/unohub
            // 192.168.0.186:25562/unohub
            var address = "http://localhost:25562/unohub";

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
                    (DispatchedHandler)(() => UIMessages.Add(new UIMessage { Message = $"Server pushed: {message}" })));
            });

            await connection.StartAsync();

            UIMessages.Add(new UIMessage { Message = $"Connected to the hub." });
            
            (sender as Button).IsEnabled = false;
        }
    }
}
