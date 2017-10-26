using ArkNet.Attributes;
using ArkNet.Messages.BaseMessages;
using System;
using System.Collections.Generic;
using System.Text;

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
