using System.Windows;
using System.Windows.Input;

namespace SteamTools.ViewModels
{
    class MessageWindowViewModel
    {
        public string Message { get; }

        public ICommand Ok { get; }

        public string Title { get; } = "SteamTools";

        public MessageWindowViewModel(string message, Window owner)
        {
            Ok = new Command(_ => owner.Close());
            Message = message;
        }
    }
}