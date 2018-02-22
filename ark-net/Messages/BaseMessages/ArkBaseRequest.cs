using System.Collections.Generic;
using System.Reflection;
using ArkNet.Attributes;

namespace ArkNet.Messages.BaseMessages
{
    public class ArkBaseRequest
    {
        protected readonly List<string> QueryParams = new List<string>();

        [ArkQueryParam(Name = "limit")]
        public int? Limit { get; set; }

        [ArkQueryParam(Name = "offset")]
        public int? Offset { get; set; }

        [ArkQueryParam(Name = "orderBy")]
        public string OrderBy { get; set; }

        public virtual string ToQuery()
        {
            var propCollection = GetType().GetRuntimeProperties();

            foreach (PropertyInfo property in propCollection)
            {
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is ArkQueryParamAttribute attr)
                    {
                        var val = property.GetValue(this);
                        if (val != null)
                            QueryParams.Add($"{attr.Name}={val}");
                    }
                }
            }

            return "?" + string.Join("&", QueryParams.ToArray());
        }
    }
}
