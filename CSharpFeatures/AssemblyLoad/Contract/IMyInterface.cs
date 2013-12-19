using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    /// <summary>
    /// 测试需要的接口组件
    /// </summary>
    public interface IMyInterface1
    {
        /// <summary>
        /// 执行动作的方法
        /// </summary>
        void Do();
    }


    public interface IMyInterface2
    {
        /// <summary>
        /// 执行动作的方法
        /// </summary>
        void Do();
        IMyInterface1 V1 { get; }
    }

    public interface IMyInterface3
    {
        /// <summary>
        /// 执行动作的方法
        /// </summary>
        void Do();

      
    }
}
