using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using AST_ProBatch_Mobile.Models.Service;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class ExecutionViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<LogItem> logitems;
        private bool toolbarisvisible;
        private string actionicon;
        private string checkicon;
        private string viewicon;
        private bool compactviewisvisible;
        private bool fullviewisvisible;
        private bool isvisibleemptyview;
        private bool isexecution;
        private bool isrefreshing = false;
        #endregion

        #region Properties
        public List<Log> Logs { get; set; }
        
        public ObservableCollection<LogItem> LogItems
        {
            get { return logitems; }
            set { SetValue(ref logitems, value); }
        }
        public bool ToolBarIsVisible
        {
            get { return toolbarisvisible; }
            set { SetValue(ref toolbarisvisible, value); }
        }
        public string ActionIcon
        {
            get { return actionicon; }
            set { SetValue(ref actionicon, value); }
        }
        public string CheckIcon
        {
            get { return checkicon; }
            set { SetValue(ref checkicon, value); }
        }
        public string ViewIcon
        {
            get { return viewicon; }
            set { SetValue(ref viewicon, value); }
        }
        public bool FullViewIsVisible
        {
            get { return fullviewisvisible; }
            set { SetValue(ref fullviewisvisible, value); }
        }
        public bool CompactViewIsVisible
        {
            get { return compactviewisvisible; }
            set { SetValue(ref compactviewisvisible, value); }
        }
        public bool IsVisibleEmptyView
        {
            get { return isvisibleemptyview; }
            set { SetValue(ref isvisibleemptyview, value); }
        }
        public bool IsExecution
        {
            get { return isexecution; }
            set { SetValue(ref isexecution, value); }
        }
        public bool IsRefreshing
        {
            get { return isrefreshing; }
            set { SetValue(ref isrefreshing, value); }
        }
        #endregion

        #region Constructors
        public ExecutionViewModel(bool IsReload, bool IsExecution)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                this.IsExecution = IsExecution;
                this.ToolBarIsVisible = false;
                this.ActionIcon = "actions";
                this.CheckIcon = "check";
                this.ViewIcon = "view_b";
                this.FullViewIsVisible = true;
                this.CompactViewIsVisible = false;
                this.IsVisibleEmptyView = false;
                GetLogs();
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new Xamarin.Forms.Command(async() =>
                {
                    try
                    {
                        this.IsRefreshing = true;
                        Response resultApiIsAvailable = await ApiSrv.ApiIsAvailable();
                        if (!resultApiIsAvailable.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(resultApiIsAvailable.Message);
                            return;
                        }
                        Response resultToken = await ApiSrv.GetToken();
                        if (!resultToken.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(resultToken.Message);
                            return;
                        }
                        else
                        {
                            Token token = JsonConvert.DeserializeObject<Token>(Crypto.DecodeString(resultToken.Data));
                            Response resultGetLogs = await ApiSrv.GetLogs(token.Key);
                            if (!resultGetLogs.IsSuccess)
                            {
                                UserDialogs.Instance.HideLoading();
                                Toast.ShowError(resultGetLogs.Message);
                                return;
                            }
                            else
                            {
                                Logs = JsonConvert.DeserializeObject<List<Log>>(Crypto.DecodeString(resultGetLogs.Data));
                                LogItems.Clear();
                                foreach (Log log in Logs)
                                {
                                    LogItems.Add(new LogItem()
                                    {
                                        IsExecution = this.IsExecution,
                                        IdLog = (Int32)log.IdLog,
                                        NameLog = log.NameLog.Trim(),
                                        IsEventual = (bool)log.IsEventual,
                                        NotIsEventual = !(bool)log.IsEventual,
                                        BarItemColor = ((bool)log.IsEventual) ? BarItemColor.HighLight : BarItemColor.Base,
                                        IdEnvironment = (Int16)log.IdEnvironment,
                                        NameEnvironment = log.NameEnvironment.Trim(),
                                        ExecutionDateTime = (log.ExecutionDateTime != null) ? (DateTime)log.ExecutionDateTime : new DateTime(),
                                        EndingDateTime = (log.EndingDateTime != null) ? (DateTime)log.EndingDateTime : new DateTime(),
                                        ExecutionDateTimeString = (log.ExecutionDateTime != null) ? ((DateTime)log.ExecutionDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                        EndingDateTimeString = (log.EndingDateTime != null) ? ((DateTime)log.EndingDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                        TotalCommands = (Int32)log.TotalCommands,
                                        ErrorCommands = (Int32)log.ErrorCommands,
                                        IsChecked = false,
                                        IsEnabled = true,
                                        IdUser = PbUser.IdUser,
                                        Operator = string.Format("{0} ({1}, {2})", PbUser.IdUser, PbUser.FisrtName.Trim(), PbUser.LastName.Trim()),
                                        TotalExecuted = log.TotalExecuted,
                                        NotificationIcon = IconSet.Notification,
                                        ActionBlock = log.ActionBlock,
                                        ActionExecute = log.ActionExecute,
                                        ActionRelease = log.ActionRelease,
                                        AccionOpen = log.AccionOpen,
                                        AccionSelection = log.AccionSelection
                                    });
                                }
                                if (LogItems.Count == 0)
                                {
                                    this.FullViewIsVisible = false;
                                    this.CompactViewIsVisible = false;
                                    this.IsVisibleEmptyView = true;
                                }
                                else
                                {
                                    this.FullViewIsVisible = true;
                                    this.CompactViewIsVisible = false;
                                    this.IsVisibleEmptyView = false;
                                }
                            }
                        }
                        this.IsRefreshing = false;
                    }
                    catch //(Exception ex)
                    {
                        Alert.Show("Ocurrió un error", "Aceptar");
                        this.IsRefreshing = false;
                    }
                });
            }
        }

        public ICommand ActionsCommand
        {
            get
            {
                return new RelayCommand(Actions);
            }
        }

        private async void Actions()
        {
            if (!this.IsExecution)
            {
                Alert.Show("Modo Monitoreo, ingrese a través de Ejecución!");
                return;
            }
            if (this.IsVisibleEmptyView)
            {
                Alert.Show("No hay datos para realizar operaciones!");
                return;
            }
            if (this.LogItems.Count == 1)
            {
                Alert.Show("Sólo hay una bitácora en la vista!");
                return;
            }

            int countItems = 0;
            foreach (LogItem item in LogItems)
            {
                if (item.IsChecked) { countItems += 1; }
            }
            if (countItems <= 1)
            {
                Alert.Show("Debe seleccionar dos o más bitácoras");
                return;
            }
            if (ToolBarIsVisible)
            {
                ToolBarIsVisible = false;
            }
            else
            {
                ToolBarIsVisible = true;
            }
        }

        public ICommand CheckCommand
        {
            get
            {
                return new RelayCommand(Check);
            }
        }

        private async void Check()
        {
            try
            {
                if (!this.IsExecution)
                {
                    Alert.Show("Modo Monitoreo, ingrese a través de Ejecución!");
                    return;
                }
                if (this.IsVisibleEmptyView)
                {
                    Alert.Show("No hay datos para realizar operaciones!");
                    return;
                }
                if (this.LogItems.Count == 1)
                {
                    Alert.Show("Sólo hay una bitácora en la vista!");
                    return;
                }

                if (string.CompareOrdinal(CheckIcon, "check") == 0)
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

                    await Task.Run(async () =>
                    {
                        var LogItemsTemp = LogItems;
                        LogItems = new ObservableCollection<LogItem>();
                        foreach (LogItem item in LogItemsTemp)
                        {
                            item.IsChecked = true;
                            item.IsEnabled = false;
                            LogItems.Add(item);
                        }
                        ToolBarIsVisible = true;
                        CheckIcon = "uncheck";

                        UserDialogs.Instance.HideLoading();
                    });
                }
                else
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

                    await Task.Run(async () =>
                    {
                        var LogItemsTemp = LogItems;
                        LogItems = new ObservableCollection<LogItem>();
                        foreach (LogItem item in LogItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            LogItems.Add(item);
                        }
                        ToolBarIsVisible = false;
                        CheckIcon = "check";

                        UserDialogs.Instance.HideLoading();
                    });
                }
            }
            catch //(Exception ex)
            {
                Alert.Show("Ocurrió un error", "Aceptar");
                UserDialogs.Instance.HideLoading();
            }

        }

        public ICommand ViewCommand
        {
            get
            {
                return new RelayCommand(View);
            }
        }

        private async void View()
        {
            try
            {
                if (this.IsVisibleEmptyView)
                {
                    Alert.Show("No hay datos para realizar operaciones!");
                    return;
                }

                if (FullViewIsVisible)
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

                    await Task.Run(async () =>
                    {
                        var LogItemsTemp = LogItems;
                        LogItems = new ObservableCollection<LogItem>();
                        foreach (LogItem item in LogItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            LogItems.Add(item);
                        }
                        FullViewIsVisible = false;
                        CompactViewIsVisible = true;
                        ViewIcon = "view_a";
                        ToolBarIsVisible = false;
                        CheckIcon = "check";

                        UserDialogs.Instance.HideLoading();
                    });
                }
                else
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

                    await Task.Run(async () =>
                    {
                        var LogItemsTemp = LogItems;
                        LogItems = new ObservableCollection<LogItem>();
                        foreach (LogItem item in LogItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            LogItems.Add(item);
                        }
                        FullViewIsVisible = true;
                        CompactViewIsVisible = false;
                        ViewIcon = "view_b";
                        ToolBarIsVisible = false;
                        CheckIcon = "check";

                        UserDialogs.Instance.HideLoading();
                    });
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Ocurrió un error: " + "/n/r/n/r" + ex.Message, "Aceptar");
                UserDialogs.Instance.HideLoading();
            }

        }
        #endregion

        #region Helpers
        private async void GetLogs()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo bitácoras...", MaskType.Black);

                Response resultApiIsAvailable = await ApiSrv.ApiIsAvailable();

                if (!resultApiIsAvailable.IsSuccess)
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(resultApiIsAvailable.Message);
                    return;
                }

                Response resultToken = await ApiSrv.GetToken();

                if (!resultToken.IsSuccess)
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(resultToken.Message);
                    return;
                }
                else
                {
                    Token token = JsonConvert.DeserializeObject<Token>(Crypto.DecodeString(resultToken.Data));
                    Response resultGetLogs = await ApiSrv.GetLogs(token.Key);
                    if (!resultGetLogs.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(resultGetLogs.Message);
                        return;
                    }
                    else
                    {
                        Logs = JsonConvert.DeserializeObject<List<Log>>(Crypto.DecodeString(resultGetLogs.Data));
                        LogItems = new ObservableCollection<LogItem>();
                        foreach (Log log in Logs)
                        {
                            LogItems.Add(new LogItem()
                            {
                                IsExecution = this.IsExecution,
                                IdLog = (Int32)log.IdLog,
                                NameLog = log.NameLog.Trim(),
                                IsEventual = (bool)log.IsEventual,
                                NotIsEventual = !(bool)log.IsEventual,
                                BarItemColor = ((bool)log.IsEventual) ? BarItemColor.HighLight : BarItemColor.Base,
                                IdEnvironment = (Int16)log.IdEnvironment,
                                NameEnvironment = log.NameEnvironment.Trim(),
                                ExecutionDateTime = (log.ExecutionDateTime != null) ? (DateTime)log.ExecutionDateTime : new DateTime(),
                                EndingDateTime = (log.EndingDateTime != null) ? (DateTime)log.EndingDateTime : new DateTime(),
                                ExecutionDateTimeString = (log.ExecutionDateTime != null) ? ((DateTime)log.ExecutionDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                EndingDateTimeString = (log.EndingDateTime != null) ? ((DateTime)log.EndingDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                TotalCommands = (Int32)log.TotalCommands,
                                ErrorCommands = (Int32)log.ErrorCommands,
                                IsChecked = false,
                                IsEnabled = true,
                                IdUser = PbUser.IdUser,
                                Operator = string.Format("{0} ({1}, {2})", PbUser.IdUser, PbUser.FisrtName.Trim(), PbUser.LastName.Trim()),
                                NotificationIcon = IconSet.Notification,
                                TotalExecuted = log.TotalExecuted,
                                ActionBlock = log.ActionBlock,
                                ActionExecute = log.ActionExecute,
                                ActionRelease = log.ActionRelease,
                                AccionOpen = log.AccionOpen,
                                AccionSelection = log.AccionSelection
                            });
                        }
                        if (LogItems.Count == 0)
                        {
                            this.FullViewIsVisible = false;
                            this.CompactViewIsVisible = false;
                            this.IsVisibleEmptyView = true;
                        }
                        else
                        {
                            this.FullViewIsVisible = true;
                            this.CompactViewIsVisible = false;
                            this.IsVisibleEmptyView = false;
                        }
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError("Ocurrió un error.");
                //return;
            }
        }
        #endregion
    }
}
