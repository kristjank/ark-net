// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkSettings.cs" company="Ark">
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

using System.ComponentModel;

namespace ArkNet.Utils.Enum
{
    /// <summary>
    /// Represents an Ark transaction type.
    /// </summary>
    public enum TransactionType : byte
    {
        /// <summary>
        /// Indicates that Ark was sent to a recepient on the Ark network.
        /// </summary>
        [Description("Send Ark")]
        SendArk,

        /// <summary>
        /// Indicates that Ark was spent to register a delegeate on the Ark network.
        /// </summary>
        [Description("Create a Delegate")]
        CreateDelegate,

        /// <summary>
        /// Indicates that Ark was spent to issue a vote for a candidate deligate.
        /// </summary>
        [Description("Vote for a Delegate")]
        VoteDelegate,

        /// <summary>
        /// Indicates that Ark was spent to acquire multi-signature transaction signing.
        /// </summary>
        [Description("Multisignature")]
        MultiSignature
    }
}
