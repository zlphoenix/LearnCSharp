using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Other
{
    public class AboutMethod
    {
        /// <summary>
        /// 参数传引用
        /// </summary>
        /// <param name="str">引用参数:可以在方法内修引用的值</param>
        public void RefParam(ref string str)
        {
            str = "Hello world";
        }
        /// <summary>
        /// 可变参数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strings">可以传入0...n个动态参数，可变参数只能是参数表的最后一个参数</param>
        public void VariableParams(string str, params string[] strings)
        {

        }

        /// <summary>
        /// 参数默认值
        /// </summary>
        /// <param name="str">参数默认值：减少定义大量重载方法链，只能应用于内部方法，不能作为外部接口使用</param>
        public void ParamDefaultValue(string str = "")
        {

        }
    }
}
