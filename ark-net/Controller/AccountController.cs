// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Ark">
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

using System.Collections.Generic;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Model.Account;
using ArkNet.Model.Transactions;
using ArkNet.Service;

namespace ArkNet.Controller
{
    /// <summary>
    /// Account controller, used to interract with the account.
    /// <inheritdoc cref="ArkNetApi"/> Start() should be called prior to using this class.
    /// </summary>
    public class AccountController
    {
        private ArkNetApi _arkNetApi;
        /// <summary>
        /// <inheritdoc cref="ArkAccount"/>
        /// </summary>
        private ArkAccount _account;

        /// <summary>
        /// User's Pass Phrase as a <inheritdoc cref="string"/>
        /// Mandatory.
        /// </summary>
        private string _passPhrase;

        /// <summary>
        /// User's Second Pass Phrase as a <inheritdoc cref="string"/>
        /// Optional.
        /// </summary>
        private readonly string _secondPassPhrase;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="passphrase">
        /// User's Pass Phrase as a <inheritdoc cref="string"/>
        /// Mandatory.
        /// </param>
        /// <param name="secondPassPhrase">
        /// User's Second Pass Phrase as a <inheritdoc cref="string"/>
        /// Optional.
        /// </param>
        public AccountController(ArkNetApi arkNetApi, string passphrase, string secondPassPhrase = null)
        {
            _arkNetApi = arkNetApi;
            _passPhrase = passphrase;
            _secondPassPhrase = secondPassPhrase;
        }

        /// <summary>
        /// Get ark account's information.
        /// </summary>
        /// <returns>
        /// The <see cref="ArkAccount"/>.
        /// </returns>
        public ArkAccount GetArkAccount()
        {
            if (_account == null)
                _account = _arkNetApi.AccountService.GetByAddress(Crypto.GetAddress(Crypto.GetKeys(_passPhrase), _arkNetApi.NetworkApi.NetworkSettings.BytePrefix)).Account;

            //Account not on chain yet because it's a new account.
            if (_account == null)
            {
                _account = new ArkAccount()
                {
                    Address = Crypto.GetAddress(Crypto.GetKeys(_passPhrase), _arkNetApi.NetworkApi.NetworkSettings.BytePrefix),
                    PublicKey = Crypto.GetKeys(_passPhrase).PubKey.ToString()
                };
            }

            return _account;
        }

        /// <summary>
        /// Get the informations about the account asynchroneously.
        /// </summary>
        /// <returns>
        /// Return an <inheritdoc cref="ArkAccount"/>
        /// </returns>
        public async Task<ArkAccount> GetArkAccountAsync()
        {
            if (_account == null)
            {
                var accountResponse = await _arkNetApi.AccountService.GetByAddressAsync(Crypto.GetAddress(Crypto.GetKeys(_passPhrase), _arkNetApi.NetworkApi.NetworkSettings.BytePrefix)).ConfigureAwait(false);
                _account = accountResponse.Account;
            }
            return _account;
        }

        #region Will be implemented in V2
        // public bool AskRemoteSignature()
        // {
        // throw new NotImplementedException();
        // }

        // public void SendMultisignArk()
        // {
        // throw new NotImplementedException();
        // }
        #endregion

        /// <summary>
        /// Send Ark from the account to a given recipient.
        /// </summary>
        /// <param name="satoshiAmount">
        /// Amount transacted in satoshi (1E-8 Ark)
        /// </param>
        /// <param name="recipientAddress">
        /// Address of transaction's recipient.
        /// </param>
        /// <param name="vendorField">
        /// => Also refered as Smartbridge
        /// 64 chars <inheritdoc cref="string"/> with Ark V1, 
        /// It will be increased to 256 in V2.
        /// </param>
        /// <returns>
        /// The <see cref="ArkTransactionPostResponse"/>.
        /// </returns>
        public ArkTransactionPostResponse SendArk(long satoshiAmount, string recipientAddress,
                   string vendorField)
        {
            return _arkNetApi.TransactionService.PostTransaction(
                CreateTransaction(satoshiAmount, recipientAddress, vendorField));
        }

