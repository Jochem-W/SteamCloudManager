using Steamworks;
using System;

namespace SteamTools.Types
{
    public class RemoteFile
    {
        public string Name { get; }

        public string Size => Utils.GetProperSize(SteamRemoteStorage.FileSize(Name));

        public DateTime Time => SteamRemoteStorage.FileTime(Name);

        public bool Exists => SteamRemoteStorage.FileExists(Name);

        public bool Persisted => SteamRemoteStorage.FilePersisted(Name);

        public bool Delete() => SteamRemoteStorage.FileDelete(Name);

        public byte[] Read() => SteamRemoteStorage.FileRead(Name);

        public RemoteFile(string filename) => Name = filename;
    }
}
