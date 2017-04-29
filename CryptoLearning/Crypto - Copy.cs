/*using io.ark.core;
using System;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;
using io.ark.utils;
using BitCoinSharp;
using SimpleBase;

namespace io.ark.core
{
    public class CryptoNET
    {

        public static byte[] Sign(TransactionNET t, String passphrase)
        {
            byte[] txbytes = GetBytes(t);
            return SignBytes(txbytes, passphrase);

        }

        public static byte[] SecondSign(TransactionNET t, String passphrase)
        {
            byte[] txbytes = GetBytes(t, false);
            return SignBytes(txbytes, passphrase);
        }

        public static byte[] SignBytes(byte[] bytes, String passphrase)
        {
            EcKey keys = GetKeys(passphrase);
            //EcKey aa = new EcKey(passphrase);

            return keys.Sign(new Sha256Hash(bytes).Bytes);
        }

        public static bool Verify(TransactionNET t)
        {
            EcKey keys = EcKey.FromAsn1(Base16.Decode(t.SenderPublicKey));

            byte[] signature = Base16.Decode(t.Signature);
            byte[] bytes = GetBytes(t);

            return VerifyBytes(bytes, signature, keys.PubKey);
  }

        public static bool SecondVerify(TransactionNET t, String secondPublicKeyHex)
        {
            EcKey keys = EcKey.FromAsn1(Base16.Decode(secondPublicKeyHex));

            byte[] signature = Base16.Decode(t.SignSignature);
            byte[] bytes = GetBytes(t, false);

            return VerifyBytes(bytes, signature, keys.PubKey);
        }

        public static bool VerifyBytes(byte[] bytes, byte[] signature, byte[] publicKey)
        {
            return EcKey.Verify(new Sha256Hash(bytes).Bytes, signature, publicKey);
        }

        public static byte[] GetBytes(TransactionNET t, bool skipSignature = true, bool skipSecondSignature = true)
        {
            return t.ToBytes(skipSignature, skipSecondSignature);
        }
        
        public static String GetId(TransactionNET t)
        {
            return Base16.EncodeLower(new Sha256Hash(GetBytes(t, false, false)).Bytes);
        }
        
        public static EcKey GetKeys(String passphrase)
        {
            byte[] sha256 = new Sha256Hash(Encoding.ASCII.GetBytes(passphrase)).Bytes;
            //new Sha256Hash(Encoding.ASCII.GetBytes(passphrase)).Bytes
            EcKey key = new EcKey();
            key.Sign(sha256);
            return key;
        }

        public static String GetAddress(EcKey keys, byte version = 0x17)
        {
            return GetAddress(keys.PubKey, version);
        }

        public static String GetAddress(byte[] publicKey, byte version = 0x17)
        {
            RipeMD160Digest digest = new RipeMD160Digest();
            digest.BlockUpdate(publicKey, 0, publicKey.Length);

            byte[] keyHash = new byte[20];
            digest.DoFinal(keyHash, 0);

            //EcKey.ECDSASignatureersionedChecksummedBytes b = new VersionedChecksummedBytes(version, keyHash);

            BCAVersionedChecksummedBytes address = new BCAVersionedChecksummedBytes(version, keyHash);            
            return address.ToString();
            
        }
        
    }
}
*/