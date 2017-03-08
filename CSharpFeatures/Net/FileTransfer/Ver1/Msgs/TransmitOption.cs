using System;

namespace J9Updater.FileTransferSvc.Ver1.Msgs
{
    /// <summary>
    /// 传输参数
    /// 用于协商双方的消息格式和处理策略
    /// </summary>
    [Flags]
    public enum TransmitOption
    {
        /// <summary>
        /// X'00 00':默认，无附加属性
        /// </summary>
        None = 0x00,
        /// <summary>
        /// X'00 01':使用身份认证
        /// </summary>
        NeedAuth = 0x01,
        /// <summary>
        /// X'00 02':消息内容加密（AES算法，对称密钥加密）
        /// </summary>
        Encryption = 0x02,
        /// <summary>
        /// X'00 04':需要保持会话
        /// </summary>
        Session = 0x04,
        /// <summary>
        /// X'00 08':内容压缩传输
        /// </summary>
        Zip = 0x08,
        /// <summary>
        /// X'00 16':单向通讯，不需要接收响应
        /// </summary>
        OneWay = 0x10
    }
}