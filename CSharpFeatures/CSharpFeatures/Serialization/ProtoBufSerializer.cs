using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf.Meta;

namespace CSharpFeatures.Serialization
{

    public class ProtoBufSerializer
    {
        public static List<T> Do<T>(List<T> list)
        {
            using (var ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, list);
                ms.Seek(0, SeekOrigin.Begin);

                var result = ProtoBuf.Serializer.Deserialize<List<T>>(ms);

                //var mt = RuntimeTypeModel.Default.Add(typeof(Model), true);
                return result;
            }
        }

    }

    public class Model
    {
        public string Code { get; set; }
        public List<string> Detail { get; set; }
    }

}
