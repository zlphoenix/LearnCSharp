namespace Inspur.Gsp.CSharpIntroduction.DemoLib.StubLib
{
    public class Customer
    {
        public Gender Gender { get; set; }

        public int Seq { get; set; }
    }

    public enum Gender
    {
        Unknown = 0,
        Male,
        Female,
        Transgender
    }

}