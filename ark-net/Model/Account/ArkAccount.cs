// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkAccount.cs" company="Ark Labs">
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

namespace ArkNet.Model.Account
{
    /// <summary>
    /// Represents an Ark Account model as an <see cref="ArkAccount"/> type.
    /// </summary>
    /// 
    public class ArkAccount
    {
        /// <summary>
        /// Address of the account.
        /// 
        /// <para>Gets/sets the value as a <see cref="System.String"/>.</para>
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/>.</value>
        /// 
        public string Address { get; set; }

        /// <summary>
        /// Unconfirmed balance of the account.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="System.Int64"/>.</value>
        /// 
        public long UnconfirmedBalance { get; set; }

        /// <summary>
        /// Balance of the account.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="System.Int64"/>.</value>
        /// 
        public long Balance { get; set; }

        /// <summary>
        /// Public key of account as a hexadecimal number.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/></value>
        /// 
        public string PublicKey { get; set; }

        /// <summary>
        /// The unconfirmed signature of account.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.Boolean"/>.</value>
        /// 
        /// <remarks>
        /// The unconfirmed signature is second account signature for accounts which have enabled but 
        /// have not confirmed it.
        /// </remarks>
        /// 
        public int UnconfirmedSignature { get; set; }

        /// <summary>
        /// If account enabled second signature.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="System.Int32"/>.</value>
        /// 
        // note: the desired property type might have been System.Boolean
        public int SecondSignature { get; set; }

        /// <summary>
        /// Second  public key of the account as as a hexadecimal number.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as a <see cref="System.String"/>.</value>
        /// 
        public object SecondPublicKey { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        /// 
        /// <value></value>
        /// 
        public object[] Multisignatures { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        /// 
        /// <value></value>
        /// 
        public object[] U_Multisignatures { get; set; }
    }
}