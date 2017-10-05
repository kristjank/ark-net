using ArkNet.Model.BaseModels;
using System.Collections.Generic;

namespace ArkNet.Model.Delegate
{
    public class ArkDelegateVoter
    {
        public object Username { get; set; }
        public string Address { get; set; }
        public string PublicKey { get; set; }
        public long Balance { get; set; }
    }
}