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

using System;
using System.Collections.Generic;
using Validation;

namespace ArkNet.Utils
{

    public class BCAVersionedChecksummedBytes
    {
        private readonly int version;
        private readonly IReadOnlyList<byte> bytes;

        protected BCAVersionedChecksummedBytes(string encoded)
        {
            byte[] tmp = Base58.DecodeChecked(encoded);
            this.version = tmp[0] & 0xFF;
            var bytes = new byte[tmp.Length - 1];
            Array.Copy(tmp, 1, bytes, 0, tmp.Length - 1);
            this.bytes = bytes;
        }

        public BCAVersionedChecksummedBytes(int version, byte[] bytes)
        {
            Requires.Range(version < 256 && version >= 0, "version");
            this.version = version;
            this.bytes = bytes;
        }

        /// <summary>
        /// Returns the "version" or "header" byte: the first byte of the data. This is used to disambiguate what the
        /// contents apply to, for example, which network the key or address is valid on.
        /// </summary>
        /// <value>A positive number between 0 and 255.</value>
        public int Version
        {
            get { return this.version; }
        }

        protected IReadOnlyList<byte> Bytes
        {
            get { return this.bytes; }
        }

        public override int GetHashCode()
        {
            return Utils.GetArrayHashCode(this.bytes);
        }

        public override bool Equals(object o)
        {
            var other = o as BCAVersionedChecksummedBytes;
            if (other == null)
            {
                return false;
            }

            return Utils.ArraysEqual(other.bytes, this.bytes);
        }

        public override string ToString()
        {
            // A stringified buffer is:
            //   1 byte version + data bytes + 4 bytes check code (a truncated hash)
            byte[] addressBytes = new byte[1 + bytes.Count + 4];
            addressBytes[0] = (byte)version;
            bytes.CopyTo(0, addressBytes, 1, bytes.Count);
            byte[] check = Utils.DoubleDigest(addressBytes, 0, bytes.Count + 1);
            Array.Copy(check, 0, addressBytes, bytes.Count + 1, 4);
            return Base58.Encode(addressBytes);
        }
    }

}
