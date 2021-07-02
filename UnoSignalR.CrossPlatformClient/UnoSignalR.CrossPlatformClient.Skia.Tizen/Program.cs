using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace UnoSignalR.CrossPlatformClient.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new UnoSignalR.CrossPlatformClient.App(), args);
            host.Run();
        }
    }
}
