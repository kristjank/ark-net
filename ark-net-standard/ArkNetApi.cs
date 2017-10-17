using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using NBitcoin.DataEncoders;
using ArkNet.Core;

namespace ArkNet
{
    public sealed class ArkNetApi
    {
        private static readonly Lazy<ArkNetApi> lazy =
            new Lazy<ArkNetApi>(() => new ArkNetApi());

        public static ArkNetApi Instance => lazy.Value;

        public ArkNetworkSettings NetworkSettings;

        private ArkNetApi()
        {
            
        }

        public async Task Start(NetworkType type)
        {
            switch (type)
            {
                case NetworkType.MainNet:
                    NetworkSettings = new ArkNetworkSettings()
                    {
                        Name = "MainNet",
                        Port = 4001,
                        BytePrefix = 23,
                        Version = "1.0.1.",
                        NetHash = "6e84d08bd299ed97c212c886c98a57e36545c8f5d645ca7eeae63a8bd62d8988",
                        MaxNumOfBroadcasts = 5,
                        Fee = new Fees
                        {
                            Send = 10000000,
                            Vote = 100000000,
                            Delegate = 2500000000,
                            SecondSignature = 500000000,
                            MultiSignature = 500000000
                        },
                        PeerSeedList = new List<string>
                        {
                            "node1.arknet.cloud:4001",
                            "node2.arknet.cloud:4001",
                            "api.arknode.net:4001"
                        }
                    };
                    break;
                case NetworkType.DevNet:
                    NetworkSettings = new ArkNetworkSettings
                    {
                        Name = "DevNet",
                        Port = 4000,
                        BytePrefix = 30,
                        Version = "1.1.0.",
                        NetHash = "578e820911f24e039733b45e4882b73e301f813a0d2c31330dafda84534ffa23",
                        MaxNumOfBroadcasts = 5,
                        Fee = new Fees
                        {
                            Send = 10000000,
                            Vote = 100000000,
                            Delegate = 2500000000,
                            SecondSignature = 500000000,
                            MultiSignature = 500000000
                        },
                        PeerSeedList = new List<string>
                        {
                            "167.114.29.55:4002"
                        }
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            await NetworkApi.Instance.WarmUp();

            return;
        }
    }
}
