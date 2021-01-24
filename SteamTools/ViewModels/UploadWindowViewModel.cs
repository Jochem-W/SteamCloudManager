using Microsoft.Win32;
using Steamworks;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SteamTools.ViewModels
{
    class UploadWindowViewModel : INotifyPropertyChanged
    {
        private bool AllowUpload
        {
            get
            {
                if (File.Exists(SourcePath) && DestinationPath != "" && DestinationPath != null)
                    return true;
                return false;
            }
        }

        public ICommand BrowseCommand { get; }

        private string destinationPath;
        public string DestinationPath
        {
            get => destinationPath;
            set
            {
                destinationPath = value;
                OnPropertyChanged();
                uploadCommand.RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string sourcePath;
        public string SourcePath
        {
            get => sourcePath;
            set
            {
                sourcePath = value;
                DestinationPath = Path.GetFileName(sourcePath);
                OnPropertyChanged();
                uploadCommand.RaiseCanExecuteChanged();
            }
        }

        private readonly Command uploadCommand;
        public ICommand UploadCommand => uploadCommand;

        public UploadWindowViewModel(Window owner)
        {
            BrowseCommand = new Command(_ =>
            {
                var dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == true)
                    SourcePath = dialog.FileName;
            });
            uploadCommand = new Command(async _ =>
            {
                await Task.Run(async () =>
                {
                    SteamRemoteStorage.FileWrite(DestinationPath, await File.ReadAllBytesAsync(SourcePath));
                });
                owner.Close();
            }, () => AllowUpload);
        }
    }
}