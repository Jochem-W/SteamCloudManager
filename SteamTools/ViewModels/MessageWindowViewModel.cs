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

        public MessageWindowViewModel(string title, string message, Window owner) : this(message, owner) => Title = title;

        public MessageWindowViewModel(string title, string message, ICommand command, Window owner)
        {
            Message = message;
            Title = title;
            Ok = new Command(_ => {
                if (command.CanExecute(_))
                    command.Execute(_);
                owner.Close();
            });
        }
    }
}
