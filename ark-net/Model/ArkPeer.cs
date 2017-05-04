using System.Linq;

namespace ArkNet.Model
{
    public class ArkPeer
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Version { get; set; }
        public string Os { get; set; }
        public int Height { get; set; }
        public string Status { get; set; }
        public int Delay { get; set; }
    }
}