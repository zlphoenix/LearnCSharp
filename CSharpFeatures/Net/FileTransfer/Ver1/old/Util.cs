using System;
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

        public static T ReadByteToEnum<T>(byte[] raw, int startIndex, int size = 1) where T : struct, IConvertible

        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("参数不是枚举类型");
            }
            var optValue = size > 1 ? BitConverter.ToInt16(raw, startIndex) : raw[startIndex];
            var result = (T)Enum.ToObject(typeof(T), optValue);
            return result;

        }

        public static T ReadByteToEnum<T>(byte raw) where T : struct, IConvertible

        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("参数不是枚举类型");
            }
            var result = (T)Enum.ToObject(typeof(T), raw);
            return result;

        }
    }
}