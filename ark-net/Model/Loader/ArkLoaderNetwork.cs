using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Loader
{
    public class ArkLoaderNetwork
    {
        public string NetHash { get; set; }
        public string Token { get; set; }
        public string Symbol { get; set; }
        public string Explorer { get; set; }
        public int Version { get; set; }
    }
}
