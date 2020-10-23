using Microsoft.Win32;
using SteamCloudManager.MVVM.ViewModels;
using Steamworks;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SteamCloudManager.Windows
{
    partial class UploadWindow : Window
    {
        private readonly UploadViewModel viewModel = new UploadViewModel();

        public UploadWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
                viewModel.SourcePath = dialog.FileName;
        }

        private async void Upload(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            SteamRemoteStorage.FileWrite(viewModel.DestinationPath, await File.ReadAllBytesAsync(viewModel.SourcePath));
            ((Button)sender).IsEnabled = true;
            Close();
        }
    }
}
