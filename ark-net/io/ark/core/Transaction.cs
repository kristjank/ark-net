using com.google.common.io;
using java.nio;
using Newtonsoft.Json;
using org.bitcoinj.core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.core
{

    public class Transaction
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
        Dictionary<String, dynamic> asset = new Dictionary<string, dynamic>();
        String username;
        ArrayList votes;
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
        public string Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public ArrayList Votes { get => votes; set => votes = value; }

        public byte[] ToBytes(bool skipSignature = true, bool skipSecondSignature = true)
        {
            ByteBuffer buffer = ByteBuffer.allocate(1000);
            buffer.order(ByteOrder.LITTLE_ENDIAN);

            buffer.put(type);
            buffer.putInt(timestamp);
            buffer.put(BaseEncoding.base16().lowerCase().decode(senderPublicKey));

            if (RequesterPublicKey != null)
            {
                buffer.put(Base58.decodeChecked(requesterPublicKey));
            }

            if (recipientId != null)
            {
                buffer.put(Base58.decodeChecked(recipientId));
            }
            else
            {
                buffer.put(new byte[21]);
            }

            if (vendorField != null)
            {
                byte[] vbytes = Encoding.ASCII.GetBytes(vendorField);
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

            buffer.putLong(amount);
            buffer.putLong(fee);

            if (type == 1)
            {
                buffer.put(BaseEncoding.base16().lowerCase().decode(this.signature));
            }
            else if (type == 2)
            {
                buffer.put(Encoding.ASCII.GetBytes(this.asset["username"]));
            }
            else if (type == 3)
            {
                buffer.put(this.asset["votes"].join("").bytes);
            }
            // TODO: multisignature
            // else if(type==4){
            //   buffer.put BaseEncoding.base16().lowerCase().decode(asset.signature)
            // }

            if (!skipSignature && signature.Length > 0)
            {
                buffer.put(BaseEncoding.base16().lowerCase().decode(signature));
            }
            if (!skipSecondSignature && signSignature != null)
            {
                buffer.put(BaseEncoding.base16().lowerCase().decode(signSignature));
            }

            byte[] outBuffer = new byte[buffer.position()];

            buffer.rewind();
            buffer.get(outBuffer);
            return outBuffer;
        }

        public dynamic ToObject(bool retJson=true)
        {
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic> {
                ["id"] = this.id,
                ["timestamp"] = this.timestamp,
                ["recipientId"] = this.recipientId,
                ["amount"] = this.amount,            
                ["fee"] = this.fee,
                ["type"] = this.type,
                ["vendorField"] = this.vendorField,
                ["signature"] = this.signature,
                ["signSignature"] = this.signSignature,
                ["senderPublicKey"] = this.senderPublicKey,
                ["requesterPublicKey"] = this.requesterPublicKey,
                ["asset"] = this.asset

            };

            if (retJson)
                return JsonConvert.SerializeObject(data);
            else
                return data;

            //this.properties.subMap(['id', 'timestamp', 'recipientId', 'amount', 'fee', 'type', 'vendorField', 'signature', 'signSignature', 
            //'senderPublicKey', 'requesterPublicKey', 'asset'])
        }


        public String Sign(String passphrase)
        {
            senderPublicKey = BaseEncoding.base16().lowerCase().encode(Crypto.GetKeys(passphrase).getPubKey());
            signature = BaseEncoding.base16().lowerCase().encode(Crypto.Sign(this, passphrase).encodeToDER());

            return signature;
        }

        public String SecondSign(String passphrase)
        {
            signSignature = BaseEncoding.base16().lowerCase().encode(Crypto.SecondSign(this, passphrase).encodeToDER());

            return signSignature;
        }

        public String ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        
        public static Transaction FromJson(String json)
        {
            Transaction tx = new Transaction();
            tx = JsonConvert.DeserializeObject<Transaction>(json);
            return tx;
        }

        private Transaction()
        {
        }

        private Transaction(byte type, String recipientId, long amount, long fee, String vendorField)
        {
            this.type = type;
            this.recipientId = recipientId;
            this.amount = amount;
            this.fee = fee;
            this.vendorField = vendorField;
        }

        private Transaction(byte type, long amount, long fee)
        {
            this.type = type;
            this.amount = amount;
            this.fee = fee;
        }

        public static Transaction CreateTransaction(String recipientId, long satoshiAmount, String vendorField, String passphrase, String secondPassphrase = null)
        {
            Transaction tx = new Transaction(0, recipientId, satoshiAmount, 10000000, vendorField);
            tx.timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            if (secondPassphrase != null)
                tx.SecondSign(secondPassphrase);

            tx.id = Crypto.GetId(tx);
            return tx;
        }

        public static Transaction CreateVote(ArrayList votes, String passphrase, String secondPassphrase = null)
        {
            Transaction tx = new Transaction(3, 0, 100000000);
            tx.asset.Add("votes", votes);
            tx.timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            if (secondPassphrase != null)
                tx.SecondSign(secondPassphrase);

            tx.id = Crypto.GetId(tx);

            return tx;
        }

        public static Transaction CreateDelegate(String username, String passphrase, String secondPassphrase = null)
        {
            Transaction tx = new Transaction(2, 0, 2500000000);
            tx.asset.Add("username", username);
            tx.timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            if (secondPassphrase != null)
                tx.SecondSign(secondPassphrase);

            tx.id = Crypto.GetId(tx);
            return tx;
        }

        public static Transaction createSecondSignature(String secondPassphrase, String passphrase)
        {
            Transaction tx = new Transaction(1, 0, 500000000);
            tx.signature = BaseEncoding.base16().lowerCase().encode(Crypto.GetKeys(secondPassphrase).getPubKey());
            tx.timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            tx.id = Crypto.GetId(tx);
            return tx;
  }

    }

}
