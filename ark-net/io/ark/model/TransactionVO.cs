using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.model
{
    public class TransactionVO
    {
        public string id { get; set; }
        public int type { get; set; }
        public int timestamp { get; set; }
        public int amount { get; set; }
        public int fee { get; set; }
        public string senderId { get; set; }
        public string recipientId { get; set; }
        public string senderPublicKey { get; set; }
        public string signature { get; set; }
        //public Asset asset { get; set; }
        public int confirmations { get; set; }
    }
}
