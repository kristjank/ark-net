using System;
using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Service;
using ArkNet.Model.Transactions;
using ArkNet.Model.Account;
using System.Threading.Tasks;
using ArkNet.Messages.Transaction;

namespace ArkNet.Controller
{
    public class AccountController
    {
        private ArkAccount _account;
        private string _passPhrase;
        private string _secondPassPhrase;

        public AccountController(string passphrase, string secondPassPhrase = null)
        {
            _passPhrase = passphrase;
            _secondPassPhrase = secondPassPhrase;
        }

        public ArkAccount GetArkAccount()
        {
            if (_account == null)
                _account = AccountService.GetByAddress(Crypto.GetAddress(Crypto.GetKeys(_passPhrase), ArkNetApi.Instance.NetworkSettings.BytePrefix)).Account;
            return _account;
        }

        public async Task<ArkAccount> GetArkAccountAsync()
        {
            if (_account == null)
            {
                var accountResponse = await AccountService.GetByAddressAsync(Crypto.GetAddress(Crypto.GetKeys(_passPhrase), ArkNetApi.Instance.NetworkSettings.BytePrefix));
                _account = accountResponse.Account;
            }
            return _account;
        }

        //public bool AskRemoteSignature()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SendMultisignArk()
        //{
        //    throw new NotImplementedException();
        //}

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

        public ArkTransactionPostResponse VoteForDelegate(List<string> votes)
        {
            var tx = TransactionApi.CreateVote(votes, _passPhrase, _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public async Task<ArkTransactionPostResponse> VoteForDelegateAsync(List<string> votes)
        {
            var tx = TransactionApi.CreateVote(votes, _passPhrase, _secondPassPhrase);

            return await TransactionService.PostTransactionAsync(tx);
        }

        public ArkTransactionPostResponse RegisterAsDelegate(string username)
        {
            var tx = TransactionApi.CreateDelegate(username, _passPhrase, _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public async Task<ArkTransactionPostResponse> RegisterAsDelegateAsync(string username)
        {
            var tx = TransactionApi.CreateDelegate(username, _passPhrase, _secondPassPhrase);

            return await TransactionService.PostTransactionAsync(tx);
        }

        public bool UpdateBalance()
        {
            var account = GetArkAccount();
            var res = AccountService.GetBalance(account.Address);

            account.Balance = res.Balance;
            account.UnconfirmedBalance = res.UnconfirmedBalance;

            return res.Success;
        }

        public async Task<bool> UpdateBalanceAsync()
        {
            var account = await GetArkAccountAsync();
            var res = await AccountService.GetBalanceAsync(account.Address);

            account.Balance = res.Balance;
            account.UnconfirmedBalance = res.UnconfirmedBalance;

            return res.Success;
        }

        public ArkTransactionList GetTransactions(int offset = 0, int limit = 50)
        {
            return TransactionService.GetTransactions(GetArkAccount().Address, offset, limit);
        }

        public async Task<ArkTransactionList> GetTransactionsAsync(int offset = 0, int limit = 50)
        {
            return await TransactionService.GetTransactionsAsync(GetArkAccount().Address, offset, limit);
        }

        public ArkTransactionList GetUnconfirmedTransactions()
        {
            return TransactionService.GetUnconfirmedTransactions(GetArkAccount().Address);
        }

        public async Task<ArkTransactionList> GetUnconfirmedTransactionsAsync()
        {
            return await TransactionService.GetUnconfirmedTransactionsAsync(GetArkAccount().Address);
        }

        //public bool RemoteSign()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool RegisterSecondSignature()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool GetVoterContribution()
        //{
        //    throw new NotImplementedException();
        //}
    }
}