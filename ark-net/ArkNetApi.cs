// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkNetApi.cs" company="Ark">
//   MIT License
// 
// Copyright (c) 2017 Kristjan Košič
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
// <summary>
//   Defines the ArkNetApi type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Logging;
using ArkNet.Model.Loader;
using ArkNet.Model.Peer;
using ArkNet.Service;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet
{
    /// <summary>
    /// ARK API's entry.
    /// </summary>
    public sealed class ArkNetApi
    {
        //Url to initial peers on MainNet
        public string PeerSeedListUrlMainNet = "https://raw.githubusercontent.com/ArkEcosystem/ARK-Peers/master/mainnet.json";

        //Url to initial peers on DevNet
        public string PeerSeedListUrlDevNet = "https://raw.githubusercontent.com/ArkEcosystem/ARK-Peers/master/devnet.json";

        /// <summary>
        /// Initial Seeds for the MainNet
        /// </summary>
        private List<ArkPeerAddress> _peerSeedListMainNet = 
            new List<ArkPeerAddress> {
                new ArkPeerAddress { Ip = "5.39.9.240", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.241", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.242", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.243", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.244", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.245", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.246", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.247", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.248", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.249", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.250", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.251", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.252", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.253", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.254", Port = 4001},
                new ArkPeerAddress { Ip = "5.39.9.255", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.160", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.161", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.162", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.163", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.164", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.165", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.166", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.167", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.168", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.169", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.170", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.171", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.172", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.173", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.174", Port = 4001},
                new ArkPeerAddress { Ip = "54.38.48.175", Port = 4001}
            };

        /// <summary>
        /// Initial Seeds for the DevNet
        /// </summary>
        private List<ArkPeerAddress> _peerSeedListDevNet =
            new List<ArkPeerAddress> {
                new ArkPeerAddress { Ip = "167.114.29.32", Port = 4002},
                new ArkPeerAddress { Ip = "167.114.29.33", Port = 4002},
                new ArkPeerAddress { Ip = "167.114.29.34", Port = 4002},
                new ArkPeerAddress { Ip = "167.114.29.35", Port = 4002},
                new ArkPeerAddress { Ip = "167.114.29.36", Port = 4002}
            };

        private IArkLogger _arkLogger { get; set; }

        private LoggingApi _loggingApi;
        public LoggingApi LoggingApi
        {
            get { return _loggingApi ?? (_loggingApi = new LoggingApi(_arkLogger)); }
        }

        private NetworkApi _networkApi;
        public NetworkApi NetworkApi
        {
            get { return _networkApi ?? (_networkApi = new NetworkApi(this)); }
        }

        private TransactionApi _transactionApi;
        public TransactionApi TransactionApi
        {
            get { return _transactionApi ?? (_transactionApi = new TransactionApi(NetworkApi, LoggingApi)); }
        }

        private AccountService _accountService;
        public AccountService AccountService
        {
            get
            {
                return _accountService ?? (_accountService = new AccountService(NetworkApi, LoggingApi));
            }
        }

        private BlockService _blockService;
        public BlockService BlockService
        {
            get
            {
                return _blockService ?? (_blockService = new BlockService(NetworkApi, LoggingApi));
            }
        }

        private DelegateService _delegateService;
        public DelegateService DelegateService
        {
            get
            {
                return _delegateService ?? (_delegateService = new DelegateService(NetworkApi, LoggingApi));
            }
        }

        private LoaderService _loaderService;
        public LoaderService LoaderService
        {
            get
            {
                return _loaderService ?? (_loaderService = new LoaderService(NetworkApi, LoggingApi));
            }
        }

        private PeerService _peerService;
        public PeerService PeerService
        {
            get
            {
                return _peerService ?? (_peerService = new PeerService(NetworkApi, LoggingApi));
            }
        }

        private TransactionService _transactionService;
        public TransactionService TransactionService
        {
            get
            {
                return _transactionService ?? (_transactionService = new TransactionService(NetworkApi, LoggingApi));
            }
        }

        /// <summary>
        /// Start the Network
        /// </summary>
        /// <param name="type">
        /// <inheritdoc cref="NetworkType"/> Can be :
        /// -- DevNet (test), ask Dark (testnet coins) on the slack.
        /// -- MainNet (live, beware real money, financial loss possible there).
        /// </param>
        /// <returns> The <inheritdoc cref="Task"/> starts the node.</returns>
        public async Task Start(NetworkType type, IArkLogger logger = null)
        {
            LoggingApi.Info(string.Format("Starting ArkNet with network <<{0}>>", type.ToString()));

            _arkLogger = logger;
            await SetNetworkSettings(await GetInitialPeer(type).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        /// Start the Network.
        /// </summary>
        /// <param name="initialPeerIp"> The Initial peer's IP</param>
        /// <param name="initialPeerPort"> The Initial Peer's Port</param>
        /// <returns> The <inheritdoc cref="Task"/> starts the node.</returns>
        public async Task Start(string initialPeerIp, int initialPeerPort, IArkLogger logger = null)
        {
            LoggingApi.Info(string.Format("Starting ArkNet. ip: <<{0}>>, Port: <<{1}>>", initialPeerIp, initialPeerPort));

            _arkLogger = logger;
            await SetNetworkSettings(GetInitialPeer(initialPeerIp, initialPeerPort)).ConfigureAwait(false);
        }

        /// <summary>
        /// Start the Network with custom list of peers.
        /// </summary>
        /// <param name="peers"> List of initial peers</param>
        /// <param name="initialPeerPort"> The Initial Peer's Port</param>
        /// <returns> The <inheritdoc cref="Task"/> starts the node.</returns>
        public async Task Start(List<ArkPeerAddress> peers, IArkLogger logger = null)
        {
            LoggingApi.Info(string.Format("Starting ArkNet with <<{0}>> peers", peers.Count));

            _arkLogger = logger;
            await SetNetworkSettings(await GetInitialPeer(peers).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        /// Switches the Network
        /// </summary>
        /// <param name="type">
        /// <inheritdoc cref="NetworkType"/> Can be :
        /// -- DevNet (test), ask Dark (testnet coins) on the slack.
        /// -- MainNet (live, beware real money, financial loss possible there).
        /// </param>
        /// <returns> The <inheritdoc cref="Task"/> switches the network.</returns>
        public async Task SwitchNetwork(NetworkType type)
        {
            LoggingApi.Info(string.Format("Switching network to <<{0}>>", type.ToString()));

            NetworkApi.NetworkSettings = null;
            await SetNetworkSettings(await GetInitialPeer(type).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        /// Switches the Network
        /// </summary>
        /// <param name="peerId"> The Initial peer's IP</param>
        /// <param name="peerPort"> The Initial Peer's Port</param>
        /// <returns> The <inheritdoc cref="Task"/> switches the network.</returns>
        public async Task SwitchNetwork(string peerId, int peerPort)
        {
            LoggingApi.Info(string.Format("Switching network. ip: <<{0}>>, port: <<{1}>>", peerId, peerPort));

            NetworkApi.NetworkSettings = null;
            await SetNetworkSettings(GetInitialPeer(peerId, peerPort)).ConfigureAwait(false);
        }

        /// <summary>
        /// Fetch the the NetworkSettings, and set the NetworkSettings variable,
        /// which is used for every subsequent request after the initial ones.
        /// </summary>
        /// <param name="initialPeer">Initial Peer <inheritdoc cref="PeerApi"/> from which the settings are fetched.</param>
        /// <returns>Instiate a <inheritdoc cref="PeerApi"/> based on the initial peer provided.</returns>
        private async Task SetNetworkSettings(PeerApi initialPeer)
        {
            try
            {
                // Request the NetworkSettings, Fees, and more peer address from the peers it connects to. 
                var responseAutoConfigure = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_AUTO_CONFIGURE).ConfigureAwait(false);
                var responseFees = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_FEES).ConfigureAwait(false);
                var responsePeer = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Peer.GET, initialPeer.Ip, initialPeer.Port)).ConfigureAwait(false);

                // Auto-configures what has been fetched previously
                var autoConfig = JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(responseAutoConfigure);
                var fees = JsonConvert.DeserializeObject<Fees>(JObject.Parse(responseFees)["fees"].ToString());
                var peer = JsonConvert.DeserializeObject<ArkPeerResponse>(responsePeer);

            // Fill the NetworkSettings with what has been fetched / auto-configured previously.
            NetworkApi.NetworkSettings = new ArkNetworkSettings()
            {
                Port = initialPeer.Port,
                BytePrefix = (byte)autoConfig.Network.Version,
                Version = peer.Peer.Version,
                NetHash = autoConfig.Network.NetHash,
                Fee = fees,
                Token = autoConfig.Network.Token,
                Symbol = autoConfig.Network.Symbol,
                Explorer = autoConfig.Network.Explorer
            };

                LoggingApi.Info(string.Format("Set network settings to <<{0}>>", JsonConvert.SerializeObject(NetworkApi.NetworkSettings)));

                await NetworkApi.WarmUp(new PeerApi(this, initialPeer.Ip, initialPeer.Port)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                LoggingApi.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Get the initial peers to connect to the blockchain.
        /// </summary>
        /// <param name="initialPeerIp">Ip of the first peer.</param>
        /// <param name="initialPeerPort">Port that the first peer listen on.</param>
        /// <returns>Return the first peer found at the IP/Port given.</returns>
        private PeerApi GetInitialPeer(string initialPeerIp, int initialPeerPort)
        {
            LoggingApi.Info(string.Format("Getting initial peer. ip: <<{0}>>, port: <<{1}>>", initialPeerIp, initialPeerPort));

            return new PeerApi(this, initialPeerIp, initialPeerPort);
        }

        /// <summary>
        /// Is needed to fetch the peer.
        /// </summary>
        /// <param name="peers">List of peers to try</param>
        /// <param name="retryCount">Number of retry before a timeout</param>
        /// <returns></returns>
        private async Task<PeerApi> GetInitialPeer(List<ArkPeerAddress> peers, int retryCount = 0)
        {
            try
            {
                //Picks a peer randomly from the list
                var peerUrl = peers[new Random().Next(peers.Count)];

            LoggingApi.Info(string.Format("Getting initial peer. ip: <<{0}>>, port: <<{1}>>, retrycount: <<{2}>>", peerUrl.Ip, peerUrl.Port, retryCount));

            // create a peer out of peerurl, and returns if the peer is online. //
            var peer = new PeerApi(this, peerUrl.Ip, peerUrl.Port);
            if (await peer.IsOnline().ConfigureAwait(false))
            {
                return peer;
            }

            LoggingApi.Warn(string.Format("Peer is not online. ip: <<{0}>>, port: <<{1}>>", peerUrl.Ip, peerUrl.Port));

            // Throw an exception if all of the initial peers have been tried. //
            if (retryCount == peers.Count)
            {
                LoggingApi.Error(string.Format("Unable to connect to a seed peer.  Retried <<{0}>> times", retryCount));
                throw new Exception("Unable to connect to a seed peer");
            }

                // redo the check and increment the retry count //
                return await GetInitialPeer(peers, retryCount + 1).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                LoggingApi.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Is needed to fetch the peer.
        /// </summary>
        /// <param name="type">
        /// <inheritdoc cref="NetworkType"/>Can be :
        /// -- DevNet (test), ask Dark (testnet coins) on the slack.
        /// -- MainNet (live, beware real money, financial loss possible there).
        /// </param>
        /// <returns>Returns the first <inheritdoc cref="PeerApi"/> that is online</returns>
        private async Task<PeerApi> GetInitialPeer(NetworkType type)
        {
            var peers = await GetInitialPeerList(type).ConfigureAwait(false);
            return await GetInitialPeer(peers, 0).ConfigureAwait(false);
        }

        /// <summary>
        /// Get initial peer list from GitHub json file
        /// </summary>
        /// <param name="type">MainNet or DevNet</param>
        /// <returns>List of peers (Ip & Port)</returns>
        private async Task<List<ArkPeerAddress>> GetInitialPeerList(NetworkType type)
        {
            LoggingApi.Warn(string.Format("Get initial peer list for <<{0}>>", type.ToString()));
            string json = null;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    json = await wc.DownloadStringTaskAsync(GetPeerSeedListUrl(type)).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    LoggingApi.Error(string.Format("Error downloading initial peer list from <<{0}>>", GetPeerSeedListUrl(type)), ex);
                }
            }

            if (!string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<List<ArkPeerAddress>>(json);
            }

            //Fallback to hardcoded values
            if (type == NetworkType.MainNet)
                return _peerSeedListMainNet;
            return _peerSeedListDevNet;
        }

        /// <summary>
        /// Get url from GitHub based on MainNet or DevNet
        /// </summary>
        /// <param name="type">MainNet or DevNet</param>
        /// <returns>Url to file</returns>
        private string GetPeerSeedListUrl(NetworkType type)
        {
            if (type == NetworkType.MainNet)
            {
                LoggingApi.Info(string.Format("Getting peer seed list from <<{0}>>", PeerSeedListUrlMainNet));
                return PeerSeedListUrlMainNet;
            }
            LoggingApi.Info(string.Format("Getting peer seed list from <<{0}>>", PeerSeedListUrlDevNet));
            return PeerSeedListUrlDevNet;
        }
    }
}
