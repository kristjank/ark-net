// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkPeerHeader.cs" company="Ark Labs">
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

namespace ArkNet.Model.Peer
{
    /// <summary>
    /// Represents an Ark Peer Header model.
    /// </summary>
    /// 
    public class ArkPeerHeader
    {
        #region Fields

        /// <summary>
        /// The peer id.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/> type.</value>
        /// 
        public string Id { get; set; }

        /// <summary>
        /// The current block of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int Height { get; set; }

        /// <summary>
        /// The version of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int Version { get; set; }

        /// <summary>
        /// The total transaction amount of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int64"/> type.</value>
        /// 
        public long TotalAmount { get; set; }

        /// <summary>
        /// The total fee of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int64"/> type.</value>
        /// 
        public long TotalFee { get; set; }

        /// <summary>
        /// The reward of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int64"/> type.</value>
        /// 
        public long Reward { get; set; }

        /// <summary>
        /// The payload hash of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/> type.</value>
        /// 
        public string PayloadHash { get; set; }

        /// <summary>
        /// The payload length of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int PayloadLength { get; set; }

        /// <summary>
        /// The timestamp of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int Timestamp { get; set; }

        /// <summary>
        /// Number of transactions of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int NumberOfTransactions { get; set; }

        /// <summary>
        /// The previous block id of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/> type.</value>
        /// 
        public string PreviousBlock { get; set; }

        /// <summary>
        /// The generator public key of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/> type.</value>
        /// 
        public string GeneratorPublicKey { get; set; }

        /// <summary>
        /// The block signature of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/> type.</value>
        /// 
        public string BlockSignature { get; set; }

        #endregion
    }
}
