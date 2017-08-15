using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspur.Gsp.CSharpIntroduction.Demo.OO;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Generic
{
    public class UseGeneric
    {
        public void UseCollection()
        {
            var intlist = new List<int>();
            intlist.Add(10);
            intlist.RemoveAt(0);

            var strlist = new List<string>();
            strlist.Add("Hello");
            strlist.RemoveAt(0);

            var animals = new List<Animal>
            {
                new Cat("Tom"),
                new Dog { Name = "Goofy" },
                new MarvellousMonkey{Name = "Sun Wukong"}
            };

            var tom = animals.FirstOrDefault(animal => animal.Name.Equals("Tom"));
            animals.ForEach(animal => Console.WriteLine(animal.Name));

            var dic = new Dictionary<string, Animal>();
            dic.Add("Tom", new Cat("Tom"));
            dic.Add("Goofy", new Dog { Name = "Goofy" });
            dic.Add("Sun Wukong", new MarvellousMonkey { Name = "Sun Wukong" });
            dic.Add("Doraemon", new MachineCat("Doraemon"));

            tom = dic["Tom"];

            //dic.Add("Tom", new Cat("Tom"));//Runtime error

            //生成Dictionary
            dic = animals.ToDictionary(item => item.Name, item => item);
        }

        public void AboutDictionary()
        {
            var dic = new Dictionary<int, string>();

            dic.Add(1, "Allen");
            dic.Add(2, "Daisy");
            dic.Add(2, "");
        }

        public void VariantGeneric()
        {
            var queue4Animal = new Queue<Animal>();
            //Queue<Cat> queueForCat = queue4Animal;//编译失败

            var action4Animal = new Action<Animal>(cat => { });
            //可接受子类实例作为参数的委托，可以指向一个使用父类实例作为参数的方法
            Action<Cat> action4Cat = action4Animal;
        }

    }
}
