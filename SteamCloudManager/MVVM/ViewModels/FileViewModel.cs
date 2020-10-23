using Steamworks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SteamCloudManager.MVVM.ViewModels
{
    public class FileViewModel : INotifyPropertyChanged
    {
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

        public IList<RemoteFile> SelectedFiles { get; set; }

        public string RemainingStorage
        {
            get => $"{Utils.GetProperSize(SteamRemoteStorage.QuotaUsedBytes)}/{Utils.GetProperSize(SteamRemoteStorage.QuotaBytes)} ({Utils.GetProperSize(SteamRemoteStorage.QuotaRemainingBytes)} remaining)";
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
