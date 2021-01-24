using Steamworks.Data;

namespace SteamTools.Types
{
    class UnlockableAchievement
    {
        private Achievement achievement;

        public bool Unlocked
        {
            get => achievement.State;
            set
            {
                if (value)
                    achievement.Trigger();
                else
                    achievement.Clear();
            }
        }

        public string Name => achievement.Name;

        public string Description => achievement.Description;

        public UnlockableAchievement(Achievement achievement) => this.achievement = achievement;
    }
}
