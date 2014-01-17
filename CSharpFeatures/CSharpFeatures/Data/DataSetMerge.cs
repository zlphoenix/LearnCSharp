// ===============================================================================
// 浪潮GSP平台
// 类型描述
// 请查看《GSP7-构件开发平台子系统概要设计说明书》来了解关于此类的更多信息。
// ===============================================================================
// 变更历史纪录
// 时间			             版本	    修改人	        描述
// 1/17/2014 3:33:57 PM                    1.0        zhoulun      创建初始版本。
// ===============================================================================
// 开发者: zhoulun
// 1/17/2014 3:33:57 PM
// (C) 2013 Genersoft Corporation 版权所有
// 保留所有权利。
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CSharpFeatures.Data
{
    /// <summary>
    /// 类型描述
    /// </summary>
    public class DataSetMerge
    {
        #region 字段
        #endregion

        #region 属性
        #endregion

        #region 构造方法
        #endregion

        #region 方法
        /// <summary>
        /// 测试不同schema的DataSet 合并规则
        /// </summary>
        public static void DataSetMerageTest()
        {
            var dsSource = new DataSet();
            var dtSrc = new DataTable("SrcDT");
            dtSrc.Columns.Add("Id", typeof(string));
            dtSrc.Columns.Add("COde", typeof(string));
            dsSource.Tables.Add(dtSrc);

            var dest = dsSource.Copy();
            dtSrc.Columns.Add("Desc", typeof(string));
            var dr = dtSrc.NewRow();
            dr["Id"] = "1";
            dr["Code"] = "No001";
            dr["Desc"] = "First";
            dest.Merge(dsSource);

        }
        #endregion
    }
}
