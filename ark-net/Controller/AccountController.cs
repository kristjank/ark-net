using System;
using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Service;
using ArkNet.Model.Transactions;
using ArkNet.Model.Account;
using System.Threading.Tasks;

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

        public bool AskRemoteSignature()
        {
            throw new NotImplementedException();
        }

        public void SendMultisignArk()
        {
            throw new NotImplementedException();
        }

        public ArkTransactionResponse SendArk(long satoshiAmount, string recipientAddress,
            string vendorField)
        {
            var tx = TransactionApi.CreateTransaction(recipientAddress,
                satoshiAmount,
                vendorField,
                _passPhrase,
                _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public async Task<ArkTransactionResponse> SendArkAsync(long satoshiAmount, string recipientAddress,
            string vendorField)
        {
            var tx = TransactionApi.CreateTransaction(recipientAddress,
                satoshiAmount,
                vendorField,
                _passPhrase,
                _secondPassPhrase);

            return await TransactionService.PostTransactionAsync(tx);
        }

        public ArkTransactionResponse VoteForDelegate(List<string> votes)
        {
            var tx = TransactionApi.CreateVote(votes, _passPhrase, _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public async Task<ArkTransactionResponse> VoteForDelegateAsync(List<string> votes)
        {
            var tx = TransactionApi.CreateVote(votes, _passPhrase, _secondPassPhrase);

            return await TransactionService.PostTransactionAsync(tx);
        }

        public ArkTransactionResponse RegisterAsDelegate(string username)
        {
            var tx = TransactionApi.CreateDelegate(username, _passPhrase, _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public async Task<ArkTransactionResponse> RegisterAsDelegateAsync(string username)
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

        public bool RemoteSign()
        {
            throw new NotImplementedException();
        }

        public bool RegisterSecondSignature()
        {
            throw new NotImplementedException();
        }

        public bool GetVoterContribution()
        {
            throw new NotImplementedException();
        }
    }

    /*public static bool ApplyTransaction(Account account, long amount)
    {


        /*Balance -= transaction.Amount + transaction.Fee;
        return Balance > -1;
    }

    public static bool UndoTransaction(TransactionApi transaction)
    {
        /*Balance += transaction.Amount + transaction.Fee;
        return Balance > -1;
    }

    public static Verification VerifyTransaction(TransactionApi transaction)
    {
        var v = new Verification();
        if (Balance >= transaction.Amount + transaction.Fee)
            v.AddError(string.Format("Account %1 does not have enough balance: %2", Address, Balance));
        // TODO: many things

        return v;
    }*/
}