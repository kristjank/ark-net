using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace ArkNet.Core
{
	public sealed class NetworkApi
	{
		private static readonly Random random = new Random();

		private static volatile NetworkApi instance;
		private static readonly object syncRoot = new object();
		private readonly List<PeerApi> peers = new List<PeerApi>();

		private readonly List<string> peerseed = new List<string>
		{
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

		private NetworkApi()
		{
			peers = new List<PeerApi>();
        }

		private NetworkApi(byte prefix, int port, string name)
		{
			Prefix = prefix;
			Port = port;
			Name = name;

			peers = new List<PeerApi>();
		}




		public static NetworkApi Mainnet
		{
			get
			{
				if (instance == null)
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new NetworkApi();
							instance.WarmUp();
						}
					}
				if (instance.Name == "testnet")
					throw new Exception("Network " + instance.Name +
					                    " already initialized.Only one network can be initialized at runtime");

				return instance;
			}
		}

		public static NetworkApi Testnet
		{
			get
			{
				if (instance == null)
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new NetworkApi(Properties.Settings.Default.TestNetBytePrefix,
						                            Properties.Settings.Default.TestNetPort, 
                                                    Properties.Settings.Default.TestNetName);
							instance.WarmUp();
						}
					}
				if (instance.Name == "testnet")
					throw new Exception("Network " + instance.Name +
					                    " already initialized.Only one network can be initialized at runtime");

				return instance;
			}
		}

		public string Nethash { get; set; } = Properties.Settings.Default.NetHash;//"6e84d08bd299ed97c212c886c98a57e36545c8f5d645ca7eeae63a8bd62d8988";
		public string Name { get; set; } = Properties.Settings.Default.MainNetName;
		public int Port { get; set; } = Properties.Settings.Default.MainNetPort;
		public byte Prefix { get; set; } = Properties.Settings.Default.MainNetBytePrefix;
		public string Version { get; set; } = Properties.Settings.Default.NetworkVersion;
		public int BroadcastMax { get; set; } = Properties.Settings.Default.MaxNumOfBroadcasts;
	    public PeerApi ActivePeer { get; set; } 

        public dynamic GetHeaders(bool retJson = false)
		{
			var data = new Dictionary<string, dynamic> {["nethash"] = Nethash, ["version"] = Version, ["port"] = Port};

			if (retJson)
				return JsonConvert.SerializeObject(data);
			return data;
		}

		private bool WarmUp()
		{
			if (peers.Count > 0) return false;
			foreach (var item in peerseed)
				peers.Add(new PeerApi(item));

		    ActivePeer = GetRandomPeer();
			return true;
		}

		public PeerApi GetRandomPeer()
		{
			return peers[random.Next(peers.Count())];
		}

		public int MultipleBroadCast(TransactionApi transaction)
		{
			var res = 0;
			for (var i = 0; i < BroadcastMax; i++)
			{
				var response = GetRandomPeer().PostTransaction(transaction);

				if (response.Item1)
					res++;
			}
			return res;
		}
	}
}