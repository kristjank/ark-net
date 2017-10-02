namespace ArkNet.Model
{
    public class ArkPeerStatus
    {
        public bool Success { get; set; }
        public int Height { get; set; }
        public bool ForgingAllowed { get; set; }
        public int CurrentSlot { get; set; }
        
        public PeerHeader Header { get; set; }

        public class PeerHeader
        {
            public string Id { get; set; }
            public int Height { get; set; }
            public int Version { get; set; }
            public long TotalAmount { get; set; }
            public long TotalFee { get; set; }
            public long Reward { get; set; }
            public string PayloadHash { get; set; }
            public int PayloadLength { get; set; }
            public int Timestamp { get; set; }
            public int NumberOfTransactions { get; set; }
            public string PreviousBlock { get; set; }
            public string GeneratorPublicKey { get; set; }
            public string BlockSignature { get; set; }
        }
    }
}