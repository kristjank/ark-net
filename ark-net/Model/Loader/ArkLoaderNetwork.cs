// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkLoaderNetwork.cs" company="Ark">
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

namespace ArkNet.Model.Loader
{
    /// <summary>
    /// Represents an Ark Loader Network model.
    /// </summary>
    /// 
    public class ArkLoaderNetwork
    {
        #region Fields

        /// <summary>
        /// The loader net hash.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string NetHash { get; set; }

        /// <summary>
        /// The loader token.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string Token { get; set; }

        /// <summary>
        /// The loader symbol.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string Symbol { get; set; }

        /// <summary>
        /// The loader explorer.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="string"/> type.</value>
        /// 
        public string Explorer { get; set; }

        /// <summary>
        /// The loader version
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="int"/> type.</value>
        /// 
        public int Version { get; set; }

        #endregion
    }
}
