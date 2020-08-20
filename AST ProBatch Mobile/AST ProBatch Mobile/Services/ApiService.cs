using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
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
            public static string PBAuthTest = "/pbauth";
            public static string PBMenuBTest = "/pbauth";
            public static string PBAuthApiAuth = "/pbauth/apiauth";
            public static string PBAuthAuthentication = "/pbauth/probatchauth";
            public static string PBMenuBApiAuth = "/pbmenub/apiauth";
            public static string PBMenuBExecute = "/pbmenub/probatchmonitoringandexecution";
        }

        public static class ApiMethod
        {
            public static string Test = "/auth";
            public static string Login = "/login";
            public static string GetLogs = "/getlogs";
        }

        public static class TokenType
        {
            public static string Scheme = "Bearer";
        }
        #endregion

        public async Task<Response> ApiIsAvailable(string urlApi, string apiConsult)
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(15);

                StringBuilder urlApiConsult = new StringBuilder();
                switch (apiConsult)
                {
                    case ApiConsult.ApiAuth:
                        urlApiConsult.Append(urlApi);
                        urlApiConsult.Append(ApiController.PBAuthTest);
                        urlApiConsult.Append(ApiMethod.Test);
                        break;
                    case ApiConsult.ApiMenuB:
                        urlApiConsult.Append(urlApi);
                        urlApiConsult.Append(ApiController.PBMenuBTest);
                        urlApiConsult.Append(ApiMethod.Test);
                        break;
                }

                var response = await client.GetAsync(urlApiConsult.ToString());

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Api en error o la misma no está disponible",
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
                    Message = "Error al intentar consultar la Api",
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
        public async Task<Response> GetToken(string urlDomain, string urlPrefix, string apiConsult)
        {
            try
            {
                var request = JsonConvert.SerializeObject(new CipherData { Data = "ssMz44FsGtYVwik/0Qvr7tTD2326nWjfWEIaLcwlbvm1P1bRK1pXiRdBTYzg+29JuKJlfhC0szINqD4EC8De0TR4KLBigHwVFdeIUNkM3bc=" });
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlDomain);

                StringBuilder urlApiConsult = new StringBuilder();
                switch (apiConsult)
                {
                    case ApiConsult.ApiAuth:
                        urlApiConsult.Append(urlPrefix);
                        urlApiConsult.Append(ApiController.PBAuthApiAuth);
                        urlApiConsult.Append(ApiMethod.Login);
                        break;
                    case ApiConsult.ApiMenuB:
                        urlApiConsult.Append(urlPrefix);
                        urlApiConsult.Append(ApiController.PBMenuBApiAuth);
                        urlApiConsult.Append(ApiMethod.Login);
                        break;
                }

                //var url = string.Format("{0}{1}{2}", urlPrefix, ApiController.ApiAuth, ApiMethod.Login);

                var response = await client.PostAsync(urlApiConsult.ToString(), content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Api en error o la misma no está disponible", //response.StatusCode.ToString(),
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
                    Message = "Error al intentar consultar la Api", //ex.Message,
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
                var url = string.Format("{0}{1}{2}", urlPrefix, ApiController.PBAuthAuthentication, ApiMethod.Login);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Api en error o la misma no está disponible", //response.StatusCode.ToString(),
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
                    Message = "Error al intentar consultar la Api", //ex.Message,
                    Data = string.Empty,
                };
            }
        }
        #endregion
    }
}
