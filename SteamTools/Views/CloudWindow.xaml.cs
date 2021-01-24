using SteamTools.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SteamTools.Views
{
    partial class CloudWindow : Window
    {
        private readonly CloudWindowViewModel viewModel = new CloudWindowViewModel();

        public CloudWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelectedFiles = ((DataGrid)sender).SelectedItems.Cast<RemoteFile>().ToList();
        }
    }
}
