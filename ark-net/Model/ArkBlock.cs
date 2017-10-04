using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model
{
    public class ArkBlock : ArkError
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public int Timestamp { get; set; }
        public int Height { get; set; }
        public string PreviousBlock { get; set; }
        public int NumberOfTransactions { get; set; }
        public long TotalAmount { get; set; }
        public long TotalFee { get; set; }
        public long Reward { get; set; }
        public int PayloadLength { get; set; }
        public string PayloadHash { get; set; }
        public string GeneratorPublicKey { get; set; }
        public string GeneratorId { get; set; }
        public string BlockSignature { get; set; }
        public long Confirmations { get; set; }
        public long TotalForged { get; set; }
    }
}
