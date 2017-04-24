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

namespace io.ark.core
{
    public class Crypto
    {

        public static ECKey getKeys(String passphrase)
        {
           /* byte[] sha256 = 
                Sha256Hash.hash(passphrase.bytes)
            ECKey keys = ECKey.fromPrivate(sha256, true)
            return keys
            */

            byte[] bytes = Encoding.Unicode.GetBytes(passphrase);
            byte[] sha256Hash = Hashes.SHA256(bytes);

            BitcoinSecret


            

        }


        public static String getAddress(byte[] publicKey, byte version = 0x17)
        {
            RipeMD160Digest digest = new RipeMD160Digest();
            digest.BlockUpdate(publicKey, 0, publicKey.Length);

            byte[] keyHash = new byte[20];
            digest.DoFinal(keyHash, 0);

            //digest.ch
            //def address = new VersionedChecksummedBytes(version, out)

            string address = Encoders.Base58Check.EncodeData(keyHash);


            return address.ToString();
        }

    }
}
