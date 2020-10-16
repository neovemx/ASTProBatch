using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
    public class LotAndCommandViewModel : BaseViewModel
    {
        #region Atributes
        private PickerTemplateItem templateselected;
        private PickerLogItem logselected;
        private PickerLotItem lotselected;
        private PickerCommandItem commandselected;
        private DatePromptResult startdatevalue;
        private DatePromptResult enddatevalue;
        private string startdatestring;
        private string enddatestring;
        private WeekDays weekdays;
        private string executions;
        private ObservableCollection<PickerTemplateItem> pickertemplateitems;
        private ObservableCollection<PickerLogItem> pickerlogitems;
        private ObservableCollection<PickerLotItem> pickerlotitems;
        private ObservableCollection<PickerCommandItem> pickercommanditems;
        private ObservableCollection<LotAndCommandReportItem> lotandcommandreportitems;
        #endregion

        #region Properties
        private Token TokenGet { get; set; }
        private List<LotAndCommandTemplate> LotAndCommandTemplates { get; set; }
        private List<LotAndCommandLog> LotAndCommandLogs { get; set; }
        private List<LotAndCommandLot> LotAndCommandLots { get; set; }
        private List<LotAndCommandCommand> LotAndCommandCommands { get; set; }
        private List<LotAndCommandResult> LotAndCommandResults { get; set; }

        public PickerTemplateItem TemplateSelected
        {
            get { return templateselected; }
            set
            {
                SetValue(ref templateselected, value);
                SelectedTemplateItemChange();
            }
        }
        public PickerLogItem LogSelected
        {
            get { return logselected; }
            set
            {
                SetValue(ref logselected, value);
                SelectedLogItemChange();
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
        public string Executions
        {
            get { return executions; }
            set { SetValue(ref executions, value); }
        }
        public ObservableCollection<PickerTemplateItem> PickerTemplateItems
        {
            get { return pickertemplateitems; }
            set { SetValue(ref pickertemplateitems, value); }
        }
        public ObservableCollection<PickerLogItem> PickerLogItems
        {
            get { return pickerlogitems; }
            set { SetValue(ref pickerlogitems, value); }
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
        public ObservableCollection<LotAndCommandReportItem> LotAndCommandReportItem
        {
            get { return lotandcommandreportitems; }
            set { SetValue(ref lotandcommandreportitems, value); }
        }
        #endregion

        #region Constructors
        public LotAndCommandViewModel(bool IsReload)
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
            if (this.LogSelected == null)
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
                    LotAndCommandResultQueryValues lotAndCommandResultQueryValues = new LotAndCommandResultQueryValues()
                    {
                        IdTemplate = this.TemplateSelected.IdTemplate,
                        IdLog = this.LogSelected.IdLog,
                        IdLot = (this.LotSelected != null ? this.LotSelected.IdLot : 0),
                        IdCommand = (this.CommandSelected != null ? this.CommandSelected.IdCommand : 0),
                        StartDate = (this.StartDateValue == null ? DateTime.Now : this.StartDateValue.SelectedDate),
                        EndDate = (this.EndDateValue == null ? DateTime.Now : this.EndDateValue.SelectedDate),
                        Executions = (string.IsNullOrEmpty(this.Executions) ? 100 : Convert.ToInt32(this.Executions)),
                        Monday = this.WeekDays.Monday,
                        Tuesday = this.WeekDays.Tuesday,
                        Wednesday = this.WeekDays.Wednesday,
                        Thursday = this.WeekDays.Thursday,
                        Friday = this.WeekDays.Friday,
                        Saturday = this.WeekDays.Saturday,
                        Sunday = this.WeekDays.Sunday
                    };
                    Response resultGetResults = await ApiSrv.LotAndCommandGetResults(TokenGet.Key, lotAndCommandResultQueryValues);
                    if (!resultGetResults.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LotAndCommandResults = JsonConvert.DeserializeObject<List<LotAndCommandResult>>(Crypto.DecodeString(resultGetResults.Data));
                        if(LotAndCommandResults.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();
                            Alert.Show("No hay datos para mostrar!");
                            return;
                        }
                        if (LotAndCommandReportItem == null)
                        {
                            LotAndCommandReportItem = new ObservableCollection<LotAndCommandReportItem>();
                        }
                        else
                        {
                            LotAndCommandReportItem.Clear();
                        }
                        foreach (LotAndCommandResult lotAndCommandResult in LotAndCommandResults)
                        {
                            LotAndCommandReportItem.Add(new LotAndCommandReportItem()
                            {
                                IdTemplate = lotAndCommandResult.IdTemplate,
                                Template = lotAndCommandResult.Template,
                                IdLog = lotAndCommandResult.IdLog,
                                Log = lotAndCommandResult.Log,
                                Instance = lotAndCommandResult.Instance,
                                IdLot = lotAndCommandResult.IdLot,
                                Lot = lotAndCommandResult.Lot,
                                IdCommand = lotAndCommandResult.IdCommand,
                                Command = lotAndCommandResult.Command,
                                IdCommandGroup = lotAndCommandResult.IdCommandGroup,
                                CommandGroup = lotAndCommandResult.CommandGroup,
                                IdEnvironment = lotAndCommandResult.IdEnvironment,
                                Environment = lotAndCommandResult.Environment,
                                ExecutionTime = lotAndCommandResult.ExecutionTime,
                                StartDate = lotAndCommandResult.StartDate,
                                StartTime = lotAndCommandResult.StartTime,
                                EndDate = lotAndCommandResult.EndDate,
                                IdStatus = lotAndCommandResult.IdStatus,
                                Status = lotAndCommandResult.Status,
                                StartDateString = (lotAndCommandResult.StartDate != null) ? ((DateTime)lotAndCommandResult.StartDate).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                StartTimeString = (lotAndCommandResult.StartTime != null) ? ((DateTime)lotAndCommandResult.StartTime).ToString(DateTimeFormatString.Time24Hour) : "",
                                EndDateString = (lotAndCommandResult.EndDate != null) ? ((DateTime)lotAndCommandResult.EndDate).ToString(DateTimeFormatString.LatinDate24Hours) : ""
                            });
                        }
                        UserDialogs.Instance.HideLoading();
                        MainViewModel.GetInstance().LotAndCommandReport = new LotAndCommandReportViewModel(true, LotAndCommandReportItem);
                        await Application.Current.MainPage.Navigation.PushModalAsync(new LotAndCommandReportPage());
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
            if (this.LogSelected == null)
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
                    LotAndCommandResultQueryValues lotAndCommandResultQueryValues = new LotAndCommandResultQueryValues()
                    {
                        IdTemplate = this.TemplateSelected.IdTemplate,
                        IdLog = this.LogSelected.IdLog,
                        IdLot = (this.LotSelected != null ? this.LotSelected.IdLot : 0),
                        IdCommand = (this.CommandSelected != null ? this.CommandSelected.IdCommand : 0),
                        StartDate = (this.StartDateValue == null ? DateTime.Now : this.StartDateValue.SelectedDate),
                        EndDate = (this.EndDateValue == null ? DateTime.Now : this.EndDateValue.SelectedDate),
                        Executions = (string.IsNullOrEmpty(this.Executions) ? 100 : Convert.ToInt32(this.Executions)),
                        Monday = this.WeekDays.Monday,
                        Tuesday = this.WeekDays.Tuesday,
                        Wednesday = this.WeekDays.Wednesday,
                        Thursday = this.WeekDays.Thursday,
                        Friday = this.WeekDays.Friday,
                        Saturday = this.WeekDays.Saturday,
                        Sunday = this.WeekDays.Sunday
                    };
                    Response resultGetResults = await ApiSrv.LotAndCommandGetResults(TokenGet.Key, lotAndCommandResultQueryValues);
                    if (!resultGetResults.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LotAndCommandResults = JsonConvert.DeserializeObject<List<LotAndCommandResult>>(Crypto.DecodeString(resultGetResults.Data));
                        if (LotAndCommandResults.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();
                            Alert.Show("No hay datos para mostrar!");
                            return;
                        }
                        //if (LotAndCommandReportItem == null)
                        //{
                        //    LotAndCommandReportItem = new ObservableCollection<LotAndCommandReportItem>();
                        //}
                        //else
                        //{
                        //    LotAndCommandReportItem.Clear();
                        //}
                        //foreach (LotAndCommandResult lotAndCommandResult in LotAndCommandResults)
                        //{
                        //    LotAndCommandReportItem.Add(new LotAndCommandReportItem()
                        //    {
                        //        IdTemplate = lotAndCommandResult.IdTemplate,
                        //        Template = lotAndCommandResult.Template,
                        //        IdLog = lotAndCommandResult.IdLog,
                        //        Log = lotAndCommandResult.Log,
                        //        Instance = lotAndCommandResult.Instance,
                        //        IdLot = lotAndCommandResult.IdLot,
                        //        Lot = lotAndCommandResult.Lot,
                        //        IdCommand = lotAndCommandResult.IdCommand,
                        //        Command = lotAndCommandResult.Command,
                        //        IdCommandGroup = lotAndCommandResult.IdCommandGroup,
                        //        CommandGroup = lotAndCommandResult.CommandGroup,
                        //        IdEnvironment = lotAndCommandResult.IdEnvironment,
                        //        Environment = lotAndCommandResult.Environment,
                        //        ExecutionTime = lotAndCommandResult.ExecutionTime,
                        //        StartDate = lotAndCommandResult.StartDate,
                        //        StartTime = lotAndCommandResult.StartTime,
                        //        EndDate = lotAndCommandResult.EndDate,
                        //        IdStatus = lotAndCommandResult.IdStatus,
                        //        Status = lotAndCommandResult.Status,
                        //        StartDateString = (lotAndCommandResult.StartDate != null) ? ((DateTime)lotAndCommandResult.StartDate).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                        //        StartTimeString = (lotAndCommandResult.StartTime != null) ? ((DateTime)lotAndCommandResult.StartTime).ToString(DateTimeFormatString.Time24Hour) : "",
                        //        EndDateString = (lotAndCommandResult.EndDate != null) ? ((DateTime)lotAndCommandResult.EndDate).ToString(DateTimeFormatString.LatinDate24Hours) : ""
                        //    });
                        //}
                        UserDialogs.Instance.HideLoading();
                        MainViewModel.GetInstance().LotAndCommandChart = new LotAndCommandChartViewModel(true, LotAndCommandResults);
                        await Application.Current.MainPage.Navigation.PushModalAsync(new LotAndCommandChartPage());
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
                        Response resultLotAndCommandGetTemplates = await ApiSrv.LotAndCommandGetTemplates(TokenGet.Key);
                        if (!resultLotAndCommandGetTemplates.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            LotAndCommandTemplates = JsonConvert.DeserializeObject<List<LotAndCommandTemplate>>(Crypto.DecodeString(resultLotAndCommandGetTemplates.Data));
                            PickerTemplateItems = new ObservableCollection<PickerTemplateItem>();
                            foreach (LotAndCommandTemplate lotAndCommandTemplate in LotAndCommandTemplates)
                            {
                                PickerTemplateItems.Add(new PickerTemplateItem()
                                {
                                    IdTemplate = lotAndCommandTemplate.IdTemplate,
                                    NameTemplate = lotAndCommandTemplate.Template

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
                    LotAndCommandLogQueryValues lotAndCommandLogQueryValues = new LotAndCommandLogQueryValues()
                    {
                        IdTemplate = this.TemplateSelected.IdTemplate
                    };
                    Response resultGetLogs = await ApiSrv.LotAndCommandGetLogs(TokenGet.Key, lotAndCommandLogQueryValues);
                    if (!resultGetLogs.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LotAndCommandLogs = JsonConvert.DeserializeObject<List<LotAndCommandLog>>(Crypto.DecodeString(resultGetLogs.Data));
                        if (PickerLogItems == null)
                        {
                            PickerLogItems = new ObservableCollection<PickerLogItem>();
                        }
                        else
                        {
                            PickerLogItems.Clear();
                        }
                        foreach (LotAndCommandLog lotAndCommandLog in LotAndCommandLogs)
                        {
                            PickerLogItems.Add(new PickerLogItem()
                            {
                                IdLog = lotAndCommandLog.IdLog,
                                NameLog = lotAndCommandLog.Log
                            });
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        private async void SelectedLogItemChange()
        {
            try
            {
                if (this.LogSelected == null)
                {
                    if (this.LotSelected != null)
                    {
                        this.LotSelected = null;
                    }
                    return;
                }

                UserDialogs.Instance.ShowLoading("Obteniendo lotes...", MaskType.Black);
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
                    LotAndCommandLotQueryValues lotAndCommandLotQueryValues = new LotAndCommandLotQueryValues()
                    {
                        IdLog = this.LogSelected.IdLog
                    };
                    Response resultGetLots = await ApiSrv.LotAndCommandGetLots(TokenGet.Key, lotAndCommandLotQueryValues);
                    if (!resultGetLots.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LotAndCommandLots = JsonConvert.DeserializeObject<List<LotAndCommandLot>>(Crypto.DecodeString(resultGetLots.Data));
                        if (PickerLotItems == null)
                        {
                            PickerLotItems = new ObservableCollection<PickerLotItem>();
                        }
                        else
                        {
                            PickerLotItems.Clear();
                        }
                        foreach (LotAndCommandLot lotAndCommandLot in LotAndCommandLots)
                        {
                            PickerLotItems.Add(new PickerLotItem()
                            {
                                IdLot = lotAndCommandLot.IdLot,
                                NameLot = lotAndCommandLot.Lot
                            });
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch (Exception ex)
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
                    LotAndCommandCommandQueryValues lotAndCommandCommandQueryValues = new LotAndCommandCommandQueryValues()
                    {
                        IdLot = this.LotSelected.IdLot
                    };
                    Response resultGetCommands = await ApiSrv.LotAndCommandGetCommands(TokenGet.Key, lotAndCommandCommandQueryValues);
                    if (!resultGetCommands.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LotAndCommandCommands = JsonConvert.DeserializeObject<List<LotAndCommandCommand>>(Crypto.DecodeString(resultGetCommands.Data));
                        if (PickerCommandItems == null)
                        {
                            PickerCommandItems = new ObservableCollection<PickerCommandItem>();
                        }
                        else
                        {
                            PickerCommandItems.Clear();
                        }
                        foreach (LotAndCommandCommand lotAndCommandCommand in LotAndCommandCommands)
                        {
                            PickerCommandItems.Add(new PickerCommandItem()
                            {
                                IdCommand = lotAndCommandCommand.IdCommand,
                                NameCommand = lotAndCommandCommand.Command
                            });
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }
        #endregion
    }
}
