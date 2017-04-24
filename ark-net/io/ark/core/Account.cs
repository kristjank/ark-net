using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.core
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
        public string PublicKey { get => publicKey; set => publicKey = value; }
        public long Balance { get => balance; set => balance = value; }
        public string Username { get => username; set => username = value; }
        public int Rate { get => rate; set => rate = value; }
        public List<string> Votes { get => votes; set => votes = value; }



    }
}
