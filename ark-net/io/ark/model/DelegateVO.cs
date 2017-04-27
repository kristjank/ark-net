using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.model
{
    public class DelegateVO
    {
        public string username { get; set; }
        public string address { get; set; }
        public string publicKey { get; set; }
        public string vote { get; set; }
        public int producedblocks { get; set; }
        public int missedblocks { get; set; }
        public int rate { get; set; }
        public float approval { get; set; }
        public float productivity { get; set; }

    }
}
