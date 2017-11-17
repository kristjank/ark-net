using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Text;
using ArkNet.Utils;

namespace ArkNet.Core
{
    public class PeerApi
    {
        private HttpClient _httpClient;
        private string _ip;
        private int _port;

        public HttpClient HttpClient { get { return _httpClient; } }
        public string Ip { get { return _ip; } }
        public int Port { get { return _port; } }

        public PeerApi(string ip, int port)
        {
            var protocol = "http://";
            if (port % 1000 == 443) protocol = "https://";

            Init(ip, port);

            _httpClient = new HttpClient()
            {
                BaseAddress = new UriBuilder(protocol, this._ip, this._port).Uri
            };

            if (ArkNetApi.Instance.NetworkSettings != null)
            {
                _httpClient.DefaultRequestHeaders.Add("nethash", ArkNetApi.Instance.NetworkSettings.NetHash);
                _httpClient.DefaultRequestHeaders.Add("version", ArkNetApi.Instance.NetworkSettings.Version);
                _httpClient.DefaultRequestHeaders.Add("port", ArkNetApi.Instance.NetworkSettings.Port.ToString());
            }
        }

        private void Init(string ip, int port)
        {
            this._ip = ip;
            this._port = port;
        }

        public async Task<string> MakeRequest(string method, string path, string body = "", int retryCount = 0)
        {
            HttpResponseMessage response;
            var methodString = new HttpMethod(method).ToString().ToUpper();

            try
            {
                switch (methodString)
                {
                    case "GET":
                    case "HEAD":
                        response = await _httpClient.GetAsync(path);
                        break;
                    case "POST":
                        response = await _httpClient.PostAsync(path, new StringContent(JObject.Parse(body).ToString(), Encoding.UTF8, "application/json"));
                        break;
                    case "PUT":
                        response = await _httpClient.PutAsync(path, new StringContent(JObject.Parse(body).ToString(), Encoding.UTF8, "application/json"));
                        break;
                    default:
                        throw new NotImplementedException();
                }
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception)
            {
                if (ArkNetApi.Instance.NetworkSettings != null && NetworkApi.Instance.ActivePeer != null)
                {
                    if (retryCount < ArkNetApi.Instance.NetworkSettings.MaxRequestRetryCount)
                    {
                        NetworkApi.Instance.SwitchPeer();
                        _httpClient = NetworkApi.Instance.ActivePeer.HttpClient;
                        return await MakeRequest(method, path, body, retryCount + 1);
                    }
                }
                throw;
            }
        }

        public async Task<bool> IsOnline()
        {
            try
            {
                await MakeRequest(ArkStaticStrings.ArkHttpMethods.HEAD, ArkStaticStrings.ArkApiPaths.Loader.GET_STATUS);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}