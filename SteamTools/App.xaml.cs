using SteamTools.Views;
using Steamworks;
using System.Threading.Tasks;
using System.Windows;

namespace SteamTools
{
    partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e) => new MainWindow().Show();

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            await Task.Run(() => SteamClient.Shutdown());
        }
    }
}
