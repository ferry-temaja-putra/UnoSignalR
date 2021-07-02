using MvvmHelpers;

namespace UnoSignalR.CrossPlatformClient
{
    public class UIMessage : ObservableObject
    {
        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
    }
}
