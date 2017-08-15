namespace Inspur.Gsp.CSharpIntroduction.Demo.OO
{
    /// <summary>
    /// 机器猫
    /// </summary>
    public class MachineCat : Cat, IChange
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public MachineCat(string name) : base(name)
        {

        }

        /// <summary>
        /// 变出东西
        /// </summary>
        /// <param name="thing"></param>
        public object Change(string thing)
        {
            return $"Take out a {thing} from pocket";
        }
    }
}