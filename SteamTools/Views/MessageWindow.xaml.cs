using SteamTools.ViewModels;
using System.Windows;

namespace SteamTools.Views
{
    partial class MessageWindow : Window
    {
        public MessageWindow(string message) : base()
        {
            InitializeComponent();
            DataContext = new MessageWindowViewModel(message, this);
        }
    }
}
