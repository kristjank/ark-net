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
        string ip;
        int port;
        string protocol = "http://";
        string status = "NEW";

        private static HttpClient httpClient = new HttpClient();
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
        public dynamic MakeRequest(String method, String path, string body="")
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
                        // Construct an HttpContent from a StringContent
                        HttpContent _Body = new StringContent(body);
                        // and add the header to this object instance
                        // optional: add a formatter option to it as well
                        _Body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        // synchronous request without the need for .ContinueWith() or await
                        response = httpClient.PostAsync(path, _Body).Result;
                    }
                    break;
                case "PUT":
                    {
                        // Construct an HttpContent from a StringContent
                        HttpContent _Body = new StringContent(body);
                        // and add the header to this object instance
                        // optional: add a formatter option to it as well
                        _Body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
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

        public void PostTransaction(Transaction transaction)
        {
            //String response = MakeRequest("POST", "/peer/transactions", transaction.ToObject());
        }

        public Dictionary<string, dynamic> GetPeerStatus()
        {
            Dictionary<string,dynamic> peerStat = MakeRequest("GET", "/peer/status");
            bool forging = peerStat["forgingAllowed"];
            return peerStat;
        }

        public Dictionary<string, dynamic> GetPeers()
        {
            Dictionary<string, dynamic> peerList = MakeRequest("GET", "/peer/list");
            return peerList;
        }
    }
}
