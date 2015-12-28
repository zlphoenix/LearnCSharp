using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo
{
    /// <summary>
    /// 入库
    /// </summary>
    public class SalesOrder
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

        /// <summary>
        /// 单据编号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public DocStatus DocStatus { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public virtual Customer Customer { get; set; }

        public virtual List<DocDetail> DocDetails { get; set; }

    }
    /// <summary>
    /// 入库状态
    /// </summary>
    public enum DocStatus
    {
        /// <summary>
        /// 制单
        /// </summary>
        Open,
        /// <summary>
        /// 审核
        /// </summary>
        Approved,
        /// <summary>
        /// 关闭
        /// </summary>
        Closed,
    }
}
