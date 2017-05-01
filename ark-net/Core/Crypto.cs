using System.Security.Cryptography;
using System.Text;
using ArkNet.Utils;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;

namespace ArkNet.Core
{
	public class Crypto
	{
		private static readonly SHA256 Sha256 = SHA256.Create();
		private static readonly RIPEMD160 Ripemd160 = RIPEMD160.Create();

		public static ECDSASignature Sign(ArkTransaction t, string passphrase)
		{
			var txbytes = GetBytes(t);
			return SignBytes(txbytes, passphrase);
		}

		public static ECDSASignature SecondSign(ArkTransaction t, string passphrase)
		{
			var txbytes = GetBytes(t, false);
			return SignBytes(txbytes, passphrase);
		}

		public static ECDSASignature SignBytes(byte[] bytes, string passphrase)
		{
			var keys = GetKeys(passphrase);
			var s1 = Sha256.ComputeHash(bytes);
			var signature = keys.Sign(new uint256(s1));
			return signature;
		}

		public static bool Verify(ArkTransaction t)
		{
			var key = new PubKey(Encoders.Hex.DecodeData(t.SenderPublicKey));
			var signature = Encoders.Hex.DecodeData(t.Signature);
			var bytes = GetBytes(t);

			return key.Verify(new uint256(Sha256.ComputeHash(bytes)), signature);
		}

		public static bool SecondVerify(ArkTransaction t, string secondPublicKeyHex)
		{
			var key = new PubKey(Encoders.Hex.DecodeData(secondPublicKeyHex));

			var signature = Encoders.Hex.DecodeData(t.SignSignature);
			var bytes = GetBytes(t, false);

			return key.Verify(new uint256(Sha256.ComputeHash(bytes)), signature);
		}


		public static byte[] GetBytes(ArkTransaction t, bool skipSignature = true, bool skipSecondSignature = true)
		{
			return t.ToBytes(skipSignature, skipSecondSignature);
		}

		public static string GetId(ArkTransaction t)
		{
			return Encoders.Hex.EncodeData(Sha256.ComputeHash(GetBytes(t, false, false)));
		}

		public static Key GetKeys(string passphrase)
		{
			var sha256h = Sha256.ComputeHash(Encoding.ASCII.GetBytes(passphrase));
			return new Key(sha256h);
		}

		public static string GetAddress(Key keys, byte version = 0x17)
		{
			//keys.S
			return GetAddress(keys.PubKey.ToBytes(), version);
		}

		public static string GetAddress(byte[] publicKey, byte version = 0x17)
		{
			var keyHash = Ripemd160.ComputeHash(publicKey, 0, publicKey.Length);
			var address = new BCAVersionedChecksummedBytes(version, keyHash);

			return address.ToString();
		}
	}
}