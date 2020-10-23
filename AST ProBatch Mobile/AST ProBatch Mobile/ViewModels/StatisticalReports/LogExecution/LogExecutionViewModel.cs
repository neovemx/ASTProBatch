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
    public class LogExecutionViewModel : BaseViewModel
    {
        #region Atributes
        private PickerTemplateItem templateselected;
        private DatePromptResult startdatevalue;
        private DatePromptResult enddatevalue;
        private string startdatestring;
        private string enddatestring;
        private WeekDays weekdays;
        private string executions;
        private ObservableCollection<PickerTemplateItem> pickertemplateitems;
        private ObservableCollection<LogExecutionReportItem> logexecutionreportitems;
        #endregion

        #region Properties
        private Token TokenGet { get; set; }
        private List<LogExecutionTemplate> LogExecutionTemplates { get; set; }
        private List<LogExecutionResult> LogExecutionResults { get; set; }

        public PickerTemplateItem TemplateSelected
        {
            get { return templateselected; }
            set
            {
                SetValue(ref templateselected, value);
            }
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
        public ObservableCollection<LogExecutionReportItem> LogExecutionReportItems
        {
            get { return logexecutionreportitems; }
            set { SetValue(ref logexecutionreportitems, value); }
        }
        #endregion

        #region Constructors
        public LogExecutionViewModel(bool IsReload)
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
                    LogExecutionResultQueryValues logExecutionResultQueryValues = new LogExecutionResultQueryValues()
                    {
                        IdTemplate = this.TemplateSelected.IdTemplate,
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
                    Response resultGetResults = await ApiSrv.LogExecutionGetResults(TokenGet.Key, logExecutionResultQueryValues);
                    if (!resultGetResults.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LogExecutionResults = JsonConvert.DeserializeObject<List<LogExecutionResult>>(Crypto.DecodeString(resultGetResults.Data));
                        if (LogExecutionResults.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();
                            Alert.Show("No hay datos para mostrar!");
                            return;
                        }
                        if (LogExecutionReportItems == null)
                        {
                            LogExecutionReportItems = new ObservableCollection<LogExecutionReportItem>();
                        }
                        else
                        {
                            LogExecutionReportItems.Clear();
                        }
                        foreach (LogExecutionResult logExecutionResult in LogExecutionResults)
                        {
                            LogExecutionReportItems.Add(new LogExecutionReportItem()
                            {
                                IdTemplate = logExecutionResult.IdTemplate,
                                Template = logExecutionResult.Template,
                                IdLog = logExecutionResult.IdLog,
                                Log = logExecutionResult.Log,
                                IdEnvironment = logExecutionResult.IdEnvironment,
                                Environment = logExecutionResult.Environment,
                                StartDate = logExecutionResult.StartDate,
                                EndDate = logExecutionResult.EndDate,
                                ExecutionTime = logExecutionResult.ExecutionTime
                            });
                        }
                        UserDialogs.Instance.HideLoading();
                        MainViewModel.GetInstance().LogExecutionReport = new LogExecutionReportViewModel(true, LogExecutionReportItems);
                        await Application.Current.MainPage.Navigation.PushModalAsync(new LogExecutionReportPage());
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
                    LogExecutionResultQueryValues logExecutionResultQueryValues = new LogExecutionResultQueryValues()
                    {
                        IdTemplate = this.TemplateSelected.IdTemplate,
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
                    Response resultGetResults = await ApiSrv.LogExecutionGetResults(TokenGet.Key, logExecutionResultQueryValues);
                    if (!resultGetResults.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        LogExecutionResults = JsonConvert.DeserializeObject<List<LogExecutionResult>>(Crypto.DecodeString(resultGetResults.Data));
                        if (LogExecutionResults.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();
                            Alert.Show("No hay datos para mostrar!");
                            return;
                        }
                        UserDialogs.Instance.HideLoading();
                        MainViewModel.GetInstance().LogExecutionChart = new LogExecutionChartViewModel(true, LogExecutionResults);
                        await Application.Current.MainPage.Navigation.PushModalAsync(new LogExecutionChartPage());
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
                        Response resultLogExecutionGetTemplates = await ApiSrv.LogExecutionGetTemplates(TokenGet.Key);
                        if (!resultLogExecutionGetTemplates.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            LogExecutionTemplates = JsonConvert.DeserializeObject<List<LogExecutionTemplate>>(Crypto.DecodeString(resultLogExecutionGetTemplates.Data));
                            PickerTemplateItems = new ObservableCollection<PickerTemplateItem>();
                            foreach (LogExecutionTemplate logExecutionTemplate in LogExecutionTemplates)
                            {
                                PickerTemplateItems.Add(new PickerTemplateItem()
                                {
                                    IdTemplate = logExecutionTemplate.IdTemplate,
                                    NameTemplate = logExecutionTemplate.Template

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
        #endregion
    }
}
