namespace Inspur.Gsp.CSharpIntroduction.Demo.OO
{
    public class MarvellousMonkey : Money, IChange
    {
        object IChange.Change(string thing)
        {
            return $"Make a new {thing}";
        }
    }
}