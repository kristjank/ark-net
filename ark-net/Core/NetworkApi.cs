// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkApi.cs" company="Ark Labs">
// MIT License
//   // 
//   // Copyright (c) 2017 Kristjan Košič
//   // 
//   // Permission is hereby granted, free of charge, to any person obtaining a copy
//   // of this software and associated documentation files (the "Software"), to deal
//   // in the Software without restriction, including without limitation the rights
//   // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   // copies of the Software, and to permit persons to whom the Software is
//   // furnished to do so, subject to the following conditions:
//   // 
//   // The above copyright notice and this permission notice shall be included in all
//   // copies or substantial portions of the Software.
//   // 
//   // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   // SOFTWARE.
// </copyright>
// <summary>
//   Defines a Block on the Blockchain
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArkNet.Service;

namespace ArkNet.Core
{
    /// <summary>
    /// The network api.
    /// </summary>
    public sealed class NetworkApi
    {
        /// <summary>
        /// Load the Network API on-need.
        /// </summary>
        private static readonly Lazy<NetworkApi> lazy = new Lazy<NetworkApi>(() => new NetworkApi());

        /// <summary>
        /// Random function initialization.
        /// </summary>
        private readonly Random _random = new Random();

        /// <summary>
        /// <see cref="List{PeerApi}"/> of peers.
        /// </summary>
        private List<PeerApi> _peers = new List<PeerApi>();

        /// <summary>
        /// Prevents a default instance of the <see cref="NetworkApi"/> class from being created. 
        /// Initialize the <see cref="NetworkApi"/> and fill its peer list.
        /// </summary>
        private NetworkApi()
        {
            _peers = new List<PeerApi>();
        }

        /// <summary>
        /// Initialize an instance of the <see cref="NetworkApi" />
        /// </summary>
        public static NetworkApi Instance => lazy.Value;
   
        public string Nethash { get; set; } = ArkNetApi.Instance.NetworkSettings.NetHash; 
        public int Port { get; set; } = ArkNetApi.Instance.NetworkSettings.Port;
        public byte Prefix { get; set; } = ArkNetApi.Instance.NetworkSettings.BytePrefix;
        public string Version { get; set; } = ArkNetApi.Instance.NetworkSettings.Version;
        public int BroadcastMax { get; set; } = ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts;
        public PeerApi ActivePeer { get; set; }

        public async Task WarmUp(PeerApi initialPeer)
        {
            ActivePeer = initialPeer;
            await SetPeerList();
            ActivePeer = GetRandomPeer();
            StartPeerCleaningTask();
        }

        public PeerApi GetRandomPeer()
        {
            return _peers[_random.Next(_peers.Count())];
        }

        public void SwitchPeer()
        {
            ActivePeer = GetRandomPeer();
        }

        private async Task SetPeerList()
        {
            var peers = await PeerService.GetAllAsync();
            var peersOrderByHeight = peers.Peers
                .Where(x => x.Status.Equals("OK") && x.Version == ArkNetApi.Instance.NetworkSettings.Version)
                .OrderByDescending(x => x.Height)
                .ToList();

            var heightToCompare = peersOrderByHeight.FirstOrDefault().Height - ArkNetApi.Instance.NetworkSettings.PeerCleaningHeightThreshold;

            var peerURLs = peersOrderByHeight.Where(x => x.Height >= heightToCompare)
                .Select(x => new { Ip = x.Ip, Port = x.Port })
                .ToList();

            var tmpPeerList = new List<PeerApi>();
            foreach (var peerURL in peerURLs)
            {
                tmpPeerList.Add(new PeerApi(peerURL.Ip, peerURL.Port));
            }

            if (!tmpPeerList.Any(x => x.Ip == NetworkApi.Instance.ActivePeer.Ip))
                tmpPeerList.Add(NetworkApi.Instance.ActivePeer);

            _peers = tmpPeerList;
        }

        private void StartPeerCleaningTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(ArkNetApi.Instance.NetworkSettings.PeerCleaningIntervalInMinutes));
                    try
                    {
                        await SetPeerList();
                    }
                    catch { }
                }
            });
        }
    }
}