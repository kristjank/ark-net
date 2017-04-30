using io.ark.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Validation;
using io.ark.utils;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using org.bitcoinj.core;

namespace io.ark.core
{
    public class Crypto
    {
	    private static readonly SHA256 Sha256 = SHA256.Create();
	    private static readonly RIPEMD160 Ripemd160 = RIPEMD160.Create();

		public static ECDSASignature Sign(Transaction t, string passphrase)
        {
            byte[] txbytes = GetBytes(t);
            return SignBytes(txbytes, passphrase);
        }

        public static ECDSASignature SecondSign(Transaction t, string passphrase)
        {
            byte[] txbytes = GetBytes(t, false);
            return SignBytes(txbytes, passphrase);
        }

        public static ECDSASignature SignBytes(byte[] bytes, string passphrase)
        {
            Key keys = GetKeys(passphrase);
	        

            //BitCoinSharp.Sha256Hash daHash =   new Sha256Hash(bytes);

            
            var data1 = Sha256Hash.of(bytes); // pravilno
            var s1 = Encoders.Hex.EncodeData(Sha256.ComputeHash(bytes));
            
            
            var signature = keys.Sign(uint256.Parse(s1));

            /*var hashes = Hashes.Hash256(data1.to);
            var signature = keys.Sign(data1.toBigInteger());
			var signature = keys.Sign();
            */
            return signature;

        }

        public static bool Verify(Transaction t)
        {
            PubKey key = new PubKey(Encoders.Hex.DecodeData(t.SenderPublicKey));
            byte[] signature = Encoders.Hex.DecodeData(t.Signature);
            byte[] bytes = GetBytes(t);

            return key.Verify(Hashes.Hash256(Sha256.ComputeHash(Sha256.ComputeHash(bytes))), signature);
        }

        public static bool SecondVerify(Transaction t, String secondPublicKeyHex)
        {
            /*ECKey keys = ECKey.fromPublicOnly(Encoders.Hex.DecodeData(secondPublicKeyHex));
            byte[] signature = Encoders.Hex.DecodeData(t.SignSignature);
            byte[] bytes = GetBytes(t, false);

            return VerifyBytes(bytes, signature, keys.getPubKey());*/
            return true;
        }

        /*public static bool VerifyBytes(byte[] bytes, byte[] signature, byte[] publicKey)
        {
            var signer = new ECDSASignature();
            signer.
	        signer.verifySignature(Sha256.ComputeHash(bytes), new BigInteger(1, signature), new BigInteger(1,publicKey));


			return ECKey.verify(Sha256Hash.hash(bytes), signature, publicKey);
            
        }*/

        public static byte[] GetBytes(Transaction t, bool skipSignature = true, bool skipSecondSignature = true)
        {
            return t.ToBytes(skipSignature, skipSecondSignature);
        }
        
        public static String GetId(Transaction t)
        {
            //return BaseEncoding.base16().lowerCase().encode(Sha256Hash.hash(GetBytes(t, false, false)));            
            return Encoders.Hex.EncodeData(Sha256.ComputeHash(GetBytes(t, false, false)));
        }
        
        public static Key GetKeys(String passphrase)
        {
			var sha256h = Sha256.ComputeHash(Encoding.ASCII.GetBytes(passphrase));
			return new Key(sha256h);      
        }

        public static String GetAddress(Key keys, byte version = 0x17)
        {
			//keys.S
            return GetAddress(keys.PubKey.ToBytes(), version);
        }

        public static String GetAddress(byte[] publicKey, byte version = 0x17)
        {
	        var keyHash = new byte[20];
	        keyHash = Ripemd160.ComputeHash(publicKey, 0, publicKey.Length);
	        var address = new BCAVersionedChecksummedBytes(0x17, keyHash);

            return address.ToString();
            
        }
        
    }
}
