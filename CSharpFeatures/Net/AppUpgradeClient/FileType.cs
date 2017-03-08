namespace J9Updater.AppUpgradeClient
{
    public enum FileType
    {
        Unknown = 0,
        /// <summary>
        /// 可执行文件
        /// </summary>
        Exec,
        /// <summary>
        /// 库文件
        /// </summary>
        Lib,
        /// <summary>
        /// 配置文件
        /// </summary>
        Config,
        /// <summary>
        /// 资源文件
        /// </summary>
        Resource,
        /// <summary>
        /// 文档
        /// </summary>
        Doc,

    }
}