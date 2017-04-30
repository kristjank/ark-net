using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.google.common.io;
using java.nio;
using Newtonsoft.Json;
using org.bitcoinj.core;

namespace io.ark.core
{
    public class Transaction
    {
        private readonly Dictionary<string, dynamic> asset = new Dictionary<string, dynamic>();

        
        private byte type;
        private ArrayList votes;

        private Transaction()
        {
        }

        private Transaction(byte type, string recipientId, long amount, long fee, string vendorField)
        {
            this.type = type;
            RecipientId = recipientId;
            Amount = amount;
            Fee = fee;
            VendorField = vendorField;
        }

        private Transaction(byte type, long amount, long fee)
        {
            this.type = type;
            Amount = amount;
            Fee = fee;
        }

        public int Timestamp { get; set; }

        public string RecipientId { get; set; }

        public long Amount { get; set; }

        public long Fee { get; set; }

        public byte Type
        {
            get => type;
            set => type = value;
        }

        public string VendorField { get; set; }

        public string Signature { get; set; }

        public string SignSignature { get; set; }

        public string SenderPublicKey { get; set; }

        public string RequesterPublicKey { get; set; }

        public string Id { get; set; }

        public string Username { get; set; }

        public string StrBytes { get; set; }

    public ArrayList Votes
        {
            get => votes;
            set => votes = value;
        }

        public byte[] ToBytes(bool skipSignature = true, bool skipSecondSignature = true)
        {
            var buffer = ByteBuffer.allocate(1000);
            buffer.order(ByteOrder.LITTLE_ENDIAN);

            buffer.put(type);
            buffer.putInt(Timestamp);
            buffer.put(BaseEncoding.base16().lowerCase().decode(SenderPublicKey));

            if (RequesterPublicKey != null)
                buffer.put(Base58.decodeChecked(RequesterPublicKey));

            if (RecipientId != null)
                buffer.put(Base58.decodeChecked(RecipientId));
            else
                buffer.put(new byte[21]);

            if (VendorField != null)
            {
                var vbytes = Encoding.ASCII.GetBytes(VendorField);
                if (vbytes.Length < 65)
                {
                    buffer.put(vbytes);
                    buffer.put(new byte[64 - vbytes.Length]);
                }
            }
            else
            {
                buffer.put(new byte[64]);
            }

            buffer.putLong(Amount);
            buffer.putLong(Fee);

            if (type == 1)
                buffer.put(BaseEncoding.base16().lowerCase().decode(Signature));
            else if (type == 2)
                buffer.put(Encoding.ASCII.GetBytes(asset["username"]));
            else if (type == 3)
                buffer.put(asset["votes"].join("").bytes);
            // TODO: multisignature
            // else if(type==4){
            //   buffer.put BaseEncoding.base16().lowerCase().decode(asset.signature)
            // }

            if (!skipSignature && Signature.Length > 0)
                buffer.put(BaseEncoding.base16().lowerCase().decode(Signature));
            if (!skipSecondSignature && SignSignature != null)
                buffer.put(BaseEncoding.base16().lowerCase().decode(SignSignature));

            var outBuffer = new byte[buffer.position()];

            buffer.rewind();
            buffer.get(outBuffer);
            return outBuffer;
        }

        public dynamic ToObject(bool retJson = false)
        {
            var data = new Dictionary<string, dynamic>
            {
                ["id"] = Id,
                ["timestamp"] = Timestamp,
                ["recipientId"] = RecipientId,
                ["amount"] = Amount,
                ["fee"] = Fee,
                ["type"] = type,
                ["vendorField"] = VendorField,
                ["signature"] = Signature,
                ["signSignature"] = SignSignature,
                ["senderPublicKey"] = SenderPublicKey,
                ["requesterPublicKey"] = RequesterPublicKey,
                ["asset"] = asset
            };

            if (retJson)
                return JsonConvert.SerializeObject(data);
            return data;

            //this.properties.subMap(['id', 'timestamp', 'recipientId', 'amount', 'fee', 'type', 'vendorField', 'signature', 'signSignature', 
            //'senderPublicKey', 'requesterPublicKey', 'asset'])
        }


        public string Sign(string passphrase)
        {
            SenderPublicKey = BaseEncoding.base16().lowerCase().encode(Crypto.GetKeys(passphrase).getPubKey());
            Signature = BaseEncoding.base16().lowerCase().encode(Crypto.Sign(this, passphrase).encodeToDER());

            return Signature;
        }

        public string SecondSign(string passphrase)
        {
            SignSignature = BaseEncoding.base16().lowerCase().encode(Crypto.SecondSign(this, passphrase).encodeToDER());

            return SignSignature;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }


        public static Transaction FromJson(string json)
        {
            var tx = new Transaction();
            tx = JsonConvert.DeserializeObject<Transaction>(json);
            return tx;
        }

        public static Transaction CreateTransaction(string recipientId, long satoshiAmount, string vendorField,
            string passphrase, string secondPassphrase = null)
        {
            var tx = new Transaction(0, recipientId, satoshiAmount, 10000000, vendorField);
            tx.Timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            tx.StrBytes = BaseEncoding.base16().lowerCase().encode(tx.ToBytes());
            if (secondPassphrase != null)
                tx.SecondSign(secondPassphrase);

            tx.Id = Crypto.GetId(tx);
            return tx;
        }

        public static Transaction CreateVote(ArrayList votes, string passphrase, string secondPassphrase = null)
        {
            var tx = new Transaction(3, 0, 100000000);
            tx.asset.Add("votes", votes);
            tx.Timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            if (secondPassphrase != null)
                tx.SecondSign(secondPassphrase);

            tx.Id = Crypto.GetId(tx);

            return tx;
        }

        public static Transaction CreateDelegate(string username, string passphrase, string secondPassphrase = null)
        {
            var tx = new Transaction(2, 0, 2500000000);
            tx.asset.Add("username", username);
            tx.Timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            if (secondPassphrase != null)
                tx.SecondSign(secondPassphrase);

            tx.Id = Crypto.GetId(tx);
            return tx;
        }

        public static Transaction createSecondSignature(string secondPassphrase, string passphrase)
        {
            var tx = new Transaction(1, 0, 500000000);
            tx.Signature = BaseEncoding.base16().lowerCase().encode(Crypto.GetKeys(secondPassphrase).getPubKey());
            tx.Timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            tx.Id = Crypto.GetId(tx);
            return tx;
        }
    }
}