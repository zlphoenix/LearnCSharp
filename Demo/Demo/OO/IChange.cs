namespace Inspur.Gsp.CSharpIntroduction.Demo.OO
{
    /// <summary>
    /// 会变接口
    /// </summary>
    /// <remarks>
    /// #.1.5.1 实现接口
    /// </remarks>
    public interface IChange
    {
        /// <summary>
        /// 变出东西
        /// </summary>
        /// <param name="thing"></param>
        object Change(string thing);
    }
}