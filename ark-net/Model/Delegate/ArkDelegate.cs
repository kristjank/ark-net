// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkDelegate.cs" company="Ark">
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

namespace ArkNet.Model.Delegate
{
    /// <summary>
    /// Represents an Ark Delegate model.
    /// </summary>
    /// 
    public class ArkDelegate
    {
        #region Fields

        /// <summary>
        /// Username of the delegate.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="string"/>.</value>
        /// 
        public string Username { get; set; }

        /// <summary>
        /// Address of the delegate.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="string"/>.</value>
        /// 
        public string Address { get; set; }

        /// <summary>
        /// Public key of the delegate as a hex string.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/></value>
        /// 
        public string PublicKey { get; set; }

        /// <summary>
        /// Amount of stake voted for this delgate.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="long"/>.</value>
        /// 
        public long Vote { get; set; }

        /// <summary>
        /// Number of blocks produced by the delgate.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="int"/>.</value>
        /// 
        public int Producedblocks { get; set; }

        /// <summary>
        /// Number of blocks missed by the delegate.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="int"/>.</value>
        /// 
        public int Missedblocks { get; set; }

        /// <summary>
        /// Rate at which the delegate processes transactions.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="int"/>.</value>
        /// 
        public int Rate { get; set; }

        /// <summary>
        /// Approval rating of the delgate (percentage).
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="float"/>.</value>
        /// 
        public float Approval { get; set; }

        /// <summary>
        /// Productivity rating of delegate (percentage).
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="float"/>.</value>
        /// 
        public float Productivity { get; set; }

        #endregion
    }
}