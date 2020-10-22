using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using AST_ProBatch_Mobile.Models.Service;
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
        private string ApiControllerSet { get; set; }
        #endregion

        #region Constructor
        public ApiService(string apiToConsult)
        {
            this.ApiToConsult = apiToConsult;
            this.DBHelper = new DataHelper();
            SetApiController();
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
                    case ApiConsult.ApiMenuA:
                        urlApiConsult.Append(this.UrlDomain + this.UrlPrefix);
                        urlApiConsult.Append(ApiController.PBMenuATest);
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
                        urlApiConsult.Append(ApiController.PBMenuAApiAuth);
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

        public async Task<Response> AuthenticateProbath(string accessToken, LoginPb QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.Login, QueryValues);
        }

        public async Task<Response> GetLogs(string accessToken)
        {
            return await HttpGet(accessToken, this.ApiControllerSet, ApiMethod.GetLogs);
        }

        public async Task<Response> GetInstancesByLogAndUser(string accessToken, InstanceQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.GetInstancesByLogAndUser, QueryValues);
        }

        public async Task<Response> GetCommandsByInstance(string accessToken, CommandQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.GetCommandsByInstance, QueryValues);
        }

        public async Task<Response> GetOperatorChangeUsers(string accessToken, OperatorChangeUserQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.GetOperatorChangeUsers, QueryValues);
        }

        public async Task<Response> GetOperatorChangeInstances(string accessToken, OperatorChangeInstanceQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.GetOperatorChangeInstances, QueryValues);
        }

        public async Task<Response> GetOperatorChangeUserIsInAllInstances(string accessToken, OperatorChangeUserIsInAllInstancesQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.GetOperatorChangeUserIsInAllInstances, QueryValues);
        }

        public async Task<Response> GetOperatorChange(string accessToken, OperatorChangeQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.GetOperatorChange, QueryValues);
        }

        public async Task<Response> GetObservationsByLogAndUser(string accessToken, ObservationGetQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.GetObservations, QueryValues);
        }

        public async Task<Response> AddObservationsByLogAndUser(string accessToken, ObservationAddQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.AddObservation, QueryValues);
        }

        public async Task<Response> ModObservationsByLogAndUser(string accessToken, ObservationModQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.ModObservation, QueryValues);
        }

        public async Task<Response> DelObservationsByLogAndUser(string accessToken, ObservationDelQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.DelObservation, QueryValues);
        }

        public async Task<Response> GetControlSchedulesExecution(string accessToken, ControlSchedulesExecutionQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.ControlSchedulesExecution, QueryValues);
        }

        public async Task<Response> LogInquirieGetLots(string accessToken)
        {
            return await HttpGet(accessToken, this.ApiControllerSet, ApiMethod.LogInquirieGetLots);
        }

        public async Task<Response> LogInquirieGetCommands(string accessToken, LogInquirieGetCommandsQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LogInquirieGetCommands, QueryValues);
        }

        public async Task<Response> LogInquirieGetOperators(string accessToken, LogInquirieGetOperatorsQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LogInquirieGetOperators, QueryValues);
        }

        public async Task<Response> LogInquirieGetLogs(string accessToken, LogInquirieGetLogsQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LogInquirieGetLogs, QueryValues);
        }

        public async Task<Response> DependenciesGetLotsThatDepends(string accessToken, DependenciesGetLotsThatDependsQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.DependenciesGetLotsThatDepends, QueryValues);
        }

        public async Task<Response> DependenciesGetDependentLotDetail(string accessToken, DependenciesGetDependentLotDetailQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.DependenciesGetDependentLotDetail, QueryValues);
        }

        public async Task<Response> DependenciesGetCommandsThatDepends(string accessToken, DependenciesGetCommandsThatDependsQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.DependenciesGetCommandsThatDepends, QueryValues);
        }

        public async Task<Response> DependenciesGetDependentCommandDetail(string accessToken, DependenciesGetDependentCommandDetailQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.DependenciesGetDependentCommandDetail, QueryValues);
        }

        public async Task<Response> BatchQueryGetLots(string accessToken, BatchAllQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.BatchQueryGetLots, QueryValues);
        }

        public async Task<Response> BatchQueryGetParameters(string accessToken, BatchAllQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.BatchQueryGetParameters, QueryValues);
        }

        public async Task<Response> BatchQueryGetCommands(string accessToken, BatchAllQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.BatchQueryGetCommands, QueryValues);
        }

        public async Task<Response> BatchQueryGetCalendars(string accessToken, BatchAllQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.BatchQueryGetCalendars, QueryValues);
        }

        public async Task<Response> LotAndCommandGetTemplates(string accessToken)
        {
            return await HttpGet(accessToken, this.ApiControllerSet, ApiMethod.LotAndCommandGetTemplates);
        }

        public async Task<Response> LotAndCommandGetLogs(string accessToken, LotAndCommandLogQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LotAndCommandGetLogs, QueryValues);
        }

        public async Task<Response> LotAndCommandGetLots(string accessToken, LotAndCommandLotQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LotAndCommandGetLots, QueryValues);
        }

        public async Task<Response> LotAndCommandGetCommands(string accessToken, LotAndCommandCommandQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LotAndCommandGetCommands, QueryValues);
        }

        public async Task<Response> LotAndCommandGetResults(string accessToken, LotAndCommandResultQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LotAndCommandGetResults, QueryValues);
        }

        public async Task<Response> LogExecutionDelayGetTemplates(string accessToken)
        {
            return await HttpGet(accessToken, this.ApiControllerSet, ApiMethod.LogExecutionDelayGetTemplates);
        }

        public async Task<Response> LogExecutionDelayGetEvents(string accessToken)
        {
            return await HttpGet(accessToken, this.ApiControllerSet, ApiMethod.LogExecutionDelayGetEvents);
        }

        public async Task<Response> LogExecutionDelayGetLots(string accessToken, LogExecutionDelayLotQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LogExecutionDelayGetLots, QueryValues);
        }

        public async Task<Response> LogExecutionDelayGetCommands(string accessToken, LogExecutionDelayCommandQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LogExecutionDelayGetCommands, QueryValues);
        }

        public async Task<Response> LogExecutionDelayGetResults(string accessToken, LogExecutionDelayResultQueryValues QueryValues)
        {
            return await HttpPost(accessToken, this.ApiControllerSet, ApiMethod.LogExecutionDelayGetResults, QueryValues);
        }
        #endregion

        #region Helpers
        private async Task<Response> HttpGet(string accessToken, string apiController, string apiMethod)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType.Scheme, accessToken);
                client.BaseAddress = new Uri(this.UrlDomain);
                client.Timeout = TimeSpan.FromSeconds(15);
                var url = string.Format("{0}{1}{2}", this.UrlPrefix, apiController, apiMethod);
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

        private async Task<Response> HttpPost(string accessToken, string apiController, string apiMethod, object queryValues)
        {
            try
            {
                string test = JsonConvert.SerializeObject(queryValues);
                var request = JsonConvert.SerializeObject(new CipherData { Data = Crypto.EncryptString(JsonConvert.SerializeObject(queryValues)) });
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType.Scheme, accessToken);
                client.BaseAddress = new Uri(this.UrlDomain);
                client.Timeout = TimeSpan.FromSeconds(15);
                var url = string.Format("{0}{1}{2}", this.UrlPrefix, apiController, apiMethod);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    var resultError = await response.Content.ReadAsStringAsync();
                    var cipherDataError = JsonConvert.DeserializeObject<CipherData>(resultError);
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Api en error o la misma no está disponible",
                        Data = cipherDataError.Data,
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

        private void GetAppConfig()
        {
            Table_Config table_Config = this.DBHelper.GetAppConfig();
            if (table_Config != null)
            {
                this.UrlDomain = table_Config.UrlDomain;
                this.UrlPrefix = table_Config.UrlPrefix;
            }
        }

        private void SetApiController()
        {
            switch (ApiToConsult)
            {
                case ApiConsult.ApiAuth:
                    this.ApiControllerSet = ApiController.PBAuthAuthentication;
                    break;
                case ApiConsult.ApiMenuA:
                    this.ApiControllerSet = ApiController.PBMenuA;
                    break;
                case ApiConsult.ApiMenuB:
                    this.ApiControllerSet = ApiController.PBMenuB;
                    break;
                case ApiConsult.ApiMenuC:
                    //this.ApiControllerSet = ApiController.PBMenuC;
                    break;
                case ApiConsult.ApiMenuD:
                    //this.ApiControllerSet = ApiController.PBMenuD;
                    break;
            }
        }
        #endregion
    }
}

