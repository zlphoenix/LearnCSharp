using System;
using System.Text;

namespace Inspur.Gsp.CSharpIntroduction.Demo.OO
{
    /// <summary>
    /// 🐱 猫，继承自动物
    /// </summary>
    public class Cat : Animal
    {
        #region ctro.

        /// <summary>
        /// 无参构造函数，级联执行父类构造函数
        /// </summary>
        public Cat() : base()
        {
            Name = "Tom";
        }

        /// <summary>
        /// 构造函数重载 overload
        /// </summary>
        /// <param name="name"></param>
        public Cat(string name) : base()
        {
            Name = name;
        }

        #endregion

        #region Fields
        /// <summary>
        /// #.1.1.4 静态变量,访问field尽量封装为Property
        /// </summary>
        //private const string meow = "meow";
        private readonly string meow = "meow";

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// # 重写 叫方法
        /// </summary>
        /// <returns>返回叫声</returns>
        public override string Shout()
        {
            //#.3.1 UnitTest
            //throw new System.NotImplementedException();
            var sound = meow;
            if (CatShout != null)
            {
                CatShout(this, sound);
                //OnShout(this, sound);
            }
            //OnCatShout(sound);
            return sound;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            //#.2.1.1 string 连接
            //1.直接相加
            //return Name + " says：" + meow; 

            //2.模板替换
            //return string.Format("I am {0}", Name);


            //3.StringBuilder拼接
            var shout = new StringBuilder();
            shout.Append(Name);
            shout.Append(" says：");
            shout.Append(meow);
            return shout.ToString();
        }

        public new string Eat()
        {
            return base.Eat() + " I'm full";
        }
        #endregion

        #region Events

        /// <summary>
        /// 猫叫事件
        /// </summary>
        /// <remarks>
        /// #.1.3.1 事件定义
        /// </remarks>
        public event CatShoutEventHandler CatShout;
        public CatShoutEventHandler OnShout { get; set; }

        /// <summary>
        /// 事件处理方法
        /// </summary>
        /// <param name="shoutsound">叫声</param>
        protected virtual void OnCatShout(string shoutsound)
        {
            //#.1.8.3 this 引用
            CatShout?.Invoke(this, shoutsound);
        }
        #endregion

    }

    /// <summary>
    /// 猫叫事件处理委托
    /// </summary>
    /// <remarks>#.1.8.1 委托类型 </remarks>
    /// <param name="sender">正在叫的猫</param>
    /// <param name="shoutSound">叫声</param>
    public delegate void CatShoutEventHandler(Cat sender, string shoutSound);
}