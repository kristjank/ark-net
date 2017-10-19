using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Text;

namespace ArkNet.Core
{
    public class PeerApi
    {
        //string status = "NEW";

        private readonly HttpClient httpClient;
        public string ip;
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

            httpClient = new HttpClient()
            {
                BaseAddress = new UriBuilder(this.protocol, this.ip, this.port).Uri
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("nethash", ArkNetApi.Instance.NetworkSettings.NetHash);
            httpClient.DefaultRequestHeaders.Add("version", ArkNetApi.Instance.NetworkSettings.Version);
            httpClient.DefaultRequestHeaders.Add("port", ArkNetApi.Instance.NetworkSettings.Port.ToString());
            //OpenServicePoint(httpClient.BaseAddress);
        }

        //private static void OpenServicePoint(Uri uri)
        //{
        //    ServicePointManager.CheckCertificateRevocationList = true;
        //    ServicePointManager.DefaultConnectionLimit = Config.Default.ArkPeer.DefaultConnectionLimit;

        //    var sp = ServicePointManager.FindServicePoint(uri);
        //    sp.UseNagleAlgorithm = true;
        //    sp.Expect100Continue = true;
        //    sp.ConnectionLimit = Config.Default.ArkPeer.ConnectionLimit;
        //    sp.ConnectionLeaseTimeout = Config.Default.ArkPeer.ConnectionLeaseTimeOut;
        //}

        private void Init(string ip, int port, string protocol)
        {
            this.ip = ip;
            this.port = port;
            this.protocol = protocol;
        }

        public async Task<string> MakeRequest(string method, string path, string body = "")
        {
            HttpResponseMessage response;
            var _Method = new HttpMethod(method);

            switch (_Method.ToString().ToUpper())
            {
                case "GET":
                    response = await httpClient.GetAsync(path);
                    break;
                case "HEAD":
                    // synchronous request without the need for .ContinueWith() or await
                    response = await httpClient.GetAsync(path);
                    break;
                case "POST":
                    var jObject = JObject.Parse(body);
                    response = await httpClient.PostAsync(path, new StringContent(jObject.ToString(), Encoding.UTF8, "application/json"));
                    break;
                /*case "PUT":
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
                    break;*/
                default:
                    throw new NotImplementedException();
            }
            // either this - or check the status to retrieve more information
            response.EnsureSuccessStatusCode();
            // get the rest/content of the response in a synchronous way
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<bool> IsOnline()
        {
            try
            {
                await MakeRequest("HEAD", "/api/loader/status");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}