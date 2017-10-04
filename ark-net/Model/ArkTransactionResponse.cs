using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model
{
    public class ArkTransactionResponse
    {
        public bool Status { get; set; }

        public string Data { get; set; }

        public string Error { get; set; }
    }
}
