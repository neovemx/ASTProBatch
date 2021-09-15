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
    public class ControlSchedulesExecutionViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<CommandsToControl> commandstocontrolitems;
        private LogItem logitem;
        #endregion

        #region Properties
        private List<ControlSchedulesExecution> ControlSchedulesExecutions { get; set; }
        private Token TokenGet { get; set; }

        public ObservableCollection<CommandsToControl> CommandsToControlItems
        {
            get { return commandstocontrolitems; }
            set { SetValue(ref commandstocontrolitems, value); }
        }

        public LogItem LogItem
        {
            get { return logitem; }
            set { SetValue(ref logitem, value); }
        }
        #endregion

        #region Constructors
        public ControlSchedulesExecutionViewModel(bool IsReload, LogItem logItem)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                this.LogItem = logItem;
                
                GetInitialData();
                //GetFakeData();
            }
        }
        #endregion

        #region Commands
        public ICommand StatusInfoCommand
        {
            get
            {
                return new RelayCommand(StatusInfo);
            }
        }

        private async void StatusInfo()
        {
            MainViewModel.GetInstance().StatusInfoControlSchedulesExecution = new StatusInfoControlSchedulesExecutionViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new StatusInfoControlSchedulesExecutionPage());
        }
        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo comandos a controlar...", MaskType.Black);
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
                        ControlSchedulesExecutionQueryValues QueryValues = new ControlSchedulesExecutionQueryValues()
                        {
                            IdLog = this.LogItem.IdLog
                        };
                        Response resultCommandsToControlGet = await ApiSrv.GetControlSchedulesExecution(TokenGet.Key, QueryValues);
                        if (!resultCommandsToControlGet.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            ControlSchedulesExecutions = JsonConvert.DeserializeObject<List<ControlSchedulesExecution>>(Crypto.DecodeString(resultCommandsToControlGet.Data));
                            CommandsToControlItems = new ObservableCollection<CommandsToControl>();
                            foreach (ControlSchedulesExecution controlSchedulesExecution in ControlSchedulesExecutions)
                            {
                                CommandsToControlItems.Add(new CommandsToControl()
                                {
                                    IdStatus = controlSchedulesExecution.IdStatus,
                                    InstanceNumber = controlSchedulesExecution.InstanceNumber,
                                    Lot = controlSchedulesExecution.Lot,
                                    Command = controlSchedulesExecution.Command,
                                    StartDate = (controlSchedulesExecution.StartDate != null) ? (DateTime)controlSchedulesExecution.StartDate : new DateTime(),
                                    EndDate = (controlSchedulesExecution.EndDate != null) ? (DateTime)controlSchedulesExecution.EndDate : new DateTime(),
                                    TimeFrom = (controlSchedulesExecution.TimeFrom != null) ? (DateTime)controlSchedulesExecution.TimeFrom : new DateTime(),
                                    TimeUntil = (controlSchedulesExecution.TimeUntil != null) ? (DateTime)controlSchedulesExecution.TimeUntil : new DateTime(),
                                    OutOfSchedule = controlSchedulesExecution.OutOfSchedule,
                                    CriticalBusiness = controlSchedulesExecution.CriticalBusiness,
                                    StartDateString = (controlSchedulesExecution.StartDate != null) ? ((DateTime)controlSchedulesExecution.StartDate).ToString(DateTimeFormatString.LatinDate) : "",
                                    EndDateString = (controlSchedulesExecution.EndDate != null) ? ((DateTime)controlSchedulesExecution.EndDate).ToString(DateTimeFormatString.LatinDate) : "",
                                    TimeFromString = (controlSchedulesExecution.TimeFrom != null) ? ((DateTime)controlSchedulesExecution.TimeFrom).ToString(DateTimeFormatString.Time24Hour) : "",
                                    TimeUntilString = (controlSchedulesExecution.TimeUntil != null) ? ((DateTime)controlSchedulesExecution.TimeUntil).ToString(DateTimeFormatString.Time24Hour) : "",
                                    StatusColorEE = GetStatusColor.ByIdStatus(controlSchedulesExecution.IdStatus.Trim()),
                                    StatusColorEC = (controlSchedulesExecution.OutOfSchedule) ? StatusColor.Red : StatusColor.White,
                                });
                            }
                            UserDialogs.Instance.HideLoading();
                            if (CommandsToControlItems.Count == 0)
                            {
                                bool result = await Confirm.Show(string.Format("No hay comandos a controlar para la bitácora: {0}, desea cerrar la vista?", this.LogItem.NameLog));
                                if (result)
                                {
                                    Application.Current.MainPage.Navigation.PopAsync();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
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

        //private void GetFakeData()
        //{
        //    CommandsToControlItems = new ObservableCollection<CommandsToControl>();
        //    CommandsToControl commandsToControl;

        //    commandsToControl = new CommandsToControl();
        //    commandsToControl.LogTitle = this.LogTitle;
        //    commandsToControl.Id = 1;
        //    commandsToControl.Instance = 1;
        //    commandsToControl.InstanceName = "Instancia 1";
        //    commandsToControl.Lot = 1;
        //    commandsToControl.LotName = "99991 - Prueba SQR 1";
        //    commandsToControl.Command = 1;
        //    commandsToControl.CommandName = "9990 - Prueba";
        //    commandsToControl.Start = "10:40";
        //    commandsToControl.End = "10:50";
        //    commandsToControl.ControlStart = "11:04";
        //    commandsToControl.ControlEnd = "11:30";
        //    commandsToControl.StatusColorEE = StatusColor.Blue;
        //    commandsToControl.StatusColorEC = StatusColor.Red;
        //    CommandsToControlItems.Add(commandsToControl);

        //    commandsToControl = new CommandsToControl();
        //    commandsToControl.LogTitle = this.LogTitle;
        //    commandsToControl.Id = 2;
        //    commandsToControl.Instance = 2;
        //    commandsToControl.InstanceName = "Instancia 2";
        //    commandsToControl.Lot = 2;
        //    commandsToControl.LotName = "99991 - Prueba SQR 1";
        //    commandsToControl.Command = 2;
        //    commandsToControl.CommandName = "9990 - Prueba";
        //    commandsToControl.Start = "10:40";
        //    commandsToControl.End = "10:50";
        //    commandsToControl.ControlStart = "11:04";
        //    commandsToControl.ControlEnd = "11:30";
        //    commandsToControl.StatusColorEE = StatusColor.Blue;
        //    commandsToControl.StatusColorEC = StatusColor.Red;
        //    CommandsToControlItems.Add(commandsToControl);

        //    commandsToControl = new CommandsToControl();
        //    commandsToControl.LogTitle = this.LogTitle;
        //    commandsToControl.Id = 3;
        //    commandsToControl.Instance = 3;
        //    commandsToControl.InstanceName = "Instancia 2";
        //    commandsToControl.Lot = 3;
        //    commandsToControl.LotName = "99991 - Prueba SQR 1";
        //    commandsToControl.Command = 2;
        //    commandsToControl.CommandName = "9990 - Prueba";
        //    commandsToControl.Start = "10:40";
        //    commandsToControl.End = "10:50";
        //    commandsToControl.ControlStart = "11:04";
        //    commandsToControl.ControlEnd = "11:30";
        //    commandsToControl.StatusColorEE = StatusColor.Green;
        //    commandsToControl.StatusColorEC = StatusColor.White;
        //    CommandsToControlItems.Add(commandsToControl);
        //}
        #endregion
    }
}
