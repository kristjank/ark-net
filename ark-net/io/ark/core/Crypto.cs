using io.ark.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;
using System.Security.Cryptography;
using Validation;
using io.ark.utils;
using org.bitcoinj.core;
using com.google.common.io;

namespace io.ark.core
{
    public class Crypto
    {

        public static ECKey.ECDSASignature Sign(Transaction t, String passphrase)
        {
            byte[] txbytes = GetBytes(t);
            return SignBytes(txbytes, passphrase);
        }

        public static ECKey.ECDSASignature SecondSign(Transaction t, String passphrase)
        {
            byte[] txbytes = GetBytes(t, false);
            return SignBytes(txbytes, passphrase);
        }

        public static ECKey.ECDSASignature SignBytes(byte[] bytes, String passphrase)
        {
            ECKey keys = GetKeys(passphrase);
            return keys.sign(Sha256Hash.of(bytes));
        }

        public static bool Verify(Transaction t)
        {
            ECKey keys = ECKey.fromPublicOnly(BaseEncoding.base16().lowerCase().decode(t.SenderPublicKey));
            byte[] signature = BaseEncoding.base16().lowerCase().decode(t.Signature);
            byte[] bytes = GetBytes(t);

            return VerifyBytes(bytes, signature, keys.getPubKey());
  }

        public static bool SecondVerify(Transaction t, String secondPublicKeyHex)
        {
            ECKey keys = ECKey.fromPublicOnly(BaseEncoding.base16().lowerCase().decode(secondPublicKeyHex));
            byte[] signature = BaseEncoding.base16().lowerCase().decode(t.SignSignature);
            byte[] bytes = GetBytes(t, false);

            return VerifyBytes(bytes, signature, keys.getPubKey());
        }

        public static bool VerifyBytes(byte[] bytes, byte[] signature, byte[] publicKey)
        {
            return ECKey.verify(Sha256Hash.hash(bytes), signature, publicKey);
        }

        public static byte[] GetBytes(Transaction t, bool skipSignature = true, bool skipSecondSignature = true)
        {
            return t.ToBytes(skipSignature, skipSecondSignature);
        }
        
        public static String GetId(Transaction t)
        {
            return BaseEncoding.base16().lowerCase().encode(Sha256Hash.hash(GetBytes(t, false, false)));
        }
        
        public static ECKey GetKeys(String passphrase)
        {
            byte[] sha256 = Sha256Hash.hash(Encoding.ASCII.GetBytes(passphrase));
            ECKey keys = ECKey.fromPrivate(sha256, true);
            
            return keys;      
        }

        public static String GetAddress(ECKey keys, byte version = 0x17)
        {
            return GetAddress(keys.getPubKey(), version);
        }

        public static String GetAddress(byte[] publicKey, byte version = 0x17)
        {
            RipeMD160Digest digest = new RipeMD160Digest();
            digest.BlockUpdate(publicKey, 0, publicKey.Length);

            byte[] keyHash = new byte[20];
            digest.DoFinal(keyHash, 0);

            //VersionedChecksummedBytes b = new VersionedChecksummedBytes(version, keyHash);

            BCAVersionedChecksummedBytes address = new BCAVersionedChecksummedBytes(version, keyHash);            
            return address.ToString();
            
        }
        
    }
}
