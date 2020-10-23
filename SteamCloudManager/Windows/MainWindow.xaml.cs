using SteamCloudManager.MVVM.ViewModels;
using Steamworks;
using System.Windows;
using System.Windows.Controls;

namespace SteamCloudManager.Windows
{
    partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            if (!uint.TryParse(viewModel.AppId, out uint appId))
            {
                MessageBox.Show("Make sure to enter a valid Steam app ID!");
                return;
            }
            if (SteamClient.IsValid)
                SteamClient.Shutdown();
            SteamClient.Init(appId);
            var fileViewModel = new FileViewModel();
            fileViewModel.Refresh();
            var window = new FileViewWindow(fileViewModel) { Owner = this };
            window.ShowDialog();
            ((Button)sender).IsEnabled = true;
        }
    }
}
