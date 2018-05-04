// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkDelegateForgedBalance.cs" company="Ark">
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

namespace ArkNet.Model.Delegate
{
    /// <summary>
    /// Extends the <see cref="ArkResponseBase"/> type for the Ark Delegate Forged balance response model.
    /// </summary>
    /// 
    public class ArkDelegateForgedBalance : ArkResponseBase
    {
        #region Fields

        /// <summary>
        /// Received transaction fee of processed block.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="string"/>.</value>
        /// 
        public long Fees { get; set; }

        /// <summary>
        /// Amount rewarded to the delegate.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="long"/>.</value>
        /// 
        public long Rewards { get; set; }

        /// <summary>
        /// Amount forged by the delegate.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a an <see cref="long"/>.</value>
        /// 
        public long Forged { get; set; }

        #endregion
    }
}