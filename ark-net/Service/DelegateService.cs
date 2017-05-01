using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Delegate = ArkNet.Model.Delegate;

namespace ArkNet.Service
{
    public static class DelegateService
    {
        public static IEnumerable<Delegate> GetAll() => NetworkApi.Mainnet.ActivePeer.GetDelegates();
        public static Delegate GetByUsername(string username) => NetworkApi.Mainnet.ActivePeer.GetDelegatebyUsername(username);
        public static Delegate GetByPubKey(string pubkey) => NetworkApi.Mainnet.ActivePeer.GetDelegatebyPubKey(pubkey);
        public static Delegate GetByAddress(string address) => NetworkApi.Mainnet.ActivePeer.GetDelegatebyAddress(address);
        public static IEnumerable<DelegateVoters> GetVoters(string username) => NetworkApi.Mainnet.ActivePeer.GetDelegateVoters(username);
    }
}
