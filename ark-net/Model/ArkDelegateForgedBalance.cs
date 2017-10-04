namespace ArkNet.Model
{
    public class ArkDelegateForgedBalance : ArkError
    {
        public long Fees { get; set; }
        public long Rewards { get; set; }
        public long Forged { get; set; }
    }
}