#region FIXED
//try
//{
//    var request = JsonConvert.SerializeObject(new CipherData { Data = Crypto.EncryptString(JsonConvert.SerializeObject(instanceQueryValues)) });
//    var content = new StringContent(request, Encoding.UTF8, "application/json");
//    var client = new HttpClient();
//    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType.Scheme, accessToken);
//    client.BaseAddress = new Uri(this.UrlDomain);
//    client.Timeout = TimeSpan.FromSeconds(15);
//    var url = string.Format("{0}{1}{2}", this.UrlPrefix, ApiController.PBMenuBExecute, ApiMethod.GetInstancesByLogAndUser);
//    var response = await client.PostAsync(url, content);

//    if (!response.IsSuccessStatusCode)
//    {
//        return new Response
//        {
//            IsSuccess = false,
//            Message = "Api en error o la misma no está disponible",
//            Data = string.Empty,
//        };
//    }

//    var result = await response.Content.ReadAsStringAsync();
//    var cipherData = JsonConvert.DeserializeObject<CipherData>(result);

//    return new Response
//    {
//        IsSuccess = true,
//        Message = "Data Obtenida",
//        Data = cipherData.Data,
//    };
//}
//catch //(Exception ex)
//{
//    return new Response
//    {
//        IsSuccess = false,
//        Message = "Error al intentar consultar la Api",
//        Data = string.Empty,
//    };
//}

