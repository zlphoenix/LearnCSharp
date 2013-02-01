using System;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.Xml;
using System.IO;

namespace CSharpFeatures.Serialization
{
    public class Serializer
    {
        private Assembly assembly = typeof(Serializer).Assembly;
        private StringBuilder _buffer;
        private DataContractSerializer _serializer;

        public Serializer()
        {
            _serializer =
                new DataContractSerializer(typeof(Object), null, int.MaxValue, false, true, null,
                    new MyDataContractResolver(assembly));
        }

        public void serialize(Type type)
        {
            Object instance = Activator.CreateInstance(type);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Serializing type: {0}", type.Name);
            Console.WriteLine();
            this._buffer = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(this._buffer))
            {
                try
                {
                    this._serializer.WriteObject(xmlWriter, instance);
                }
                catch (SerializationException error)
                {
                    Console.WriteLine(error.ToString());
                }
            }
            Console.WriteLine(this._buffer.ToString());
        }


        public void deserialize(Type type)
        {
            Console.WriteLine();
            Console.WriteLine("Deserializing type: {0}", type.Name);
            Console.WriteLine();
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(this._buffer.ToString())))
            {
                Object obj = this._serializer.ReadObject(xmlReader);
            }
        }
    }
}
