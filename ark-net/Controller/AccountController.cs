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

        public AccountController(string passphrase)
        {
            _account = new ArkAccount()
            {
                Address = Crypto.GetAddress(Crypto.GetKeys(passphrase), ArkNetApi.Instance.NetworkSettings.BytePrefix),
                PublicKey = Crypto.GetKeys(passphrase).PubKey.ToString()
            };
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

        public (bool status, string data, string error) SendArk(long satosshiAmount, string recepientAddres,
            string vendorFiend, string passPhrase, string secondPassPhrase = null)
        {
            var tx = TransactionApi.CreateTransaction(recepientAddres,
                satosshiAmount,
                vendorFiend,
                passPhrase,
                secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public (bool status, string data, string error) VoteForDelegate(List<string> votes, string passPhrase,
            string secondPassPhrase = null)
        {
            var tx = TransactionApi.CreateVote(votes, passPhrase, secondPassPhrase);
            return TransactionService.PostTransaction(tx);
        }

        public (bool status, string data, string error) RegisterAsDelegate(string username, string passPhrase,
            string secondPassPhrase = null)
        {
            var tx = TransactionApi.CreateDelegate(username, passPhrase, secondPassPhrase);

            return TransactionService.PostTransaction(tx);
        }

        public bool UpdateBalance()
        {
            var res = AccountService.GetBalance(_account.Address);

            _account.Balance = res.balance;
            _account.UnconfirmedBalance = res.unconfirmedBalance;

            return res.status;
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