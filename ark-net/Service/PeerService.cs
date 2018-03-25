// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PeerService.cs" company="Ark Labs">
//   MIT License
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
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Model.Peer;
using ArkNet.Utils;
using Newtonsoft.Json;

namespace ArkNet.Service
{
    /// <summary>
    /// Provides functionality for requesting peer information.
    /// </summary>
    /// 
    public static class PeerService
    {
        #region Methods

        /// <summary>
        /// Gets a list of all peers.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkPeerList"/> type.</returns>
        /// 
        public static ArkPeerList GetAll()
        {
            return GetAllAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets a list of all peers.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkPeerList}"/> type.</returns>
        /// 
        public async static Task<ArkPeerList> GetAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Peer.GET_ALL);

            return JsonConvert.DeserializeObject<ArkPeerList>(response);
        }

        /// <summary>
        /// Gets a by by ip address and port.
        /// </summary>
        /// 
        /// <param name="ip">The peer ip address.</param>
        /// 
        /// <param name="port">The peer port.</param>
        /// 
        /// <returns>Returns an <see cref="ArkPeerResponse"/> type.</returns>
        /// 
        public static ArkPeerResponse GetPeer(string ip, int port)
        {
            return GetPeerAsync(ip, port).Result;
        }

        /// <summary>
        /// Asynchronously gets a by by ip address and port.
        /// </summary>
        /// 
        /// <param name="ip">The peer ip address.</param>
        /// 
        /// <param name="port">The peer port.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkPeerResponse}"/> type.</returns>
        /// 
        public async static Task<ArkPeerResponse> GetPeerAsync(string ip, int port)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Peer.GET, ip, port));

            return JsonConvert.DeserializeObject<ArkPeerResponse>(response);
        }

        /// <summary>
        /// Gets the current peer status.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkPeerStatus"/> type.</returns>
        /// 
        public static ArkPeerStatus GetPeerStatus()
        {
            return GetPeerStatusAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the current peer status.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkPeerStatus}"/> type.</returns>
        /// 
        public async static Task<ArkPeerStatus> GetPeerStatusAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Peer.GET_STATUS);

            return JsonConvert.DeserializeObject<ArkPeerStatus>(response);
        }

        #endregion
    }
}