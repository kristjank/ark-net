using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Transactions
{
    public class ArkTransactionResponse : ArkResponseBase
    {
        public ArkTransaction Transaction { get; set; }
    }
}
