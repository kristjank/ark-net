using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using ArkDelegate = ArkNet.Model.ArkDelegate;

namespace ArkNet.Service
{
    public static class DelegateService
    {
        public static IEnumerable<ArkDelegate> GetAll() => NetworkApi.Mainnet.ActivePeer.GetDelegates();
        public static ArkDelegate GetByUsername(string username) => NetworkApi.Mainnet.ActivePeer.GetDelegatebyUsername(username);
        public static ArkDelegate GetByPubKey(string pubkey) => NetworkApi.Mainnet.ActivePeer.GetDelegatebyPubKey(pubkey);
        public static ArkDelegate GetByAddress(string address) => NetworkApi.Mainnet.ActivePeer.GetDelegatebyAddress(address);
        public static IEnumerable<DelegateVoters> GetVoters(string username) => NetworkApi.Mainnet.ActivePeer.GetDelegateVoters(username);
    }
}
