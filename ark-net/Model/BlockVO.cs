namespace ArkNet.Model
{
    public class BlockVO
    {
        public string id { get; set; }
        public int version { get; set; }
        public int timestamp { get; set; }
        public int height { get; set; }
        public string previousBlock { get; set; }
        public int numberOfTransactions { get; set; }
        public int totalAmount { get; set; }
        public int totalFee { get; set; }
        public int reward { get; set; }
        public int payloadLength { get; set; }
        public string payloadHash { get; set; }
        public string generatorPublicKey { get; set; }
        public string generatorId { get; set; }
        public string blockSignature { get; set; }
        public int confirmations { get; set; }
        public string totalForged { get; set; }
    }
}