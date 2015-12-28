using System;

namespace EFDemo
{
    public class Customer
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

        public string Code { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}