using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Models.Service;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using Newtonsoft.Json;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<ExecutionResultItem> resultitems;
        private ObservableCollection<ExecutionResultErrorLogItem> logerroritems;
        private List<ExecutionResult> ExecutionResults;
        private List<ExecutionResultErrorLog> ExecutionResultErrorLogs;
        private string resuldetail;
        private string errordetail;
        private ExecutionResultItem resultselected;
        private ExecutionResultErrorLogItem errorselected;
        #endregion

        #region Properties
        private CommandItem CommandItem { get; set; }
        private Token TokenGet { get; set; }
        public ObservableCollection<ExecutionResultItem> ResultItems
        {
            get { return resultitems; }
            set { SetValue(ref resultitems, value); }
        }
        public ObservableCollection<ExecutionResultErrorLogItem> LogErrorItems
        {
            get { return logerroritems; }
            set { SetValue(ref logerroritems, value); }
        }
        public string ResultDetail
        {
            get { return resuldetail; }
            set { SetValue(ref resuldetail, value); }
        }
        public string ErrorDetail
        {
            get { return errordetail; }
            set { SetValue(ref errordetail, value); }
        }
        public ExecutionResultItem ResultSelected
        {
            get { return resultselected; }
            set
            {
                SetValue(ref resultselected, value);
                HandleResultSelected();
            }
        }
        public ExecutionResultErrorLogItem ErrorSelected
        {
            get { return errorselected; }
            set
            {
                SetValue(ref errorselected, value);
                HandleErrorSelected();
            }
        }
        #endregion

        #region Constructor
        public ResultViewModel(bool IsReload, CommandItem commandItem)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                this.CommandItem = commandItem;

                GetInitialData();
            }
        }
        #endregion

        #region Commands

        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo datos...", MaskType.Black);
                if (!await ApiIsOnline())
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(AlertMessages.Error);
                    return;
                }
                else
                {
                    if (!await GetTokenSuccess())
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    ExecutionResultQueryValues executionResultQueryValues = new ExecutionResultQueryValues()
                    {
                        IdInstance = this.CommandItem.InstanceItem.IdInstance,
                        IdLot = this.CommandItem.IdLot,
                        IdCommand = this.CommandItem.IdCommand
                    };
                    Response resultExecutionResult = await ApiSrv.ExecutionResultGet(TokenGet.Key, executionResultQueryValues);
                    if (!resultExecutionResult.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        ExecutionResults = JsonConvert.DeserializeObject<List<ExecutionResult>>(Crypto.DecodeString(resultExecutionResult.Data));
                        if (ExecutionResults.Count > 0)
                        {
                            ResultItems = new ObservableCollection<ExecutionResultItem>();
                            ExecutionResultItem executionResultItem;
                            foreach (ExecutionResult item in ExecutionResults)
                            {
                                executionResultItem = new ExecutionResultItem()
                                {
                                    Id = item.Id,
                                    Result = item.Result,
                                    StartDate = item.StartDate,
                                    EndDate = item.EndDate,
                                    ResultName = string.Format("Resultado: {0} - {1}", item.Id.ToString(), item.StartDate.ToString(DateTimeFormatString.AmericanDate))
                                };
                                ResultItems.Add(executionResultItem);
                            }
                        }
                    }
                    Response resultExecutionErrorLogResult = await ApiSrv.ExecutionResultErrorLogGet(TokenGet.Key, executionResultQueryValues);
                    if (!resultExecutionErrorLogResult.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        ExecutionResultErrorLogs = JsonConvert.DeserializeObject<List<ExecutionResultErrorLog>>(Crypto.DecodeString(resultExecutionErrorLogResult.Data));
                        if (ExecutionResultErrorLogs.Count > 0)
                        {
                            LogErrorItems = new ObservableCollection<ExecutionResultErrorLogItem>();
                            ExecutionResultErrorLogItem executionResultErrorLogItem;
                            foreach (ExecutionResultErrorLog item in ExecutionResultErrorLogs)
                            {
                                executionResultErrorLogItem = new ExecutionResultErrorLogItem()
                                {
                                    Id = item.Id,
                                    Error = item.Error,
                                    Date = item.Date,
                                    ErrorName = string.Format("Error: {0} - {1}", item.Id.ToString(), item.Date.ToString(DateTimeFormatString.AmericanDate))
                                };
                                LogErrorItems.Add(executionResultErrorLogItem);
                            }
                        }
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        private async Task<bool> ApiIsOnline()
        {
            bool result = false;
            Response resultApiIsAvailable = await ApiSrv.ApiIsAvailable();
            if (resultApiIsAvailable.IsSuccess)
            {
                result = true;
            }
            return result;
        }

        private async Task<bool> GetTokenSuccess()
        {
            bool result = false;
            Response resultToken = await ApiSrv.GetToken();
            if (resultToken.IsSuccess)
            {
                TokenGet = JsonConvert.DeserializeObject<Token>(Crypto.DecodeString(resultToken.Data));
                result = true;
            }
            return result;
        }

        private async void HandleResultSelected()
        {
            try
            {
                this.ResultDetail = this.ResultSelected.Result;
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        private async void HandleErrorSelected()
        {
            try
            {
                this.ErrorDetail = this.ErrorSelected.Error;
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }
        #endregion
    }
}
