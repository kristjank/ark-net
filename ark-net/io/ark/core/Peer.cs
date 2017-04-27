using ark.io.ark.model;
using io.ark.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.core
{
    public class Peer
    {
        public string ip;
        int port;
        string protocol = "http://";
        //string status = "NEW";

        private HttpClient httpClient;
        private Dictionary<string, dynamic> networkHeaders = Network.Mainnet.GetHeaders();

        private void OpenServicePoint(Uri uri)
        {
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.DefaultConnectionLimit = 10000;

            ServicePoint sp = ServicePointManager.FindServicePoint(uri);
            sp.UseNagleAlgorithm = true;
            sp.Expect100Continue = true;
            sp.ConnectionLimit = 10000;
            sp.ConnectionLeaseTimeout = 30000;

        }

        private void Init(string ip, int port, string protocol)
        {
            this.ip = ip;
            this.port = port;
            this.protocol = protocol;
        }

        public Peer(String peerData)
        {
            string[] data = peerData.Split(':');
            int port = Convert.ToInt32(data[1]);
            string ip = data[0];
            string protocol = "http://";
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

        // return Future that will deliver the JSON as a Map
        private string MakeRequest(String method, String path, string body="")
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
                        JObject jObject = JObject.Parse(body);
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

        public string PostTransaction(Transaction transaction)
        {
            string body = "{transactions: [" + transaction.ToObject(true) + "]} ";

            string response = MakeRequest("POST", "/peer/transactions", body);
            return response;
        }

        public List<PeerVO> GetPeers()
        {
            string response = MakeRequest("GET", "/peer/list");
            JObject parsed = JObject.Parse(response);
            JArray array = (JArray)parsed["peers"];

            List<PeerVO> peerList = JsonConvert.DeserializeObject<List<PeerVO>>(array.ToString());
            return peerList;
        }

        public PeerStatusVO GetPeerStatus()
        {
            string response = MakeRequest("GET", "/peer/status");
            JObject parsed = JObject.Parse(response);

            PeerStatusVO peerStat = JsonConvert.DeserializeObject<PeerStatusVO>(response);
            return peerStat;
        }

        public List<TransactionVO> GetTransactions(bool unconfirmed=false)
        {
            string path = "/api/transactions";
            if (unconfirmed)
                path += "/unconfirmed";

            string response = MakeRequest("GET", path);
            JObject parsed = JObject.Parse(response);
            JArray array = (JArray)parsed["transactions"];

            List<TransactionVO> tranList = JsonConvert.DeserializeObject<List<TransactionVO>>(array.ToString());
            return tranList;
        }

        public TransactionVO GetTransaction(string id, bool unconfirmed=false)
        {
            string path = "/api/transactions";
            if (unconfirmed)
                path += "/unconfirmed";

            string response = MakeRequest("GET", path + "/get?id="+id);
            JObject parsed = JObject.Parse(response);

            TransactionVO trans = new TransactionVO();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                trans.id = parsed["error"].ToString();
            }
            else
            {
                trans = JsonConvert.DeserializeObject<TransactionVO>(parsed["transaction"].ToString());
            }
           
            return trans;
        }        

        public List<DelegateVO> GetDelegates()
        {
            string response = MakeRequest("GET", "/api/delegates");
            JObject parsed = JObject.Parse(response);
            JArray array = (JArray)parsed["delegates"];

            List<DelegateVO> delegList = JsonConvert.DeserializeObject<List<DelegateVO>>(array.ToString());
            return delegList;
        }

        public DelegateVO GetDelegatebyUsername(string username)
        {
            string response = MakeRequest("GET", "/api/delegates/get?username=" + username);
            JObject parsed = JObject.Parse(response);

            DelegateVO dele = new DelegateVO();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                dele.username = parsed["error"].ToString();
            }
            else
            {
                dele = JsonConvert.DeserializeObject<DelegateVO>(parsed["delegate"].ToString());
            }
            return dele;
        }

        public DelegateVO GetDelegatebyPubKey(string pubKey)
        {
            string response = MakeRequest("GET", "/api/delegates/get?publicKey=" + pubKey);
            JObject parsed = JObject.Parse(response);

            DelegateVO dele = new DelegateVO();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                dele.username = parsed["error"].ToString();
            }
            else
            {
                dele = JsonConvert.DeserializeObject<DelegateVO>(parsed["delegate"].ToString());
            }
            return dele;
        }

        public DelegateVO GetDelegatebyAddress(string address)
        {
            string response = MakeRequest("GET", "/api/delegates/get?address=" + address);
            JObject parsed = JObject.Parse(response);

            DelegateVO dele = new DelegateVO();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                dele.username = parsed["error"].ToString();
            }
            else
            {
                dele = JsonConvert.DeserializeObject<DelegateVO>(parsed["delegate"].ToString());
            }
            return dele;
        }


        public List<DelegateVotersVO> GetDelegateVoters(string pubKey)
        {
            string response = MakeRequest("GET", "/api/delegates/voters?publicKey=" + pubKey);
            JObject parsed = JObject.Parse(response);
            JArray array = (JArray)parsed["accounts"];

            List<DelegateVotersVO> delegVotersList = new List<DelegateVotersVO>();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                DelegateVotersVO dele = new DelegateVotersVO();
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
            string response = MakeRequest("GET", "/api/accounts/?address=" + address);
            JObject parsed = JObject.Parse(response);

            AccountVO account = new AccountVO();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                account.address = parsed["error"].ToString();
            }
            else
            {
                account = JsonConvert.DeserializeObject<AccountVO>(parsed["account"].ToString());
            }
            return account;
        }
    }
}
