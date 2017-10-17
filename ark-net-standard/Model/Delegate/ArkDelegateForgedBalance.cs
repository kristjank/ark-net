using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Delegate
{
    public class ArkDelegateForgedBalance : ArkResponseBase
    {
        public long Fees { get; set; }
        public long Rewards { get; set; }
        public long Forged { get; set; }
    }
}