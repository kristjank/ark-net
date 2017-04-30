using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet.Core
{
	public class Peer
	{
		//string status = "NEW";

		private readonly HttpClient httpClient;
		public string ip;
		private Dictionary<string, dynamic> networkHeaders = Network.Mainnet.GetHeaders();
		private int port;
		private string protocol = "http://";

		public Peer(string peerData)
		{
			var data = peerData.Split(':');
			var port = Convert.ToInt32(data[1]);
			var ip = data[0];
			var protocol = "http://";
			if (port % 1000 == 443) protocol = "https://";

			Init(ip, port, protocol);

			httpClient = new HttpClient();
			httpClient.BaseAddress = new UriBuilder(this.protocol, this.ip, this.port).Uri;
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Add("nethash", Network.Mainnet.Nethash);
			httpClient.DefaultRequestHeaders.Add("version", Network.Mainnet.Version);
			httpClient.DefaultRequestHeaders.Add("port", Network.Mainnet.Port.ToString());
			OpenServicePoint(httpClient.BaseAddress);
		}

		private static void OpenServicePoint(Uri uri)
		{
			ServicePointManager.CheckCertificateRevocationList = true;
			ServicePointManager.DefaultConnectionLimit = Properties.Settings.Default.DefaultConnectionLimit;

			var sp = ServicePointManager.FindServicePoint(uri);
			sp.UseNagleAlgorithm = true;
			sp.Expect100Continue = true;
			sp.ConnectionLimit = Properties.Settings.Default.ConnectionLimit;
			sp.ConnectionLeaseTimeout = Properties.Settings.Default.ConnectionLeaseTimeOut;
		}

		private void Init(string ip, int port, string protocol)
		{
			this.ip = ip;
			this.port = port;
			this.protocol = protocol;
		}

		// return Future that will deliver the JSON as a Map
		private string MakeRequest(string method, string path, string body = "")
		{
			HttpResponseMessage response;
			var _Method = new HttpMethod(method);

			switch (_Method.ToString().ToUpper())
			{
				case "GET":
					response = httpClient.GetAsync(path).Result;
					break;
				case "HEAD":
					// synchronous request without the need for .ContinueWith() or await
					response = httpClient.GetAsync(path).Result;
					break;
				case "POST":
				{
					var jObject = JObject.Parse(body);
					response = httpClient.PostAsJsonAsync(path, jObject).Result;
				}
					break;
				case "PUT":
				{
					// Construct an HttpContent from a StringContent
					HttpContent _Body = new StringContent(body);

					// and add the header to this object instance
					// optional: add a formatter option to it as well

					// synchronous request without the need for .ContinueWith() or await


					response = httpClient.PutAsync(path, _Body).Result;
				}
					break;
				case "DELETE":
					response = httpClient.DeleteAsync(path).Result;
					break;
				default:
					throw new NotImplementedException();
					break;
			}
			// either this - or check the status to retrieve more information
			response.EnsureSuccessStatusCode();
			// get the rest/content of the response in a synchronous way
			var content = response.Content.ReadAsStringAsync().Result;

			return content;
		}

		public Tuple<bool, string, string> PostTransaction(Transaction transaction)
		{
			string body = "{transactions: [" + transaction.ToObject(true) + "]} ";

			var response = MakeRequest("POST", "/peer/transactions", body);

			var parsed = JObject.Parse(response);
			var status = Convert.ToBoolean(parsed["success"]);


			return new Tuple<bool, string, string>(status, parsed["message"]?.ToString() ?? "",
				parsed["error"]?.ToString() ?? "");
		}

		public List<PeerVO> GetPeers()
		{
			var response = MakeRequest("GET", "/peer/list");
			var parsed = JObject.Parse(response);
			var array = (JArray) parsed["peers"];

			var peerList = JsonConvert.DeserializeObject<List<PeerVO>>(array.ToString());
			return peerList;
		}

		public PeerStatusVO GetPeerStatus()
		{
			var response = MakeRequest("GET", "/peer/status");
			var parsed = JObject.Parse(response);

			var peerStat = JsonConvert.DeserializeObject<PeerStatusVO>(response);
			return peerStat;
		}

		public List<TransactionVO> GetTransactions(bool unconfirmed = false)
		{
			var path = "/api/transactions";
			if (unconfirmed)
				path += "/unconfirmed";

			var response = MakeRequest("GET", path);
			var parsed = JObject.Parse(response);
			var array = (JArray) parsed["transactions"];

			var tranList = JsonConvert.DeserializeObject<List<TransactionVO>>(array.ToString());
			return tranList;
		}

		public TransactionVO GetTransaction(string id, bool unconfirmed = false)
		{
			var path = "/api/transactions";
			if (unconfirmed)
				path += "/unconfirmed";

			var response = MakeRequest("GET", path + "/get?id=" + id);
			var parsed = JObject.Parse(response);

			var trans = new TransactionVO();
			if (!Convert.ToBoolean(parsed["success"]))
				trans.id = parsed["error"].ToString();
			else
				trans = JsonConvert.DeserializeObject<TransactionVO>(parsed["transaction"].ToString());

			return trans;
		}

		public List<DelegateVO> GetDelegates()
		{
			var response = MakeRequest("GET", "/api/delegates");
			var parsed = JObject.Parse(response);
			var array = (JArray) parsed["delegates"];

			var delegList = JsonConvert.DeserializeObject<List<DelegateVO>>(array.ToString());
			return delegList;
		}

		public DelegateVO GetDelegatebyUsername(string username)
		{
			var response = MakeRequest("GET", "/api/delegates/get?username=" + username);
			var parsed = JObject.Parse(response);

			var dele = new DelegateVO();
			if (!Convert.ToBoolean(parsed["success"]))
				dele.username = parsed["error"].ToString();
			else
				dele = JsonConvert.DeserializeObject<DelegateVO>(parsed["delegate"].ToString());
			return dele;
		}

		public DelegateVO GetDelegatebyPubKey(string pubKey)
		{
			var response = MakeRequest("GET", "/api/delegates/get?publicKey=" + pubKey);
			var parsed = JObject.Parse(response);

			var dele = new DelegateVO();
			if (!Convert.ToBoolean(parsed["success"]))
				dele.username = parsed["error"].ToString();
			else
				dele = JsonConvert.DeserializeObject<DelegateVO>(parsed["delegate"].ToString());
			return dele;
		}

		public DelegateVO GetDelegatebyAddress(string address)
		{
			var response = MakeRequest("GET", "/api/delegates/get?address=" + address);
			var parsed = JObject.Parse(response);

			var dele = new DelegateVO();
			if (!Convert.ToBoolean(parsed["success"]))
				dele.username = parsed["error"].ToString();
			else
				dele = JsonConvert.DeserializeObject<DelegateVO>(parsed["delegate"].ToString());
			return dele;
		}


		public List<DelegateVotersVO> GetDelegateVoters(string pubKey)
		{
			var response = MakeRequest("GET", "/api/delegates/voters?publicKey=" + pubKey);
			var parsed = JObject.Parse(response);
			var array = (JArray) parsed["accounts"];

			var delegVotersList = new List<DelegateVotersVO>();
			if (!Convert.ToBoolean(parsed["success"]))
			{
				var dele = new DelegateVotersVO();
				dele.username = parsed["error"].ToString();
			}
			else
			{
				delegVotersList = JsonConvert.DeserializeObject<List<DelegateVotersVO>>(array.ToString());
			}
			return delegVotersList;
		}

		public AccountVO GetAccountbyAddress(string address)
		{
			var response = MakeRequest("GET", "/api/accounts/?address=" + address);
			var parsed = JObject.Parse(response);

			var account = new AccountVO();
			if (!Convert.ToBoolean(parsed["success"]))
				account.Address = parsed["error"].ToString();
			else
				account = JsonConvert.DeserializeObject<AccountVO>(parsed["account"].ToString());
			return account;
		}
	}
}