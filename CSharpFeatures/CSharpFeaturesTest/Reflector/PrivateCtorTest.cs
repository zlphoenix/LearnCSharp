using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Reflector
{
    [TestClass]
    public class PrivateCtorTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(MissingMethodException))]
        public void CreateWithActivator()
        {
            var obj = Activator.CreateInstance(typeof(ClassWithPrivateCtor),
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is ClassWithPrivateCtor);
        }


    }

    public class ClassWithPrivateCtor
    {
        private ClassWithPrivateCtor()
        {

        }
    }
}
