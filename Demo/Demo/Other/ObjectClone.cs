using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspur.Gsp.CSharpIntroduction.Demo.OO;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Other
{
    public class ObjectClone : ICloneable
    {

        public Cat Pet { get; set; }
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var clone = new ObjectClone();
            if (Pet != null)
            {
                clone.Pet = new Cat(Pet.Name);
            }
            return clone;
        }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns></returns>
        public ObjectClone ShallowCopy()
        {
            return (ObjectClone)MemberwiseClone();
        }
    }
}
