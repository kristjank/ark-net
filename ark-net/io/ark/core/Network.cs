using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.core
{
    public sealed class Network
    {
        string nethash = "6e84d08bd299ed97c212c886c98a57e36545c8f5d645ca7eeae63a8bd62d8988";
        string name = "mainnet";
        int port = 4001;
        byte prefix = 0x17;
        string version = "1.0";
        int broadcastMax = 5;

        List<Peer> peers = new List<Peer>();

        static Random random = new Random();

        List<string> peerseed = new List<string>{
            "5.39.9.240:4001",
            "5.39.9.241:4001",
            "5.39.9.242:4001",
            "5.39.9.243:4001",
            "5.39.9.244:4001",
            "5.39.9.250:4001",
            "5.39.9.251:4001",
            "5.39.9.252:4001",
            "5.39.9.253:4001",
            "5.39.9.254:4001",
            "5.39.9.255:4001",
            "5.39.53.48:4001",
            "5.39.53.49:4001",
            "5.39.53.50:4001",
            "5.39.53.51:4001",
            "5.39.53.52:4001",
            "5.39.53.53:4001",
            "5.39.53.54:4001",
            "5.39.53.55:4001",
            "37.59.129.160:4001",
            "37.59.129.161:4001",
            "37.59.129.162:4001",
            "37.59.129.163:4001",
            "37.59.129.164:4001",
            "37.59.129.165:4001",
            "37.59.129.166:4001",
            "37.59.129.167:4001",
            "37.59.129.168:4001",
            "37.59.129.169:4001",
            "37.59.129.170:4001",
            "37.59.129.171:4001",
            "37.59.129.172:4001",
            "37.59.129.173:4001",
            "37.59.129.174:4001",
            "37.59.129.175:4001",
            "193.70.72.80:4001",
            "193.70.72.81:4001",
            "193.70.72.82:4001",
            "193.70.72.83:4001",
            "193.70.72.84:4001",
            "193.70.72.85:4001",
            "193.70.72.86:4001",
            "193.70.72.87:4001",
            "193.70.72.88:4001",
            "193.70.72.89:4001",
            "193.70.72.90:4001",
            "167.114.29.37:4001",
            "167.114.29.38:4001",
            "167.114.29.39:4001",
            "167.114.29.40:4001",
            "167.114.29.41:4001",
            "167.114.29.42:4001",
            "167.114.29.43:4001",
            "167.114.29.44:4001",
            "167.114.29.45:4001",
            "167.114.29.46:4001",
            "167.114.29.47:4001",
            "167.114.29.48:4001",
            "167.114.29.49:4001",
            "167.114.29.50:4001",
            "167.114.29.51:4001",
            "167.114.29.52:4001",
            "167.114.29.53:4001",
            "167.114.29.54:4001",
            "167.114.29.55:4001",
            "167.114.29.56:4001",
            "167.114.29.57:4001",
            "167.114.29.58:4001",
            "167.114.29.59:4001",
            "167.114.29.60:4001",
            "167.114.29.61:4001",
            "167.114.29.62:4001",
            "167.114.29.63:4001",
            "167.114.43.32:4001",
            "167.114.43.33:4001",
            "167.114.43.34:4001"
            };

        private static volatile Network instance;
        private static object syncRoot = new Object();

        public static Network Mainnet
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        { 
                            instance = new Network();
                            instance.WarmUp();
                        }
                    }
                }
                if (instance.name == "testnet")
                    throw new Exception("Network " + instance.name + " already initialized.Only one network can be initialized at runtime");
                
                return instance;
            }
        }

        public static Network Testnet
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Network(0x18, 4000, "testnet");
                            instance.WarmUp();
                        }
                    }
                }
                if (instance.name == "testnet")
                    throw new Exception("Network " + instance.name + " already initialized.Only one network can be initialized at runtime");

                return instance;
            }
        }

        private Network()
        {
           peers = new List<Peer>();
           System.Net.ServicePointManager.DefaultConnectionLimit = 1000;
        }
     
        private Network(byte prefix, int port, string name)
        {
            this.prefix = prefix;
            this.port = port;
            this.name = name;

            peers = new List<Peer>();
        }
        
        /*public static Network Mainnet = new Network();
        public static Network Testnet = new Network(0x18, 4000, "testnet");*/

        public string Nethash { get => nethash; set => nethash = value; }
        public string Name { get => name; set => name = value; }
        public int Port { get => port; set => port = value; }
        public byte Prefix { get => prefix; set => prefix = value; }
        public string Version { get => version; set => version = value; }
        public int BroadcastMax { get => broadcastMax; set => broadcastMax = value; }

        public dynamic GetHeaders(bool retJson=false)
        {
            Dictionary<string,dynamic> data = new Dictionary<string, dynamic> { ["nethash"] = this.nethash, ["version"] = this.version, ["port"] = this.port };

            if (retJson)
                return JsonConvert.SerializeObject(data);
            else
                return data;
        }

        private bool WarmUp()
        {
            if (peers.Count > 0) return false;
            foreach (string item in peerseed)
            {
                peers.Add(new Peer(item));
            }
            return true;
        }

        public Peer GetRandomPeer()
        {
            return peers[random.Next(peers.Count())];
        }

        public int MultipleBroadCast(Transaction transaction)
        {
            int res = 0;
            for (int i =0; i < 10; i++)
            {
                string response = GetRandomPeer().PostTransaction(transaction);

                JObject jObject = JObject.Parse(response);
                if (Convert.ToBoolean(jObject["success"]))
                    res++;
            }
            return res;
        }



    }
}
