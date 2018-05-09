// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkBlock.cs" company="Ark">
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


namespace ArkNet.Model.Block
{
    /// <summary>
    /// Represents an Ark Block model.
    /// </summary>
    /// 
    public class ArkBlock
    {
        #region Fields

        /// <summary>
        /// Id of the block. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/>.</value>
        /// 
        public string Id { get; set; }

        /// <summary>
        /// Version of the block. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="int"/>.</value>
        /// 
        public int Version { get; set; }

        /// <summary>
        /// Timestamp of the block. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="int"/>.</value>
        /// 
        public int Timestamp { get; set; }

        /// <summary>
        /// Height of the block. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="int"/>.</value>
        /// 
        public int Height { get; set; }

        /// <summary>
        /// Previous block id. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/>.</value>
        /// 
        public string PreviousBlock { get; set; }

        /// <summary>
        /// Number of transactions in the block.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="int"/>.</value>
        /// 
        public int NumberOfTransactions { get; set; }

        /// <summary>
        /// Total amount of block. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="long"/>.</value>
        /// 
        public long TotalAmount { get; set; }

        /// <summary>
        /// Total fee of block. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="long"/>.</value>
        /// 
        public long TotalFee { get; set; }

        /// <summary>
        /// Reward of block.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="long"/>.</value>
        /// 
        public long Reward { get; set; }

        /// <summary>
        /// Payload length of block. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="int"/>.</value>
        /// 
        public int PayloadLength { get; set; }

        /// <summary>
        /// Payload hash of block.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/>.</value>
        /// 
        public string PayloadHash { get; set; }

        /// <summary>
        /// Generator public key as a hexadecimal number.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/>.</value>
        public string GeneratorPublicKey { get; set; }

        /// <summary>
        /// Generator id. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/>.</value>
        /// 
        public string GeneratorId { get; set; }

        /// <summary>
        /// Block signature as a hexadecimal number.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/>.</value>
        /// 
        public string BlockSignature { get; set; }

        /// <summary>
        /// Number of confirmations.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="long"/>.</value>
        /// 
        public long Confirmations { get; set; }

        /// <summary>
        /// Total forged by delegates. 
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="long"/>.</value>
        /// 
        public long TotalForged { get; set; }

        #endregion
    }
}
