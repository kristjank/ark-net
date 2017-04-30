using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace io.io.ark.utils
{
    public static class ArkUtils
    {
        public static string SerializeObject2Xml<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public static string SerializeObject2JSon<T>(this T toSerialize)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
            
        }
    }
}
