using ArkNet.Model.BaseModels;
using ArkNet.Service;
using System.Collections.Generic;
using System.Numerics;

namespace ArkNet.Model.Account
{
    public class ArkAccountTop
    {
        public string Address { get; set; }
        public long Balance { get; set; }
        public string PublicKey { get; set; }
    }
}