using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using CSharpFeatures.Serialization;

namespace CSharpFeaturesTest.Serializable
{
    /// <summary>
    /// SerializableTest 的摘要说明
    /// </summary>
    [TestClass]
    public class SerializableTest
    {
        public SerializableTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {

            var obj = new Derived() { ParentProperty = "P", DerivedProperty = "D" };


            var buffer = serialize(obj);
            var result = deserialize<Derived>(buffer);


            Assert.IsNotNull(result, "反序列化返回Null");
            Assert.AreEqual(obj.ParentProperty, result.ParentProperty, "基类属性没有返回正确");
            Assert.AreEqual(obj.DerivedProperty, result.DerivedProperty, "子类属性没有返回正确");


            serializeFormat(obj);
        }

        public StringBuilder serialize(object obj)
        {
            var _buffer = new StringBuilder();

            var _serializer = new XmlSerializer(obj.GetType());
            using (XmlWriter xmlWriter = XmlWriter.Create(_buffer))
            {
                try
                {
                    _serializer.Serialize(xmlWriter, obj);
                }
                catch (SerializationException error)
                {
                    Console.WriteLine(error.ToString());
                }
            }
            return _buffer;
        }
        public void serializeFormat(object obj)
        {
            try
            {
                using (FileStream s = new FileStream("a.txt", FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(s, obj);
                    s.Close();
                }
            }
            catch (SerializationException error)
            {
                Console.WriteLine(error.ToString());
            }

        }


        public T deserialize<T>(StringBuilder xml)
        {
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml.ToString())))
            {
                var _serializer = new XmlSerializer(typeof(T));
                T obj = (T)_serializer.Deserialize(xmlReader);
                return obj;
            }

        }
    }
}
