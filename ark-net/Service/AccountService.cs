using ArkNet.Core;
using ArkNet.Model;

namespace ArkNet.Service
{
    public class AccountService
    {
        public static ArkAccount GetByAddress(string address) => NetworkApi.Mainnet.ActivePeer.GetAccountbyAddress(address);
    }
}
