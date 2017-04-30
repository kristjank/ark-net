using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils;
using BitCoinSharp;
using NBitcoin;
using Org.BouncyCastle.Math;

namespace CryptoLearning
{
    class Program
    {
        public static string GetKeys(string pass)
        {
	        /*var sha256 = Sha256Hash.hash(Encoding.ASCII.GetBytes(passphrase));
	        var keys = ECKey.fromPrivate(sha256, true);
			*/
			return null;
        }

        

        static void Main(string[] args)
        {
            SHA256 sha256 = SHA256.Create();
            var sha256h = sha256.ComputeHash(Encoding.ASCII.GetBytes("this is a top secret passphrase"));
			// Sha256Hash.hash(Encoding.ASCII.GetBytes(passphrase));

			// dobim public private key od NBITCOIN
            Key aa = new Key(sha256h);


			//zračunam naslov 
	        var keyHash = new byte[20];
			RIPEMD160 myRIPEMD160 = RIPEMD160Managed.Create();
	        keyHash = myRIPEMD160.ComputeHash(aa.PubKey.ToBytes(), 0, aa.PubKey.ToBytes().Length);
	        var address = new BCAVersionedChecksummedBytes(0x17, keyHash);
			// return address.ToString();

	        //Crypto.Sign(this, passphrase).encodeToDER()
	        //aa.Sign().ToDER();

			BitcoinAddress abra = BitcoinAddress.Create("");
            //abra.V


        }
	}
}
