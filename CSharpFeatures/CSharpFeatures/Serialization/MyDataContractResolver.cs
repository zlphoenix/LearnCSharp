using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Reflection;

namespace CSharpFeatures.Serialization
{
    public class MyDataContractResolver : DataContractResolver
    {
        private Dictionary<string, XmlDictionaryString> dictionary
            = new Dictionary<string, XmlDictionaryString>();
        private Assembly assembly;

        public MyDataContractResolver(Assembly assembly)
        {
            this.assembly = assembly;
        }


        /// <summary>
        /// 反序列化时通过解析类型名称得到类型
        /// Allows users to map xsi:type name to any Type 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="typeNamespace"></param>
        /// <param name="declaredType"></param>
        /// <param name="knownTypeResolver"></param>
        /// <returns></returns>
        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            var fullName = typeNamespace + "." + typeName;
            if (declaredType.FullName == fullName)
                return declaredType;
            else
                return this.assembly.GetType(fullName);




            //XmlDictionaryString tName;
            //XmlDictionaryString tNamespace;
            //if (!(dictionary.TryGetValue(typeName, out tName) && dictionary.TryGetValue(typeNamespace, out tNamespace)))
            //{

            //    var dtoType = this.assembly.GetType(fullName);
            //    if (dtoType != null)
            //    {
            //        if (!dictionary.ContainsKey(typeName))
            //        {
            //            tName = CreateXmlDicString(typeName);
            //            dictionary.Add(typeName, tName);
            //        }
            //        if (!dictionary.ContainsKey(typeNamespace))
            //        {
            //            tNamespace = CreateXmlDicString(typeNamespace);
            //            dictionary.Add(typeNamespace, tNamespace);
            //        }
            //    }               
            //}

        }

        // Used at serialization
        // Maps any Type to a new xsi:type representation
        /// <summary>
        /// 序列化时将类确定传输用的类型名称
        /// </summary>
        /// <param name="type"></param>
        /// <param name="declaredType"></param>
        /// <param name="knownTypeResolver"></param>
        /// <param name="typeName"></param>
        /// <param name="typeNamespace"></param>
        /// <returns></returns>
        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
        {
            typeName = new XmlDictionaryString(XmlDictionary.Empty, "", 0);
            typeNamespace = new XmlDictionaryString(XmlDictionary.Empty, "", 0);

            //var dataContract = dataContractType.GetCustomAttributes(typeof(DataContractAttribute), false).FirstOrDefault() as DataContractAttribute;
            //if (dataContract == null)
            //{
            //    return false;
            //}
            try
            {

                string name = type.Name;
                string namesp = type.Namespace;
                typeName = CreateXmlDicString(name);
                typeNamespace = CreateXmlDicString(namesp);
                if (!dictionary.ContainsKey(name))
                {
                    dictionary.Add(name, typeName);
                }
                if (!dictionary.ContainsKey(namesp))
                {
                    dictionary.Add(namesp, typeNamespace);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static XmlDictionaryString CreateXmlDicString(string name)
        {
            var typeName = new XmlDictionaryString(XmlDictionary.Empty, name, 0);
            return typeName;
        }
    }
}
