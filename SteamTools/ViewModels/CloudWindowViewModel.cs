using SteamTools.Views;
using Steamworks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SteamTools.ViewModels
{
    class CloudWindowViewModel : INotifyPropertyChanged
    {
        private readonly Command deleteCommand;
        public ICommand DeleteCommand => deleteCommand;

        private readonly Command downloadCommand;
        public ICommand DownloadCommand => downloadCommand;


        private ObservableCollection<RemoteFile> files;
        public ObservableCollection<RemoteFile> Files
        {
            get => files;
            set
            {
                {
                    files = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static string RemainingStorage => $"{Utils.WithSizeUnits(SteamRemoteStorage.QuotaUsedBytes)}/" +
                        $"{Utils.WithSizeUnits(SteamRemoteStorage.QuotaBytes)} (" +
                        $"{Utils.WithSizeUnits(SteamRemoteStorage.QuotaRemainingBytes)} remaining)";

        private IList<RemoteFile> selectedFiles = new List<RemoteFile>();
        public IList<RemoteFile> SelectedFiles
        {
            get => selectedFiles;
            set
            {
                selectedFiles = value;
                deleteCommand.RaiseCanExecuteChanged();
                downloadCommand.RaiseCanExecuteChanged();
                uploadCommand.RaiseCanExecuteChanged();
            }
        }

        private readonly Command uploadCommand;
        public ICommand UploadCommand => uploadCommand;

        public void Refresh()
        {
            IList<RemoteFile> files = new List<RemoteFile>();
            foreach (var file in SteamRemoteStorage.Files)
                files.Add(new RemoteFile(file));
            SetFiles(files);
            OnPropertyChanged("RemainingStorage");
        }

        public void SetFiles(IList<RemoteFile> files)
        {
            Files = new ObservableCollection<RemoteFile>(files);
            Files.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Remove:
                        foreach (RemoteFile file in e.OldItems)
                            file.Delete();
                        break;
                }
            };
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public CloudWindowViewModel()
        {
            deleteCommand = new Command(_ =>
            {
                foreach (var file in SelectedFiles)
                    file.Delete();
                Refresh();
            }, () => selectedFiles.Count > 0);
            downloadCommand = new Command(async _ =>
            {
                foreach (var file in SelectedFiles)
                    await File.WriteAllBytesAsync(file.Name, file.Read());
            }, () => selectedFiles.Count > 0);
            uploadCommand = new Command(_ =>
            {
                new UploadWindow().ShowDialog();
                Refresh();
            }, () => true);
            Refresh();
        }
    }
}
