namespace Inspur.Gsp.CSharpIntroduction.DemoLib.StubLib
{
    /// <summary>
    /// 收款接口
    /// </summary>
    public interface ICheckInFee
    {
        decimal GetFee(Customer customer);
    }
}