using Cozify.Devices;
using Cozify.Hubs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cozify
{
    public class CozifyClient : ICozifyClient
    {
        public event EventHandler<ConnectionInfo> ConnectionInfoChanged;
        private string remoteUrl;
        private string remoteToken;
        private string email;
        private string password;
        private string hubId;
        private string hubIp;
        private int hubPort = 8893;
        private string hubVersion;
        private string hubToken;
        private string LocalUrl
        {
            get
            {
                return $"http://{hubIp}:{hubPort}";
            }
        }
        public CozifyClient(ConnectionInfo connection)
        {
            this.remoteUrl = connection.RemoteUrl.TrimEnd('/');
            this.remoteToken = connection.RemoteToken;
            this.email = connection.Email;
            this.password = connection.Password;
        }
        public Response RequestPassword(string email)
        {
            var url = remoteUrl + "/user/requestlogin";
            var query = new Dictionary<string, object>
            {
                {"email",email }
            };
            return Post(url, query);
        }
        public void Connect()
        {
            if (!string.IsNullOrWhiteSpace(remoteToken))
            {
                var refreshResponse = RefreshToken();
                if (refreshResponse.Success)
                {
                    remoteToken = refreshResponse.Data;
                }
                else
                {
                    return;
                }
            }
            else
            {
                var loginResponse = Login();
                if (loginResponse.Success)
                {
                    remoteToken = loginResponse.Data;
                }
                else
                {
                    return;
                }
            }
            if(remoteToken == null)
            {
                return;
            }

            var ipResponse = GetLanIp();
            if (!ipResponse.Success)
            {
                return;
            }
            hubIp = ipResponse.Data;

            var hubResponse = GetHub();
            if (!hubResponse.Success)
            {
                return;
            }
            var hub = hubResponse.Data;
            hubId = hub.Id;
            hubVersion = string.Join(".",hub.Version.Split('.').Take(2));

            var hubTokenResponse = GetHubTokens();
            if (!hubTokenResponse.Success)
            {
                return;
            }
            hubToken = hubTokenResponse.Data[hub.Id];

            ConnectionInfoChanged?.Invoke(this, new ConnectionInfo
            {
                RemoteToken = remoteToken,
                RemoteUrl = remoteUrl
            });
        }
        
        public Response<Hub> GetHub()
        {
            var url = LocalUrl + "/hub";
            var headers = GetLocalHeaders();
            var response = Get<Hub>(url, null, headers);
            return response; 
        }
        public Response<User> GetUser(string email)
        {
            var url = remoteUrl + "/user";
            var query = new Dictionary<string, object> { { "email", email } };
            var headers = GetRemoteHeaders();

            return Get<User>(url, query, headers);
        }
        public Response<string> GetHubFeatures()
        {
            var url = $"{LocalUrl}/cc/{hubVersion}/hub/features";
            var headers = GetLocalHeaders();
            return Get<string>(url, null, headers);
        }
        public Response<PollDelta> Poll()
        {
            var url = $"{LocalUrl}/cc/{hubVersion}/hub/poll";
            var headers = GetLocalHeaders();
            return Get<PollDelta>(url, null, headers);
        }
        public Response SendCommand(string deviceId, string type)
        {
            var url = $"{LocalUrl}/cc/{hubVersion}/devices/command";
            var headers = GetLocalHeaders();
            var data = new[] { new { id = deviceId, type = type } };
            return Put(url, null, headers, data);
        }
        public Response SendCommand(Command command)
        {
            var url = $"{LocalUrl}/cc/{hubVersion}/devices/command";
            var headers = GetLocalHeaders();
            var data = new[] { command };
            return Put(url, null, headers, data);
        }
        private Response<string> Login()
        {
            var url = remoteUrl + "/user/emaillogin";
            var headers = new Dictionary<string, string> { { "Content-Type", "application/x-www-form-urlencoded" } };
            var data = new Dictionary<string, object>
            {
                { "email", email },
                { "password", password }
            };
            var response = PostForm<string>(url, null, headers, data);
            return response;
        }
        private Response<string> GetLanIp()
        {
            var url = remoteUrl + "/hub/lan_ip";
            var response = Get<string[]>(url, null, null);
            return new Response<string>
            {
                Data = response.Data.First(),
                Status = response.Status,
                Success = response.Success
            };
        }

        private Response<string> RefreshToken()
        {
            var url = remoteUrl + "/user/refreshsession";
            var headers = GetRemoteHeaders();
            var response = Get<string>(url, null, headers);
            return response;
        }
        private Response<Dictionary<string, string>> GetHubTokens()
        {
            var url = remoteUrl + "/user/hubkeys";
            var headers = GetRemoteHeaders();
            var response = Get<Dictionary<string, string>>(url, null, headers);
            return response;
        }
        private Dictionary<string, string> GetRemoteHeaders()
        {
            return new Dictionary<string, string>
            {
                {"Authorization", remoteToken }
            };
        }
        private Dictionary<string, string> GetLocalHeaders()
        {
            return new Dictionary<string, string>
            {
                {"Authorization", hubToken }
            };
        }
        private string MakeFullUrl(string url, Dictionary<string, object> query)
        {
            if (query == null)
            {
                return url;
            }
            url += "?";
            foreach (var q in query)
            {
                url += $"{HttpUtility.UrlEncode(q.Key)}={HttpUtility.UrlEncode(q.Value.ToString())}&";
            }
            return url.Substring(0, url.Length - 1);
        }
        private WebClient CreateWebClient(Dictionary<string, string> headers)
        {
            var client = new WebClient();
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.Headers.Add(header.Key, header.Value);
                }
            }
            return client;
        }
        private T Deserialize<T>(string json)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(json, typeof(T));
            }
            return JsonConvert.DeserializeObject<T>(json, new CozifyJsonConverter(), new TimestampConverter());
        }
        private Response Get(string url, Dictionary<string, object> query)
        {
            return null;
        }
        private Response<T> Get<T>(string url, Dictionary<string, object> query = null, Dictionary<string, string> headers = null)
        {
            var fullUrl = MakeFullUrl(url, query);
            using (var client = CreateWebClient(headers))
            {
                try
                {
                    var body = client.DownloadString(fullUrl);
                    return new Response<T>
                    {
                        Success = true,
                        Data = Deserialize<T>(body)
                    };
                }
                catch (WebException ex)
                {
                    var response = ex.Response as HttpWebResponse;
                    return new Response<T> { Success = false, Status = response != null ? response.StatusCode : 0 };
                }
            }
        }
        private Response Post(string url, Dictionary<string, object> query = null, Dictionary<string, string> headers = null, object data = null)
        {
            var fullUrl = MakeFullUrl(url, query);
            using (var client = CreateWebClient(headers))
            {
                try
                {
                    if (data == null)
                    {
                        client.UploadString(fullUrl, "POST", "");
                    }
                    else
                    {
                        var json = JsonConvert.SerializeObject(data);
                        client.UploadString(fullUrl, "POST", json);
                    }
                    return new Response { Success = true };
                }
                catch (WebException ex)
                {
                    var response = ex.Response as HttpWebResponse;
                    return new Response { Success = false, Status = response != null ? response.StatusCode : 0 };
                }
            }
        }
        private Response<T> Post<T>(string url, Dictionary<string, object> query = null, Dictionary<string, string> headers = null, object data = null)
        {
            var fullUrl = MakeFullUrl(url, query);
            using (var client = CreateWebClient(headers))
            {
                try
                {
                    string body;
                    if (data == null)
                    {
                        body = client.UploadString(fullUrl, "POST", "");
                    }
                    else
                    {
                        var json = JsonConvert.SerializeObject(data);
                        body = client.UploadString(fullUrl, "POST", json);
                    }
                    return new Response<T>
                    {
                        Success = true,
                        Data = Deserialize<T>(body)
                    };
                }
                catch (WebException ex)
                {
                    var response = ex.Response as HttpWebResponse;
                    return new Response<T> { Success = false, Status = response != null ? response.StatusCode : 0 };
                }
            }
        }
        private Response<T> PostForm<T>(string url, Dictionary<string, object> query = null, Dictionary<string, string> headers = null, Dictionary<string, object> data = null)
        {
            var fullUrl = MakeFullUrl(url, query);
            using (var client = CreateWebClient(headers))
            {
                try
                {
                    string body;
                    if (data == null)
                    {
                        body = client.UploadString(fullUrl, "POST", "");
                    }
                    else
                    {
                        var form = "";
                        foreach (var x in data)
                        {
                            form += $"{x.Key}={HttpUtility.UrlEncode(x.Value.ToString())}&";
                        }
                        body = client.UploadString(fullUrl, "POST", form.Substring(0, form.Length - 1));
                    }
                    return new Response<T>
                    {
                        Success = true,
                        Data = Deserialize<T>(body)
                    };
                }
                catch (WebException ex)
                {
                    var response = ex.Response as HttpWebResponse;
                    return new Response<T> { Success = false, Status = response != null ? response.StatusCode : 0 };
                }
            }
        }
        private Response Put(string url, Dictionary<string, object> query = null, Dictionary<string, string> headers = null, object data = null)
        {
            var fullUrl = MakeFullUrl(url, query);
            using (var client = CreateWebClient(headers))
            {
                try
                {
                    if (data == null)
                    {
                        client.UploadString(fullUrl, "PUT", "");
                    }
                    else
                    {
                        client.Headers.Add("Content-Type", "application/json");
                        var json = JsonConvert.SerializeObject(data, new CozifyJsonConverter());
                        client.UploadString(fullUrl, "PUT", json);
                    }
                    return new Response { Success = true };
                }
                catch (WebException ex)
                {
                    var response = ex.Response as HttpWebResponse;
                    return new Response { Success = false, Status = response != null ? response.StatusCode : 0 };
                }
            }
        }
    }
}
