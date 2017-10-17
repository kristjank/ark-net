﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using NBitcoin.DataEncoders;
using ArkNet.Core;
using JsonConfig.Net;

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
                    NetworkSettings = new ArkNetworkSettings(Config.Global.MainNet);
                    break;
                case NetworkType.TestNet:
                    NetworkSettings = new ArkNetworkSettings(Config.Global.TestNet);
                    break;
                case NetworkType.DevNet:
                    NetworkSettings = new ArkNetworkSettings(Config.Global.DevNet);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            await NetworkApi.Instance.WarmUp();

            return;
        }
    }
}
