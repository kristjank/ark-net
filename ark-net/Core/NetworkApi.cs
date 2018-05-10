// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkApi.cs" company="Ark">
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
using ArkNet.Logging;
using ArkNet.Service;
using ArkNet.Utils;

namespace ArkNet.Core
{
    /// <summary>
    /// The network api for managing peers.
    /// </summary>
    public class NetworkApi
    {
        private ArkNetApi _arkNetApi;
        public NetworkApi(ArkNetApi arkNetApi)
        {
            _arkNetApi = arkNetApi;
        }

        #region Fields

        /// <summary>
        /// Store the network settings.
        /// </summary>
        public ArkNetworkSettings NetworkSettings;

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
        /// Initialize the <see cref="NetworkApi"/> and fill the <see cref="List{PeerApi}"/> with peers.
        /// </summary>
        private NetworkApi()
        {
            _peers = new List<PeerApi>();
        }

        public PeerApi ActivePeer { get; set; }

        #endregion

        /// <summary>
        /// Initializes the class.
        /// </summary>
        /// 
        /// <param name="initialPeer">The intial peer.</param>
        /// 
        /// <returns>Retuns a <see cref="Task"/> type.</returns>
        /// 
        public async Task WarmUp(PeerApi initialPeer)
        {
            ActivePeer = initialPeer;
            await SetPeerList().ConfigureAwait(false);
            ActivePeer = GetRandomPeer();
            StartPeerCleaningTask();
        }

        /// <summary>
        /// Gets a random peer.
        /// </summary>
        /// 
        /// <returns>Returns the next peer randomly picked as a <see cref="PeerApi"/></returns>
        /// 
        public PeerApi GetRandomPeer()
        {
            return _peers[_random.Next(_peers.Count())];
        }

        /// <summary>
        /// Switches to a random peer.
        /// </summary>
        /// 
        public void SwitchPeer()
        {
            ActivePeer = GetRandomPeer();
        }

        /// <summary>
        /// Gets all peers.
        /// </summary>
        /// 
        /// <returns>Set the peer list</returns>
        /// 
        private async Task SetPeerList()
        {
            var peers = await _arkNetApi.PeerService.GetAllAsync().ConfigureAwait(false);
            var peersOrderByHeight = peers.Peers
                .Where(x => x.Status.Equals("OK") && x.Version == NetworkSettings.Version)
                .OrderByDescending(x => x.Height)
                .ToList();

            var heightToCompare = peersOrderByHeight.FirstOrDefault().Height - NetworkSettings.PeerCleaningHeightThreshold;

            var peerURLs = peersOrderByHeight.Where(x => x.Height >= heightToCompare)
                .Select(x => new { Ip = x.Ip, Port = x.Port })
                .ToList();

            var tmpPeerList = new List<PeerApi>();
            foreach (var peerURL in peerURLs)
            {
                tmpPeerList.Add(new PeerApi(this, peerURL.Ip, peerURL.Port));
            }

            if (!tmpPeerList.Any(x => x.Ip == ActivePeer.Ip))
                tmpPeerList.Add(ActivePeer);

            _peers = tmpPeerList;
        }

        /// <summary>
        /// Searches for and drops disconnected peers.
        /// </summary>
        private void StartPeerCleaningTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(NetworkSettings.PeerCleaningIntervalInMinutes));
                    try
                    {
                        await SetPeerList();
                    }
                    catch (Exception ex)
                    {
                        _arkNetApi.LoggingApi.Error("Error setting peer seed list.", ex);
                    }
                }
            });
        }
    }
}
