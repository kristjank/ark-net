using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Account
{
    public class ArkAccountBalance : ArkResponseBase
    {
        public long Balance { get; set; }
        public long UnconfirmedBalance { get; set; }
    }
}
