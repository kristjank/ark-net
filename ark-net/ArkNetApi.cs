using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using JsonConfig;
using NBitcoin.DataEncoders;

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

        public void Start(NetworkType type)
        {
            switch (type)
            {
                case NetworkType.MainNet:
                    NetworkSettings = new ArkNetworkSettings(Config.Default.MainNet);
                    break;
                case NetworkType.TestNet:
                    NetworkSettings = new ArkNetworkSettings(Config.Default.TestNet);
                    break;
                case NetworkType.DevNet:
                    NetworkSettings = new ArkNetworkSettings(Config.Default.DevNet);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