        /// <summary>
        /// Send Ark from the account to a given recipient asynchroneously.
        /// </summary>
        /// <param name="satoshiAmount">
        /// Amount transacted in satoshi (1E-8 Ark)
        /// </param>
        /// <param name="recipientAddress">
        /// Address of transaction's recipient.
        /// </param>
        /// <param name="vendorField">
        /// => Also referred as the Smartbridge
        /// 64 chars <inheritdoc cref="string"/> with Ark V1, 
        /// It will be increased to 256 in V2.
        /// </param>
        /// <returns>
        /// The <see cref="ArkTransactionPostResponse"/> from the transaction.
        /// </returns>
        public async Task<ArkTransactionPostResponse> SendArkAsync(long satoshiAmount, string recipientAddress,
            string vendorField)
        {
            return await _arkNetApi.TransactionService.PostTransactionAsync(
                CreateTransaction(satoshiAmount, recipientAddress, vendorField)).ConfigureAwait(false);
        }

        /// <summary>
        /// Send ark to multiple peers asynchroneously.
        /// </summary>
        /// <param name="satoshiAmount">
        /// Amount transacted in satoshi (1E-8 Ark)
        /// </param>
        /// <param name="recipientAddress">
        /// Address of transaction's recipient.
        /// </param>
        /// <param name="vendorField">
        /// => Also referred as Smartbridge
        /// 64 chars <inheritdoc cref="string"/> with Ark V1, 
        /// It will be increased to 256 in V2.
        /// </param>
        /// <returns>
        /// A list of <see cref="ArkTransactionPostResponse"/>.
        /// </returns>
        public List<ArkTransactionPostResponse> SendArkUsingMultiBroadCast(long satoshiAmount, string recipientAddress,
            string vendorField)
        {
            return _arkNetApi.TransactionService.MultipleBroadCast(
                CreateTransaction(satoshiAmount, recipientAddress, vendorField));
        }

        /// <summary>
        /// Send ark to multiple peers asynchroneously.
        /// </summary>
        /// <param name="satoshiAmount">
        /// Amount in satoshi (1E-8 Ark)
        /// </param>
        /// <param name="recipientAddress">
        /// Address of transaction's recipient.
        /// </param>
        /// <param name="vendorField">
        /// => Also Referred as Smartbridge
        /// 64 chars <inheritdoc cref="string"/> with Ark V1, 
        /// will be increased to 256 in V2.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<List<ArkTransactionPostResponse>> SendArkUsingMultiBroadCastAsync(long satoshiAmount, string recipientAddress,
            string vendorField)
        {
            return await _arkNetApi.TransactionService.MultipleBroadCastAsync(
                CreateTransaction(satoshiAmount, recipientAddress, vendorField)).ConfigureAwait(false);
        }

        /// <summary>
        /// Vote for a delegate.
        /// </summary>
        /// <param name="votes">
        /// TODO: The votes.
        /// </param>
        /// <returns>
        /// The <see cref="ArkTransactionPostResponse"/> of the voting transaction.
        /// </returns>
        public ArkTransactionPostResponse VoteForDelegate(List<string> votes)
        {
            var tx = _arkNetApi.TransactionApi.CreateVote(votes, _passPhrase, _secondPassPhrase);

            return _arkNetApi.TransactionService.PostTransaction(tx);
        }

        /// <summary>
        /// Vote for a delegate asynchroneously.
        /// </summary>
        /// <param name="votes">
        /// TODO: The votes.
        /// </param>
        /// <returns>
        /// The <see cref="ArkTransactionPostResponse"/> of the voting transaction.
        /// </returns>
        public async Task<ArkTransactionPostResponse> VoteForDelegateAsync(List<string> votes)
        {
            var tx = _arkNetApi.TransactionApi.CreateVote(votes, _passPhrase, _secondPassPhrase);

            return await _arkNetApi.TransactionService.PostTransactionAsync(tx).ConfigureAwait(false);
        }

        /// <summary>
        /// Register as a delegate.
        /// </summary>
        /// <param name="username">
        /// Username registered as a Delegate Name.
        /// </param>
        /// <returns>
        /// The <see cref="Task{ArkTransactionPostResponse}"/> of the Vote.
        /// </returns>
        public ArkTransactionPostResponse RegisterAsDelegate(string username)
        {
            var tx = _arkNetApi.TransactionApi.CreateDelegate(username, _passPhrase, _secondPassPhrase);

            return _arkNetApi.TransactionService.PostTransaction(tx);
        }

        /// <summary>
        /// Register as delegate asynchroneously.
        /// </summary>
        /// <param name="username">
        /// Username registered as a Delegate Name.
        /// </param>
        /// <returns>
        /// The <see cref="Task{ArkTransactionPostResponse}"/>.
        /// </returns>
        public async Task<ArkTransactionPostResponse> RegisterAsDelegateAsync(string username)
        {
            var tx = _arkNetApi.TransactionApi.CreateDelegate(username, _passPhrase, _secondPassPhrase);

            return await _arkNetApi.TransactionService.PostTransactionAsync(tx).ConfigureAwait(false);
        }

