namespace ArkNet.Model
{
    public class Delegate
    {
        public string Username { get; set; }
        public string Address { get; set; }
        public string PublicKey { get; set; }
        public string Vote { get; set; }
        public int Producedblocks { get; set; }
        public int Missedblocks { get; set; }
        public int Rate { get; set; }
        public float Approval { get; set; }
        public float Productivity { get; set; }
    }
}