using System;

namespace Inspur.Gsp.CSharpIntroduction.Demo.OO
{
    /// <summary>
    /// 🐭
    /// </summary>
    public class Mouse : Animal
    {
        /// <inheritdoc />
        public override string Shout()
        {
            return "squeak";
        }

        public void Run(Cat sender, string sound)
        {
            Console.WriteLine("{0} is Runing", Name);
        }
    }
}