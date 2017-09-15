using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Controllers;

namespace WebApi.Models
{
    public class Model
    {
        public string Id { get; set; }

        public string Name { get; set; }

        private List<string> detail;

        public List<string> Detail
        {
            get { return detail ?? (detail = new List<string>()); }
            set { detail = value; }
        }
        private Dictionary<string, Object> dic;

        public Dictionary<string, Object> Dic
        {
            get { return dic ?? (dic = new Dictionary<string, Object>()); }
            set { dic = value; }
        }

        public override string ToString()
        {
            return $"Id:{Id},Name:{Name},Detail:{Detail.Display()},Dic:{Dic.Display((kvp) => $"{{{kvp.Key},{kvp.Value}}}")}";
        }

    }

    public class Paging
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public override string ToString()
        {
            return $"TotalCount:{TotalCount},"
                  + $"PageIndex:{PageIndex},"
                 + $"PageSize:{PageSize},";
        }
    }

}