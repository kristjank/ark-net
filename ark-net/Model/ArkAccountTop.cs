using ArkNet.Service;
using System.Collections.Generic;
using System.Numerics;

namespace ArkNet.Model
{
    public class ArkAccountTopCollection : ArkError
    {
        public List<ArkAccountTop> Accounts { get; set; }
    }

    public class ArkAccountTop
    {
        public string Address { get; set; }
        public long Balance { get; set; }
        public string PublicKey { get; set; }
    }
}