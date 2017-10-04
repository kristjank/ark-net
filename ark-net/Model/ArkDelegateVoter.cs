using System.Collections.Generic;

namespace ArkNet.Model
{
    public class ArkDelegateVoterCollection : ArkError
    {
        public List<ArkDelegateVoter> Accounts { get; set; }
    }
    public class ArkDelegateVoter
    {
        public object Username { get; set; }
        public string Address { get; set; }
        public string PublicKey { get; set; }
        public long Balance { get; set; }
    }
}