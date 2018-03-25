// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkUnconfirmedTransactionRequest.cs" company="Ark Labs">
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

using ArkNet.Attributes;
using ArkNet.Messages.BaseMessages;

namespace ArkNet.Messages.Transaction
{
    // References:
    // - TransactionService.cs

    /// <summary>
    /// Extrends the <see cref="ArkBaseRequest"/> type for unconfirmed transaction requests.
    /// </summary>
    public class ArkUnconfirmedTransactionRequest : ArkBaseRequest
    {
        #region Fields

        /// <summary>
        /// Query parameter. The transaction's sender public key as a hexadecimal string.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
        [ArkQueryParam(Name = "senderPublicKey")]
        public string SenderPublickey { get; set; }

        /// <summary>
        /// Query parameter. The address.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
        [ArkQueryParam(Name = "address")]
        public string Address { get; set; }

        #endregion
    }
}
