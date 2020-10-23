using Steamworks;
using System;

namespace SteamCloudManager
{
    public class RemoteFile
    {
        public string Name { get; }

        public string Size { get; }

        public DateTime Time { get; }

        public bool Exists { get; }

        public bool Persisted { get; }

        public bool Delete() => SteamRemoteStorage.FileDelete(Name);

        public byte[] Read() => SteamRemoteStorage.FileRead(Name);

        public RemoteFile(string filename)
        {
            Exists = SteamRemoteStorage.FileExists(filename);
            Name = filename;
            Persisted = SteamRemoteStorage.FilePersisted(filename);
            Size = Utils.GetProperSize(SteamRemoteStorage.FileSize(filename));
            Time = SteamRemoteStorage.FileTime(filename);
        }
    }
}
