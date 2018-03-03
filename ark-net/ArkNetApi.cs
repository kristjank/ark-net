// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkNetApi.cs" company="Ark Labs">
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
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Model.Loader;
using ArkNet.Model.Peer;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// ARK API.
    /// </summary>
    public sealed class ArkNetApi
    {
        /// <summary>
        /// Initial Seeds for the MainNet
        /// </summary>
        private List<Tuple<string, int>> _peerSeedListMainNet = 
            new List<Tuple<string, int>> {
            Tuple.Create("5.39.9.240", 4001),
            Tuple.Create("5.39.9.241", 4001),
            Tuple.Create("5.39.9.242", 4001),
            Tuple.Create("5.39.9.243", 4001),
            Tuple.Create("5.39.9.244", 4001),
            Tuple.Create("5.39.9.250", 4001),
            Tuple.Create("5.39.9.251", 4001),
            Tuple.Create("5.39.9.252", 4001),
            Tuple.Create("5.39.9.253", 4001),
            Tuple.Create("5.39.9.254", 4001),
            Tuple.Create("5.39.9.255", 4001)
            };

        /// <summary>
        /// Initial Seeds for the DevNet
        /// </summary>
        private List<Tuple<string, int>> _peerSeedListDevNet =
            new List<Tuple<string, int>> {
            Tuple.Create("167.114.43.48", 4002),
            Tuple.Create("167.114.29.49", 4002),
            Tuple.Create("167.114.43.43", 4002),
            Tuple.Create("167.114.29.54", 4002),
            Tuple.Create("167.114.29.45", 4002),
            Tuple.Create("167.114.29.40", 4002),
            Tuple.Create("167.114.29.56", 4002),
            Tuple.Create("167.114.43.35", 4002),
            Tuple.Create("167.114.29.51", 4002),
            Tuple.Create("167.114.29.59", 4002),
            Tuple.Create("167.114.43.42", 4002),
            Tuple.Create("167.114.29.34", 4002),
            Tuple.Create("167.114.29.62", 4002),
            Tuple.Create("167.114.43.49", 4002),
            Tuple.Create("167.114.29.44", 4002)
            };

        /// <summary>
        /// Lazy Load of the API, calls to <inheritdoc cref="Instance"/>
        /// </summary>
        private static readonly Lazy<ArkNetApi> _lazy = new Lazy<ArkNetApi>(() => new ArkNetApi());

        /// <summary>
        /// Instanciate the API
        /// Called through <inheritdoc cref="_lazy"/> to allow lazy load of the API.
        /// </summary>
        public static ArkNetApi Instance => _lazy.Value;

        // TODO: Fields should be declared as private, and exposed through properties (Verify Network Settings)
        // TODO: All private fields must be placed before all Public Fields

        /// <summary>
        /// Store the network settings.
        /// </summary>
        public ArkNetworkSettings NetworkSettings;

        // TODO : All properties must be placed after all constructors.

        /// <summary>
        /// Prevents a default instance of the <see cref="ArkNetApi"/> class from being created.
        /// </summary>
        private ArkNetApi()
        {
            
        }

        /// <summary>
        /// Start the Network
        /// </summary>
        /// <param name="type">
        /// <inheritdoc cref="NetworkType"/>Can be :
        /// -- DevNet (test), ask Dark (testnet coins) on the slack.
        /// -- MainNet (live, beware real money, financial loss possible there).
        /// </param>
        /// <returns> The <inheritdoc cref="Task"/> starts the node.</returns>
        public async Task Start(NetworkType type)
        {
            await SetNetworkSettings(await GetInitialPeer(type));
        }

        /// <summary>
        /// Start the Network.
        /// </summary>
        /// <param name="initialPeerIp"> The Initial peer's IP</param>
        /// <param name="initialPeerPort"> The Initial Peer's Port</param>
        /// <returns> The <inheritdoc cref="Task"/> starts the node.</returns>
        public async Task Start(string initialPeerIp, int initialPeerPort)
        {
            await SetNetworkSettings(GetInitialPeer(initialPeerIp, initialPeerPort));
        }

        /// <summary>
        /// Fetch the the NetworkSettings, and set the NetworkSettings variable,
        /// which is used for every subsequent request after the initial ones.
        /// </summary>
        /// <param name="initialPeer">Initial Peer <inheritdoc cref="PeerApi"/> from which the settings are fetched.</param>
        /// <returns>Instiate a <inheritdoc cref="PeerApi"/> based on the initial peer provided.</returns>
        private async Task SetNetworkSettings(PeerApi initialPeer)
        {
            // Request the NetworkSettings, Fees, and more peer address from the peers it connects to. 
            var responseAutoConfigure = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_AUTO_CONFIGURE);
            var responseFees = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_FEES);
            var responsePeer = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Peer.GET, initialPeer.Ip, initialPeer.Port));

            // Auto-configures what has been fetched previously
            var autoConfig = JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(responseAutoConfigure);
            var fees = JsonConvert.DeserializeObject<Fees>(JObject.Parse(responseFees)["fees"].ToString());
            var peer = JsonConvert.DeserializeObject<ArkPeerResponse>(responsePeer);

            // Fill the NetworkSettings with what has been fetched / auto-configured previously.
            NetworkSettings = new ArkNetworkSettings()
            {
                Port = initialPeer.Port,
                BytePrefix = (byte)autoConfig.Network.Version,
                Version = peer.Peer.Version,
                NetHash = autoConfig.Network.NetHash,
                Fee = fees
            };

            await NetworkApi.Instance.WarmUp(new PeerApi(initialPeer.Ip, initialPeer.Port));
        }

        /// <summary>
        /// Get the initial peers to connect to the blockchain.
        /// </summary>
        /// <param name="initialPeerIp">Ip of the first peer.</param>
        /// <param name="initialPeerPort">Port that the first peer listen on.</param>
        /// <returns>Return the first peer found at the IP/Port given.</returns>
        private PeerApi GetInitialPeer(string initialPeerIp, int initialPeerPort)
        {
            return new PeerApi(initialPeerIp, initialPeerPort);
        }

        /// <summary>
        /// Is needed to fetch the peer.
        /// </summary>
        /// <param name="type">
        /// <inheritdoc cref="NetworkType"/>Can be :
        /// -- DevNet (test), ask Dark (testnet coins) on the slack.
        /// -- MainNet (live, beware real money, financial loss possible there).
        /// </param>
        /// <param name="retryCount">Number of retry before a timeout</param>
        /// <returns>Returns the first <inheritdoc cref="PeerApi"/> that is online</returns>
        private async Task<PeerApi> GetInitialPeer(NetworkType type, int retryCount = 0)
        {
            // Pick a peer randomly in _peerSeedListMainNet //
            var peerUrl = _peerSeedListMainNet[new Random().Next(_peerSeedListMainNet.Count)];

            // If the Network is set to DevNet, change the peer picked above by a peer from _peerSeedListDevNet //
            if (type == NetworkType.DevNet)
                peerUrl = _peerSeedListDevNet[new Random().Next(_peerSeedListDevNet.Count)];

            // create a peer out of peerurl, and returns if the peer is online. //
            var peer = new PeerApi(peerUrl.Item1, peerUrl.Item2);
            if (await peer.IsOnline())
            {
                return peer;
            }

            // Throw an exception if all of the initial peers have been tried. //
            if ((type == NetworkType.DevNet && retryCount == _peerSeedListDevNet.Count) 
             || (type == NetworkType.MainNet && retryCount == _peerSeedListMainNet.Count))
                throw new Exception("Unable to connect to a seed peer");

            // redo the check and increment the retry count //
            return await GetInitialPeer(type, retryCount + 1);
        }
    }
}
