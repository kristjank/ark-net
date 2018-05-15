// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkSettings.cs" company="Ark">
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

namespace ArkNet.Utils
{
    /// <summary>
    /// Settings for interacting with the Ark network.
    /// </summary>
    public class ArkNetworkSettings
    {
        #region Fields

        /// <summary>
        /// The network port.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public int Port { get; set; }

        /// <summary>
        /// Same as the network version.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public byte BytePrefix { get; set; }

        /// <summary>
        /// The network port.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public string Version { get; set; }

        /// <summary>
        /// The network net hash.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public string NetHash { get; set; }

        /// <summary>
        /// Maximum number of request braodcasts to peers.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public int MaxNumOfBroadcasts { get; set; }

        /// <summary>
        /// Interval at which bad peers are dropped.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public int PeerCleaningIntervalInMinutes { get; set; }

        /// <summary>
        /// The height threshold at which bad peers are dropped.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public int PeerCleaningHeightThreshold { get; set; }

        /// <summary>
        /// The retry count when a peer request fails.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public int MaxRequestRetryCount { get; set; }

        /// <summary>
        /// The network fee information.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="Fees"/>.</value>
        /// 
        public Fees Fee { get; set; }

        /// <summary>
        /// Token name (Dark or Ark)
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Network symbol (DA or A)
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Url of explorer
        /// </summary>
        public string Explorer { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="ArkNetworkSettings"/> type.
        /// </summary>
        /// 
        public ArkNetworkSettings()
        {
            Fee = new Fees();
            PeerCleaningIntervalInMinutes = 1;
            PeerCleaningHeightThreshold = 5;
            MaxRequestRetryCount = 3;
            MaxNumOfBroadcasts = 5;
        }

        #endregion
    }


    public class Fees
    {
        /// <summary>
        /// The transaction fee.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public int Send { get; set; }

        /// <summary>
        /// The vote fee.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public int Vote { get; set; }

        /// <summary>
        /// The delegate fee.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public long Delegate { get; set; }

        /// <summary>
        /// The second signature fee.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public int SecondSignature { get; set; }

        /// <summary>
        /// The multi signature fee.
        /// </summary>
        /// 
        /// <value>Gets/sets the value as an <see cref="int"/>.</value>
        /// 
        public int MultiSignature { get; set; }
    }
}