using System;
using ArkNet.Model;

namespace ArkNet.Controller
{
    public class AccountController
    {
        private Account _account;

        public AccountController(Account arkaccount)
        {
            this._account = arkaccount;
        }

        public bool AskRemoteSignature()
        {
            throw new NotImplementedException();
        }

        public void SendMultisignArk()
        {
            throw new NotImplementedException();
        }

        public bool OpenAccount(string passphrase)
        {


            throw new NotImplementedException();
        }

        public bool SendArk()
        {
            throw new NotImplementedException();
        }

        public bool VoteForDelegate()
        {
            throw new NotImplementedException();
        }

        public bool RegisterAsDelegate()
        {
            throw new NotImplementedException();
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