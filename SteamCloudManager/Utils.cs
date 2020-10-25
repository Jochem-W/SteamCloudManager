using System;
using System.Collections.Generic;

namespace SteamCloudManager
{
    static class Utils
    {
        private static readonly IList<string> SizeSuffixes = new List<string> { "bytes", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB" };

        public static string GetProperSize(int size)
        {
            return WithSizeUnits((ulong)size);
        }

        public static string GetProperSize(long size)
        {
            if (size < 0)
                return $"-{GetProperSize(-size)}";
            return GetProperSize(size);
        }

        public static string WithSizeUnits(ulong size)
        {
            var power = 0;
            if (size < 1000)
                return $"{size} {SizeSuffixes[power]}";
            double dividedSize;
            do
            {
                power++;
                dividedSize = size / Math.Pow(1024, power);
            }
            while (dividedSize >= 1000 && power < SizeSuffixes.Count - 1);
            return $"{dividedSize:0.0} {SizeSuffixes[power]}";
        }
    }
}
