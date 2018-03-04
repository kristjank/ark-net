// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Crypto.cs" company="Ark Labs">
//   MIT License
//   // 
//   // Copyright (c) 2017 Kristjan Košič
//   // 
//   // Permission is hereby granted, free of charge, to any person obtaining a copy
//   // of this software and associated documentation files (the "Software"), to deal
//   // in the Software without restriction, including without limitation the rights
//   // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   // copies of the Software, and to permit persons to whom the Software is
//   // furnished to do so, subject to the following conditions:
//   // 
//   // The above copyright notice and this permission notice shall be included in all
//   // copies or substantial portions of the Software.
//   // 
//   // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   // SOFTWARE.
// </copyright>
// <summary>
//   Defines a Block on the Blockchain
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;
using ArkNet.Utils;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;

namespace ArkNet.Core
{
    /// <summary>
    /// Cryptography part of the API.
    /// </summary>
    public class Crypto
	{
	    /// <summary>
	    /// Initialize the Sha 256.
	    /// </summary>
	    private static readonly SHA256 Sha256 = SHA256.Create();

	    /// <summary>
	    /// Initialize The ripemd 160.
	    /// </summary>
	    private static readonly SshNet.Security.Cryptography.RIPEMD160 Ripemd160 = new SshNet.Security.Cryptography.RIPEMD160();

	    /// <summary>
	    /// Sign Transactions.
	    /// </summary>
	    /// <param name="t">
	    /// <see cref="TransactionApi"/> passed on to be signed.
	    /// </param>
	    /// <param name="passphrase">
	    /// User's Passphrase
	    /// </param>
	    /// <returns>
	    /// The <see cref="ECDSASignature"/> signed Transaction.
	    /// </returns>
	    public static ECDSASignature Sign(TransactionApi t, string passphrase)
		{
			var txbytes = GetBytes(t);
			return SignBytes(txbytes, passphrase);
		}

	    /// <summary>
	    /// Sign Transaction with the second passphrasse.
	    /// </summary>
	    /// <param name="t">
	    /// <see cref="TransactionApi"/> to be signed.
	    /// </param>
	    /// <param name="passphrase">
	    /// The SECOND passphrase.
	    /// </param>
	    /// <returns>
	    /// The <see cref="ECDSASignature"/> signed transaction.
	    /// </returns>
	    public static ECDSASignature SecondSign(TransactionApi t, string passphrase)
		{
			var txbytes = GetBytes(t, false);
			return SignBytes(txbytes, passphrase);
		}

	    /// <summary>
	    ///  Sign a string of bytes.
	    /// </summary>
	    /// <param name="bytes">
	    /// bytes to be signed.
	    /// </param>
	    /// <param name="passphrase">
	    /// User's passphrase.
	    /// </param>
	    /// <returns>
	    /// The <see cref="ECDSASignature"/> signed bytes.
	    /// </returns>
	    public static ECDSASignature SignBytes(byte[] bytes, string passphrase)
		{
			var keys = GetKeys(passphrase);
			var s1 = Sha256.ComputeHash(bytes);
			var signature = keys.Sign(new uint256(s1));
			return signature;
		}

	    /// <summary>
	    /// Verify a transaction.
	    /// </summary>
	    /// <param name="t">
	    /// Transaction to verify.
	    /// </param>
	    /// <returns>
	    /// The <see cref="bool"/> sucess of the verification.
	    /// </returns>
	    public static bool Verify(TransactionApi t)
		{
			var key = new PubKey(Encoders.Hex.DecodeData(t.SenderPublicKey));
			var signature = Encoders.Hex.DecodeData(t.Signature);
			var bytes = GetBytes(t);

			return key.Verify(new uint256(Sha256.ComputeHash(bytes)), signature);
		}

	    /// <summary>
	    /// Verification of a transaction with the second passphrase.
	    /// </summary>
	    /// <param name="t">
	    /// The <see cref="TransactionApi"/> that is being verified.
	    /// </param>
	    /// <param name="secondPublicKeyHex">
	    /// User's second Passphrase.
	    /// </param>
	    /// <returns>
	    /// The <see cref="bool"/> reporting the success of the transaction.
	    /// </returns>
	    public static bool SecondVerify(TransactionApi t, string secondPublicKeyHex)
		{
			var key = new PubKey(Encoders.Hex.DecodeData(secondPublicKeyHex));

			var signature = Encoders.Hex.DecodeData(t.SignSignature);
			var bytes = GetBytes(t, false);

			return key.Verify(new uint256(Sha256.ComputeHash(bytes)), signature);
		}

	    /// <summary>
	    /// Get the bytes of a transaction.
	    /// </summary>
	    /// <param name="t">
	    /// The <see cref="TransactionApi"/> whose bytes you want to get.
	    /// </param>
	    /// <param name="skipSignature">
	    /// <see cref="bool"/> Skip first signature.
	    /// Default = true.
	    /// </param>
	    /// <param name="skipSecondSignature">
	    /// <see cref="bool"/> Skip second signature.
	    /// </param>
	    /// <returns>
	    /// The <see cref="byte[]"/>.
	    /// </returns>
	    public static byte[] GetBytes(TransactionApi t, bool skipSignature = true, bool skipSecondSignature = true)
		{
			return t.ToBytes(skipSignature, skipSecondSignature);
		}

	    /// <summary>
	    /// Get the Transaction's id.
	    /// </summary>
	    /// <param name="t">
	    /// Transaction whose ID is wanted.
	    /// </param>
	    /// <returns>
	    /// The <see cref="string"/> ID of the transaction.
	    /// </returns>
	    public static string GetId(TransactionApi t)
		{
			return Encoders.Hex.EncodeData(Sha256.ComputeHash(GetBytes(t, false, false)));
		}

	    /// <summary>
	    /// Get the Key from the Passphrase.
	    /// </summary>
	    /// <param name="passphrase">
	    /// User's Passphrase.
	    /// </param>
	    /// <returns>
	    /// The <see cref="Key"/> of the user's account.
	    /// </returns>
	    public static Key GetKeys(string passphrase)
		{
			var sha256h = Sha256.ComputeHash(Encoding.ASCII.GetBytes(passphrase));
			return new Key(sha256h);
		}

	    /// <summary>
	    /// Get the Private Address from the Key.
	    /// </summary>
	    /// <param name="keys">
	    /// Account's Key.
	    /// </param>
	    /// <param name="version">
	    /// Version of the Blockchain.
	    /// </param>
	    /// <returns>
	    /// The Address of the Account, as a <see cref="string"/>.
	    /// </returns>
	    public static string GetAddress(Key keys, byte version)
		{
		    // keys.S
			return GetAddress(keys.PubKey.ToBytes(), version);
		}

	    /// <summary>
	    /// Get the Public Addres from the key.
	    /// </summary>
	    /// <param name="publicKey">
	    /// The public key.
	    /// </param>
	    /// <param name="version">
	    /// The version of the Blockchain.
	    /// </param>
	    /// <returns>
	    /// The Public Address as a <see cref="string"/>.
	    /// </returns>
	    public static string GetAddress(byte[] publicKey, byte version)
		{
			var keyHash = Ripemd160.ComputeHash(publicKey, 0, publicKey.Length);
			var address = new BCAVersionedChecksummedBytes(version, keyHash);

			return address.ToString();
		}
	}
}