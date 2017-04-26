using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.model
{
    public class PeerVO
    {

        public class Rootobject
        {
            public bool success { get; set; }
            public int height { get; set; }
            public bool forgingAllowed { get; set; }
            public int currentSlot { get; set; }
            public Header header { get; set; }
        }

        public class Header
        {
            public string id { get; set; }
            public int height { get; set; }
            public int version { get; set; }
            public int totalAmount { get; set; }
            public int totalFee { get; set; }
            public int reward { get; set; }
            public string payloadHash { get; set; }
            public int payloadLength { get; set; }
            public int timestamp { get; set; }
            public int numberOfTransactions { get; set; }
            public string previousBlock { get; set; }
            public string generatorPublicKey { get; set; }
            public string blockSignature { get; set; }
        }

    }
}
