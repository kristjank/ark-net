namespace ArkNet.Model.Peer
{
    public class ArkPeerHeader
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
