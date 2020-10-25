using SteamCloudManager.MVVM.ViewModels;
using Steamworks;
using System;
using System.IO;
using System.Linq;
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

        private void Connect(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            if (!uint.TryParse(viewModel.AppId, out uint appId))
            {
                MessageBox.Show("Please enter a valid app ID!");
                ((Button)sender).IsEnabled = true;
                return;
            }
            if (SteamClient.IsValid)
            {
                SteamClient.Shutdown();
                UploadButton.IsEnabled = false;
            }
            try
            {
                SteamClient.Init(appId);
                viewModel.Refresh();
                UploadButton.IsEnabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Please make sure that Steam is running and you entered a valid app ID!");
            }
            ((Button)sender).IsEnabled = true;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            foreach (var file in viewModel.SelectedFiles)
                file.Delete();
            viewModel.Refresh();
        }

        private async void Download(object sender, RoutedEventArgs e)
        {
            foreach (var file in ((MainViewModel)DataContext).SelectedFiles)
                await File.WriteAllBytesAsync(file.Name, file.Read());
        }

        private void Upload(object sender, RoutedEventArgs e)
        {
            new UploadWindow { Owner = this }.ShowDialog();
            viewModel.Refresh();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelectedFiles = ((DataGrid)sender).SelectedItems.Cast<RemoteFile>().ToList();
            if (viewModel.SelectedFiles.Count > 0)
            {
                DownloadButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                return;
            }
            DownloadButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }
    }
}
