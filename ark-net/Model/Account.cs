namespace ArkNet.Model
{
    public class Account
    {
        public string Address { get; set; }
        public string AnconfirmedBalance { get; set; }
        public string Balance { get; set; }
        public string PublicKey { get; set; }
        public int UnconfirmedSignature { get; set; }
        public int SecondSignature { get; set; }
        public object SecondPublicKey { get; set; }
        public object[] Multisignatures { get; set; }
        public object[] U_Multisignatures { get; set; }
    }
}