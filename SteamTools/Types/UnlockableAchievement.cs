using Steamworks.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SteamTools.Types
{
    class UnlockableAchievement
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

        public async void LoadIcon()
        {
            var icon = await achievement.GetIconAsync();
            if (icon == null)
                return;
            var writeableBitmap = new WriteableBitmap((int)icon.Value.Width, (int)icon.Value.Height, 72, 72, PixelFormats.Bgra32, null);
            IList<byte> pixels = new List<byte>((int)(icon.Value.Height * icon.Value.Width * 4));
            for (var x = 0; x < icon.Value.Width; x++)
                for (var y = 0; y < icon.Value.Height; y++)
                {
                    var colour = icon.Value.GetPixel(x, y);
                    pixels.Add(colour.b);
                    pixels.Add(colour.g);
                    pixels.Add(colour.r);
                    pixels.Add(colour.a);
                }
            writeableBitmap.WritePixels(new Int32Rect(0, 0, (int)icon.Value.Width, (int)icon.Value.Height), pixels.ToArray(), 4 * (int)icon.Value.Width, 0);
            this.icon = writeableBitmap;
        }

        public UnlockableAchievement(Achievement achievement)
        {
            this.achievement = achievement;
        }
    }
}
