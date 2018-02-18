using ArkNet.Attributes;
using ArkNet.Messages.BaseMessages;

namespace ArkNet.Messages.Transaction
{
    public class ArkTransactionRequest : ArkBaseRequest
    {
        [ArkQueryParam(Name = "senderPublicKey")]
        public string SenderPublickey { get; set; }

        [ArkQueryParam(Name = "ownerPublicKey")]
        public string OwnerPublicKey { get; set; }

        [ArkQueryParam(Name = "ownerAddress")]
        public string OwnerAddress { get; set; }

        [ArkQueryParam(Name = "senderId")]
        public string SenderId { get; set; }

        [ArkQueryParam(Name = "recipientId")]
        public string RecipientId { get; set; }

        [ArkQueryParam(Name = "amount")]
        public long? Amount { get; set; }

        [ArkQueryParam(Name = "fee")]
        public int? Fee { get; set; }

        [ArkQueryParam(Name = "type")]
        public int? Type { get; set; }

        [ArkQueryParam(Name = "blockId")]
        public string BlockId { get; set; }
    }
}
