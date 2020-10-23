using SteamCloudManager.MVVM.ViewModels;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SteamCloudManager.Windows
{
    partial class FileViewWindow : Window
    {
        private readonly FileViewModel viewModel;

        public FileViewWindow()
        {
            InitializeComponent();
        }

        public FileViewWindow(FileViewModel fileViewModel) : base()
        {
            InitializeComponent();
            viewModel = fileViewModel;
            DataContext = viewModel;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            foreach (var file in viewModel.SelectedFiles)
                file.Delete();
            viewModel.Refresh();
        }

        private async void Download(object sender, RoutedEventArgs e)
        {
            foreach (var file in ((FileViewModel)DataContext).SelectedFiles)
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
        }
    }
}
