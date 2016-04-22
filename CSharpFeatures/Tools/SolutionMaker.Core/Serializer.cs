using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SolutionMaker.Core
{
    /// <summary>
    /// Serialization utility methods to convert objects to and from strings
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Converts a serializable object to the XML string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>Serialized object value</returns>
        public static string ToXmlString(object value)
        {
            if (value == null)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);

                var settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Indent = true;

                var writer = XmlWriter.Create(stream, settings);

                var serializer = new XmlSerializer(value.GetType());
                serializer.Serialize(writer, value, ns);
                stream.Flush();
                stream.Position = 0;

                return new StreamReader(stream).ReadToEnd();
            }
        }

        /// <summary>
        /// Converts a serializable object from the XML string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">The string to be converted.</param>
        /// <returns>Deserialized object value</returns>
        public static T FromXmlString<T>(string text)
        {
            return (T)FromXmlString(text, typeof(T));
        }

        /// <summary>
        /// Converts a serializable object from the XML string.
        /// </summary>
        /// <param name="text">The string to be converted.</param>
        /// <param name="type">The object type.</param>
        /// <returns>Deserialized object value</returns>
        public static object FromXmlString(string text, Type type)
        {
            if (text == null)
            {
                if (type.IsValueType)
                    return Activator.CreateInstance(type);
                else
                    return null;
            }

            using (var stream = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                stream.Write(bytes, 0, bytes.Count());
                stream.Position = 0;
                var serializer = new XmlSerializer(type);
                var reader = XmlReader.Create(stream);
                return serializer.Deserialize(reader);
            }
        }
    }
}
