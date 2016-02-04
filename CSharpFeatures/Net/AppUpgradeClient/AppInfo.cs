using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace J9Updater.AppUpgradeClient
{
    [Serializable]
    public class AppInfo : ISerializable
    {
        public string AppName { get; set; }
        public Version Version { get; set; }
        public string AppBasePath { get; set; }
        public string ExecName { get; set; }
        public List<FileDetail> Files { get; set; } = new List<FileDetail>();
        public List<string> GetFileNames() => Files.Select(item => Path.Combine(AppBasePath, item.Name)).ToList();

        public AppInfo()
        {
        }

        protected AppInfo(SerializationInfo info, StreamingContext context)
        {
            foreach (var item in info)
            {
                switch (item.Name)
                {
                    case "Version":
                        this.Version = new Version(item.Value.ToString());
                        break;
                    case "FileDetail":
                        Files = item.Value as List<FileDetail>;
                        break;
                    default:
                        break;
                }
            }



        }
        /// <summary>
        /// 使用将目标对象序列化所需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo"/>。
        /// </summary>
        /// <param name="info">要填充数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo"/>。</param><param name="context">此序列化的目标（请参见 <see cref="T:System.Runtime.Serialization.StreamingContext"/>）。</param><exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Version", this.Version.ToString());
            info.AddValue("FileDetail", this.Files);
        }
    }
}