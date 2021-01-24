using SteamTools.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SteamTools.Views
{
    partial class MessageWindow : Window
    {
        public MessageWindow(string message) : base()
        {
            InitializeComponent();
            DataContext = new MessageWindowViewModel(message, this);
        }

        public MessageWindow(string title, string message) : base()
        {
            InitializeComponent();
            DataContext = new MessageWindowViewModel(title, message, this);
        }

        public MessageWindow(string title, string message, ICommand command) : base()
        {
            InitializeComponent();
            DataContext = new MessageWindowViewModel(title, message, command, this);
        }
    }
}
