using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonConfig;
using NBitcoin.DataEncoders;

namespace ArkNet
{
    public static class ArkNetApi
    {
        public static void Start(string stinsad)
        {
            //Let's do some init here...
            var  storeOwner = Config.Default.ArkPeer.ConnectionLimit;
            var fee = Config.Default.TestNet.Fees.MultiSignature;
            byte a = Convert.ToByte(Config.Default.TestNet.BytePrefix);
            /*foreach (var fruit in Config.Default.Fruits)
                Console.WriteLine(fruit);

            var hahss = Config.Default.Mainnet.NetHash;
            //byte ver = Encoders.Hex.EncodeData()ecodeData()ncodeData(Config.Default.Mainnet.Byte);
            byte a = Convert.ToByte(23);
            */
        }
    }
}
