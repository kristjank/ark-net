// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkPeer.cs" company="Ark Labs">
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
    /// Represents an Ark Peer model.
    /// </summary>
    /// 
    public class ArkPeer
    {
        #region Fields

        /// <summary>
        /// Ip address of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/> type.</value>
        /// 
        public string Ip { get; set; }

        /// <summary>
        /// The port of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int Port { get; set; }

        /// <summary>
        /// The version of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/> type.</value>
        /// 
        public string Version { get; set; }

        /// <summary>
        /// The operating system of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/> type.</value>
        /// 
        public string Os { get; set; }

        /// <summary>
        /// The current block of the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int Height { get; set; }

        /// <summary>
        /// The status of the peer.
        /// 
        /// 1 - disconnected
        /// 2 - connected
        /// 0 - banned
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/> type.</value>
        /// 
        public string Status { get; set; }

        /// <summary>
        /// The round-trip time to the peer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int Delay { get; set; }

        /// <summary>
        /// Number of errors.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int Errors { get; set; }

        #endregion
    }
}