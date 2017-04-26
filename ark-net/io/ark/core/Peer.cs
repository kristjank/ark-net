using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        string status = "NEW";

        private static HttpClient httpClient;
        private Dictionary<string, dynamic> networkHeaders = Network.Mainnet.GetHeaders();

        private Peer(string ip, int port, string protocol)
        {
            this.ip = ip;
            this.port = port;
            this.protocol = protocol;
        }
        private string GetUri()
        {
            return String.Format(this.protocol + "//" + this.ip + ":" + this.port);
        }

        public static Peer Create(String peerData)
        {
            string[] data = peerData.Split(':');
            int port = Convert.ToInt32(data[1]);
            string ip = data[0];
            string protocol = "http://";
            if (port % 1000 == 443) protocol = "https://";
            return new Peer(ip, port, protocol);
        }

        // return Future that will deliver the JSON as a Map
        private string MakeRequest(String method, String path, string body="")
        {
            if (httpClient == null)
            {
                httpClient = new HttpClient()
                {
                    BaseAddress = new UriBuilder(this.protocol, this.ip, this.port).Uri
                };
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            httpClient.DefaultRequestHeaders.Add("nethash", Network.Mainnet.Nethash);
            httpClient.DefaultRequestHeaders.Add("version", Network.Mainnet.Version);
            httpClient.DefaultRequestHeaders.Add("port", Network.Mainnet.Port.ToString());

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
                        Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(body);
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

        public string GetPeerStatus()
        {
            string response = MakeRequest("GET", "/peer/status");
            return response;

        }

        public string GetPeers()
        {
            string response = MakeRequest("GET", "/peer/list");
            //Dictionary<string, dynamic> peerList = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);
            return response;
        }
    }
}
