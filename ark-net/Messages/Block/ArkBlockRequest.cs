using ArkNet.Attributes;
using ArkNet.Messages.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkNet.Messages.Block
{
    public class ArkBlockRequest : ArkBaseRequest
    {
        [ArkQueryParam(Name = "generatorPublicKey")]
        public string GeneratorPublickey { get; set; }

        [ArkQueryParam(Name = "totalAmount")]
        public long? TotalAmount { get; set; }

        [ArkQueryParam(Name = "totalFee")]
        public int? TotalFee { get; set; }

        [ArkQueryParam(Name = "reward")]
        public int? Reward { get; set; }

        [ArkQueryParam(Name = "previousBlock")]
        public string PreviousBlock { get; set; }

        [ArkQueryParam(Name = "height")]
        public int? Height { get; set; }
    }
}
