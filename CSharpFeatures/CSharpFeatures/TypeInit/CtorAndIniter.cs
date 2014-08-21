// ===============================================================================
// 浪潮GSP平台
// 类型描述
// 请查看《GSP7-构件开发平台子系统概要设计说明书》来了解关于此类的更多信息。
// ===============================================================================
// 变更历史纪录
// 时间			             版本	    修改人	        描述
// 1/3/2014 11:44:16 AM      1.0        zhoulun      创建初始版本。
// ===============================================================================
// 开发者: zhoulun
// 1/3/2014 11:44:16 AM
// (C) 2013 Genersoft Corporation 版权所有
// 保留所有权利。
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CSharpFeatures.TypeInit
{
    /// <summary>
    /// 类型描述
    /// </summary>
    public class CtorAndIniter
    {
        #region 字段
        #endregion

        #region 属性
        public int MyProperty { get; set; }
        #endregion

        #region 构造方法
        public CtorAndIniter()
        {
            MyProperty = 1;
        }
        #endregion

        #region 方法
        public static void Test()
        {
            var instance = new CtorAndIniter
            {
                MyProperty = 2,
            };
            //输出2 说明类型初始化器是在构造函数执行完成之后执行的.
            Trace.WriteLine(instance.MyProperty);
        }
        #endregion
    }

}
