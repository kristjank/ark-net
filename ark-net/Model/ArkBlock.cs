using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model
{
    public class ArkBlock
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public int Timestamp { get; set; }
        public int Height { get; set; }
        public string PreviousBlock { get; set; }
        public int NumberOfTransactions { get; set; }
        public BigInteger TotalAmount { get; set; }
        public BigInteger TotalFee { get; set; }
        public int PayloadLength { get; set; }
        public string PayloadHash { get; set; }
        public string GeneratorPublicKey { get; set; }
        public string GeneratorId { get; set; }
        public string BlockSignature { get; set; }
    }
}
