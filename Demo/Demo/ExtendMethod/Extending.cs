using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspur.Gsp.CSharpIntroduction.Demo.ExtendMethod
{
    public static class StringExtending
    {
        /// <summary>
        /// 添加前缀
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pre">前缀</param>
        /// <returns>连接后的新字符串</returns>
        public static string Fill(this string s, string pre)
        {
            return pre + s;
        }
    }
}
