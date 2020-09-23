using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Models.Service;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LogInquiriesViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<PickerLotItem> lotitems;
        private ObservableCollection<PickerCommandItem> commanditems;
        private ObservableCollection<ResultLogInquirieItem> resultloginquirieitems;
        private ObservableCollection<LogInquirieOperatorItem> loginquirieoperatoritems;
        private LogItem logitem;
        private PickerLotItem lotselected;
        private PickerCommandItem commandselected;
        private bool resultisvisible;
        private bool formisvisible;
        private string namelot;
        private string namecommand;
        private int lotselectedindex;
        private int commandselectedindex;
        private string lotid;
        private string commandid;
        #endregion

        #region Properties
        private List<LogInquiriesLot> Lots { get; set; }
        private List<LogInquiriesCommand> Commands { get; set; }
        private List<LogInquiriesOperator> Operators { get; set; }
        private List<LogInquiriesResult> Logs { get; set; }
        private Token TokenGet { get; set; }
        private Token TokenMenuB { get; set; }

        public ObservableCollection<PickerLotItem> LotItems
        {
            get { return lotitems; }
            set { SetValue(ref lotitems, value); }
        }
        public PickerLotItem LotSelected
        {
            get { return lotselected; }
            set
            {
                SetValue(ref lotselected, value);
                LotSelectedItemChange();
            }
        }
        public ObservableCollection<PickerCommandItem> CommandItems
        {
            get { return commanditems; }
            set { SetValue(ref commanditems, value); }
        }
        public PickerCommandItem CommandSelected
        {
            get { return commandselected; }
            set
            {
                SetValue(ref commandselected, value);
                CommandSelectedItemChange();
            }
        }
        public ObservableCollection<ResultLogInquirieItem> ResultLogInquirieItems
        {
            get { return resultloginquirieitems; }
            set { SetValue(ref resultloginquirieitems, value); }
        }
        public LogItem LogItem
        {
            get { return logitem; }
            set { SetValue(ref logitem, value); }
        }
        public bool ResultIsVisible
        {
            get { return resultisvisible; }
            set { SetValue(ref resultisvisible, value); }
        }
        public bool FormIsVisible
        {
            get { return formisvisible; }
            set { SetValue(ref formisvisible, value); }
        }
        public ObservableCollection<LogInquirieOperatorItem> LogInquirieOperatorItems
        {
            get { return loginquirieoperatoritems; }
            set { SetValue(ref loginquirieoperatoritems, value); }
        }
        public string NameLot
        {
            get { return namelot; }
            set { SetValue(ref namelot, value); }
        }
        public string NameCommand
        {
            get { return namecommand; }
            set { SetValue(ref namecommand, value); }
        }
        public int LotSelectedIndex
        {
            get { return lotselectedindex; }
            set { SetValue(ref lotselectedindex, value); }
        }
        public int CommandSelectedIndex
        {
            get { return commandselectedindex; }
            set { SetValue(ref commandselectedindex, value); }
        }
        public string LotId
        {
            get { return lotid; }
            set { SetValue(ref lotid, value); }
        }
        public string CommandId
        {
            get { return commandid; }
            set { SetValue(ref commandid, value); }
        }
        #endregion

        #region Constructors
        public LogInquiriesViewModel(bool IsReload, LogItem logitem)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);

                this.LogItem = logitem;;
                this.NameLot = "Seleccione un lote...";
                this.NameCommand = "Seleccione un comando...";
                this.ResultIsVisible = false;
                this.FormIsVisible = true;
                this.LotSelectedIndex = -1;
                this.CommandSelectedIndex = -1;

                GetInitialData();
                //GetFakeData();
            }
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private async void Search()
        {
            try
            {
                if (this.LotSelected == null)
                {
                    Alert.Show("Debe seleccionar por lo menos un lote.");
                    return;
                }
                UserDialogs.Instance.ShowLoading("Obteniendo bitácoras...", MaskType.Black);

                if (!await ApiIsOnline())
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(AlertMessages.Error);
                    return;
                }
                else
                {
                    if (!TokenValidator.IsValid(TokenMenuB))
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
                            else
                            {
                                TokenMenuB = TokenGet;
                            }
                        }
                    }
                    LogInquirieGetLogsQueryValues logInquirieGetLogsQueryValues = new LogInquirieGetLogsQueryValues()
                    {
                        IdLog = this.LogItem.IdLog,
                        IdLot = this.LotSelected.IdLot,
                        IdCommand = (this.CommandSelected != null) ? this.CommandSelected.IdCommand : -1
                    };
                    Response resultGetLogs = await ApiSrv.LogInquirieGetLogs(TokenMenuB.Key, logInquirieGetLogsQueryValues);
                    if (!resultGetLogs.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        Logs = JsonConvert.DeserializeObject<List<LogInquiriesResult>>(Crypto.DecodeString(resultGetLogs.Data));
                        if (ResultLogInquirieItems == null)
                        {
                            ResultLogInquirieItems = new ObservableCollection<ResultLogInquirieItem>();
                        }
                        else
                        {
                            ResultLogInquirieItems.Clear();
                        }
                        foreach (LogInquiriesResult logInquiriesResult in Logs)
                        {
                            ResultLogInquirieItems.Add(new ResultLogInquirieItem()
                            {
                                IdLog = this.LogItem.IdLog,
                                NameLog = this.LogItem.NameLog,
                                NameInstance = logInquiriesResult.NameInstance,
                                StartHour = (logInquiriesResult.StartTime != null) ? ((DateTime)logInquiriesResult.StartTime).ToString(DateTimeFormatString.Time24Hour) : "",
                                Pause = (logInquiriesResult.Pause) ? "SI" : "NO",
                                IdLot = logInquiriesResult.IdLot,
                                NameLot = logInquiriesResult.Lot,
                                IdCommand = logInquiriesResult.IdCommand,
                                NameCommand = logInquiriesResult.Command,
                                Status = logInquiriesResult.Status,
                                StatusColor = GetStatusColor.ByIdStatus(logInquiriesResult.IdStatus),
                                StatusResult = logInquiriesResult.Result,
                                StartDateTime = (logInquiriesResult.DateStart != null) ? ((DateTime)logInquiriesResult.DateStart).ToString(DateTimeFormatString.LatinDate) : "",
                                EndDateTime = (logInquiriesResult.DateEnd != null) ? ((DateTime)logInquiriesResult.StartTime).ToString(DateTimeFormatString.LatinDate) : ""
                            });
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                }

                this.ResultIsVisible = true;
                UserDialogs.Instance.HideLoading();
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        public ICommand LotSelectCommand
        {
            get
            {
                return new RelayCommand(LotSelect);
            }
        }

        private async void LotSelect()
        {
            try
            {
                if (string.IsNullOrEmpty(this.LotId))
                {
                    Alert.Show("Debe ingresar un códgio de lote.");
                    return;
                }
                foreach (PickerLotItem pickerLotItem in this.LotItems)
                {
                    if (string.CompareOrdinal(this.LotId, pickerLotItem.CodeLot) == 0)
                    {
                        this.LotSelectedIndex = this.LotItems.IndexOf(pickerLotItem);
                        return;
                    }
                }
                Alert.Show("Codigo de lote no encontrado.");
                this.LotId = string.Empty;
                this.CommandId = string.Empty;
                this.LotSelectedIndex = -1;
                this.CommandSelectedIndex = -1;
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        public ICommand CommandSelectCommand
        {
            get
            {
                return new RelayCommand(CommandSelect);
            }
        }

        private async void CommandSelect()
        {
            try
            {
                if (string.IsNullOrEmpty(this.CommandId))
                {
                    Alert.Show("Debe ingresar un códgio de comando.");
                    return;
                }
                foreach (PickerCommandItem pickerCommandItem in this.CommandItems)
                {
                    if (string.CompareOrdinal(this.CommandId, pickerCommandItem.CodeCommand) == 0)
                    {
                        this.CommandSelectedIndex = this.CommandItems.IndexOf(pickerCommandItem);
                        return;
                    }
                }
                Alert.Show("Codigo de comando no encontrado.");
                this.CommandId = string.Empty;
                this.CommandSelectedIndex = -1;
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        public ICommand LotCleanCommand
        {
            get
            {
                return new RelayCommand(LotClean);
            }
        }

        private async void LotClean()
        {
            try
            {
                this.LotId = string.Empty;
                this.LotSelectedIndex = -1;
                this.CommandId = string.Empty;
                this.CommandSelectedIndex = -1;
                if (this.CommandItems != null)
                {
                    this.CommandItems.Clear();
                }
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        public ICommand CommandCleanCommand
        {
            get
            {
                return new RelayCommand(CommandClean);
            }
        }

        private async void CommandClean()
        {
            try
            {
                this.CommandId = string.Empty;
                this.CommandSelectedIndex = -1;
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }
        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo lotes...", MaskType.Black);
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
                        TokenMenuB = TokenGet;
                        Response resultGetLots = await ApiSrv.LogInquirieGetLots(TokenMenuB.Key);
                        if (!resultGetLots.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            Lots = JsonConvert.DeserializeObject<List<LogInquiriesLot>>(Crypto.DecodeString(resultGetLots.Data));
                            LotItems = new ObservableCollection<PickerLotItem>();
                            int ContItems = 1;
                            foreach (LogInquiriesLot logInquiriesLot in Lots)
                            {
                                LotItems.Add(new PickerLotItem()
                                {
                                    IdLot = logInquiriesLot.IdLot,
                                    NameLot = logInquiriesLot.Lot.Split('-')[1].Trim(),
                                    NameDisplay = string.Format("{0} - Lote {1}", logInquiriesLot.Lot.Split('-')[0].Trim(), ContItems),
                                    CodeLot = logInquiriesLot.Lot.Split('-')[0].Trim()
                                });
                                ContItems += 1;
                            }
                            UserDialogs.Instance.HideLoading();
                            UserDialogs.Instance.ShowLoading("Obteniendo operadores...", MaskType.Black);
                            LogInquirieGetOperatorsQueryValues logInquirieGetOperatorsQueryValues = new LogInquirieGetOperatorsQueryValues()
                            {
                                 IdLog = this.LogItem.IdLog
                            };
                            Response resultGetOperators = await ApiSrv.LogInquirieGetOperators(TokenMenuB.Key, logInquirieGetOperatorsQueryValues);
                            if (!resultGetOperators.IsSuccess)
                            {
                                UserDialogs.Instance.HideLoading();
                                Toast.ShowError(AlertMessages.Error);
                                return;
                            }
                            else
                            {
                                Operators = JsonConvert.DeserializeObject<List<LogInquiriesOperator>>(Crypto.DecodeString(resultGetOperators.Data));
                                LogInquirieOperatorItems = new ObservableCollection<LogInquirieOperatorItem>();
                                foreach (LogInquiriesOperator logInquiriesOperator in Operators)
                                {
                                    LogInquirieOperatorItems.Add(new LogInquirieOperatorItem()
                                    {
                                        NameInstance = logInquiriesOperator.NameInstance,
                                        Number = logInquiriesOperator.Number,
                                        NameOperator = logInquiriesOperator.Operator
                                    });
                                }
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

        private async void LotSelectedItemChange()
        {
            try
            {
                if (this.LotSelected == null)
                {
                    this.NameLot = "Seleccione un lote...";
                    return;
                }
                this.NameLot = this.LotSelected.NameLot;
                UserDialogs.Instance.ShowLoading("Obteniendo comandos...", MaskType.Black);
                ApiSrv = new Services.ApiService(ApiConsult.ApiAuth);
                if (!await ApiIsOnline())
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(AlertMessages.Error);
                    return;
                }
                else
                {
                    if (!TokenValidator.IsValid(TokenMenuB))
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
                            else
                            {
                                TokenMenuB = TokenGet;
                            }
                        }
                    }
                    LogInquirieGetCommandsQueryValues logInquirieGetCommandsQueryValues = new LogInquirieGetCommandsQueryValues()
                    {
                        IdLot = this.LotSelected.IdLot
                    };
                    Response resultGetCommands = await ApiSrv.LogInquirieGetCommands(TokenMenuB.Key, logInquirieGetCommandsQueryValues);
                    if (!resultGetCommands.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        Commands = JsonConvert.DeserializeObject<List<LogInquiriesCommand>>(Crypto.DecodeString(resultGetCommands.Data));
                        if (CommandItems == null)
                        {
                            CommandItems = new ObservableCollection<PickerCommandItem>();
                        }
                        else
                        {
                            CommandItems.Clear();
                        }
                        int ContItems = 1;
                        foreach (LogInquiriesCommand logInquiriesCommand in Commands)
                        {
                            CommandItems.Add(new PickerCommandItem()
                            {
                                IdCommand = logInquiriesCommand.IdCommand,
                                NameCommand = logInquiriesCommand.Command.Split('-')[1].Trim(),
                                NameDisplay = string.Format("{0} - Comando {1}", logInquiriesCommand.Command.Split('-')[0].Trim(), ContItems),
                                CodeCommand = logInquiriesCommand.Command.Split('-')[0].Trim()
                            });
                            ContItems += 1;
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                }
                //Alert.Show(this.LotSelected.NameLot.ToString());
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        private void CommandSelectedItemChange()
        {
            if (this.CommandSelected == null)
            {
                this.NameCommand = "Seleccione un comando...";
                return;
            }
            this.NameCommand = this.CommandSelected.NameCommand;
        }
        #endregion

        #region Fake Data
        //private void GetFakeData()
        //{
        //    ResultLogInquirieItems = new ObservableCollection<ResultLogInquirieItem>();
        //    ResultLogInquirieItem resultLogInquirieItem;

        //    resultLogInquirieItem = new ResultLogInquirieItem();
        //    resultLogInquirieItem.LogTitle = this.LogTitle;
        //    resultLogInquirieItem.IdLog = this.LogId;
        //    resultLogInquirieItem.IdInstance = 1;
        //    resultLogInquirieItem.Pause = "";
        //    resultLogInquirieItem.NameInstance = "Inst - 1";
        //    resultLogInquirieItem.IdLog = 1;
        //    resultLogInquirieItem.StartHour = "10:13";
        //    resultLogInquirieItem.NameLot = "Lote 14";
        //    resultLogInquirieItem.IdCommand = 1;
        //    resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
        //    resultLogInquirieItem.Status = "FIN";
        //    resultLogInquirieItem.StatusColor = StatusColor.Blue;
        //    resultLogInquirieItem.StatusResult = "OK";
        //    resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
        //    resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
        //    ResultLogInquirieItems.Add(resultLogInquirieItem);

        //    resultLogInquirieItem = new ResultLogInquirieItem();
        //    resultLogInquirieItem.LogTitle = this.LogTitle;
        //    resultLogInquirieItem.IdLog = this.LogId;
        //    resultLogInquirieItem.IdInstance = 1;
        //    resultLogInquirieItem.Pause = "";
        //    resultLogInquirieItem.NameInstance = "Inst - 1";
        //    resultLogInquirieItem.IdLog = 1;
        //    resultLogInquirieItem.StartHour = "10:13";
        //    resultLogInquirieItem.NameLot = "Lote 14";
        //    resultLogInquirieItem.IdCommand = 1;
        //    resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
        //    resultLogInquirieItem.Status = "FIN";
        //    resultLogInquirieItem.StatusColor = StatusColor.Red;
        //    resultLogInquirieItem.StatusResult = "ERROR";
        //    resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
        //    resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
        //    ResultLogInquirieItems.Add(resultLogInquirieItem);

        //    resultLogInquirieItem = new ResultLogInquirieItem();
        //    resultLogInquirieItem.LogTitle = this.LogTitle;
        //    resultLogInquirieItem.IdLog = this.LogId;
        //    resultLogInquirieItem.IdInstance = 1;
        //    resultLogInquirieItem.Pause = "";
        //    resultLogInquirieItem.NameInstance = "Inst - 1";
        //    resultLogInquirieItem.IdLog = 1;
        //    resultLogInquirieItem.StartHour = "10:13";
        //    resultLogInquirieItem.NameLot = "Lote 14";
        //    resultLogInquirieItem.IdCommand = 1;
        //    resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
        //    resultLogInquirieItem.Status = "FIN";
        //    resultLogInquirieItem.StatusColor = StatusColor.Blue;
        //    resultLogInquirieItem.StatusResult = "OK";
        //    resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
        //    resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
        //    ResultLogInquirieItems.Add(resultLogInquirieItem);

        //    resultLogInquirieItem = new ResultLogInquirieItem();
        //    resultLogInquirieItem.LogTitle = this.LogTitle;
        //    resultLogInquirieItem.IdLog = this.LogId;
        //    resultLogInquirieItem.IdInstance = 1;
        //    resultLogInquirieItem.Pause = "";
        //    resultLogInquirieItem.NameInstance = "Inst - 1";
        //    resultLogInquirieItem.IdLog = 1;
        //    resultLogInquirieItem.StartHour = "10:13";
        //    resultLogInquirieItem.NameLot = "Lote 14";
        //    resultLogInquirieItem.IdCommand = 1;
        //    resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
        //    resultLogInquirieItem.Status = "FIN";
        //    resultLogInquirieItem.StatusColor = StatusColor.Blue;
        //    resultLogInquirieItem.StatusResult = "OK";
        //    resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
        //    resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
        //    ResultLogInquirieItems.Add(resultLogInquirieItem);

        //    resultLogInquirieItem = new ResultLogInquirieItem();
        //    resultLogInquirieItem.LogTitle = this.LogTitle;
        //    resultLogInquirieItem.IdLog = this.LogId;
        //    resultLogInquirieItem.IdInstance = 1;
        //    resultLogInquirieItem.Pause = "";
        //    resultLogInquirieItem.NameInstance = "Inst - 1";
        //    resultLogInquirieItem.IdLog = 1;
        //    resultLogInquirieItem.StartHour = "10:13";
        //    resultLogInquirieItem.NameLot = "Lote 14";
        //    resultLogInquirieItem.IdCommand = 1;
        //    resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
        //    resultLogInquirieItem.Status = "EJECUTANDO";
        //    resultLogInquirieItem.StatusColor = StatusColor.Green;
        //    resultLogInquirieItem.StatusResult = "";
        //    resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
        //    resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
        //    ResultLogInquirieItems.Add(resultLogInquirieItem);

        //    LotItems = new ObservableCollection<PickerLotItem>();
        //    PickerLotItem lotItem;

        //    lotItem = new PickerLotItem();
        //    lotItem.Id = 1;
        //    lotItem.LotName = "LOTE / COMANDO 1";
        //    LotItems.Add(lotItem);

        //    lotItem = new PickerLotItem();
        //    lotItem.Id = 2;
        //    lotItem.LotName = "LOTE / COMANDO 2";
        //    LotItems.Add(lotItem);

        //    lotItem = new PickerLotItem();
        //    lotItem.Id = 3;
        //    lotItem.LotName = "LOTE / COMANDO 3";
        //    LotItems.Add(lotItem);

        //    CommandItems = new ObservableCollection<PickerCommandItem>();
        //    PickerCommandItem commandItem;

        //    commandItem = new PickerCommandItem();
        //    commandItem.Id = 1;
        //    commandItem.CommandName = "COMANDO 1";
        //    CommandItems.Add(commandItem);

        //    commandItem = new PickerCommandItem();
        //    commandItem.Id = 2;
        //    commandItem.CommandName = "COMANDO 2";
        //    CommandItems.Add(commandItem);

        //    commandItem = new PickerCommandItem();
        //    commandItem.Id = 3;
        //    commandItem.CommandName = "COMANDO 3";
        //    CommandItems.Add(commandItem);

        //    LogInquirieOperatorItems = new ObservableCollection<LogInquirieOperatorItem>();
        //    LogInquirieOperatorItem logInquirieOperatorItem;

        //    logInquirieOperatorItem = new LogInquirieOperatorItem();
        //    logInquirieOperatorItem.IdInstance = 1;
        //    logInquirieOperatorItem.NameInstance = "Inst - 1";
        //    logInquirieOperatorItem.IdOperator = 1;
        //    logInquirieOperatorItem.NameOperator = "ADMINISTRADOR";
        //    LogInquirieOperatorItems.Add(logInquirieOperatorItem);

        //    logInquirieOperatorItem = new LogInquirieOperatorItem();
        //    logInquirieOperatorItem.IdInstance = 1;
        //    logInquirieOperatorItem.NameInstance = "Inst - 1";
        //    logInquirieOperatorItem.IdOperator = 2;
        //    logInquirieOperatorItem.NameOperator = "ADMINISTRADOR WEB";
        //    LogInquirieOperatorItems.Add(logInquirieOperatorItem);

        //    logInquirieOperatorItem = new LogInquirieOperatorItem();
        //    logInquirieOperatorItem.IdInstance = 1;
        //    logInquirieOperatorItem.NameInstance = "Inst - 1";
        //    logInquirieOperatorItem.IdOperator = 3;
        //    logInquirieOperatorItem.NameOperator = "PLANIFICADOR";
        //    LogInquirieOperatorItems.Add(logInquirieOperatorItem);

        //    logInquirieOperatorItem = new LogInquirieOperatorItem();
        //    logInquirieOperatorItem.IdInstance = 1;
        //    logInquirieOperatorItem.NameInstance = "Inst - 1";
        //    logInquirieOperatorItem.IdOperator = 4;
        //    logInquirieOperatorItem.NameOperator = "SUPERVISOR";
        //    LogInquirieOperatorItems.Add(logInquirieOperatorItem);

        //    logInquirieOperatorItem = new LogInquirieOperatorItem();
        //    logInquirieOperatorItem.IdInstance = 1;
        //    logInquirieOperatorItem.NameInstance = "Inst - 1";
        //    logInquirieOperatorItem.IdOperator = 5;
        //    logInquirieOperatorItem.NameOperator = "TESTER";
        //    LogInquirieOperatorItems.Add(logInquirieOperatorItem);
        //}
        #endregion
    }
}
