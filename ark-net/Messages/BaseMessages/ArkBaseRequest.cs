// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkBaseRequest.cs" company="Ark">
//   MIT License
//   // 
//   // Copyright (c) 2017 Kristjan Košič
//   // 
//   // Permission is hereby granted, free of charge, to any person obtaining a copy
//   // of this software and associated documentation files (the "Software"), to deal
//   // in the Software without restriction, including without limitation the rights
//   // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   // copies of the Software, and to permit persons to whom the Software is
//   // furnished to do so, subject to the following conditions:
//   // 
//   // The above copyright notice and this permission notice shall be included in all
//   // copies or substantial portions of the Software.
//   // 
//   // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   // SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System.Collections.Generic;
using System.Reflection;
using ArkNet.Attributes;

namespace ArkNet.Messages.BaseMessages
{
    // References:
    // - ArkUnconfirmedTransactionRequest.cs
    // - ArkTransactionRequest.cs
    // - ArkBlockRequest.cs
    // - DelgateService.cs
    // - ArkBlockRequest.cs
    // - DelegateService.cs

    /// <summary>
    /// Provides a base class for formulating parameterized strings for ARK peer requests.
    /// </summary>
    /// 
    public class ArkBaseRequest
    {
        #region Read Only

        /// <summary>
        /// An ordered collection of query parameters.
        /// </summary>
        /// 
        protected readonly List<string> QueryParams = new List<string>();

        #endregion

        #region Fields

        /// <summary>
        /// Query parameter. Specifies the maximum number of records.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        [ArkQueryParam(Name = "limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// Query parameter. Specifies the number of records to skip.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        [ArkQueryParam(Name = "offset")]
        public int? Offset { get; set; }

        /// <summary>
        /// Query parameter. Specifies the column by which to sort the records.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="string"/>.</value>
        /// 
        [ArkQueryParam(Name = "orderBy")]
        public string OrderBy { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a parameterized string with local <see cref="ArkQueryParamAttribute"/> attributed properties.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="string"/>.</returns>
        /// 
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

        #endregion
    }
}
