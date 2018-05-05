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
        public AccountController(string passphrase, string secondPassPhrase = null)
        {
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
                _account = AccountService.GetByAddress(Crypto.GetAddress(Crypto.GetKeys(_passPhrase), ArkNetApi.Instance.NetworkSettings.BytePrefix)).Account;

            //Account not on chain yet because it's a new account.
            if (_account == null)
            {
                _account = new ArkAccount()
                {
                    Address = Crypto.GetAddress(Crypto.GetKeys(_passPhrase), ArkNetApi.Instance.NetworkSettings.BytePrefix),
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
                var accountResponse = await AccountService.GetByAddressAsync(Crypto.GetAddress(Crypto.GetKeys(_passPhrase), ArkNetApi.Instance.NetworkSettings.BytePrefix));
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
            var tx = TransactionApi.CreateTransaction(recipientAddress,
                satoshiAmount,
                vendorField,
                _passPhrase,
                _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
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
            var tx = TransactionApi.CreateTransaction(recipientAddress,
                satoshiAmount,
                vendorField,
                _passPhrase,
                _secondPassPhrase);

            return await TransactionService.PostTransactionAsync(tx);
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
            var tx = TransactionApi.CreateTransaction(recipientAddress,
                satoshiAmount,
                vendorField,
                _passPhrase,
                _secondPassPhrase);

            return TransactionService.MultipleBroadCast(tx);
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
            var tx = TransactionApi.CreateTransaction(recipientAddress,
                satoshiAmount,
                vendorField,
                _passPhrase,
                _secondPassPhrase);

            return await TransactionService.MultipleBroadCastAsync(tx);
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
            var tx = TransactionApi.CreateVote(votes, _passPhrase, _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
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
            var tx = TransactionApi.CreateVote(votes, _passPhrase, _secondPassPhrase);

            return await TransactionService.PostTransactionAsync(tx);
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
            var tx = TransactionApi.CreateDelegate(username, _passPhrase, _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
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
            var tx = TransactionApi.CreateDelegate(username, _passPhrase, _secondPassPhrase);

            return await TransactionService.PostTransactionAsync(tx);
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
            var res = AccountService.GetBalance(account.Address);

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
            var account = await GetArkAccountAsync();
            var res = await AccountService.GetBalanceAsync(account.Address);

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
            return TransactionService.GetTransactions(GetArkAccount().Address, offset, limit);
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
            return await TransactionService.GetTransactionsAsync(GetArkAccount().Address, offset, limit);
        }

        /// <summary>
        /// Get all unconfirmed transactions from the account.
        /// </summary>
        /// <returns>
        /// The <see cref="ArkTransactionList"/> of all unconfirmed transactions of the account.
        /// </returns>
        public ArkTransactionList GetUnconfirmedTransactions()
        {
            return TransactionService.GetUnconfirmedTransactions(GetArkAccount().Address);
        }

        /// <summary>
        /// Get all unconfirmed transactions from the account.
        /// </summary>
        /// <returns>
        /// The <see cref="ArkTransactionList"/> of all unconfirmed transactions of the account.
        /// </returns>
        public async Task<ArkTransactionList> GetUnconfirmedTransactionsAsync()
        {
            return await TransactionService.GetUnconfirmedTransactionsAsync(GetArkAccount().Address);
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
