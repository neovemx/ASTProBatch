using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Models.Service;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using AST_ProBatch_Mobile.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class OperationsLogViewModel : BaseViewModel
    {
        #region Atributes
        private bool logfinished;
        private PickerLogItem logitemselected;
        private PickerOperatorItem operatoritemselected;
        private PickerStatusItem statusitemselected;
        private PickerEnvironmentItem environmentitemselected;
        private PickerServiceItem serviceitemselected;
        private PickerLotItem lotitemselected;
        private PickerCommandItem commanditemselected;
        private ObservableCollection<PickerLogItem> pickerlogitems;
        private ObservableCollection<PickerOperatorItem> pickeroperatoritems;
        private ObservableCollection<PickerStatusItem> pickerstatusitems;
        private ObservableCollection<PickerEnvironmentItem> pickerenvironmentitems;
        private ObservableCollection<PickerServiceItem> pickerserviceitems;
        private ObservableCollection<PickerLotItem> pickerlotitems;
        private ObservableCollection<PickerCommandItem> pickercommanditems;
        private ObservableCollection<OperationsLogReportItem> operationslogreportitems;
        private DatePromptResult startdatefromvalue;
        private DatePromptResult startdatetovalue;
        private DatePromptResult enddatefromvalue;
        private DatePromptResult enddatetovalue;
        private string startdatefromstring;
        private string startdatetostring;
        private string enddatefromstring;
        private string enddatetostring;
        #endregion

        #region Properties
        private Token TokenGet { get; set; }
        private List<OperationsLogLog> OperationsLogLogs { get; set; }
        private List<OperationsLogUser> OperationsLogUsers { get; set; }
        private List<OperationsLogStatus> OperationsLogStatuses { get; set; }
        private List<OperationsLogEnvironment> OperationsLogEnvironments { get; set; }
        private List<OperationsLogService> OperationsLogServices { get; set; }
        private List<OperationsLogLot> OperationsLogLots { get; set; }
        private List<OperationsLogCommand> OperationsLogCommands { get; set; }
        private List<OperationsLogResult> OperationsLogResults { get; set; }
        public bool LogFinished
        {
            get { return logfinished; }
            set
            {
                SetValue(ref logfinished, value);
                LogFinishedCheckedChange();
            }
        }
        public ObservableCollection<PickerLogItem> PickerLogItems
        {
            get { return pickerlogitems; }
            set { SetValue(ref pickerlogitems, value); }
        }
        public ObservableCollection<PickerOperatorItem> PickerOperatorItems
        {
            get { return pickeroperatoritems; }
            set { SetValue(ref pickeroperatoritems, value); }
        }
        public ObservableCollection<PickerStatusItem> PickerStatusItems
        {
            get { return pickerstatusitems; }
            set { SetValue(ref pickerstatusitems, value); }
        }
        public ObservableCollection<PickerEnvironmentItem> PickerEnvironmentItems
        {
            get { return pickerenvironmentitems; }
            set { SetValue(ref pickerenvironmentitems, value); }
        }
        public ObservableCollection<PickerServiceItem> PickerServiceItems
        {
            get { return pickerserviceitems; }
            set { SetValue(ref pickerserviceitems, value); }
        }
        public ObservableCollection<PickerLotItem> PickerLotItems
        {
            get { return pickerlotitems; }
            set { SetValue(ref pickerlotitems, value); }
        }
        public ObservableCollection<PickerCommandItem> PickerCommandItems
        {
            get { return pickercommanditems; }
            set { SetValue(ref pickercommanditems, value); }
        }
        public ObservableCollection<OperationsLogReportItem> OperationsLogReportItems
        {
            get { return operationslogreportitems; }
            set { SetValue(ref operationslogreportitems, value); }
        }
        public PickerLogItem LogItemSelected
        {
            get { return logitemselected; }
            set
            {
                SetValue(ref logitemselected, value);
                //LogSelectedItemChange();
            }
        }
        public PickerOperatorItem OperatorItemSelected
        {
            get { return operatoritemselected; }
            set { SetValue(ref operatoritemselected, value); }
        }
        public PickerStatusItem StatusItemSelected
        {
            get { return statusitemselected; }
            set { SetValue(ref statusitemselected, value); }
        }
        public PickerEnvironmentItem EnvironmentItemSelected
        {
            get { return environmentitemselected; }
            set { SetValue(ref environmentitemselected, value); }
        }
        public PickerServiceItem ServiceItemSelected
        {
            get { return serviceitemselected; }
            set { SetValue(ref serviceitemselected, value); }
        }
        public PickerLotItem LotItemSelected
        {
            get { return lotitemselected; }
            set
            {
                SetValue(ref lotitemselected, value);
                SelectedLotItemChange();
            }
        }
        public PickerCommandItem CommandItemSelected
        {
            get { return commanditemselected; }
            set { SetValue(ref commanditemselected, value); }
        }
        public DatePromptResult StartDateFromValue
        {
            get { return startdatefromvalue; }
            set { SetValue(ref startdatefromvalue, value); }
        }
        public DatePromptResult StartDateToValue
        {
            get { return startdatetovalue; }
            set { SetValue(ref startdatetovalue, value); }
        }
        public DatePromptResult EndDateFromValue
        {
            get { return enddatefromvalue; }
            set { SetValue(ref enddatefromvalue, value); }
        }
        public DatePromptResult EndDateToValue
        {
            get { return enddatetovalue; }
            set { SetValue(ref enddatetovalue, value); }
        }

        public string StartDateFromString
        {
            get { return startdatefromstring; }
            set { SetValue(ref startdatefromstring, value); }
        }
        public string StartDateToString
        {
            get { return startdatetostring; }
            set { SetValue(ref startdatetostring, value); }
        }
        public string EndDateFromString
        {
            get { return enddatefromstring; }
            set { SetValue(ref enddatefromstring, value); }
        }
        public string EndDateToString
        {
            get { return enddatetostring; }
            set { SetValue(ref enddatetostring, value); }
        }
        #endregion

        #region Constructors
        public OperationsLogViewModel(bool IsReload)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuC);

                this.StartDateFromString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);
                this.StartDateToString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);
                this.EndDateFromString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);
                this.EndDateToString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);

                GetInitialData();
            }
        }
        #endregion

        #region Commands
        public ICommand ClearCommand
        {
            get
            {
                return new RelayCommand(Clear);
            }
        }

        private async void Clear()
        {
            this.LogItemSelected = new PickerLogItem();
            this.OperatorItemSelected = new PickerOperatorItem();
            this.StatusItemSelected = new PickerStatusItem();
            this.EnvironmentItemSelected = new PickerEnvironmentItem();
            this.ServiceItemSelected = new PickerServiceItem();
            this.LotItemSelected = new PickerLotItem();
            this.CommandItemSelected = new PickerCommandItem();
            this.StartDateFromString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);
            this.StartDateToString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);
            this.EndDateFromString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);
            this.EndDateToString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);
            this.StartDateFromValue = null;
            this.StartDateToValue = null;
            this.EndDateFromValue = null;
            this.EndDateToValue = null;
        }

        public ICommand ReportCommand
        {
            get
            {
                return new RelayCommand(Report);
            }
        }

        private async void Report()
        {
            if (this.LogItemSelected == null)
            {
                Alert.Show("Debe seleccionar una bitácora!");
                return;
            }

            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo datos del reporte...", MaskType.Black);
                if (!await ApiIsOnline())
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(AlertMessages.Error);
                    return;
                }
                else
                {
                    if (!TokenValidator.IsValid(TokenGet))
                    {
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
                        }
                    }
                    OperationsLogResultQueryValues operationsLogResultQueryValues = new OperationsLogResultQueryValues()
                    {
                        IdLog = this.LogItemSelected.IdLog,
                        IdUser = (this.OperatorItemSelected != null ? this.OperatorItemSelected.IdUser : ""),
                        IdStatus = (this.StatusItemSelected != null ? this.StatusItemSelected.IdStatus : ""),
                        IdEnvironment = (this.EnvironmentItemSelected != null ? this.EnvironmentItemSelected.IdEnvironment : 0),
                        IdService = (this.ServiceItemSelected != null ? this.ServiceItemSelected.IdService : 0),
                        IdLot = (this.LotItemSelected != null ? this.LotItemSelected.IdLot : 0),
                        IdCommand = (this.CommandItemSelected != null ? this.CommandItemSelected.IdCommand : 0),
                        StartDateFrom = (this.StartDateFromValue == null ? DateTime.Now : this.StartDateFromValue.SelectedDate),
                        StartDateTo = (this.StartDateToValue == null ? DateTime.Now : this.StartDateToValue.SelectedDate),
                        EndDateFrom = (this.EndDateFromValue == null ? DateTime.Now : this.EndDateFromValue.SelectedDate),
                        EndDateTo = (this.EndDateToValue == null ? DateTime.Now : this.EndDateToValue.SelectedDate),
                    };
                    Response resultGetResults = await ApiSrv.OperationsLogGetResults(TokenGet.Key, operationsLogResultQueryValues);
                    if (!resultGetResults.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        OperationsLogResults = JsonConvert.DeserializeObject<List<OperationsLogResult>>(Crypto.DecodeString(resultGetResults.Data));
                        if (OperationsLogResults.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();
                            Alert.Show("No hay datos para mostrar!");
                            return;
                        }
                        if (OperationsLogReportItems == null)
                        {
                            OperationsLogReportItems = new ObservableCollection<OperationsLogReportItem>();
                        }
                        else
                        {
                            OperationsLogReportItems.Clear();
                        }
                        foreach (OperationsLogResult item in OperationsLogResults)
                        {
                            OperationsLogReportItems.Add(new OperationsLogReportItem()
                            {
                                IdLog = item.IdLog,
                                IdInstance = item.IdInstance,
                                IdLot = item.IdLot,
                                IdCommand = item.IdCommand,
                                IdUser = item.IdUser,
                                Environment = item.Environment,
                                Service = item.Service,
                                CodeCommand = item.CodeCommand,
                                NameCommand = item.NameCommand,
                                Product = item.Product,
                                StartDate = (item.StartDate != null) ? ((DateTime)item.StartDate) : new DateTime(),
                                EndDate = (item.EndDate != null) ? ((DateTime)item.EndDate) : new DateTime(),
                                Duration = item.Duration,
                                Status = item.Status,
                                IPClient = item.IPClient,
                                NameLog = item.NameLog,
                                InstanceNumber = item.InstanceNumber,
                                CodeLot = item.CodeLot,
                                NameLot = item.NameLot,
                                Source = item.Source,
                                Critical = item.Critical,
                                StartDateString = (item.StartDate != null) ? ((DateTime)item.StartDate).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                EndDateString = (item.EndDate != null) ? ((DateTime)item.EndDate).ToString(DateTimeFormatString.LatinDate24Hours) : ""
                            });
                        }
                        UserDialogs.Instance.HideLoading();
                        MainViewModel.GetInstance().OperationsLogReport = new OperationsLogReportViewModel(true, OperationsLogReportItems);
                        await Application.Current.MainPage.Navigation.PushModalAsync(new OperationsLogReportPage());
                    }
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError("Ocurrió un error.");
            }
        }

        public ICommand StartDateFromCommand
        {
            get
            {
                return new RelayCommand(StartDateFrom);
            }
        }

        private async void StartDateFrom()
        {
            DatePromptConfig datePromptConfig = new DatePromptConfig();
            datePromptConfig.MaximumDate = DateTime.Now;
            datePromptConfig.CancelText = "Cancelar";
            datePromptConfig.OkText = "Aceptar";
            this.StartDateFromValue = await UserDialogs.Instance.DatePromptAsync(datePromptConfig);
            this.StartDateFromString = this.StartDateFromValue.SelectedDate.ToString(DateTimeFormatString.LatinDate);
        }

        public ICommand StartDateToCommand
        {
            get
            {
                return new RelayCommand(StartDateTo);
            }
        }

        private async void StartDateTo()
        {
            DatePromptConfig datePromptConfig = new DatePromptConfig();
            datePromptConfig.MaximumDate = DateTime.Now;
            datePromptConfig.CancelText = "Cancelar";
            datePromptConfig.OkText = "Aceptar";
            this.StartDateToValue = await UserDialogs.Instance.DatePromptAsync(datePromptConfig);
            this.StartDateToString = this.StartDateToValue.SelectedDate.ToString(DateTimeFormatString.LatinDate);
        }

        public ICommand EndDateFromCommand
        {
            get
            {
                return new RelayCommand(EndDateFrom);
            }
        }

        private async void EndDateFrom()
        {
            DatePromptConfig datePromptConfig = new DatePromptConfig();
            datePromptConfig.MaximumDate = DateTime.Now;
            datePromptConfig.CancelText = "Cancelar";
            datePromptConfig.OkText = "Aceptar";
            this.EndDateFromValue = await UserDialogs.Instance.DatePromptAsync(datePromptConfig);
            this.EndDateFromString = this.EndDateFromValue.SelectedDate.ToString(DateTimeFormatString.LatinDate);
        }

        public ICommand EndDateToCommand
        {
            get
            {
                return new RelayCommand(EndDateTo);
            }
        }

        private async void EndDateTo()
        {
            DatePromptConfig datePromptConfig = new DatePromptConfig();
            datePromptConfig.MaximumDate = DateTime.Now;
            datePromptConfig.CancelText = "Cancelar";
            datePromptConfig.OkText = "Aceptar";
            this.EndDateToValue = await UserDialogs.Instance.DatePromptAsync(datePromptConfig);
            this.EndDateToString = this.EndDateToValue.SelectedDate.ToString(DateTimeFormatString.LatinDate);
        }
        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo datos iniciales...", MaskType.Black);
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
                    else
                    {
                        OperationsLogLogQueryValues operationsLogLogQueryValues = new OperationsLogLogQueryValues()
                        {
                            Finished = false
                        };
                        Response resultLogs = await ApiSrv.OperationsLogGetLogs(TokenGet.Key, operationsLogLogQueryValues);
                        if (!resultLogs.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            OperationsLogLogs = JsonConvert.DeserializeObject<List<OperationsLogLog>>(Crypto.DecodeString(resultLogs.Data));
                            PickerLogItems = new ObservableCollection<PickerLogItem>();
                            foreach (OperationsLogLog item in OperationsLogLogs)
                            {
                                PickerLogItems.Add(new PickerLogItem()
                                {
                                    IdLog = item.IdLog,
                                    NameDisplay = item.NameLog

                                });
                            }
                        }

                        Response resultUsers = await ApiSrv.OperationsLogGetUsers(TokenGet.Key);
                        if (!resultUsers.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            OperationsLogUsers = JsonConvert.DeserializeObject<List<OperationsLogUser>>(Crypto.DecodeString(resultUsers.Data));
                            PickerOperatorItems = new ObservableCollection<PickerOperatorItem>();
                            foreach (OperationsLogUser item in OperationsLogUsers)
                            {
                                PickerOperatorItems.Add(new PickerOperatorItem()
                                {
                                    IdUser = item.IdUser,
                                    FullNameUser = string.Format("{0}, {1}", item.FisrtName.Trim(), item.LastName.Trim())

                                });
                            }
                        }

                        Response resultStatuses = await ApiSrv.OperationsLogGetStatuses(TokenGet.Key);
                        if (!resultStatuses.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            OperationsLogStatuses = JsonConvert.DeserializeObject<List<OperationsLogStatus>>(Crypto.DecodeString(resultStatuses.Data));
                            PickerStatusItems = new ObservableCollection<PickerStatusItem>();
                            foreach (OperationsLogStatus item in OperationsLogStatuses)
                            {
                                PickerStatusItems.Add(new PickerStatusItem()
                                {
                                    IdStatus = item.IdStatus,
                                    StatusName = item.Status
                                });
                            }
                        }

                        Response resultEnvironments = await ApiSrv.OperationsLogGetEnvironments(TokenGet.Key);
                        if (!resultEnvironments.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            OperationsLogEnvironments = JsonConvert.DeserializeObject<List<OperationsLogEnvironment>>(Crypto.DecodeString(resultEnvironments.Data));
                            PickerEnvironmentItems = new ObservableCollection<PickerEnvironmentItem>();
                            foreach (OperationsLogEnvironment item in OperationsLogEnvironments)
                            {
                                PickerEnvironmentItems.Add(new PickerEnvironmentItem()
                                {
                                    IdEnvironment = item.IdEnvironment,
                                    Environment = item.Environment
                                });
                            }
                        }

                        Response resultServices = await ApiSrv.OperationsLogGetServices(TokenGet.Key);
                        if (!resultServices.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            OperationsLogServices = JsonConvert.DeserializeObject<List<OperationsLogService>>(Crypto.DecodeString(resultServices.Data));
                            PickerServiceItems = new ObservableCollection<PickerServiceItem>();
                            foreach (OperationsLogService item in OperationsLogServices)
                            {
                                PickerServiceItems.Add(new PickerServiceItem()
                                {
                                    IdService = item.IdService,
                                    Service = item.Service
                                });
                            }
                        }

                        Response resultLots = await ApiSrv.OperationsLogGetLots(TokenGet.Key);
                        if (!resultLots.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            OperationsLogLots = JsonConvert.DeserializeObject<List<OperationsLogLot>>(Crypto.DecodeString(resultLots.Data));
                            PickerLotItems = new ObservableCollection<PickerLotItem>();
                            foreach (OperationsLogLot item in OperationsLogLots)
                            {
                                PickerLotItems.Add(new PickerLotItem()
                                {
                                    IdLot = item.IdLot,
                                    NameLot = item.Lot
                                });
                            }
                        }

                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError("Ocurrió un error.");
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

        private async void SelectedLotItemChange()
        {
            try
            {
                if (this.LotItemSelected == null || this.LotItemSelected.IdLot == 0)
                {
                    if (PickerCommandItems != null)
                    {
                        PickerCommandItems.Clear();
                    }
                    return;
                }

                UserDialogs.Instance.ShowLoading("Obteniendo comandos...", MaskType.Black);
                if (!await ApiIsOnline())
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(AlertMessages.Error);
                    return;
                }
                else
                {
                    if (!TokenValidator.IsValid(TokenGet))
                    {
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
                        }
                    }
                    OperationsLogCommandQueryValues operationsLogCommandQueryValues = new OperationsLogCommandQueryValues()
                    {
                        IdLot = this.LotItemSelected.IdLot
                    };
                    Response resultGetCommands = await ApiSrv.OperationsLogGetCommands(TokenGet.Key, operationsLogCommandQueryValues);
                    if (!resultGetCommands.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        OperationsLogCommands = JsonConvert.DeserializeObject<List<OperationsLogCommand>>(Crypto.DecodeString(resultGetCommands.Data));
                        if (PickerCommandItems == null)
                        {
                            PickerCommandItems = new ObservableCollection<PickerCommandItem>();
                        }
                        else
                        {
                            PickerCommandItems.Clear();
                        }
                        foreach (OperationsLogCommand item in OperationsLogCommands)
                        {
                            PickerCommandItems.Add(new PickerCommandItem()
                            {
                                IdCommand = item.IdCommand,
                                NameCommand = item.Command
                            });
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        private async void LogFinishedCheckedChange()
        {
            try
            {
                if (!this.LogFinished)
                {
                    UserDialogs.Instance.ShowLoading("Obteniendo bitácoras...", MaskType.Black);
                    if (!await ApiIsOnline())
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        if (!TokenValidator.IsValid(TokenGet))
                        {
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
                            }
                        }
                        OperationsLogLogQueryValues operationsLogLogQueryValues = new OperationsLogLogQueryValues()
                        {
                            Finished = false
                        };
                        Response resultLogs = await ApiSrv.OperationsLogGetLogs(TokenGet.Key, operationsLogLogQueryValues);
                        if (!resultLogs.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            OperationsLogLogs = JsonConvert.DeserializeObject<List<OperationsLogLog>>(Crypto.DecodeString(resultLogs.Data));
                            if (PickerLogItems == null)
                            {
                                PickerLogItems = new ObservableCollection<PickerLogItem>();
                            }
                            else
                            {
                                PickerLogItems.Clear();
                            }

                            foreach (OperationsLogLog item in OperationsLogLogs)
                            {
                                PickerLogItems.Add(new PickerLogItem()
                                {
                                    IdLog = item.IdLog,
                                    NameDisplay = item.NameLog

                                });
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                    }
                }
                else
                {
                    UserDialogs.Instance.ShowLoading("Obteniendo bitácoras...", MaskType.Black);
                    if (!await ApiIsOnline())
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        if (!TokenValidator.IsValid(TokenGet))
                        {
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
                            }
                        }
                        OperationsLogLogQueryValues operationsLogLogQueryValues = new OperationsLogLogQueryValues()
                        {
                            Finished = true
                        };
                        Response resultLogs = await ApiSrv.OperationsLogGetLogs(TokenGet.Key, operationsLogLogQueryValues);
                        if (!resultLogs.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            OperationsLogLogs = JsonConvert.DeserializeObject<List<OperationsLogLog>>(Crypto.DecodeString(resultLogs.Data));
                            if (PickerLogItems == null)
                            {
                                PickerLogItems = new ObservableCollection<PickerLogItem>();
                            }
                            else
                            {
                                PickerLogItems.Clear();
                            }

                            foreach (OperationsLogLog item in OperationsLogLogs)
                            {
                                PickerLogItems.Add(new PickerLogItem()
                                {
                                    IdLog = item.IdLog,
                                    NameDisplay = item.NameLog

                                });
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                    }
                }
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
