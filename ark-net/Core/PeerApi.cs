// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PeerApi.cs" company="Ark">
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

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils;
using Newtonsoft.Json.Linq;

namespace ArkNet.Core
{
    // References:
    // - TransactionService.cs
    // - ArkNetApi.cs
    // - NetworkApi.cs

    /// <summary>
    /// Provides functionality for interacting with an Ark peer.
    /// </summary>
    public class PeerApi
    {
        #region Fields

        /// <summary>
        /// A reference to an instance of the <see cref="HttpClient"/>.
        /// </summary>
        // note: possibly not initialized, may cause errors.
        private HttpClient _httpClient;

        /// <summary>
        /// The peer ip address.
        /// </summary>
        private string _ip;

        /// <summary>
        /// The peer port.
        /// </summary>
        private int _port;

        /// <summary>
        /// A reference to an instance of the <see cref="HttpClient"/>.
        /// </summary>
        /// <value>Gets/sets the value as an <see cref="HttpClient"/>.</value>
        public HttpClient HttpClient { get { return _httpClient; } }

        /// <summary>
        /// The peer ip address.
        /// </summary>
        /// <value>Gets/sets the value as an <see cref="String"/>.</value>
        public string Ip { get { return _ip; } }

        /// <summary>
        /// The peer port.
        /// </summary>
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        public int Port { get { return _port; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the <see cref="PeerApi"/> class.
        /// </summary>
        /// <param name="ip">The peer ip address.</param>
        /// <param name="port">The peer port.</param>
        public PeerApi(string ip, int port)
        {
            var protocol = "http://";
            if (port % 1000 == 443) protocol = "https://";

            Init(ip, port);

            _httpClient = new HttpClient()
            {
                BaseAddress = new UriBuilder(protocol, this._ip, this._port).Uri
            };

            if (ArkNetApi.Instance.NetworkSettings != null)
            {
                _httpClient.DefaultRequestHeaders.Add("nethash", ArkNetApi.Instance.NetworkSettings.NetHash);
                _httpClient.DefaultRequestHeaders.Add("version", ArkNetApi.Instance.NetworkSettings.Version);
                _httpClient.DefaultRequestHeaders.Add("port", ArkNetApi.Instance.NetworkSettings.Port.ToString());
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes this instance of the <see cref="PeerApi"/> class.
        /// </summary>
        /// <param name="ip">The peer ip address.</param>
        /// <param name="port">The port on which the Peer listens.</param>
        private void Init(string ip, int port)
        {
            this._ip = ip;
            this._port = port;
        }

        /// <summary>
        /// Creates an asynchronous HTTP web request to the peer to acquire a resource.
        /// </summary>
        /// <param name="method">The HTTP request method to use to contact the web resource.</param>
        /// <param name="path">The path of the requested web resource.</param>
        /// <param name="body">The resource body content when creating or updating a resource.</param>
        /// <returns>Returns an instance of the <see cref=Task{string}""/> type.</returns>
        public async Task<string> MakeRequest(string method, string path, string body = "")
        {
            return await MakeRequestInternal(method, path, body, 0);
        }

        /// <summary>
        /// Creates an asynchronous HTTP web request to the peer.
        /// </summary> 
        /// <param name="method">The HTTP request method to use to contact the web resource.</param>
        /// <param name="path">The path of the requested web resource.</param>
        /// <param name="body">The resource body content when creating or updating a resource.</param>
        /// <returns>Returns an instance of the <see cref=Task{string}""/> type.</returns>
        private async Task<string> MakeRequestInternal(string method, string path, string body = "", int retryCount = 0)
        {
            HttpResponseMessage response;
            var methodString = new HttpMethod(method).ToString().ToUpper();

            try
            {
                switch (methodString)
                {
                    case "GET":
                    case "HEAD":
                        response = await _httpClient.GetAsync(path);
                        break;
                    case "POST":
                        response = await _httpClient.PostAsync(path, new StringContent(JObject.Parse(body).ToString(), Encoding.UTF8, "application/json"));
                        break;
                    case "PUT":
                        response = await _httpClient.PutAsync(path, new StringContent(JObject.Parse(body).ToString(), Encoding.UTF8, "application/json"));
                        break;
                    default:
                        throw new NotImplementedException();
                }
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception)
            {
                if (ArkNetApi.Instance.NetworkSettings != null && NetworkApi.Instance.ActivePeer != null)
                {
                    if (retryCount < ArkNetApi.Instance.NetworkSettings.MaxRequestRetryCount)
                    {
                        NetworkApi.Instance.SwitchPeer();
                        _httpClient = NetworkApi.Instance.ActivePeer.HttpClient;
                        return await MakeRequestInternal(method, path, body, retryCount + 1);
                    }
                }
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets the online status of the peer.
        /// </summary>
        /// <returns>Returns an instance of the <see cref="Task{bool}"/> indicating if a peer is online.</returns>
        public async Task<bool> IsOnline()
        {
            try
            {
                await MakeRequest(ArkStaticStrings.ArkHttpMethods.HEAD, ArkStaticStrings.ArkApiPaths.Loader.GET_STATUS);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
