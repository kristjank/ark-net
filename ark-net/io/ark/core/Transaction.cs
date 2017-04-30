using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Support;
using NBitcoin.DataEncoders;
using io.ark.utils;
using NBitcoin.Crypto;

namespace io.ark.core
{

    public class Transaction
    {
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

        public int Timestamp { get; set; }

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
            ByteBuffer buffer = ByteBuffer.Allocate(1000);
            buffer.Order = ByteOrder.LITTLE_ENDIAN;
            
            buffer.Put(type);
            buffer.PutInt32(Timestamp);
            buffer.Put(Encoders.Hex.DecodeData(senderPublicKey));

            if (RequesterPublicKey != null)
            {
                buffer.Put(Encoders.Base58Check.DecodeData(requesterPublicKey));
            }

            if (recipientId != null)
            {
                buffer.Put(Encoders.Base58Check.DecodeData(recipientId));
            }
            else
            {
                buffer.Put(new byte[21]);
            }

            if (vendorField != null)
            {
                byte[] vbytes = Encoding.ASCII.GetBytes(vendorField);
                if (vbytes.Length < 65)
                {
                    buffer.Put(vbytes);
                    buffer.Put(new byte[64 - vbytes.Length]);
                }
            }
            else
            {
                buffer.Put(new byte[64]);
            }

            buffer.PutInt64(amount);
            buffer.PutInt64(fee);

            if (type == 1)
            {
                buffer.Put(Encoders.Hex.DecodeData(this.signature));
            }
            else if (type == 2)
            {
                buffer.Put(Encoding.ASCII.GetBytes(this.asset["username"]));
            }
            else if (type == 3)
            {
                buffer.Put(this.asset["votes"].join("").bytes);
            }
            // TODO: multisignature
            // else if(type==4){
            //   buffer.put BaseEncoding.base16().lowerCase().decode(asset.signature)
            // }

            if (!skipSignature && signature.Length > 0)
            {
                buffer.Put(Encoders.Hex.DecodeData(signature));
            }
            if (!skipSecondSignature && signSignature != null)
            {
                buffer.Put(Encoders.Hex.DecodeData(signSignature));
            }

            byte[] outBuffer = new byte[buffer.Position];

            buffer.Rewind();
            buffer.Get(outBuffer);
            return outBuffer;
        }

        public dynamic ToObject(bool retJson=false)
        {
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic> {
                ["id"] = this.id,
                ["timestamp"] = this.Timestamp,
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
            senderPublicKey = Encoders.Hex.EncodeData(Crypto.GetKeys(passphrase).PubKey.ToBytes());
            signature = Encoders.Hex.EncodeData(Crypto.Sign(this, passphrase).ToDER());
	        


			return signature;
        }

        public String SecondSign(String passphrase)
        {
            signSignature = Encoders.Hex.EncodeData(Crypto.SecondSign(this, passphrase).ToDER());

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
            tx.Timestamp = Slot.GetTime();
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
            tx.Timestamp = Slot.GetTime();
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
            tx.Timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            if (secondPassphrase != null)
                tx.SecondSign(secondPassphrase);

            tx.id = Crypto.GetId(tx);
            return tx;
        }

        public static Transaction createSecondSignature(String secondPassphrase, String passphrase)
        {
            Transaction tx = new Transaction(1, 0, 500000000);
            tx.signature = Encoders.Hex.EncodeData(Crypto.GetKeys(secondPassphrase).PubKey.ToBytes());
            tx.Timestamp = Slot.GetTime();
            tx.Sign(passphrase);
            tx.id = Crypto.GetId(tx);
            return tx;
  }

    }

}
