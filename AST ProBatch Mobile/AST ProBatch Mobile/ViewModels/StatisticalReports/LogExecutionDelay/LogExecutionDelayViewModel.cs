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
using System.Linq;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LogExecutionDelayViewModel : BaseViewModel
    {
        #region Atributes
        private PickerTemplateItem templateselected;
        private PickerEventItem eventselected;
        private PickerLotItem lotselected;
        private PickerCommandItem commandselected;
        private DatePromptResult startdatevalue;
        private DatePromptResult enddatevalue;
        private string startdatestring;
        private string enddatestring;
        private WeekDays weekdays;
        private ObservableCollection<PickerTemplateItem> pickertemplateitems;
        private ObservableCollection<PickerEventItem> pickereventitems;
        private ObservableCollection<PickerLotItem> pickerlotitems;
        private ObservableCollection<PickerCommandItem> pickercommanditems;
        private ObservableCollection<LogExecutionDelayReportItem> logexecutiondelayreportitems;
        #endregion

        #region Properties
        private Token TokenGet { get; set; }
        private List<LogExecutionDelayTemplate> LogExecutionDelayTemplates { get; set; }
        private List<LogExecutionDelayEvent> LogExecutionDelayEvents { get; set; }
        private List<LogExecutionDelayLot> LogExecutionDelayLots { get; set; }
        private List<LogExecutionDelayCommand> LogExecutionDelayCommands { get; set; }
        private List<LogExecutionDelayResult> LogExecutionDelayResults { get; set; }

        public PickerTemplateItem TemplateSelected
        {
            get { return templateselected; }
            set
            {
                SetValue(ref templateselected, value);
                SelectedTemplateItemChange();
            }
        }
        public PickerLotItem LotSelected
        {
            get { return lotselected; }
            set
            {
                SetValue(ref lotselected, value);
                SelectedLotItemChange();
            }
        }
        public PickerCommandItem CommandSelected
        {
            get { return commandselected; }
            set { SetValue(ref commandselected, value); }
        }
        public PickerEventItem EventSelected
        {
            get { return eventselected; }
            set { SetValue(ref eventselected, value); }
        }
        public DatePromptResult StartDateValue
        {
            get { return startdatevalue; }
            set { SetValue(ref startdatevalue, value); }
        }
        public string StartDateString
        {
            get { return startdatestring; }
            set { SetValue(ref startdatestring, value); }
        }
        public DatePromptResult EndDateValue
        {
            get { return enddatevalue; }
            set { SetValue(ref enddatevalue, value); }
        }
        public string EndDateString
        {
            get { return enddatestring; }
            set { SetValue(ref enddatestring, value); }
        }
        public WeekDays WeekDays
        {
            get { return weekdays; }
            set { SetValue(ref weekdays, value); }
        }
        public ObservableCollection<PickerTemplateItem> PickerTemplateItems
        {
            get { return pickertemplateitems; }
            set { SetValue(ref pickertemplateitems, value); }
        }
        public ObservableCollection<PickerEventItem> PickerEventItems
        {
            get { return pickereventitems; }
            set { SetValue(ref pickereventitems, value); }
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
        public ObservableCollection<LogExecutionDelayReportItem> LogExecutionDelayReportItems
        {
            get { return logexecutiondelayreportitems; }
            set { SetValue(ref logexecutiondelayreportitems, value); }
        }
        #endregion

        #region Constructors
        public LogExecutionDelayViewModel(bool IsReload)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuA);

                this.StartDateString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);
                this.EndDateString = DateTime.Now.ToString(DateTimeFormatString.LatinDate);
                this.WeekDays = new WeekDays();
                this.WeekDays.Monday = true;
                this.WeekDays.Tuesday = true;
                this.WeekDays.Wednesday = true;
                this.WeekDays.Thursday = true;
                this.WeekDays.Friday = true;
                this.WeekDays.Saturday = true;
                this.WeekDays.Sunday = true;

                GetInitialData();
            }
        }
        #endregion

        #region Commands
        //public ICommand ClearCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(Clear);
        //    }
        //}

        //private async void Clear()
        //{
        //    this.EventSelected = new PickerEventItem();
        //}

        public ICommand ReportCommand
        {
            get
            {
                return new RelayCommand(Report);
            }
        }

        private async void Report()
        {
            if (this.TemplateSelected == null)
            {
                Alert.Show("Debe seleccionar una plantilla!");
                return;
            }
            if (this.LotSelected == null)
            {
                Alert.Show("Debe seleccionar un lote!");
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
                    LogExecutionDelayResultQueryValues logExecutionDelayResultQueryValues = new LogExecutionDelayResultQueryValues()
                    {
                        IdTemplate = this.TemplateSelected.IdTemplate,
                        IdLog = 0,
                        IdLot = (this.LotSelected != null ? this.LotSelected.IdLot : 0),
                        IdCommand = (this.CommandSelected != null ? this.CommandSelected.IdCommand : 0),
                        IdEvent = (this.EventSelected != null ? this.EventSelected.IdEvent : 0),
                        StartDate = (this.StartDateValue == null ? DateTime.Now : this.StartDateValue.SelectedDate),
                        EndDate = (this.EndDateValue == null ? DateTime.Now : this.EndDateValue.SelectedDate),
                        Monday = this.WeekDays.Monday,
                        Tuesday = this.WeekDays.Tuesday,
                        Wednesday = this.WeekDays.Wednesday,
                        Thursday = this.WeekDays.Thursday,
                        Friday = this.WeekDays.Friday,
                        Saturday = this.WeekDays.Saturday,
                        Sunday = this.WeekDays.Sunday
                    };
                    Response resultGetResults = await ApiSrv.LogExecutionDelayGetResults(TokenGet.Key, logExecutionDelayResultQueryValues);
                    if (!resultGetResults.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LogExecutionDelayResults = JsonConvert.DeserializeObject<List<LogExecutionDelayResult>>(Crypto.DecodeString(resultGetResults.Data));
                        if (LogExecutionDelayResults.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();
                            Alert.Show("No hay datos para mostrar!");
                            return;
                        }
                        if (LogExecutionDelayReportItems == null)
                        {
                            LogExecutionDelayReportItems = new ObservableCollection<LogExecutionDelayReportItem>();
                        }
                        else
                        {
                            LogExecutionDelayReportItems.Clear();
                        }
                        foreach (LogExecutionDelayResult logExecutionDelayResult in LogExecutionDelayResults)
                        {
                            LogExecutionDelayReportItems.Add(new LogExecutionDelayReportItem()
                            {
                                Id = logExecutionDelayResult.Id,
                                IdTemplate = logExecutionDelayResult.IdTemplate,
                                Template = logExecutionDelayResult.Template,
                                Log = logExecutionDelayResult.Log,
                                IdLot = logExecutionDelayResult.IdLot,
                                Lot = logExecutionDelayResult.Lot,
                                IdCommand = logExecutionDelayResult.IdCommand,
                                Command = logExecutionDelayResult.Command,
                                Event = logExecutionDelayResult.Event,
                                IdEvent = logExecutionDelayResult.IdEvent,
                                StartDate = (logExecutionDelayResult.StartDate != null) ? (DateTime)logExecutionDelayResult.StartDate : new DateTime(),
                                EndDate = (logExecutionDelayResult.EndDate != null) ? (DateTime)logExecutionDelayResult.EndDate : new DateTime(),
                                DelayTime = logExecutionDelayResult.DelayTime,
                                User = logExecutionDelayResult.User,
                                Supervisor = logExecutionDelayResult.Supervisor,
                                StartDateString = (logExecutionDelayResult.StartDate != null) ? ((DateTime)logExecutionDelayResult.StartDate).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                EndDateString = (logExecutionDelayResult.EndDate != null) ? ((DateTime)logExecutionDelayResult.EndDate).ToString(DateTimeFormatString.LatinDate24Hours) : ""
                            });
                        }
                        UserDialogs.Instance.HideLoading();
                        MainViewModel.GetInstance().LogExecutionDelayReport = new LogExecutionDelayReportViewModel(true, LogExecutionDelayReportItems);
                        await Application.Current.MainPage.Navigation.PushModalAsync(new LogExecutionDelayReportPage());
                    }
                }
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError("Ocurrió un error.");
            }
        }

        public ICommand GraphCommand
        {
            get
            {
                return new RelayCommand(Graph);
            }
        }

        private async void Graph()
        {
            if (this.TemplateSelected == null)
            {
                Alert.Show("Debe seleccionar una plantilla!");
                return;
            }
            if (this.LotSelected == null)
            {
                Alert.Show("Debe seleccionar un lote!");
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
                    LogExecutionDelayResultQueryValues logExecutionDelayResultQueryValues = new LogExecutionDelayResultQueryValues()
                    {
                        IdTemplate = this.TemplateSelected.IdTemplate,
                        IdLog = 0,
                        IdLot = (this.LotSelected != null ? this.LotSelected.IdLot : 0),
                        IdCommand = (this.CommandSelected != null ? this.CommandSelected.IdCommand : 0),
                        IdEvent = (this.EventSelected != null ? this.EventSelected.IdEvent : 0),
                        StartDate = (this.StartDateValue == null ? DateTime.Now : this.StartDateValue.SelectedDate),
                        EndDate = (this.EndDateValue == null ? DateTime.Now : this.EndDateValue.SelectedDate),
                        Monday = this.WeekDays.Monday,
                        Tuesday = this.WeekDays.Tuesday,
                        Wednesday = this.WeekDays.Wednesday,
                        Thursday = this.WeekDays.Thursday,
                        Friday = this.WeekDays.Friday,
                        Saturday = this.WeekDays.Saturday,
                        Sunday = this.WeekDays.Sunday
                    };
                    Response resultGetResults = await ApiSrv.LogExecutionDelayGetResults(TokenGet.Key, logExecutionDelayResultQueryValues);
                    if (!resultGetResults.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LogExecutionDelayResults = JsonConvert.DeserializeObject<List<LogExecutionDelayResult>>(Crypto.DecodeString(resultGetResults.Data));
                        if (LogExecutionDelayResults.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();
                            Alert.Show("No hay datos para mostrar!");
                            return;
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            if (LogExecutionDelayResults.Count > 100)
                            {
                                List<LogExecutionDelayResult> logExecutionDelayResultsFiltred = LogExecutionDelayResults.Take(100).ToList();
                                MainViewModel.GetInstance().LogExecutionDelayChart = new LogExecutionDelayChartViewModel(true, logExecutionDelayResultsFiltred);
                                await Application.Current.MainPage.Navigation.PushModalAsync(new LogExecutionDelayChartPage());
                                Alert.Show("Consulta supera los 100 registros sólo se mostraran los 100 primeros!");
                            }
                            else
                            {
                                MainViewModel.GetInstance().LogExecutionDelayChart = new LogExecutionDelayChartViewModel(true, LogExecutionDelayResults);
                                await Application.Current.MainPage.Navigation.PushModalAsync(new LogExecutionDelayChartPage());
                            }
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError("Ocurrió un error.");
            }
        }

        public ICommand StartDateCommand
        {
            get
            {
                return new RelayCommand(StartDate);
            }
        }

        private async void StartDate()
        {
            DatePromptConfig datePromptConfig = new DatePromptConfig();
            datePromptConfig.MaximumDate = DateTime.Now;
            datePromptConfig.CancelText = "Cancelar";
            datePromptConfig.OkText = "Aceptar";
            this.StartDateValue = await UserDialogs.Instance.DatePromptAsync(datePromptConfig);
            this.StartDateString = this.StartDateValue.SelectedDate.ToString(DateTimeFormatString.LatinDate);
        }

        public ICommand EndDateCommand
        {
            get
            {
                return new RelayCommand(EndDate);
            }
        }

        private async void EndDate()
        {
            DatePromptConfig datePromptConfig = new DatePromptConfig();
            datePromptConfig.MaximumDate = DateTime.Now;
            datePromptConfig.CancelText = "Cancelar";
            datePromptConfig.OkText = "Aceptar";
            this.EndDateValue = await UserDialogs.Instance.DatePromptAsync(datePromptConfig);
            this.EndDateString = this.EndDateValue.SelectedDate.ToString(DateTimeFormatString.LatinDate);
        }
        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo plantillas...", MaskType.Black);
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
                        Response LogExecutionDelayGetTemplates = await ApiSrv.LogExecutionDelayGetTemplates(TokenGet.Key);
                        if (!LogExecutionDelayGetTemplates.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            LogExecutionDelayTemplates = JsonConvert.DeserializeObject<List<LogExecutionDelayTemplate>>(Crypto.DecodeString(LogExecutionDelayGetTemplates.Data));
                            PickerTemplateItems = new ObservableCollection<PickerTemplateItem>();
                            foreach (LogExecutionDelayTemplate logExecutionDelayTemplate in LogExecutionDelayTemplates)
                            {
                                PickerTemplateItems.Add(new PickerTemplateItem()
                                {
                                    IdTemplate = logExecutionDelayTemplate.IdTemplate,
                                    NameTemplate = logExecutionDelayTemplate.Template

                                });
                            }
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                }

                UserDialogs.Instance.ShowLoading("Obteniendo eventos...", MaskType.Black);
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
                        Response LogExecutionDelayGetEvents = await ApiSrv.LogExecutionDelayGetEvents(TokenGet.Key);
                        if (!LogExecutionDelayGetEvents.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            LogExecutionDelayEvents = JsonConvert.DeserializeObject<List<LogExecutionDelayEvent>>(Crypto.DecodeString(LogExecutionDelayGetEvents.Data));
                            PickerEventItems = new ObservableCollection<PickerEventItem>();
                            foreach (LogExecutionDelayEvent logExecutionDelayEvent in LogExecutionDelayEvents)
                            {
                                PickerEventItems.Add(new PickerEventItem()
                                {
                                    IdEvent = logExecutionDelayEvent.IdEvent,
                                    Event = logExecutionDelayEvent.Event

                                });
                            }
                            UserDialogs.Instance.HideLoading();
                        }
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

        private async void SelectedTemplateItemChange()
        {
            try
            {
                if (this.TemplateSelected == null)
                {
                    return;
                }

                UserDialogs.Instance.ShowLoading("Obteniendo lotes...", MaskType.Black);
                this.EventSelected = new PickerEventItem();
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
                    LogExecutionDelayLotQueryValues logExecutionDelayLotQueryValues = new LogExecutionDelayLotQueryValues()
                    {
                        IdLog = this.TemplateSelected.IdTemplate
                    };
                    Response resultGetLots = await ApiSrv.LogExecutionDelayGetLots(TokenGet.Key, logExecutionDelayLotQueryValues);
                    if (!resultGetLots.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LogExecutionDelayLots = JsonConvert.DeserializeObject<List<LogExecutionDelayLot>>(Crypto.DecodeString(resultGetLots.Data));
                        if (PickerLotItems == null)
                        {
                            PickerLotItems = new ObservableCollection<PickerLotItem>();
                        }
                        else
                        {
                            PickerLotItems.Clear();
                        }
                        foreach (LogExecutionDelayLot logExecutionDelayLot in LogExecutionDelayLots)
                        {
                            PickerLotItems.Add(new PickerLotItem()
                            {
                                IdLot = logExecutionDelayLot.IdLot,
                                NameLot = logExecutionDelayLot.Lot
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

        private async void SelectedLotItemChange()
        {
            try
            {
                if (this.LotSelected == null)
                {
                    if (this.CommandSelected != null)
                    {
                        this.CommandSelected = null;
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
                    LogExecutionDelayCommandQueryValues LogExecutionDelayCommandQueryValues = new LogExecutionDelayCommandQueryValues()
                    {
                        IdLot = this.LotSelected.IdLot
                    };
                    Response resultGetCommands = await ApiSrv.LogExecutionDelayGetCommands(TokenGet.Key, LogExecutionDelayCommandQueryValues);
                    if (!resultGetCommands.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LogExecutionDelayCommands = JsonConvert.DeserializeObject<List<LogExecutionDelayCommand>>(Crypto.DecodeString(resultGetCommands.Data));
                        if (PickerCommandItems == null)
                        {
                            PickerCommandItems = new ObservableCollection<PickerCommandItem>();
                        }
                        else
                        {
                            PickerCommandItems.Clear();
                        }
                        foreach (LogExecutionDelayCommand logExecutionDelayCommand in LogExecutionDelayCommands)
                        {
                            PickerCommandItems.Add(new PickerCommandItem()
                            {
                                IdCommand = logExecutionDelayCommand.IdCommand,
                                NameCommand = logExecutionDelayCommand.Command
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
        #endregion
    }
}
