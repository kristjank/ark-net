using ArkNet.Model.Peer;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArkNet.Tests
{
    [TestClass()]
    public class ArkNetApiTests
    {
        private ArkNetApi _arkNetApi;
        public ArkNetApi ArkNetApi
        {
            get { return _arkNetApi ?? (_arkNetApi = new ArkNetApi()); }
        }

        [TestMethod()]
        public void StartTest()
        {
            ArkNetApi.Start(NetworkType.MainNet).Wait();

            Assert.IsNotNull(ArkNetApi.NetworkApi.NetworkSettings);
        }

        [TestMethod()]
        public async Task SwitchNetwork()
        {
            await ArkNetApi.Start(NetworkType.DevNet);
            await ArkNetApi.SwitchNetwork(NetworkType.MainNet);
            await ArkNetApi.SwitchNetwork(NetworkType.DevNet);
        }

        [TestMethod()]
        public async Task StartWithPeerListTest()
        {
            var peerList = new List<ArkPeerAddress>();
            //Good peer
            peerList.Add(new ArkPeerAddress { Ip = "5.39.9.240", Port = 4001 });
            //Bad peers
            peerList.Add(new ArkPeerAddress { Ip = "5.40.9.240", Port = 4001 });
            peerList.Add(new ArkPeerAddress { Ip = "5.40.9.241", Port = 4001 });
            peerList.Add(new ArkPeerAddress { Ip = "5.40.9.242", Port = 4001 });

            await ArkNetApi.Start(peerList);
        }
    }
}