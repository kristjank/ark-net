using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.core
{

    class Transaction
    {
        int timestamp;
        String recipientId;
        long amount;
        long fee;
        byte type;
        String vendorField;
        String signature;
        String signSignature;
        String senderPublicKey;
        String requesterPublicKey;
        Dictionary<String, Object> asset;
        String id;

        public int Timestamp { get => timestamp; set => timestamp = value; }
        public string RecipientId { get => recipientId; set => recipientId = value; }
        public long Amount { get => amount; set => amount = value; }
        public long Fee { get => fee; set => fee = value; }
        public byte Type { get => type; set => type = value; }
        public string VendorField { get => vendorField; set => vendorField = value; }
        public string Signature { get => signature; set => signature = value; }
        public string SignSignature { get => signSignature; set => signSignature = value; }
        public string SenderPublicKey { get => senderPublicKey; set => senderPublicKey = value; }
        public string RequesterPublicKey { get => requesterPublicKey; set => requesterPublicKey = value; }
        public Dictionary<string, object> Asset { get => asset; set => asset = value; }
        public string Id { get => id; set => id = value; }








        public byte[] toBytes(Boolean skipSignature = true, Boolean skipSecondSignature = true)
        {
            MemoryStream aa = new MemoryStream(1000);
            aa.By

            BinaryReader reader = new BinaryReader()
            req

            ByteBuffer buffer = ByteBuffer.allocate(1000)
    buffer.order(ByteOrder.LITTLE_ENDIAN)

    buffer.put type
    buffer.put Int timestamp
    buffer.put BaseEncoding.base16().lowerCase().decode(senderPublicKey)

    if (requesterPublicKey)
            {
                buffer.put Base58.decodeChecked(requesterPublicKey)
    }

            if (recipientId)
            {
                buffer.put Base58.decodeChecked(recipientId)
            }
            else
            {
                buffer.put new byte[21]
            }

            if (vendorField)
            {
                byte[] vbytes = vendorField.bytes
              if (vbytes.size() < 65)
                {
                    buffer.put vbytes
        buffer.put new byte[64 - vbytes.size()]
      }
            }
            else
            {
                buffer.put new byte[64]
            }

            buffer.putLong amount
    buffer.putLong fee

    if (type == 1)
            {
                buffer.put BaseEncoding.base16().lowerCase().decode(asset.signature)
    }
            else if (type == 2)
            {
                buffer.put asset.username.bytes
            }
            else if (type == 3)
            {
                buffer.put asset.votes.join("").bytes
            }
            // TODO: multisignature
            // else if(type==4){
            //   buffer.put BaseEncoding.base16().lowerCase().decode(asset.signature)
            // }

            if (!skipSignature && signature)
            {
                buffer.put BaseEncoding.base16().lowerCase().decode(signature)
            }
            if (!skipSecondSignature && signSignature)
            {
                buffer.put BaseEncoding.base16().lowerCase().decode(signSignature)
            }

            def outBuffer = new byte[buffer.position()]
    buffer.rewind()
    buffer.get(outBuffer)
    return outBuffer
  }














    }

}
