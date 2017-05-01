namespace ArkNet.Model
{
    public class PeerStatus
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
            public int TotalAmount { get; set; }
            public int TotalFee { get; set; }
            public int Reward { get; set; }
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