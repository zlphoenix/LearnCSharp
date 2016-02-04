using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace J9Updater.AppUpgradeClient
{

    public class AppUpgradeManager
    {
        public void Upload(string appName, Version ver, string appBasePath)
        {
            if (string.IsNullOrEmpty(appName)) throw new ArgumentNullException(nameof(appName));
            if (ver == null) throw new ArgumentNullException(nameof(ver));
            if (string.IsNullOrEmpty(appName)) throw new ArgumentNullException(nameof(appBasePath));
            if (!Directory.Exists(appBasePath))
            {
                throw new DirectoryNotFoundException(appBasePath);
            }

            var indexFile = GenerateIndexFile(appName, ver, appBasePath);
            if (indexFile.GetFileNames().Count == 0) return;
            var client = new FileTransferSvc.Ver1.TcpFileTransmitServiceClient();
            foreach (var fileInfo in indexFile.GetFileNames())
            {
                client.Upload(fileInfo, null);
            }
        }

        public IList<AppInfo> GetServerApps()
        {
            var client = new FileTransferSvc.Ver1.TcpFileTransmitServiceClient();

            client.DownLoad("index.xml", null);
            var serializer = new XmlSerializer(typeof(AppInfo), new Type[] { typeof(FileDetail) });
            var indexFileName = Path.Combine(@"R:\DownloadFiles", "index.xml");
            using (var fileReader = File.OpenRead(indexFileName))
            {
                var appInfo = serializer.Deserialize(fileReader);
                return new List<AppInfo>() { appInfo as AppInfo };
            }
        }


        private AppInfo GenerateIndexFile(string appName, Version ver, string appBasePath)
        {
            var dir = new DirectoryInfo(appBasePath);
            var files = dir.GetFiles();
            if (files.Length == 0) return null;
            var indexFile = new AppInfo()
            {
                AppBasePath = appBasePath,
                AppName = appName,
                Version = ver,
            };
            foreach (var fileInfo in files)
            {
                var fileDetail = new FileDetail(fileInfo);
                indexFile.Files.Add(fileDetail);
            }

            var serializer = new XmlSerializer(typeof(AppInfo), new Type[] { typeof(FileDetail) });
            var indexFileName = Path.Combine(appBasePath, "index.xml");
            using (var file = File.OpenWrite(indexFileName))
            {
                serializer.Serialize(file, indexFile);
            }

            indexFile.Files.Insert(0, new FileDetail(new FileInfo(indexFileName)));


            return indexFile;
        }
    }
}
