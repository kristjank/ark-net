using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkDelegate = ArkNet.Model.ArkDelegate;

namespace ArkNet.Core
{
	public class PeerApi
	{
		//string status = "NEW";

		private readonly HttpClient httpClient;
		public string ip;
		private Dictionary<string, dynamic> networkHeaders = NetworkApi.Mainnet.GetHeaders();
		private int port;
		private string protocol = "http://";

		public PeerApi(string peerData)
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
			httpClient.DefaultRequestHeaders.Add("nethash", NetworkApi.Mainnet.Nethash);
			httpClient.DefaultRequestHeaders.Add("version", NetworkApi.Mainnet.Version);
			httpClient.DefaultRequestHeaders.Add("port", NetworkApi.Mainnet.Port.ToString());
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
        //SERVICE CALLS -----------------------------------------------------------------------------------
		public (bool status, string data, string error) PostTransaction(TransactionApi transaction)
		{
			string body = "{transactions: [" + transaction.ToObject(true) + "]} ";

			var response = MakeRequest("POST", "/peer/transactions", body);

			var parsed = JObject.Parse(response);
			var status = Convert.ToBoolean(parsed["success"]);


			return new ValueTuple<bool, string, string>(status, parsed["message"]?.ToString() ?? parsed["transactionIds"]?.ToString() ?? "",
				parsed["error"]?.ToString() ?? "");
		}

		public IReadOnlyCollection<ArkPeer> GetPeers()
		{
			var response = MakeRequest("GET", "/peer/list");
			var parsed = JObject.Parse(response);
			var array = (JArray) parsed["peers"];

			var peerList = JsonConvert.DeserializeObject<IReadOnlyCollection<ArkPeer>>(array.ToString());
		    return peerList;
		}

		public PeerStatus GetPeerStatus()
		{
			var response = MakeRequest("GET", "/peer/status");
			var parsed = JObject.Parse(response);

			var peerStat = JsonConvert.DeserializeObject<PeerStatus>(response);
			return peerStat;
		}

		public IReadOnlyCollection<ArkTransaction> GetTransactions(bool unconfirmed = false)
		{
			var path = "/api/transactions";
			if (unconfirmed)
				path += "/unconfirmed";

			var response = MakeRequest("GET", path);
			var parsed = JObject.Parse(response);
			var array = (JArray) parsed["transactions"];

			var tranList = JsonConvert.DeserializeObject<IReadOnlyCollection<ArkTransaction>>(array.ToString());
			return tranList;
		}

		public ArkTransaction GetTransaction(string id, bool unconfirmed = false)
		{
			var path = "/api/transactions";
			if (unconfirmed)
				path += "/unconfirmed";

			var response = MakeRequest("GET", path + "/get?id=" + id);
			var parsed = JObject.Parse(response);

			var trans = new ArkTransaction();
			if (!Convert.ToBoolean(parsed["success"]))
				trans.Id = parsed["error"].ToString();
			else
				trans = JsonConvert.DeserializeObject<ArkTransaction>(parsed["transaction"].ToString());

			return trans;
		}

		public IReadOnlyCollection<ArkDelegate> GetDelegates()
		{
			var response = MakeRequest("GET", "/api/delegates");
			var parsed = JObject.Parse(response);
			var array = (JArray) parsed["delegates"];

			var delegList = JsonConvert.DeserializeObject<IReadOnlyCollection<ArkDelegate>>(array.ToString());
			return delegList;
		}

		public ArkDelegate GetDelegatebyUsername(string username)
		{
			var response = MakeRequest("GET", "/api/delegates/get?username=" + username);
			var parsed = JObject.Parse(response);

			var dele = new ArkDelegate();
			if (!Convert.ToBoolean(parsed["success"]))
				dele.Username = parsed["error"].ToString();
			else
				dele = JsonConvert.DeserializeObject<ArkDelegate>(parsed["delegate"].ToString());
			return dele;
		}

		public ArkDelegate GetDelegatebyPubKey(string pubKey)
		{
			var response = MakeRequest("GET", "/api/delegates/get?publicKey=" + pubKey);
			var parsed = JObject.Parse(response);

			var dele = new Model.ArkDelegate();
			if (!Convert.ToBoolean(parsed["success"]))
				dele.Username = parsed["error"].ToString();
			else
				dele = JsonConvert.DeserializeObject<ArkDelegate>(parsed["delegate"].ToString());
			return dele;
		}

		public ArkDelegate GetDelegatebyAddress(string address)
		{
			var response = MakeRequest("GET", "/api/delegates/get?address=" + address);
			var parsed = JObject.Parse(response);

			var dele = new ArkDelegate();
			if (!Convert.ToBoolean(parsed["success"]))
				dele.Username = parsed["error"].ToString();
			else
				dele = JsonConvert.DeserializeObject<ArkDelegate>(parsed["delegate"].ToString());
			return dele;
		}


		public List<DelegateVoters> GetDelegateVoters(string pubKey)
		{
			var response = MakeRequest("GET", "/api/delegates/voters?publicKey=" + pubKey);
			var parsed = JObject.Parse(response);
			var array = (JArray) parsed["accounts"];

			var delegVotersList = new List<DelegateVoters>();
			if (!Convert.ToBoolean(parsed["success"]))
			{
				var dele = new DelegateVoters();
				dele.Username = parsed["error"].ToString();
			}
			else
			{
				delegVotersList = JsonConvert.DeserializeObject<List<DelegateVoters>>(array.ToString());
			}
			return delegVotersList;
		}

		public ArkAccount GetAccountbyAddress(string address)
		{
			var response = MakeRequest("GET", "/api/accounts/?address=" + address);
			var parsed = JObject.Parse(response);

			var account = new Model.ArkAccount();
			if (!Convert.ToBoolean(parsed["success"]))
				account.Address = parsed["error"].ToString();
			else
				account = JsonConvert.DeserializeObject<Model.ArkAccount>(parsed["account"].ToString());
			return account;
		}
	}
}