using SteamTools.Views;
using Steamworks;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamTools.ViewModels
{
    class MainWindowViewModel
    {
        private readonly Command achievementCommand;
        public ICommand AchievementCommand => achievementCommand;

        private uint? appId;
        public string AppId
        {
            get => appId.ToString();
            set
            {
                try
                {
                    appId = uint.Parse(value);
                }
                catch (FormatException)
                {
                    appId = null;
                    new MessageWindow("Invalid app ID").ShowDialog();
                }
            }
        }

        public ICommand ConnectCommand { get; }

        private bool connected;
        private bool Connected
        {
            get => connected;
            set
            {
                connected = value;
                achievementCommand.RaiseCanExecuteChanged();
                cloudCommand.RaiseCanExecuteChanged();
            }
        }

        private readonly Command cloudCommand;
        public ICommand CloudCommand => cloudCommand;

        public MainWindowViewModel()
        {
            achievementCommand = new Command(_ => new AchievementWindow().ShowDialog(), () => Connected);
            ConnectCommand = new Command(async _ =>
            {
                if (appId != null)
                {
                    try
                    {
                        if (Connected)
                        {
                            Connected = false;
                            await Task.Run(() => SteamClient.Shutdown());
                        }
                        await Task.Run(() => SteamClient.Init(appId.Value));
                        Connected = true;
                    }
                    catch (Exception)
                    {
                        Connected = false;
                        await Task.Run(() => SteamClient.Shutdown());
                        new MessageWindow("SteamClient.Shutdown()").ShowDialog();
                    }
                }
            });
            cloudCommand = new Command(_ => new CloudWindow().ShowDialog(), () => Connected);
        }
    }
}
