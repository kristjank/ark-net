using io.ark.core;
using org.bitcoinj.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NBitcoin;

namespace ark_console
{
    class Program
    {
        static void Main(string[] args)
        {

            /* Network arkMainNet = new Network("ark");
             var passphraseCode = new BitcoinPassphraseCode("my secret", "ark", null);

             EncryptedKeyResult encryptedKeyResult = passphraseCode.GenerateEncryptedSecret();

             var generatedAddress = encryptedKeyResult.GeneratedAddress;
             var encryptedKey = encryptedKeyResult.EncryptedKey;
             var confirmationCode = encryptedKeyResult.ConfirmationCode;

             Console.WriteLine(generatedAddress); // 14KZsAVLwafhttaykXxCZt95HqadPXuz73
             Console.WriteLine(encryptedKey); // 6PnWtBokjVKMjuSQit1h1Ph6rLMSFz2n4u3bjPJH1JMcp1WHqVSfr5ebNS
             Console.WriteLine(confirmationCode); // cfrm38VUcrdt2zf1dCgf4e8gPNJJxnhJSdxYg6STRAEs7QuAuLJmT5W7uNqj88hzh9bBnU9GFkN

             Console.WriteLine(confirmationCode.Check("my secret", generatedAddress)); // True
             var bitcoinPrivateKey = encryptedKey.GetSecret("my secret");
             Console.WriteLine(bitcoinPrivateKey.GetAddress() == generatedAddress); // True
             Console.WriteLine(bitcoinPrivateKey); // KzzHhrkr39a7upeqHzYNNeJuaf1SVDBpxdFDuMvFKbFhcBytDF1R

             Console.ReadLine();*/

            /*byte[] passphrase = Encoding.Unicode.GetBytes("this is my passphrase");

            byte[] sha256 = Sha256Hash.hash(passphrase);
            ECKey keys = ECKey.fromPrivate(sha256, true);*/


            /*String a1 = Crypto.GetAddress(Crypto.GetKeys("this is a top secret passphrase"));
            String a2 = "AGeYmgbg2LgGxRW2vNNJvQ88PknEJsYizC";

            if (a1 == a2)
                Console.WriteLine("asdsaklda");

            Console.Write(Crypto.GetKeys("this is a top secret passphrase").getPrivKey());
            Console.ReadLine();*/

            io.ark.core.Transaction tx = io.ark.core.Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                           133380000000,
                                                           "This is first transaction from JAVA",
                                                           "this is a top secret passphrase");
            String json = tx.ToJson();
            Console.WriteLine(json);

            io.ark.core.Transaction tx2 = io.ark.core.Transaction.FromJson(json);

            Console.WriteLine(json);
            Console.ReadLine();

        }
    }
}