        /// <summary>
        /// Fetch the balance from the Network.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/> indicating if the balance of the account has been updated successfully or not.
        /// </returns>
        public bool UpdateBalance()
        {
            var account = GetArkAccount();
            var res = _arkNetApi.AccountService.GetBalance(account.Address);

            account.Balance = res.Balance;
            account.UnconfirmedBalance = res.UnconfirmedBalance;

            return res.Success;
        }

        /// <summary>
        /// Fetch the balance from the Network asynchroneously.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/> indicating if the balance has been successfully updated.
        /// </returns>
        public async Task<bool> UpdateBalanceAsync()
        {
            var account = await GetArkAccountAsync().ConfigureAwait(false);
            var res = await _arkNetApi.AccountService.GetBalanceAsync(account.Address).ConfigureAwait(false);

            account.Balance = res.Balance;
            account.UnconfirmedBalance = res.UnconfirmedBalance;

            return res.Success;
        }

        /// <summary>
        /// Get transactions from the network.
        /// </summary>
        /// <param name="offset">
        /// An unsigned integer that specified the number of records to skip.
        /// Default is "0"
        /// </param>
        /// <param name="limit">
        /// An unsigned integer that specifies the maximum number of records.
        /// Default is "50".
        /// </param>
        /// <returns>
        /// The <see cref="ArkTransactionList"/> returned by the network.
        /// </returns>
        public ArkTransactionList GetTransactions(int offset = 0, int limit = 50)
        {
            return _arkNetApi.TransactionService.GetTransactions(GetArkAccount().Address, offset, limit);
        }

