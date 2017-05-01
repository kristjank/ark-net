using System.Collections;
using System.Collections.Generic;
using System.Text;
using ArkNet.Utils;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;

namespace ArkNet.Core
{
	public class ArkTransaction
	{
		private readonly Dictionary<string, dynamic> asset = new Dictionary<string, dynamic>();

		private ArkTransaction()
		{
		}

		private ArkTransaction(byte type, string recipientId, long amount, long fee, string vendorField)
		{
			Type = type;
			RecipientId = recipientId;
			Amount = amount;
			Fee = fee;
			VendorField = vendorField;
		}

		private ArkTransaction(byte type, long amount, long fee)
		{
			Type = type;
			Amount = amount;
			Fee = fee;
		}

		public int Timestamp { get; set; }

		public string RecipientId { get; set; }

		public long Amount { get; set; }

		public long Fee { get; set; }

		public byte Type { get; set; }

		public string VendorField { get; set; }

		public string Signature { get; set; }

		public string SignSignature { get; set; }

		public string SenderPublicKey { get; set; }

		public string RequesterPublicKey { get; set; }

		public string Id { get; set; }

		public string Username { get; set; }

		public ArrayList Votes { get; set; }

		public string StrBytes { get; set; }

		public byte[] ToBytes(bool skipSignature = true, bool skipSecondSignature = true)
		{
			var buffer = ByteBuffer.Allocate(1000);
			buffer.Order = ByteOrder.LITTLE_ENDIAN;

			buffer.Put(Type);
			buffer.PutInt32(Timestamp);
			buffer.Put(Encoders.Hex.DecodeData(SenderPublicKey));

			if (RequesterPublicKey != null)
				buffer.Put(Encoders.Base58Check.DecodeData(RequesterPublicKey));

			if (RecipientId != null)
				buffer.Put(Encoders.Base58Check.DecodeData(RecipientId));
			else
				buffer.Put(new byte[21]);

			if (VendorField != null)
			{
				var vbytes = Encoding.ASCII.GetBytes(VendorField);
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

			buffer.PutInt64(Amount);
			buffer.PutInt64(Fee);

			if (Type == 1)
				buffer.Put(Encoders.Hex.DecodeData(Signature));
			else if (Type == 2)
				buffer.Put(Encoding.ASCII.GetBytes(asset["username"]));
			else if (Type == 3)
				buffer.Put(asset["votes"].join("").bytes);
			// TODO: multisignature
			// else if(type==4){
			//   buffer.put BaseEncoding.base16().lowerCase().decode(asset.signature)
			// }

			if (!skipSignature && Signature.Length > 0)
				buffer.Put(Encoders.Hex.DecodeData(Signature));
			if (!skipSecondSignature && SignSignature != null)
				buffer.Put(Encoders.Hex.DecodeData(SignSignature));

			var outBuffer = new byte[buffer.Position];

			buffer.Rewind();
			buffer.Get(outBuffer);
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
				["type"] = Type,
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
			SenderPublicKey = Encoders.Hex.EncodeData(Crypto.GetKeys(passphrase).PubKey.ToBytes());
			Signature = Encoders.Hex.EncodeData(Crypto.Sign(this, passphrase).ToDER());

			return Signature;
		}

		public string SecondSign(string passphrase)
		{
			SignSignature = Encoders.Hex.EncodeData(Crypto.SecondSign(this, passphrase).ToDER());

			return SignSignature;
		}

		public string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}


		public static ArkTransaction FromJson(string json)
		{
			var tx = new ArkTransaction();
			tx = JsonConvert.DeserializeObject<ArkTransaction>(json);
			return tx;
		}

		public static ArkTransaction CreateTransaction(string recipientId, long satoshiAmount, string vendorField,
			string passphrase, string secondPassphrase = null)
		{
			var tx = new ArkTransaction(0, recipientId, satoshiAmount, 10000000, vendorField);
			tx.Timestamp = Slot.GetTime();

			tx.Sign(passphrase);
			tx.StrBytes = Encoders.Hex.EncodeData(tx.ToBytes());
			if (secondPassphrase != null)
				tx.SecondSign(secondPassphrase);

			tx.Id = Crypto.GetId(tx);
			return tx;
		}

		public static ArkTransaction CreateVote(ArrayList votes, string passphrase, string secondPassphrase = null)
		{
			var tx = new ArkTransaction(3, 0, 100000000);
			tx.asset.Add("votes", votes);
			tx.Timestamp = Slot.GetTime();
			tx.Sign(passphrase);
			if (secondPassphrase != null)
				tx.SecondSign(secondPassphrase);

			tx.Id = Crypto.GetId(tx);

			return tx;
		}

		public static ArkTransaction CreateDelegate(string username, string passphrase, string secondPassphrase = null)
		{
			var tx = new ArkTransaction(2, 0, 2500000000);
			tx.asset.Add("username", username);
			tx.Timestamp = Slot.GetTime();
			tx.Sign(passphrase);
			if (secondPassphrase != null)
				tx.SecondSign(secondPassphrase);

			tx.Id = Crypto.GetId(tx);
			return tx;
		}

		public static ArkTransaction createSecondSignature(string secondPassphrase, string passphrase)
		{
			var tx = new ArkTransaction(1, 0, 500000000);
			tx.Signature = Encoders.Hex.EncodeData(Crypto.GetKeys(secondPassphrase).PubKey.ToBytes());
			tx.Timestamp = Slot.GetTime();
			tx.Sign(passphrase);
			tx.Id = Crypto.GetId(tx);
			return tx;
		}
	}
}