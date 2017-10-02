using ArkNet.Service;
using System.Numerics;

namespace ArkNet.Model
{
    public class ArkAccountTop
    {
        public string Address { get; set; }
        public long Balance { get; set; }
        public string PublicKey { get; set; }
    }
}