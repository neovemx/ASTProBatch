using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using ASTProBatchMobile.Models.Service;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
        #endregion

        #region Constructors
        public ExecutionViewModel(bool IsReload)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                ToolBarIsVisible = false;
                ActionIcon = "actions";
                CheckIcon = "check";
                ViewIcon = "view_b";
                FullViewIsVisible = true;
                CompactViewIsVisible = false;
                GetLogs();
                //GetFakeData();
            }
        }
        #endregion

        #region Helpers
        private async void GetLogs()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

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
                                IdLog = (Int32)log.IdLog,
                                NameLog = log.NameLog.Trim(),
                                IsEventual = (bool)log.IsEventual,
                                NotIsEventual = !(bool)log.IsEventual,
                                BarItemColor = ((bool)log.IsEventual) ? BarItemColor.HighLight : BarItemColor.Base,
                                IdEnvironment = (Int16)log.IdEnvironment,
                                NameEnvironment = log.NameEnvironment.Trim(),
                                ExecutionDateTime = (log.ExecutionDateTime != null) ? ((DateTime)log.ExecutionDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                EndingDateTime = (log.EndingDateTime != null) ? ((DateTime)log.EndingDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                TotalCommands = (Int32)log.TotalCommands,
                                ErrorCommands = (Int32)log.ErrorCommands,
                                IsChecked = false,
                                IsEnabled = true,
                                IdUser = PbUser.IdUser,
                                Operator = string.Format("{0} ({1}, {2})", PbUser.IdUser, PbUser.FisrtName.Trim(), PbUser.LastName.Trim()),
                                NotificationIcon = IconSet.Notification
                            });
                        }
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError("Ocurrió un error.");
                return;
            }
        }
        #endregion

        #region Commands
        public ICommand ActionsCommand
        {
            get
            {
                return new RelayCommand(Actions);
            }
        }

        private async void Actions()
        {
            int countItems = 0;
            foreach (LogItem item in LogItems)
            {
                if (item.IsChecked) { countItems += 1; }
            }
            if (countItems <= 1)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
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
                if (string.CompareOrdinal(CheckIcon, "check") == 0)
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

                    await Task.Delay(1000);

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

                    await Task.Delay(1000);

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
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Ocurrió un error: " + "/n/r/n/r" + ex.Message, "Aceptar");
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
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Ocurrió un error: " + "/n/r/n/r" + ex.Message, "Aceptar");
                UserDialogs.Instance.HideLoading();
            }

        }
        #endregion

        #region FakeData
        //private void GetFakeData()
        //{
        //    LogItems = new ObservableCollection<LogItem>();
        //    LogItem logItem;

        //    logItem = new LogItem();
        //    logItem.Id = 1;
        //    logItem.IsChecked = false;
        //    logItem.IsEnabled = true;
        //    logItem.Title = "BITACORA 1";
        //    logItem.Notifications = "notification";
        //    logItem.Status = "";
        //    logItem.StatusColor = StatusColor.Green;
        //    logItem.Environment = "WINDOWS";
        //    logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.End = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.Operator = "ADMINISTRADOR (FIRST NAME, LAST NAME)";
        //    logItem.CommandsNumber = "400";
        //    logItem.CommandsFail = "4";
        //    logItem.Eventual = true;
        //    LogItems.Add(logItem);

        //    logItem = new LogItem();
        //    logItem.Id = 2;
        //    logItem.IsChecked = false;
        //    logItem.IsEnabled = true;
        //    logItem.Title = "BITACORA 2";
        //    logItem.Notifications = "notification";
        //    logItem.Status = "";
        //    logItem.StatusColor = StatusColor.Orange;
        //    logItem.Environment = "WINDOWS";
        //    logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.End = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.Operator = "ADMINISTRADOR (FIRST NAME, LAST NAME)";
        //    logItem.CommandsNumber = "400";
        //    logItem.CommandsFail = "4";
        //    logItem.Eventual = false;
        //    LogItems.Add(logItem);

        //    logItem = new LogItem();
        //    logItem.Id = 3;
        //    logItem.IsChecked = false;
        //    logItem.IsEnabled = true;
        //    logItem.Title = "BITACORA 3";
        //    logItem.Notifications = "notification_n";
        //    logItem.Status = "";
        //    logItem.StatusColor = StatusColor.Green;
        //    logItem.Environment = "WINDOWS";
        //    logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.End = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.Operator = "ADMINISTRADOR (FIRST NAME, LAST NAME)";
        //    logItem.CommandsNumber = "400";
        //    logItem.CommandsFail = "4";
        //    logItem.Eventual = true;
        //    LogItems.Add(logItem);

        //    logItem = new LogItem();
        //    logItem.Id = 4;
        //    logItem.IsChecked = false;
        //    logItem.IsEnabled = true;
        //    logItem.Title = "BITACORA 4";
        //    logItem.Notifications = "notification_n";
        //    logItem.Status = "";
        //    logItem.StatusColor = StatusColor.White;
        //    logItem.Environment = "WINDOWS";
        //    logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.End = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.Operator = "ADMINISTRADOR (FIRST NAME, LAST NAME)";
        //    logItem.CommandsNumber = "400";
        //    logItem.CommandsFail = "4";
        //    logItem.Eventual = true;
        //    LogItems.Add(logItem);

        //    logItem = new LogItem();
        //    logItem.Id = 5;
        //    logItem.IsChecked = false;
        //    logItem.IsEnabled = true;
        //    logItem.Title = "BITACORA 5";
        //    logItem.Notifications = "notification";
        //    logItem.Status = "";
        //    logItem.StatusColor = StatusColor.White;
        //    logItem.Environment = "WINDOWS";
        //    logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.End = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    logItem.Operator = "ADMINISTRADOR (FIRST NAME, LAST NAME)";
        //    logItem.CommandsNumber = "400";
        //    logItem.CommandsFail = "4";
        //    logItem.Eventual = false;
        //    LogItems.Add(logItem);
        //}
        #endregion
    }
}
