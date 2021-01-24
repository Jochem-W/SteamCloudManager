using SteamTools.ViewModels;
using System.Windows;

namespace SteamTools.Views
{
    partial class AchievementWindow : Window
    {
        public AchievementWindow()
        {
            InitializeComponent();
            DataContext = new AchievementWindowViewModel();
        }
    }
}
