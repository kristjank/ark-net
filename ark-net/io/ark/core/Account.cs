using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ark_net.io.ark.core
{
    class Account
    {
        String address;
        String publicKey;
        long balance;
        String username;
        int rate;
        List<String> votes;

        public string Address { get => address; set => address = value; }
    }
}
