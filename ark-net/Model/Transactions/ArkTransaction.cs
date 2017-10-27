using ArkNet.Model.Shared;
using System.Numerics;

namespace ArkNet.Model.Transactions
{
    public class ArkTransaction
    {
        public string Id { get; set; }
        public string BlockId { get; set; }
        public long Height { get; set; }
        public int Type { get; set; }
        public int Timestamp { get; set; }
        public long Amount { get; set; }
        public long Fee { get; set; }
        public string VendorField { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string SenderPublicKey { get; set; }
        public string Signature { get; set; }
        public string SignSignature { get; set; }
        public ArkAsset Asset { get; set; }
        public int Confirmations { get; set; }
    }
}