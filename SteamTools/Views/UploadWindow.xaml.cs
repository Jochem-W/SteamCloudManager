using SteamTools.ViewModels;
using System.Windows;

namespace SteamTools.Views
{
    partial class UploadWindow : Window
    {
        public UploadWindow() : base()
        {
            InitializeComponent();
            DataContext = new UploadWindowViewModel(this);
        }
    }
}
