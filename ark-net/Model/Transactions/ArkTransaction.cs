// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkTransaction.cs" company="Ark">
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

using ArkNet.Model.Shared;

namespace ArkNet.Model.Transactions
{
    /// <summary>
    /// Represents an Ark Transaction model.
    /// </summary>
    /// 
    public class ArkTransaction
    {
        #region Fields

        /// <summary>
        /// The transaction id.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string Id { get; set; }

        /// <summary>
        /// The block id.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string BlockId { get; set; }

        /// <summary>
        /// The block that processed the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="long"/> type.</value>
        /// 
        public long Height { get; set; }

        /// <summary>
        /// The transaction type.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="int"/> type.</value>
        /// 
        public int Type { get; set; }

        /// <summary>
        /// The timestamp of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="int"/> type.</value>
        /// 
        public int Timestamp { get; set; }

        /// <summary>
        /// The amount the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="long"/> type.</value>
        /// 
        public long Amount { get; set; }

        /// <summary>
        /// The loader id.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="long"/> type.</value>
        /// 
        public long Fee { get; set; }

        /// <summary>
        /// The vendor field of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string VendorField { get; set; }

        /// <summary>
        /// The sender if of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string SenderId { get; set; }

        /// <summary>
        /// The recepient id of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string RecipientId { get; set; }

        /// <summary>
        /// The sender public key of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string SenderPublicKey { get; set; }

        /// <summary>
        /// The signature that signed the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string Signature { get; set; }

        /// <summary>
        /// The second signature that signed the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string SignSignature { get; set; }

        /// <summary>
        /// The asset of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="ArkAsset"/> type.</value>
        /// 
        public ArkAsset Asset { get; set; }

        /// <summary>
        /// The number of confirmations received by the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="int"/> type.</value>
        /// 
        public int Confirmations { get; set; }

        #endregion
    }
}