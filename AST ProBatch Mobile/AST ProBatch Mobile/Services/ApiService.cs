using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AST_ProBatch_Mobile.Services
{
    public class ApiService
    {
        #region Static Data
        public static class ApiController
        {
            public static string ApiAuth = "/apiauth";
            public static string ProbatchAuth = "/probatchauth";
        }

        public static class ApiMethod
        {
            public static string Login = "/login";
        }

        public static class TokenType
        {
            public static string Scheme = "Bearer";
        }
        #endregion

        public async Task<Response> ApiIsAvailable(string urlApi)
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                var response = await client.GetAsync(urlApi);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Api no disponible.",
                        Data = string.Empty,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Api Online",
                    Data = string.Empty,
                };
            }
            catch
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Api no disponible.",
                    Data = string.Empty,
                };
            }
        }

        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Por favor active el acceso a los datos o configure el acceso a una red WiFi.",
                    Data = string.Empty,
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("http://google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Revise su conexión a internet.",
                    Data = string.Empty,
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
                Data = string.Empty,
            };
        }

        #region Methods
        public async Task<Response> GetToken(string urlDomain, string urlPrefix)
        {
            try
            {
                var request = JsonConvert.SerializeObject(new CipherData { Data = "ssMz44FsGtYVwik/0Qvr7tTD2326nWjfWEIaLcwlbvm1P1bRK1pXiRdBTYzg+29JuKJlfhC0szINqD4EC8De0TR4KLBigHwVFdeIUNkM3bc=" });
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlDomain);
                var url = string.Format("{0}{1}", urlPrefix + ApiController.ApiAuth, ApiMethod.Login);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                        Data = string.Empty,
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var cipherData = JsonConvert.DeserializeObject<CipherData>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Token Obtenido",
                    Data = cipherData.Data,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = string.Empty,
                };
            }
        }

        public async Task<Response> AuthenticateProbath(string accessToken, string urlDomain, string urlPrefix, LoginPb loginPb)
        {
            try
            {
                var request = JsonConvert.SerializeObject(new CipherData { Data = Crypto.EncryptString(JsonConvert.SerializeObject(loginPb)) });
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType.Scheme, accessToken);
                client.BaseAddress = new Uri(urlDomain);
                var url = string.Format("{0}{1}", urlPrefix + ApiController.ProbatchAuth, ApiMethod.Login);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                        Data = string.Empty,
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var cipherData = JsonConvert.DeserializeObject<CipherData>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Data Obtenida",
                    Data = cipherData.Data,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = string.Empty,
                };
            }
        }
        #endregion
    }
}
