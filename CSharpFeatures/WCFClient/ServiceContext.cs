using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFClient
{
    [Serializable]
    public class ServiceContext
    {
        public string UserID { get; set; }
        public string UserCode { get; set; }
        public Dictionary<string, string> Content { get; set; }
    }
}
