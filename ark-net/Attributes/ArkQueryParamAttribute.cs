using System;
using System.Collections.Generic;
using System.Text;

namespace ArkNet.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ArkQueryParamAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
