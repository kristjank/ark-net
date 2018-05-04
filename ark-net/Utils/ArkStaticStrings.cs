// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Ark">
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
/// <summary>
/// Contains Ark API
/// </summary>

namespace ArkNet.Utils
{
    /// <summary>
    /// Contains the API paths for network queries.
    /// </summary>
    /// 
    public static class ArkStaticStrings
    {
        public static class ArkApiPaths
        {
            public static class Account
            {
                public static readonly string GET_ACCOUNT = "/api/accounts?address={0}";
                public static readonly string GET_BALANCE = "/api/accounts/getBalance?address={0}";
                public static readonly string GET_DELEGATES = "/api/accounts/delegates?address={0}";
                public static readonly string GET_TOP_ACCOUNTS = "/api/accounts/top?limit={0}&offset={1}";
            }

            public static class Block
            {
                public static readonly string GET_BLOCK = "/api/blocks/get?id={0}";
                public static readonly string GET_ALL = "/api/blocks";
                public static readonly string GET_EPOCH = "/api/blocks/getEpoch";
                public static readonly string GET_HEIGHT = "/api/blocks/getHeight";
                public static readonly string GET_NETHASH = "/api/blocks/getNethash";
                public static readonly string GET_FEES = "/api/blocks/getFees";
                public static readonly string GET_MILESTONE = "/api/blocks/getMilestone";
                public static readonly string GET_REWARD = "/api/blocks/getReward";
                public static readonly string GET_SUPPLY = "/api/blocks/getSupply";
                public static readonly string GET_STATUS = "/api/blocks/getStatus";
            }

            public static class Delegate
            {
                public static readonly string GET_ALL = "/api/delegates";
                public static readonly string GET_BY_USERNAME = "/api/delegates/get?username={0}";
                public static readonly string GET_BY_PUBLIC_KEY = "/api/delegates/get?publicKey={0}";
                public static readonly string GET_VOTERS = "/api/delegates/voters?publicKey={0}";
                public static readonly string GET_FEE = "/api/delegates/fee";
                public static readonly string GET_FORGED = "/api/delegates/forging/getForgedByAccount?generatorPublicKey={0}";
                public static readonly string GET_NEXT_FORGERS = "/api/delegates/getNextForgers";
            }

            public static class Loader
            {
                public static readonly string GET_STATUS = "/api/loader/status";
                public static readonly string GET_SYNC_STATUS = "/api/loader/status/sync";
                public static readonly string GET_AUTO_CONFIGURE = "/api/loader/autoconfigure";
            }

            public static class Peer
            {
                public static readonly string GET = "/api/peers/get?ip={0}&port={1}";
                public static readonly string GET_ALL = "/peer/list";
                public static readonly string GET_STATUS = "/peer/status";
            }

            public static class Transaction
            {
                public static readonly string GET_ALL = "/api/transactions";
                public static readonly string GET_ALL_UNCONFIRMED = "/api/transactions/unconfirmed";
                public static readonly string GET_BY_ID = "/api/transactions/get?id={0}";
                public static readonly string GET_BY_ID_UNCONFIRMED = "/api/transactions/unconfirmed/get?id={0}";
                public static readonly string POST = "/peer/transactions";
            }
        }

        public static class ArkHttpMethods
        {
            public static readonly string GET = "GET";
            public static readonly string HEAD = "HEAD";
            public static readonly string POST = "POST";
        }
    }
}