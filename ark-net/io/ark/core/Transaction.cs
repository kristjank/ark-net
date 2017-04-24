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









    }

}
