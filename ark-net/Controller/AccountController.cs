using System;
using ArkNet.Core;
using ArkNet.Model;
using ArkNet.Service;

namespace ArkNet.Controller
{
    public class AccountController
    {
        private ArkAccount _account;

        public AccountController()
        {
            _account = new ArkAccount();
        }

        public bool AskRemoteSignature()
        {
            throw new NotImplementedException();
        }

        public void SendMultisignArk()
        {
            throw new NotImplementedException();
        }

        public ArkAccount OpenAccount(string passphrase)
        {
            if (_account?.Address != null) return _account;

            _account = AccountService.GetByAddress(Crypto.GetAddress(Crypto.GetKeys(passphrase)));
            _account.PublicKey = Crypto.GetKeys(passphrase).PubKey.ToString();

            return _account;
        }

        public (bool status, string data, string error) SendArk(long satosshiAmount, string recepientAddres, string vendorFiend, string passPhrase, string secondPassPhrase=null)
        {
            var tx = TransactionApi.CreateTransaction(recepientAddres,
                satosshiAmount,
                vendorFiend,
                passPhrase,
                secondPassPhrase);
          
            return NetworkApi.Mainnet.ActivePeer.PostTransaction(tx);           
        }

        public bool VoteForDelegate()
        {
            throw new NotImplementedException();
        }

        public (bool status, string data, string error)  RegisterAsDelegate(string username, string passPhrase, string secondPassPhrase = null)
        {
            var accCtnrl = new AccountController();
            var tx = TransactionApi.CreateDelegate(username, passPhrase, secondPassPhrase);

            return NetworkApi.Mainnet.ActivePeer.PostTransaction(tx);
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