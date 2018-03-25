using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ArkNet.Utils
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
