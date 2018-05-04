// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkTransactionRequest.cs" company="Ark">
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
    /// Extends the <see cref="ArkBaseRequest"/> type for transaction requests.
    /// </summary>
    public class ArkTransactionRequest : ArkBaseRequest
    {
        #region fields

        /// <summary>
        /// Query parameter. The sender's public key of the transaction as a hexadecimal string.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
        [ArkQueryParam(Name = "senderPublicKey")]
        public string SenderPublickey { get; set; }

        /// <summary>
        /// Query parameter. The sender's public key of the owner.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
        [ArkQueryParam(Name = "ownerPublicKey")]
        public string OwnerPublicKey { get; set; }

        /// <summary>
        /// Query parameter. The owner"s address.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
        [ArkQueryParam(Name = "ownerAddress")]
        public string OwnerAddress { get; set; }

        /// <summary>
        /// Query parameter. The sender's public address.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
        [ArkQueryParam(Name = "senderId")]
        public string SenderId { get; set; }

        /// <summary>
        /// Query parameter. The transaction recipient's address.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
        [ArkQueryParam(Name = "recipientId")]
        public string RecipientId { get; set; }

        /// <summary>
        /// Query parameter. The amount transacted.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="long"/>.</value>
        /// 
        [ArkQueryParam(Name = "amount")]
        public long? Amount { get; set; }

        /// <summary>
        /// Query parameter. The fee of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="int"/>.</value>
        /// 
        [ArkQueryParam(Name = "fee")]
        public int? Fee { get; set; }

        /// <summary>
        /// Query parameter. The type of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref= "int" />.</value>
        /// 
        [ArkQueryParam(Name = "type")]
        public int? Type { get; set; }

        /// <summary>
        /// Query parameter. The type of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref= "string" />.</value>
        /// 
        [ArkQueryParam(Name = "blockId")]
        public string BlockId { get; set; }

        #endregion
    }
}
