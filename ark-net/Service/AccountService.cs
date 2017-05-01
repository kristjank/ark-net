using ArkNet.Core;
using ArkNet.Model;

namespace ArkNet.Service
{
    public class AccountService
    {
        public static Account GetByAddress(string address) => NetworkApi.Mainnet.ActivePeer.GetAccountbyAddress(address);
    }
}
