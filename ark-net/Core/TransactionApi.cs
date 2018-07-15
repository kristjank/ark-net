// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionApi.cs" company="Ark">
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
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using ArkNet.Utils;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;

namespace ArkNet.Core
{
    // References:
    // Slot.cs
    // Crypto.cs
    // TransactionService.cs
    // AccountController.cs

    /// <summary>
    /// Provides functionality for creating Ark transaction representations.
    /// </summary>
    public class TransactionApi
    {
        private NetworkApi _networkApi;
        private LoggingApi _logger;
        #region Read Only

        /// <summary>
        /// The assets assiociated with this transaction.
        /// </summary>
        /// 
        private readonly Dictionary<string, dynamic> asset = new Dictionary<string, dynamic>();

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the <see cref="TransactionApi"/> type.
        /// </summary>
        /// 
        public TransactionApi(NetworkApi networkApi, LoggingApi logger)
        {
            _networkApi = networkApi;
            _logger = logger;
        }

        /// <summary>
        /// Instantiates a new instance of the <see cref="TransactionApi"/> type.
        /// </summary>
        /// <param name="type">The type of the transaction.</param>
        /// <param name="recipientId">The address of the transaction's recipient.</param>
        /// <param name="amount">The amount transacted.</param>
        /// <param name="fee">The fee of the transaction.</param>
        /// <param name="vendorField">
        /// The vendor field of the transaction.
        /// This is a 64 char long for V1, should 256 for V2.
        /// </param>
        /// 
		private TransactionApi(byte type, string recipientId, long amount, long fee, string vendorField)
        {
            Type = type;
            RecipientId = recipientId;
            Amount = amount;
            Fee = fee;
            VendorField = vendorField;
        }

        /// <summary>
        /// Instantiates a new instance of the <see cref="TransactionApi"/> type.
        /// </summary>
        /// <param name="type">The type of the transaction.</param>
        /// <param name="amount">The amount transacted.</param>
        /// <param name="fee">The fee of the transaction.</param>
		private TransactionApi(byte type, long amount, long fee)
        {
            Type = type;
            Amount = amount;
            Fee = fee;
        }

        #endregion

        #region Fields

        /// <summary>
        /// The timestamp of the transaction.
        /// </summary>
        /// <value>Gets/sets the value an <see cref="int"/>.</value>
        public int Timestamp { get; set; }

        /// <summary>
        /// The address of the transaction's recipient.
        /// </summary>
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
		public string RecipientId { get; set; }

        /// <summary>
        /// The amount transacted.
        /// </summary>
        /// <value>Gets/sets the value an <see cref="long"/>.</value>
		public long Amount { get; set; }

        /// <summary>
        /// The fee of the transaction.
        /// </summary>
        /// <value>Gets/sets the value an <see cref="long"/>.</value>
		public long Fee { get; set; }

        /// <summary>
        /// The type of the transaction.
        /// 
        /// <para>See <see cref="TransactionType"/>.</para>
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref= "int" />.</value>
        /// 
		public byte Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
		public string VendorField { get; set; }

        /// <summary>
        /// The first signature as a hexadecimal string.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
		public string Signature { get; set; }

        /// <summary>
        /// The second signature as a hexadecimal string.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
		public string SignSignature { get; set; }

        /// <summary>
        /// The sender public key of the transaction as a hexadecimal string.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
		public string SenderPublicKey { get; set; }

        /// <summary>
        /// The requester public key as a hexadecimal string.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
		public string RequesterPublicKey { get; set; }

        /// <summary>
        /// The id of the transaction.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
        public string Id { get; set; }

        /// <summary>
        /// The username.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
		public string Username { get; set; }

