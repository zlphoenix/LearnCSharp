using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Base : ISerializable
    {
        public Base()
        { }
        public string ParentProperty
        {
            get;
            set;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ParentProperty", ParentProperty, typeof(string));
        }

        public Base(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            ParentProperty = (string)info.GetValue("ParentProperty", typeof(string));
        }
    }
    [Serializable]
    public class Derived : Base, ISerializable
    {
        public string DerivedProperty
        {
            get;
            set;
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("DerivedProperty", DerivedProperty, typeof(string));        
        }
        public Derived()
        { }
        public Derived(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Reset the property value using the GetValue method.
            DerivedProperty = (string)info.GetValue("ParentProperty", typeof(string));
        }
    }
}
