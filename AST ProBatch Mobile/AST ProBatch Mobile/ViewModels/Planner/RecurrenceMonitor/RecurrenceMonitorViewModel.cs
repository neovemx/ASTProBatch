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
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class RecurrenceMonitorViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<RecurrenceItem> recurrenceitems;
        private bool toolbarisvisible;
        private string checkicon;
        #endregion

        #region Properties
        public List<Monitor> MonitorData { get; set; }
        public ObservableCollection<RecurrenceItem> RecurrenceItems
        {
            get { return recurrenceitems; }
            set { SetValue(ref recurrenceitems, value); }
        }
        public bool ToolBarIsVisible
        {
            get { return toolbarisvisible; }
            set { SetValue(ref toolbarisvisible, value); }
        }
        public string CheckIcon
        {
            get { return checkicon; }
            set { SetValue(ref checkicon, value); }
        }
        #endregion

        #region Constructors
        public RecurrenceMonitorViewModel(bool IsReload)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuD);

                ToolBarIsVisible = false;
                CheckIcon = "check";

                GetInitialData();
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
            foreach (RecurrenceItem item in RecurrenceItems)
            {
                if (item.IsChecked) { countItems += 1; }
            }
            if (countItems <= 1)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más procesos", "Aceptar");
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
                        var RecurrenceItemsTemp = RecurrenceItems;
                        RecurrenceItems = new ObservableCollection<RecurrenceItem>();
                        foreach (RecurrenceItem item in RecurrenceItemsTemp)
                        {
                            item.IsChecked = true;
                            item.IsEnabled = false;
                            RecurrenceItems.Add(item);
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
                        var RecurrenceItemsTemp = RecurrenceItems;
                        RecurrenceItems = new ObservableCollection<RecurrenceItem>();
                        foreach (RecurrenceItem item in RecurrenceItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            RecurrenceItems.Add(item);
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
        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo datos del monitor de procesos...", MaskType.Black);

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
                    MonitorDataQueryValues query = new MonitorDataQueryValues()
                    {
                        CurrentDate = DateTime.Now,
                        IsRecurrent = 1
                    };
                    Response resultGetMonitorData = await ApiSrv.MonitorGetData(token.Key, query);
                    if (!resultGetMonitorData.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(resultGetMonitorData.Message);
                        return;
                    }
                    else
                    {
                        MonitorData = JsonConvert.DeserializeObject<List<Monitor>>(Crypto.DecodeString(resultGetMonitorData.Data));
                        RecurrenceItems = new ObservableCollection<RecurrenceItem>();
                        foreach (Monitor item in MonitorData)
                        {
                            RecurrenceItems.Add(new RecurrenceItem()
                            {
                                IdCalendar = item.IdCalendar,
                                PID = item.PID,
                                IdLot = (item.IdLot != null) ? (Int32)item.IdLot : 0,
                                NameLot = item.NameLot,
                                IdCommand = (item.IdCommand != null) ? (Int32)item.IdCommand : 0,
                                NameCommand = item.NameCommand,
                                IdEnvironment = (item.IdEnvironment) != null ? (short)item.IdEnvironment : new short(),
                                NameEnvironment = item.NameEnvironment,
                                IPAddress = item.IPAddress,
                                IdService = (item.IdService != null) ? (short)item.IdService : new short(),
                                NameService = item.NameService,
                                IsServicePD = (item.IsServicePD != null) ? (bool)item.IsServicePD : false,
                                StartHour = (item.StartHour != null) ? (TimeSpan)item.StartHour : new TimeSpan(),
                                IdStatus = (item.IdStatus != null) ? (string)item.IdStatus : "",
                                Ommited = (item.Ommited != null) ? (string)item.Ommited : "",
                                ExecutionStart = (item.ExecutionStart != null) ? (DateTime)item.ExecutionStart : new DateTime(),
                                ExecutionEnd = (item.ExecutionEnd != null) ? (DateTime)item.ExecutionEnd : new DateTime(),
                                ReExecution = (item.ReExecution != null) ? (bool)item.ReExecution : false,
                                RecurrenceTime = (item.RecurrenceTime != null) ? (TimeSpan)item.RecurrenceTime : new TimeSpan(),
                                EndHour = (item.EndHour != null) ? (TimeSpan)item.EndHour : new TimeSpan(),
                                Order = (item.Order) != null ? (short)item.Order : new short(),
                                StartHourString = (item.StartHour != null) ? ((TimeSpan)item.StartHour).ToString() : "",
                                ExecutionStartString = (item.ExecutionStart != null) ? ((DateTime)item.ExecutionStart).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                ExecutionEndString = (item.ExecutionEnd != null) ? ((DateTime)item.ExecutionEnd).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                RecurrenceTimeString = (item.RecurrenceTime != null) ? ((TimeSpan)item.RecurrenceTime).ToString() : "",
                                EndHourString = (item.EndHour != null) ? ((TimeSpan)item.EndHour).ToString() : "",
                                IsChecked = false,
                                IsEnabled = true,
                                Status = GetExecutionStatus.ByIdStatus((item.IdStatus != null) ? (string)item.IdStatus.Trim() : "")
                            });
                        }
                        //if (LogItems.Count == 0)
                        //{
                        //    this.FullViewIsVisible = false;
                        //    this.CompactViewIsVisible = false;
                        //    this.IsVisibleEmptyView = true;
                        //}
                        //else
                        //{
                        //    this.FullViewIsVisible = true;
                        //    this.CompactViewIsVisible = false;
                        //    this.IsVisibleEmptyView = false;
                        //}
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError("Ocurrió un error.");
            }
        }
        #endregion
    }
}
