using SteamTools.Types;
using Steamworks;
using System.Collections.Generic;

namespace SteamTools.ViewModels
{
    class AchievementWindowViewModel
    {
        public IList<UnlockableAchievement> Achievements { get; }

        public AchievementWindowViewModel()
        {
            Achievements = new List<UnlockableAchievement>();
            foreach (var achievement in SteamUserStats.Achievements)
                Achievements.Add(new UnlockableAchievement(achievement));
        }
    }
}
