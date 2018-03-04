// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Block.cs" company="Ark Labs">
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
// <summary>
//   Defines a Block on the Blockchain
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ArkNet.Core
{
    using JetBrains.Annotations;

    /// <summary>
    /// A block in the blockchain.
    /// (Implementation is for V2)
    /// </summary>
    [UsedImplicitly]
    public class Block
    {
       /* byte version = 0;
        int height;
        string previousBlock;
        long totalAmount;
        long totalFee;
        long reward;
        string payloadHash;
        int timestamp;
        int numberOfTransactions;
        int payloadLength;
        int size;
        string generatorPublicKey;
        List<Transaction> transactions = new List<Transaction>();
        List<Transaction> transactionsIds = new List<Transaction>();
        string blockSignature;
        string id;

        public byte[] getBytes(bool includeSignature = false)
        {
            ByteBuffer buffer = ByteBuffer.allocate(1000);
            buffer.order(ByteOrder.LITTLE_ENDIAN);

            buffer.put(version);
            buffer.putInt(timestamp);
            buffer.putInt(height);
            buffer.put(new BigInteger(previousBlock).toByteArray());  //ported to C#
            buffer.putInt(numberOfTransactions);
            buffer.putLong(totalAmount);
            buffer.putLong(totalFee);
            buffer.putLong(reward);

            buffer.putInt(payloadLength);

            if (payloadHash == null)
            {
                //TODO: create payloadhash from transactions
            }
            buffer.put(BaseEncoding.base16().lowerCase().decode(payloadHash));
            buffer.put(BaseEncoding.base16().lowerCase().decode(generatorPublicKey));

            if (includeSignature)
            {
                buffer.put(BaseEncoding.base16().lowerCase().decode(blockSignature));
            }

            byte[] outBuffer = new byte[buffer.position()];

            buffer.rewind();
            buffer.get(outBuffer);

            return outBuffer;
        }

        public String Sign(String passphrase)
        {
            blockSignature = BaseEncoding.base16().lowerCase().encode(Crypto.SignBytes(getBytes(), passphrase).encodeToDER());
            return blockSignature;
        }

        public bool Verify()
        {
            ECKey keys = ECKey.fromPublicOnly(BaseEncoding.base16().lowerCase().decode(generatorPublicKey));
            byte[] signature = BaseEncoding.base16().lowerCase().decode(blockSignature);
            byte[] bytes = getBytes();

            return Crypto.VerifyBytes(bytes, signature, keys.getPubKey());
        }

        public String GetId()
        {
            byte[] bytes = getBytes(true);
            byte[] bytesid = bytes.Take(7).ToArray();
            id = new BigInteger(bytesid).toString();
            return id;
        }*/

    }
}
