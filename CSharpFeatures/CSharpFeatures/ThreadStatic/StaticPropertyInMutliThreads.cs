using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpFeatures.ThreadStatic
{
    public class StaticPropertyInMutliThreads
    {

        /// <summary>
        /// 静态属性
        /// </summary>
        public static string StaticProperty
        {
            get { return _staticProperty; }
            set { _staticProperty = value; }
        }

        public static string ThreadStaticProperty
        {
            get { return _threadStaticProperty; }
            set { _threadStaticProperty = value; }
        }


        private static string _staticProperty;


        [ThreadStatic]
        private static string _threadStaticProperty;


        public static string GetProperty()
        {
            return StaticProperty;
        }

    }
}
