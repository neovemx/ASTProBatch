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
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

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
        private ObservableCollection<PickerTemplateItem> pickertemplateitems;
        #endregion

        #region Properties
        private Token TokenGet { get; set; }
        private List<LotAndCommandTemplate> LotAndCommandTemplates { get; set; }

        public PickerTemplateItem TemplateSelected
        {
            get { return templateselected; }
            set { SetValue(ref templateselected, value); }
        }
        public PickerLogItem LogSelected
        {
            get { return logselected; }
            set { SetValue(ref logselected, value); }
        }
        public PickerLotItem LotSelected
        {
            get { return lotselected; }
            set { SetValue(ref lotselected, value); }
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
        public ObservableCollection<PickerTemplateItem> PickerTemplateItems
        {
            get { return pickertemplateitems; }
            set { SetValue(ref pickertemplateitems, value); }
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
                                    NameTemplate = (lotAndCommandTemplate.Template.Length > 20) ? lotAndCommandTemplate.Template.Substring(0, 20) + "..." : lotAndCommandTemplate.Template

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
