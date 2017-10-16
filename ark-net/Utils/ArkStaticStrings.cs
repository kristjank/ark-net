using System;
using System.Collections.Generic;

namespace ArkNet.Utils
{
    public static class ArkStaticStrings
    {
        public static class ArkApiPaths
        {
            public static class Account
            {
                public static readonly string GET_ACCOUNT = "/api/accounts?address={0}";
                public static readonly string GET_BALANCE = "/api/accounts/getBalance?address={0}";
                public static readonly string GET_DELEGATES = "/api/accounts/delegates?address={0}";
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