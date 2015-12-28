using System;

namespace EFDemo
{
    public class DocDetail
    {
        /// <summary>
        /// 技术主键
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        public int LineNum { get; set; }

        public string Item { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public virtual SalesOrder Order { get; set; }
    }
}