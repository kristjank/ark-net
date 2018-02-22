using ArkNet.Attributes;
using ArkNet.Messages.BaseMessages;

namespace ArkNet.Messages.Transaction
{
    public class ArkUnconfirmedTransactionRequest : ArkBaseRequest
    {
        [ArkQueryParam(Name = "senderPublicKey")]
        public string SenderPublickey { get; set; }

        [ArkQueryParam(Name = "address")]
        public string Address { get; set; }
    }
}
