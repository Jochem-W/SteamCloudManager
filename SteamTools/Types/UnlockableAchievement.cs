using Steamworks.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SteamTools.Types
{
    class UnlockableAchievement : INotifyPropertyChanged
    {
        private Achievement achievement;

        private WriteableBitmap icon;
        public WriteableBitmap Icon
        {
            get
            {
                if (icon == null)
                    LoadIcon();
                return icon;
            }
            private set
            {
                icon = value;
                OnPropertyChanged();
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        public async void LoadIcon()
        {
            var icon = await achievement.GetIconAsync();
            if (!icon.HasValue)
                return;
            var value = icon.Value;
            var width = (int)value.Width;
            var height = (int)value.Height;
            var writeableBitmap = new WriteableBitmap(width, height, 72, 72, PixelFormats.Bgra32, null);
            var pixels = value.Data;
            for (var i = 0; i < pixels.Length; i += 4)
            {
                var r = pixels[i];
                pixels[i] = pixels[i + 2];
                pixels[i + 2] = r;
            }
            writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, 4 * width, 0);
            writeableBitmap.Freeze();
            Icon = writeableBitmap;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public UnlockableAchievement(Achievement achievement)
        {
            this.achievement = achievement;
        }
    }
}
