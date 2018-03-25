// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkDelegateNextForgers.cs" company="Ark Labs">
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

namespace ArkNet.Model.Delegate
{
    /// <summary>
    /// Represents an Ark Delgate Next Forgers model.
    /// </summary>
    /// 
    public class ArkDelegateNextForgers
    {
        #region Fields

        /// <summary>
        /// The current block height.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="long"/>.</value>
        /// 
        public long CurrentBlock { get; set; }

        /// <summary>
        /// The current delegate slot.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="long"/>.</value>
        /// 
        public long CurrentSlot { get; set; }

        /// <summary>
        /// A list of next forger delegates.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="long"/>.</value>
        /// 
        public List<string> Delegates { get; set; }

        #endregion
    }
}