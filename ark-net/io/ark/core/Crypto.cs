using io.ark.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin.DataEncoders;
using NBitcoin.Crypto;
using NBitcoin.JsonConverters;
using Org.BouncyCastle.Crypto.Digests;
using System.Security.Cryptography;
using NBitcoin;
using Validation;
using io.ark.utils;

namespace io.ark.core
{
    public class Crypto
    {

        public static byte[] getKeys(String passphrase)
        {
            /* byte[] sha256 = 
                 Sha256Hash.hash(passphrase.bytes)
             ECKey keys = ECKey.fromPrivate(sha256, true)
             return keys
             */                      

            byte[] bytes = Encoding.Unicode.GetBytes(passphrase);
            byte[] sha256Hash = Hashes.SHA256(bytes);


            Key key = new Key(); //Create a new key
            BitcoinSecret secret = key.GetBitcoinSecret(Network.Main);
            Console.WriteLine(secret); //Will print the key in base58 check format
            BitcoinEncryptedSecret encrypted = secret.Encrypt(passphrase);
            Console.WriteLine(encrypted); //Will print the encrypted key in base58 check format
            key = encrypted.GetKey(passphrase); //Get the same key as before

            return key.PubKey.ToBytes();

        }


        public static String getAddress(byte[] publicKey, byte version = 0x17)
        {
            RipeMD160Digest digest = new RipeMD160Digest();
            digest.BlockUpdate(publicKey, 0, publicKey.Length);

            byte[] keyHash = new byte[20];
            digest.DoFinal(keyHash, 0);
            
            return new VersionedChecksummedBytes(version, keyHash).ToString();
            
        }

        

    }
}