        /// <summary>
        /// Get the transactions asynchroneously.
        /// </summary>
        /// <param name="offset">
        /// An unsigned integer that specified the number of records to skip.
        /// Default is "0".
        /// </param>
        /// <param name="limit">
        /// An unsigned integer that specifies the maximum number of records.
        /// Default is "50".
        /// </param>
        /// <returns>
        /// The <see cref="ArkTransactionList"/> of transactions within the given boundaries.
        /// </returns>
        public async Task<ArkTransactionList> GetTransactionsAsync(int offset = 0, int limit = 50)
        {
            return await _arkNetApi.TransactionService.GetTransactionsAsync(GetArkAccount().Address, offset, limit).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all unconfirmed transactions from the account.
        /// </summary>
        /// <returns>
        /// The <see cref="ArkTransactionList"/> of all unconfirmed transactions of the account.
        /// </returns>
        public ArkTransactionList GetUnconfirmedTransactions()
        {
            return _arkNetApi.TransactionService.GetUnconfirmedTransactions(GetArkAccount().Address);
        }

        /// <summary>
        /// Get all unconfirmed transactions from the account.
        /// </summary>
        /// <returns>
        /// The <see cref="ArkTransactionList"/> of all unconfirmed transactions of the account.
        /// </returns>
        public async Task<ArkTransactionList> GetUnconfirmedTransactionsAsync()
        {
            return await _arkNetApi.TransactionService.GetUnconfirmedTransactionsAsync(GetArkAccount().Address).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates transaction, doesnt post to chain.
        /// </summary>
        /// <param name="satoshiAmount">
        /// Amount in satoshi (1E-8 Ark)
        /// </param>
        /// <param name="recipientAddress">
        /// Address of transaction's recipient.
        /// </param>
        /// <param name="vendorField">
        /// => Also Referred as Smartbridge
        /// 64 chars <inheritdoc cref="string"/> with Ark V1, 
        /// will be increased to 256 in V2.
        /// </param>
        /// <returns>
        /// Transaction object.
        /// </returns>
        public TransactionApi CreateTransaction(long satoshiAmount, string recipientAddress,
           string vendorField)
        {
            return _arkNetApi.TransactionApi.CreateTransaction(recipientAddress,
                satoshiAmount,
                vendorField,
                _passPhrase,
                _secondPassPhrase);
        }

        /// <summary>
        /// Creates transaction in JSON format, doesnt post to chain.
        /// </summary>
        /// <param name="satoshiAmount">
        /// Amount in satoshi (1E-8 Ark)
        /// </param>
        /// <param name="recipientAddress">
        /// Address of transaction's recipient.
        /// </param>
        /// <param name="vendorField">
        /// => Also Referred as Smartbridge
        /// 64 chars <inheritdoc cref="string"/> with Ark V1, 
        /// will be increased to 256 in V2.
        /// </param>
        /// <returns>
        /// JSON string
        /// </returns>
        public string CreateTransactionJSON(long satoshiAmount, string recipientAddress, 
            string vendorField)
        {
            return CreateTransaction(satoshiAmount, recipientAddress, vendorField).ToJson();
        }

        /// <summary>
        /// Posts transaction to chain
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>
        /// The ArkTransactionPostResponse object
        /// </returns>
        public ArkTransactionPostResponse SendTransaction(TransactionApi transaction)
        {
            return _arkNetApi.TransactionService.PostTransaction(transaction);
        }

        /// <summary>
        /// Posts transaction to chain async
        /// </summary>
        /// <param name="transaction">Transaction to post to the chain</param>
        /// <returns>
        /// The <see cref="Task{ArkTransactionPostResponse}"/> of the transaction.
        /// </returns>
        public async Task<ArkTransactionPostResponse> SendTransactionAsync(TransactionApi transaction)
        {
            return await _arkNetApi.TransactionService.PostTransactionAsync(transaction).ConfigureAwait(false);
        }

        /// <summary>
        /// Post transaction to multiple peers at once
        /// </summary>
        /// <param name="transaction">Transaction to post to the chain</param>
        /// <returns>List of ArkTransactionPostResponse objects</returns>
        public List<ArkTransactionPostResponse> SendTransactionUsingMultiBroadCast(TransactionApi transaction)
        {
            return _arkNetApi.TransactionService.MultipleBroadCast(transaction);
        }

        /// <summary>
        /// Post transaction to multiple peers at once async
        /// </summary>
        /// <param name="transaction">Transaction to post to the chain</param>
        /// <returns>
        /// The <see cref="Task{ArkTransactionPostResponse}"/> of the transaction.
        /// </returns>
        public async Task<List<ArkTransactionPostResponse>> SendTransactionUsingMultiBroadCastAsync(TransactionApi transaction)
        {
            return await _arkNetApi.TransactionService.MultipleBroadCastAsync(transaction).ConfigureAwait(false);
        }

        /// <summary>
        /// Post JSON transaction to chain
        /// </summary>
        /// <param name="json">JSON in form of TransactionApi</param>
        /// <returns>ArkTransactionPostResponse object</returns>
        public ArkTransactionPostResponse SendTransaction(string json)
        {
            return _arkNetApi.TransactionService.PostTransaction(_arkNetApi.TransactionApi.FromJson(json));
        }

        /// <summary>
        /// Post JSON transaction to chain async
        /// </summary>
        /// <param name="json">JSON in form of TransactionApi</param>
        /// <returns>ArkTransactionPostResponse</returns>
        public async Task<ArkTransactionPostResponse> SendTransactionAsync(string json)
        {
            return await _arkNetApi.TransactionService.PostTransactionAsync(_arkNetApi.TransactionApi.FromJson(json)).ConfigureAwait(false);
        }

        /// <summary>
        /// Post JSON transaction to multiple peers
        /// </summary>
        /// <param name="json">JSON in form of TransactionApi</param>
        /// <returns>List of ArkTransactionPostResponse</returns>
        public List<ArkTransactionPostResponse> SendTransactionUsingMultiBroadCast(string json)
        {
            return _arkNetApi.TransactionService.MultipleBroadCast(_arkNetApi.TransactionApi.FromJson(json));
        }

        /// <summary>
        /// Post JSON transaction to multiple peers async
        /// </summary>
        /// <param name="json">JSON in form of TransactionApi</param>
        /// <returns>List of ArkTransactionPostResponse</returns>
        public async Task<List<ArkTransactionPostResponse>> SendTransactionUsingMultiBroadCastAsync(string json)
        {
            return await _arkNetApi.TransactionService.MultipleBroadCastAsync(_arkNetApi.TransactionApi.FromJson(json)).ConfigureAwait(false);
        }

        #region V2 preparation

        // public bool RemoteSign()
        // {
        // throw new NotImplementedException();
        // }

        // public bool RegisterSecondSignature()
        // {
        // throw new NotImplementedException();
        // }

        // public bool GetVoterContribution()
        // {
        // throw new NotImplementedException();
        // }
        #endregion
    }
}
