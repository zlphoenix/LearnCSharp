namespace J9Updater.FileTransferSvc.Ver1
{
    public enum ResponseStatus
    {

        /// <summary>
        /// X'10':本次消息执行成功，但是尚未完成，需要继续发送信息（一般用于文件上传下载）
        //	对于文件上传场景：握手通过后返回, 通知对方服务器已经做好接收文件的准备
        /// </summary>
        Ready = 0x10,
        /// <summary>
        /// X'20':成功，对方完成执行（文件上传结束返回此状态）
        /// </summary>
        Succeed = 0x20,
        /// <summary>
        /// X'30':校验失败, 需要重发上次请求的数据
        /// </summary>
        TransferError = 0x30,
        /// <summary>
        /// X'40':客户端错误 通过MSG提供消息错误原因 
        /// </summary>
        ClientError = 0x40,
        /// <summary>
        /// X'50':服务端错误
        /// </summary>
        ServerError = 0x50


    }
}