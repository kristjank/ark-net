// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkPeerStatus.cs" company="Ark Labs">
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

using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Peer
{
    /// <summary>
    /// Extends the <see cref="ArkResponseBase"/> type for the Ark Peer Status response model.
    /// </summary>
    /// 
    public class ArkPeerStatus : ArkResponseBase
    {
        #region Fields

        /// <summary>
        /// The loader id.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int Height { get; set; }

        /// <summary>
        /// The loader id.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Boolean"/> type.</value>
        /// 
        public bool ForgingAllowed { get; set; }

        /// <summary>
        /// The loader id.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Int32"/> type.</value>
        /// 
        public int CurrentSlot { get; set; }

        /// <summary>
        /// The loader id.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="ArkPeerHeader"/> type.</value>
        /// 
        public ArkPeerHeader Header { get; set; }

        #endregion
    }
}