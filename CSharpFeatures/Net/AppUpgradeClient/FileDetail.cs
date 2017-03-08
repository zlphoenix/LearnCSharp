using System;
using System.IO;
using System.Security.Cryptography;

namespace J9Updater.AppUpgradeClient
{
    [Serializable]
    public class FileDetail
    {
        #region Properties  

        public FileType Type { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Length { get; set; }
        public DateTime LastChangedOn { get; set; }
        public string CheckSum { get; set; }
        #endregion

        public FileDetail() { }
        public FileDetail(FileInfo fileInfo)
        {

            Name = fileInfo.Name;
            Length = fileInfo.Length;
            LastChangedOn = fileInfo.LastWriteTime;
            CheckSum = GetMD5(fileInfo);
            Type = GetFileType(fileInfo);
        }

        private FileType GetFileType(FileInfo fileInfo)
        {
            switch (fileInfo.Extension.ToLower())
            {
                case ".exe":
                    return FileType.Exec;
                case ".dll":
                case ".pdb":
                    return FileType.Lib;
                case ".config":
                case ".ini":
                case ".cfg":
                    return FileType.Config;
                case ".jpg":
                case ".bmp":
                    return FileType.Resource;
                case ".xml":
                case ".txt":
                    return FileType.Doc;
                default:
                    return FileType.Unknown;

            }
        }

        private static string GetMD5(FileSystemInfo fileInfo)
        {

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileInfo.FullName))
                {
                    return Convert.ToBase64String(md5.ComputeHash(stream));
                }
            }
        }
    }
}