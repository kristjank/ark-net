using System.Numerics;

namespace ArkNet.Model
{
    public class TransactionVO
    {
        public string id { get; set; }
        public int type { get; set; }
        public int timestamp { get; set; }
        public BigInteger amount { get; set; }
        public BigInteger fee { get; set; }
        public string senderId { get; set; }
        public string recipientId { get; set; }
        public string senderPublicKey { get; set; }
        public string signature { get; set; }
        //public Asset asset { get; set; }
        public int confirmations { get; set; }
    }
}
