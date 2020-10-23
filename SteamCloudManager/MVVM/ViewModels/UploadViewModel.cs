using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SteamCloudManager.MVVM.ViewModels
{
    class UploadViewModel : INotifyPropertyChanged
    {
        private string destinationPath;
        public string DestinationPath
        {
            get => destinationPath;

            set
            {
                {
                    destinationPath = value;
                    OnPropertyChanged();
                }
            }
        }

        private string sourcePath;
        public string SourcePath
        {
            get => sourcePath;

            set
            {
                {
                    sourcePath = value;
                    OnPropertyChanged();
                    DestinationPath = sourcePath.Split('\\').Last();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
