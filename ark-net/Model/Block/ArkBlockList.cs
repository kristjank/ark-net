// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkBlockList.cs" company="Ark Labs">
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
using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Block
{
    /// <summary>
    /// Extends the <see cref="ArkResponseBase"/> type for the Ark Block List response model.
    /// </summary>
    /// 
    public class ArkBlockList : ArkResponseBase
    {
        #region Fields

        /// <summary>
        /// The requested list of blocks.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="List"/> of type <see cref="ArkBlock"/>.</value>
        /// 
        public List<ArkBlock> Blocks { get; set; }

        #endregion
    }
}
