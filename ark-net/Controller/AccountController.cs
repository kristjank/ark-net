using System;
using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using ArkNet.Service;

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
            _account = AccountService.GetByAddress(Crypto.GetAddress(Crypto.GetKeys(passphrase), ArkNetApi.Instance.NetworkSettings.BytePrefix));
        }

        public ArkAccount GetArkAccount()
        {
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

        public ArkTransactionResponse SendArk(long satosshiAmount, string recepientAddres,
            string vendorField)
        {
            var tx = TransactionApi.CreateTransaction(recipientAddress,
                satoshiAmount,
                vendorField,
                _passPhrase,
                _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public ArkTransactionResponse VoteForDelegate(List<string> votes)
        {
            var tx = TransactionApi.CreateVote(votes, _passPhrase, _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public ArkTransactionResponse RegisterAsDelegate(string username)
        {
            var tx = TransactionApi.CreateDelegate(username, _passPhrase, _secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public bool UpdateBalance()
        {
            var res = AccountService.GetBalance(_account.Address);

            _account.Balance = res.Balance;
            _account.UnconfirmedBalance = res.UnconfirmedBalance;

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