using System.IO;

namespace J9Updater.FileTransferSvc.Ver1
{
    internal class Util
    {
        public static bool CheckDiskSpace(string fileServerDir, long fileSize)
        {
            //TODO DriveSpace
            DriveInfo r = new DriveInfo("R");
            long cAvailableSpace = r.AvailableFreeSpace;

            return cAvailableSpace > fileSize;
        }
    }
}