using SteamTools.ViewModels;
using System.Windows;

namespace SteamTools.Views
{
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