//try
//{
//    var client = new HttpClient();
//    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType.Scheme, accessToken);
//    client.BaseAddress = new Uri(this.UrlDomain);
//    client.Timeout = TimeSpan.FromSeconds(15);
//    var url = string.Format("{0}{1}{2}", this.UrlPrefix, ApiController.PBMenuBExecute, ApiMethod.GetLogs);
//    var response = await client.GetAsync(url);

//    if (!response.IsSuccessStatusCode)
//    {
//        return new Response
//        {
//            IsSuccess = false,
//            Message = "Api en error o la misma no está disponible",
//            Data = string.Empty,
//        };
//    }

//    var result = await response.Content.ReadAsStringAsync();
//    var cipherData = JsonConvert.DeserializeObject<CipherData>(result);

//    return new Response
//    {
//        IsSuccess = true,
//        Message = "Data Obtenida",
//        Data = cipherData.Data,
//    };
//}
//catch //(Exception ex)
//{
//    return new Response
//    {
//        IsSuccess = false,
//        Message = "Error al intentar consultar la Api",
//        Data = string.Empty,
//    };
//}

//public async Task<Response> AuthenticateProbath(string accessToken, LoginPb loginPb)
//{
//    try
//    {
//        var request = JsonConvert.SerializeObject(new CipherData { Data = Crypto.EncryptString(JsonConvert.SerializeObject(loginPb)) });
//        var content = new StringContent(request, Encoding.UTF8, "application/json");
//        var client = new HttpClient();
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType.Scheme, accessToken);
//        client.BaseAddress = new Uri(this.UrlDomain);
//        client.Timeout = TimeSpan.FromSeconds(15);
//        var url = string.Format("{0}{1}{2}", this.UrlPrefix, ApiController.PBAuthAuthentication, ApiMethod.Login);
//        var response = await client.PostAsync(url, content);

//        if (!response.IsSuccessStatusCode)
//        {
//            return new Response
//            {
//                IsSuccess = false,
//                Message = "Api en error o la misma no está disponible",
//                Data = string.Empty,
//            };
//        }

//        var result = await response.Content.ReadAsStringAsync();
//        var cipherData = JsonConvert.DeserializeObject<CipherData>(result);

//        return new Response
//        {
//            IsSuccess = true,
//            Message = "Data Obtenida",
//            Data = cipherData.Data,
//        };
//    }
//    catch //(Exception ex)
//    {
//        return new Response
//        {
//            IsSuccess = false,
//            Message = "Error al intentar consultar la Api",
//            Data = string.Empty,
//        };
//    }
//}
#endregion