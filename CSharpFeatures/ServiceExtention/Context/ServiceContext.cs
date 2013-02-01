using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceExtention.Context
{
    [Serializable]
    public class ServiceContext : ISerializable
    {
        public ServiceContext(SerializationInfo info, StreamingContext context)
        {
        }
        public string LoginTime { get; set; }
        public string LoginUser { get; set; }
        public string LoginUserID { get; set; }
        public string Token { get; set; }
        public Dictionary<string, string> Content { get; set; }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