        /// <summary>
        /// The encoded hexadecimal representation of this object.
        /// </summary>
        /// 
        /// <value>Gets/sets the value an <see cref="string"/>.</value>
        /// 
        [JsonIgnore]
        public string StrBytes { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a byte buffer from the transaction data.
        /// </summary>
        /// 
        /// <param name="skipSignature">Determines whether to skip the first signature.</param>
        /// 
        /// <param name="skipSecondSignature">Determines whether to skip the second signature.</param>
        /// 
        /// <returns>Returns the byte buffer.</returns>
        /// 
        public byte[] ToBytes(bool skipSignature = true, bool skipSecondSignature = true)
        {
            try
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
                {
                    buffer.Put(Encoding.ASCII.GetBytes(string.Join(string.Empty, asset["votes"])));
                }

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
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Creates an object from the transaction data.
        /// </summary>
        /// 
        /// <param name="retJson">Determines whether the returned object will be a Json representation.</param>
        /// 
        /// <returns>
        /// Returns a <see cref="Dictionary{string,dynamic}"/> type or a <see cref="string"/> type if the <c>retJson</c>
        /// paramater is set to true.
        /// </returns>
        /// 
		public dynamic ToObject(bool retJson = false)
        {
            try
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
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Signs this instance of the <see cref="TransactionApi"/> with the first passphrase.
        /// </summary>
        /// 
        /// <param name="passphrase">The first passphrase.</param>
        /// 
        /// <returns>Returns the the encoded first signature as a <see cref="string"/>.</returns>
        /// 
		public string Sign(string passphrase)
        {
            try
            {
                SenderPublicKey = Encoders.Hex.EncodeData(Crypto.GetKeys(passphrase).PubKey.ToBytes());
                Signature = Encoders.Hex.EncodeData(Crypto.Sign(this, passphrase).ToDER());

                return Signature;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Signs this instance of the <see cref="TransactionApi"/> with the second passphrase.
        /// </summary>
        /// 
        /// <param name="passphrase">The second passphrase.</param>
        /// 
        /// <returns>Returns the the encoded second signature as a <see cref="string"/>.</returns>
        /// 
		public string SecondSign(string passphrase)
        {
            try
            {
                SignSignature = Encoders.Hex.EncodeData(Crypto.SecondSign(this, passphrase).ToDER());

                return SignSignature;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Creates a Json representation of this <see cref="TransactionApi"/> instance.
        /// </summary>
        /// 
        /// <returns>Returns a Json <see cref="string"/> type.</returns>
        /// 
		public string ToJson()
        {
            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Creates a <see cref="TransactionApi"/> type object from a Json representation.
        /// </summary>
        /// 
        /// <param name="json">The Json representation of the object.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="TransactionApi"/> type.</returns>
        /// 
		public TransactionApi FromJson(string json)
        {
            try
            {
                var tx = JsonConvert.DeserializeObject<TransactionApi>(json);
                tx._networkApi = _networkApi;
                tx._logger = _logger;
                return tx;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Creates a transaction.
        /// </summary>
        /// 
        /// <param name="recipientId">The address of the transaction's recipient.</param>
        /// 
        /// <param name="satoshiAmount">The amount in satoshi of the transaction.</param>
        /// 
        /// <param name="vendorField">The vendorfield of the transaction.</param>
        /// 
        /// <param name="passphrase">The account first's passphrase.</param>
        /// 
        /// <param name="secondPassphrase">The account's second passphrase.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="TransactionApi"/> type.</returns>
        /// 
		public TransactionApi CreateTransaction(string recipientId, long satoshiAmount, string vendorField,
            string passphrase, string secondPassphrase = null)
        {
            try
            {
                var tx = new TransactionApi(0, recipientId, satoshiAmount, _networkApi.NetworkSettings.Fee.Send, vendorField);
                tx.Timestamp = Slot.GetTime();
                tx.Sign(passphrase);
                tx.StrBytes = Encoders.Hex.EncodeData(tx.ToBytes());
                if (secondPassphrase != null)
                    tx.SecondSign(secondPassphrase);

                tx.Id = Crypto.GetId(tx);
                _logger.Info(string.Format("Creating transaction <<{0}>>", JsonConvert.SerializeObject(tx)));
                return tx;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Creates a vote transaction.
        /// </summary>
        /// 
        /// <param name="votes">The votes.</param>
        /// 
        /// <param name="passphrase">The account's first passphrase.</param>
        /// 
        /// <param name="secondPassphrase">The account's second passphrase.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="TransactionApi"/> type.</returns>
        /// 
		public TransactionApi CreateVote(List<string> votes, string passphrase, string secondPassphrase = null)
        {
            try
            {
                var tx = new TransactionApi(3, 0, _networkApi.NetworkSettings.Fee.Vote);
                tx.asset.Add("votes", votes);
                tx.Timestamp = Slot.GetTime();
                tx.RecipientId = Crypto.GetAddress(Crypto.GetKeys(passphrase), _networkApi.NetworkSettings.BytePrefix);
                tx.Sign(passphrase);
                tx.StrBytes = Encoders.Hex.EncodeData(tx.ToBytes());
                if (secondPassphrase != null)
                    tx.SecondSign(secondPassphrase);

                tx.Id = Crypto.GetId(tx);
                _logger.Info(string.Format("Creating vote transaction <<{0}>>", JsonConvert.SerializeObject(tx)));

                return tx;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Creates a delegate transaction.
        /// </summary>
        /// 
        /// <param name="username">The delegate's username.</param>
        /// 
        /// <param name="passphrase">The account's first passphrase.</param>
        /// 
        /// <param name="secondPassphrase">The account's second passphrase.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="TransactionApi"/> type.</returns>
        /// 
		public TransactionApi CreateDelegate(string username, string passphrase, string secondPassphrase = null)
        {
            try
            {
                var tx = new TransactionApi(2, 0, _networkApi.NetworkSettings.Fee.Delegate);
                tx.asset.Add("username", username);
                tx.Timestamp = Slot.GetTime();
                tx.Sign(passphrase);
                if (secondPassphrase != null)
                    tx.SecondSign(secondPassphrase);

                tx.Id = Crypto.GetId(tx);
                _logger.Info(string.Format("Creating delegate transaction <<{0}>>", JsonConvert.SerializeObject(tx)));
                return tx;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Creates a second signature transaction.
        /// </summary>
        /// 
        /// <param name="secondPassphrase">The second pass phrase.</param>
        /// 
        /// <param name="passphrase">The first passphrase.</param>
        /// 
        /// <returns>Returns a signed instance of a <see cref="TransactionApi"/> type.</returns>
        /// 
		public TransactionApi createSecondSignature(string secondPassphrase, string passphrase)
        {
            try
            {
                var tx = new TransactionApi(1, 0, _networkApi.NetworkSettings.Fee.SecondSignature)
                {
                    Signature = Encoders.Hex.EncodeData(Crypto.GetKeys(secondPassphrase).PubKey.ToBytes()),
                    Timestamp = Slot.GetTime()
                };

                tx.Sign(passphrase);
                tx.Id = Crypto.GetId(tx);
                _logger.Info(string.Format("Creating second signature transaction <<{0}>>", JsonConvert.SerializeObject(tx)));
                return tx;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        #endregion
    }
}
