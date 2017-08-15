using System;
using System.Collections.Generic;

namespace Inspur.Gsp.CSharpIntroduction.Demo.OO
{
    /// <summary>
    /// 动物 基类
    /// </summary>
    /// <remarks>
    /// #.1.4.1 抽象类
    /// </remarks>
    public abstract class Animal
    {


        /// <summary>
        /// 在构造方法中为静态属性赋值
        /// </summary>
        protected Animal()
        {
            Count++;
        }


        #region Properties

        /// <summary>
        /// 总数
        /// </summary>
        /// <remarks>
        /// #.1.1.3 属性访问控制,静态属性,避免使用静态属性
        /// </remarks>
        /// <remarks>
        /// #.1.1.2 Auto-property 对比Name
        /// </remarks>
        public static int Count { get; private set; }

        /// <summary>
        /// 名字
        /// </summary>
        /// <remarks>
        /// #.1.1.1 属性展开
        /// </remarks>
        public string Name { get; set; } = "no one";

        private List<string> myList;
        private int shoutNum;

        public List<string> MyProperty
        {
            get
            {
                var result = myList == null
                         ? (myList = new List<string>())
                         : myList;
                return myList ?? (myList = new List<string>());

            }
            set { myList = value; }
        }

        /// <summary>
        /// 猫叫次数
        /// </summary>
        public int ShoutNum
        {
            get { return shoutNum; }
            set
            {
                //#.1.1.5 属性封装逻辑
                if (value < 0)
                {
                    throw new NotSupportedException("Are you kidding (⊙ˍ⊙)");
                }
                shoutNum = value <= 10 ? value : 10;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// 叫
        /// </summary>
        /// <remarks>
        /// #.1.4.x 抽象方法
        /// </remarks>
        /// <returns>返回叫声</returns>
        public abstract string Shout();

        /// <summary>
        /// 吃
        /// </summary>
        /// <returns></returns>
        public string Eat()
        {
            return "I'm eating...";
        }
        #endregion
    }
}
