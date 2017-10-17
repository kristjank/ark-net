namespace ArkNet.Core
{
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
