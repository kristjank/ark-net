using System.Numerics;

namespace ArkNet.Model
{
    public class ArkTransaction : ArkError
    {
        public string Id { get; set; }
        public int Type { get; set; }
        public int Timestamp { get; set; }
        public long Amount { get; set; }
        public long Fee { get; set; }
        public string SenderId { get; set; }
        public string TecipientId { get; set; }
        public string SenderPublicKey { get; set; }

        public string Signature { get; set; }

        //public Asset asset { get; set; }
        public int Confirmations { get; set; }
    }
}