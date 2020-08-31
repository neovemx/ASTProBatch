using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using ASTProBatchMobile.Models.Service;
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
        #region Private Properties
        private string UrlDomain { get; set; }
        private string UrlPrefix { get; set; }
        private DataHelper DBHelper { get; set; }
        private string ApiToConsult { get; set; }
        #endregion

        #region Constructor
        public ApiService(string apiToConsult)
        {
            this.ApiToConsult = apiToConsult;
            this.DBHelper = new DataHelper();
            GetAppConfig();
        }
        #endregion

        #region Methods
        public async Task<Response> ApiIsAvailable()
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(15);

                StringBuilder urlApiConsult = new StringBuilder();
                switch (ApiToConsult)
                {
                    case ApiConsult.ApiAuth:
                        urlApiConsult.Append(this.UrlDomain + this.UrlPrefix);
                        urlApiConsult.Append(ApiController.PBAuthTest);
                        urlApiConsult.Append(ApiMethod.Test);
                        break;
                    case ApiConsult.ApiMenuB:
                        urlApiConsult.Append(this.UrlDomain + this.UrlPrefix);
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

        public async Task<Response> GetToken()
        {
            try
            {
                var request = JsonConvert.SerializeObject(new CipherData { Data = "ssMz44FsGtYVwik/0Qvr7tTD2326nWjfWEIaLcwlbvm1P1bRK1pXiRdBTYzg+29JuKJlfhC0szINqD4EC8De0TR4KLBigHwVFdeIUNkM3bc=" });
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(this.UrlDomain);
                client.Timeout = TimeSpan.FromSeconds(15);
                StringBuilder urlApiConsult = new StringBuilder();
                switch (ApiToConsult)
                {
                    case ApiConsult.ApiAuth:
                        urlApiConsult.Append(this.UrlPrefix);
                        urlApiConsult.Append(ApiController.PBAuthApiAuth);
                        urlApiConsult.Append(ApiMethod.Login);
                        break;
                    case ApiConsult.ApiMenuA:
                        urlApiConsult.Append(this.UrlPrefix);
                        //urlApiConsult.Append(ApiController.PBMenuAApiAuth);
                        urlApiConsult.Append(ApiMethod.Login);
                        break;
                    case ApiConsult.ApiMenuB:
                        urlApiConsult.Append(this.UrlPrefix);
                        urlApiConsult.Append(ApiController.PBMenuBApiAuth);
                        urlApiConsult.Append(ApiMethod.Login);
                        break;
                    case ApiConsult.ApiMenuC:
                        urlApiConsult.Append(this.UrlPrefix);
                        //urlApiConsult.Append(ApiController.PBMenuCApiAuth);
                        urlApiConsult.Append(ApiMethod.Login);
                        break;
                    case ApiConsult.ApiMenuD:
                        urlApiConsult.Append(this.UrlPrefix);
                        //urlApiConsult.Append(ApiController.PBMenuDApiAuth);
                        urlApiConsult.Append(ApiMethod.Login);
                        break;
                }

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

        public async Task<Response> AuthenticateProbath(string accessToken, LoginPb loginPb)
        {
            try
            {
                var request = JsonConvert.SerializeObject(new CipherData { Data = Crypto.EncryptString(JsonConvert.SerializeObject(loginPb)) });
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType.Scheme, accessToken);
                client.BaseAddress = new Uri(this.UrlDomain);
                client.Timeout = TimeSpan.FromSeconds(15);
                var url = string.Format("{0}{1}{2}", this.UrlPrefix, ApiController.PBAuthAuthentication, ApiMethod.Login);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Api en error o la misma no está disponible",
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
            catch //(Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error al intentar consultar la Api",
                    Data = string.Empty,
                };
            }
        }

        public async Task<Response> GetLogs(string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType.Scheme, accessToken);
                client.BaseAddress = new Uri(this.UrlDomain);
                client.Timeout = TimeSpan.FromSeconds(15);
                var url = string.Format("{0}{1}{2}", this.UrlPrefix, ApiController.PBMenuBExecute, ApiMethod.GetLogs);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Api en error o la misma no está disponible",
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
            catch //(Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error al intentar consultar la Api",
                    Data = string.Empty,
                };
            }
        }

        public async Task<Response> GetInstancesByLogAndUser(string accessToken, InstanceQueryValues instanceQueryValues)
        {
            try
            {
                var request = JsonConvert.SerializeObject(new CipherData { Data = Crypto.EncryptString(JsonConvert.SerializeObject(instanceQueryValues)) });
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType.Scheme, accessToken);
                client.BaseAddress = new Uri(this.UrlDomain);
                client.Timeout = TimeSpan.FromSeconds(15);
                var url = string.Format("{0}{1}{2}", this.UrlPrefix, ApiController.PBMenuBExecute, ApiMethod.GetInstancesByLogAndUser);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Api en error o la misma no está disponible",
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
            catch //(Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error al intentar consultar la Api",
                    Data = string.Empty,
                };
            }
        }
        #endregion

        #region Helpers
        private void GetAppConfig()
        {
            Table_Config table_Config = this.DBHelper.GetAppConfig();
            if (table_Config != null)
            {
                this.UrlDomain = table_Config.UrlDomain;
                this.UrlPrefix = table_Config.UrlPrefix;
            }
        }
        #endregion
    }
}
