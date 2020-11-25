using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class CommandDataViewModel : BaseViewModel
    {
        #region Atributes
        //private ObservableCollection<PickerOperatorItem> operatorsitems;
        //private LogItem logitem;
        //private PickerOperatorItem selectedoperator;
        //private bool isloadingdata;
        public string selectedmodule;
        public string selectedmodulechild;
        public bool toolbarachild;
        public bool toolbardchild;

        #region Atributes ToolBar A B C D
        // A
        public bool aisactive;
        public string abackgroundcolor;
        public int awidth;
        public int aimagebuttonwidth;
        public string atext;
        public string afontcolor;
        public string aicon;
        // B
        public bool bisactive;
        public string bbackgroundcolor;
        public int bwidth;
        public int bimagebuttonwidth;
        public string btext;
        public string bfontcolor;
        public string bicon;
        // C
        public bool cisactive;
        public string cbackgroundcolor;
        public int cwidth;
        public int cimagebuttonwidth;
        public string ctext;
        public string cfontcolor;
        public string cicon;
        // D
        public bool disactive;
        public string dbackgroundcolor;
        public int dwidth;
        public int dimagebuttonwidth;
        public string dtext;
        public string dfontcolor;
        public string dicon;
        #endregion
        #region Atributes ToolBar a b c
        // a
        public bool aaisactive;
        public string aabackgroundcolor;
        public int aawidth;
        public int aaimagebuttonwidth;
        public string aatext;
        public string aafontcolor;
        public string aaicon;
        // b
        public bool bbisactive;
        public string bbbackgroundcolor;
        public int bbwidth;
        public int bbimagebuttonwidth;
        public string bbtext;
        public string bbfontcolor;
        public string bbicon;
        // c
        public bool ccisactive;
        public string ccbackgroundcolor;
        public int ccwidth;
        public int ccimagebuttonwidth;
        public string cctext;
        public string ccfontcolor;
        public string ccicon;
        #endregion
        #region Atributes ToolBar d e
        // d
        public bool ddisactive;
        public string ddbackgroundcolor;
        public int ddwidth;
        public int ddimagebuttonwidth;
        public string ddtext;
        public string ddfontcolor;
        public string ddicon;
        // e
        public bool eeisactive;
        public string eebackgroundcolor;
        public int eewidth;
        public int eeimagebuttonwidth;
        public string eetext;
        public string eefontcolor;
        public string eeicon;
        #endregion
        #region Atributes Modules A B C D
        // A
        public string acode;
        public bool acritical;
        public string aname;
        public string adescription;
        public string agroup;
        public string atype;
        public bool areexecution;
        // B
        // C
        private ObservableCollection<CommandDataInterfaceItem> interfaceitems;
        // D
        //private bool doptionaischecked;
        //private bool doptionbischecked;
        //private bool doptioncischecked;
        private ObservableCollection<CommandDataOptionItem> optionitems;
        private DatePromptResult startdatevalue;
        private DatePromptResult enddatevalue;
        private string startdatestring;
        private string enddatestring;
        #endregion
        #region Atributes Modules b c d e
        // b
        public string bbcommand;
        public string bbdefaultpath;
        public string bbcurrentpath;
        public bool bbhassubprocess;
        public string bbmonitoringservice;
        public bool bbisuser;
        public string bbuser;
        public bool bbissubprocess;
        public string bbsubprocess;
        public string bbmonitoringtime;
        public bool bbwaitendsubprocess;
        private ObservableCollection<CommandDataProcessItem> processitems;
        // c
        public string ccenvironment;
        public string ccpatch;
        private ObservableCollection<CommandDataOriginActionItem> originactionitems;
        private ObservableCollection<CommandDataDestinationItem> destinationsitems;
        private ObservableCollection<CommandDataDestinationActionItem> destinationactionitems;
        // d
        public string ddenvironment;
        public string ddcommandline;
        // e
        private ObservableCollection<CommandDataObservationItem> observationitems;
        private ObservableCollection<CommandDataObservationDetailItem> observationdetailitems;
        #endregion
        #endregion

        #region Properties
        //private List<OperatorChangeUser> Users { get; set; }
        //private List<OperatorChangeInstance> Instances { get; set; }
        //private List<OperatorChangeUserIsInAllInstances> UserInInstances { get; set; }
        //private List<OperatorChange> OperatorChanges { get; set; }
        //private Token TokenGet { get; set; }
        //private Token TokenPbAuth { get; set; }
        //private Token TokenMenuB { get; set; }
        //private Response ApiResponse { get; set; }

        //public PickerOperatorItem SelectedOperator
        //{
        //    get { return selectedoperator; }
        //    set
        //    {
        //        SetValue(ref selectedoperator, value);
        //    }
        //}
        //public ObservableCollection<InstanceRunningItem> InstanceRunningItems
        //{
        //    get { return instancerunningitems; }
        //    set { SetValue(ref instancerunningitems, value); }
        //}
        //public ObservableCollection<PickerOperatorItem> OperatorsItems
        //{
        //    get { return operatorsitems; }
        //    set { SetValue(ref operatorsitems, value); }
        //}
        //public LogItem LogItem
        //{
        //    get { return logitem; }
        //    set { SetValue(ref logitem, value); }
        //}
        public string SelectedModule
        {
            get { return selectedmodule; }
            set { SetValue(ref selectedmodule, value); }
        }
        public string SelectedModule_Child
        {
            get { return selectedmodulechild; }
            set { SetValue(ref selectedmodulechild, value); }
        }
        public bool ToolBar_A_Child
        {
            get { return toolbarachild; }
            set { SetValue(ref toolbarachild, value); }
        }
        public bool ToolBar_D_Child
        {
            get { return toolbardchild; }
            set { SetValue(ref toolbardchild, value); }
        }
        #region Properties ToolBar A B C D
        // A
        public bool A_IsActive
        {
            get { return aisactive; }
            set { SetValue(ref aisactive, value); }
        }
        public string A_BackgroundColor
        {
            get { return abackgroundcolor; }
            set { SetValue(ref abackgroundcolor, value); }
        }
        public int A_Width
        {
            get { return awidth; }
            set { SetValue(ref awidth, value); }
        }
        public int A_ImageButtonWidth
        {
            get { return aimagebuttonwidth; }
            set { SetValue(ref aimagebuttonwidth, value); }
        }
        public string A_Text
        {
            get { return atext; }
            set { SetValue(ref atext, value); }
        }
        public string A_FontColor
        {
            get { return afontcolor; }
            set { SetValue(ref afontcolor, value); }
        }
        public string A_Icon
        {
            get { return aicon; }
            set { SetValue(ref aicon, value); }
        }
        // B
        public bool B_IsActive
        {
            get { return bisactive; }
            set { SetValue(ref bisactive, value); }
        }
        public string B_BackgroundColor
        {
            get { return bbackgroundcolor; }
            set { SetValue(ref bbackgroundcolor, value); }
        }
        public int B_Width
        {
            get { return bwidth; }
            set { SetValue(ref bwidth, value); }
        }
        public int B_ImageButtonWidth
        {
            get { return bimagebuttonwidth; }
            set { SetValue(ref bimagebuttonwidth, value); }
        }
        public string B_Text
        {
            get { return btext; }
            set { SetValue(ref btext, value); }
        }
        public string B_FontColor
        {
            get { return bfontcolor; }
            set { SetValue(ref bfontcolor, value); }
        }
        public string B_Icon
        {
            get { return bicon; }
            set { SetValue(ref bicon, value); }
        }
        // C
        public bool C_IsActive
        {
            get { return cisactive; }
            set { SetValue(ref cisactive, value); }
        }
        public string C_BackgroundColor
        {
            get { return cbackgroundcolor; }
            set { SetValue(ref cbackgroundcolor, value); }
        }
        public int C_Width
        {
            get { return cwidth; }
            set { SetValue(ref cwidth, value); }
        }
        public int C_ImageButtonWidth
        {
            get { return cimagebuttonwidth; }
            set { SetValue(ref cimagebuttonwidth, value); }
        }
        public string C_Text
        {
            get { return ctext; }
            set { SetValue(ref ctext, value); }
        }
        public string C_FontColor
        {
            get { return cfontcolor; }
            set { SetValue(ref cfontcolor, value); }
        }
        public string C_Icon
        {
            get { return cicon; }
            set { SetValue(ref cicon, value); }
        }
        // D
        public bool D_IsActive
        {
            get { return disactive; }
            set { SetValue(ref disactive, value); }
        }
        public string D_BackgroundColor
        {
            get { return dbackgroundcolor; }
            set { SetValue(ref dbackgroundcolor, value); }
        }
        public int D_Width
        {
            get { return dwidth; }
            set { SetValue(ref dwidth, value); }
        }
        public int D_ImageButtonWidth
        {
            get { return dimagebuttonwidth; }
            set { SetValue(ref dimagebuttonwidth, value); }
        }
        public string D_Text
        {
            get { return dtext; }
            set { SetValue(ref dtext, value); }
        }
        public string D_FontColor
        {
            get { return dfontcolor; }
            set { SetValue(ref dfontcolor, value); }
        }
        public string D_Icon
        {
            get { return dicon; }
            set { SetValue(ref dicon, value); }
        }
        #endregion
        #region Properties ToolBar a b c
        // a
        public bool A_a_IsActive
        {
            get { return aaisactive; }
            set { SetValue(ref aaisactive, value); }
        }
        public string A_a_BackgroundColor
        {
            get { return aabackgroundcolor; }
            set { SetValue(ref aabackgroundcolor, value); }
        }
        public int A_a_Width
        {
            get { return aawidth; }
            set { SetValue(ref aawidth, value); }
        }
        public int A_a_ImageButtonWidth
        {
            get { return aaimagebuttonwidth; }
            set { SetValue(ref aaimagebuttonwidth, value); }
        }
        public string A_a_Text
        {
            get { return aatext; }
            set { SetValue(ref aatext, value); }
        }
        public string A_a_FontColor
        {
            get { return aafontcolor; }
            set { SetValue(ref aafontcolor, value); }
        }
        public string A_a_Icon
        {
            get { return aaicon; }
            set { SetValue(ref aaicon, value); }
        }
        // b
        public bool B_b_IsActive
        {
            get { return bbisactive; }
            set { SetValue(ref bbisactive, value); }
        }
        public string B_b_BackgroundColor
        {
            get { return bbbackgroundcolor; }
            set { SetValue(ref bbbackgroundcolor, value); }
        }
        public int B_b_Width
        {
            get { return bbwidth; }
            set { SetValue(ref bbwidth, value); }
        }
        public int B_b_ImageButtonWidth
        {
            get { return bbimagebuttonwidth; }
            set { SetValue(ref bbimagebuttonwidth, value); }
        }
        public string B_b_Text
        {
            get { return bbtext; }
            set { SetValue(ref bbtext, value); }
        }
        public string B_b_FontColor
        {
            get { return bbfontcolor; }
            set { SetValue(ref bbfontcolor, value); }
        }
        public string B_b_Icon
        {
            get { return bbicon; }
            set { SetValue(ref bbicon, value); }
        }
        // d
        public bool C_c_IsActive
        {
            get { return ccisactive; }
            set { SetValue(ref ccisactive, value); }
        }
        public string C_c_BackgroundColor
        {
            get { return ccbackgroundcolor; }
            set { SetValue(ref ccbackgroundcolor, value); }
        }
        public int C_c_Width
        {
            get { return ccwidth; }
            set { SetValue(ref ccwidth, value); }
        }
        public int C_c_ImageButtonWidth
        {
            get { return ccimagebuttonwidth; }
            set { SetValue(ref ccimagebuttonwidth, value); }
        }
        public string C_c_Text
        {
            get { return cctext; }
            set { SetValue(ref cctext, value); }
        }
        public string C_c_FontColor
        {
            get { return ccfontcolor; }
            set { SetValue(ref ccfontcolor, value); }
        }
        public string C_c_Icon
        {
            get { return ccicon; }
            set { SetValue(ref ccicon, value); }
        }
        #endregion
        #region Properties ToolBar d e
        // d
        public bool D_d_IsActive
        {
            get { return ddisactive; }
            set { SetValue(ref ddisactive, value); }
        }
        public string D_d_BackgroundColor
        {
            get { return ddbackgroundcolor; }
            set { SetValue(ref ddbackgroundcolor, value); }
        }
        public int D_d_Width
        {
            get { return ddwidth; }
            set { SetValue(ref ddwidth, value); }
        }
        public int D_d_ImageButtonWidth
        {
            get { return ddimagebuttonwidth; }
            set { SetValue(ref ddimagebuttonwidth, value); }
        }
        public string D_d_Text
        {
            get { return ddtext; }
            set { SetValue(ref ddtext, value); }
        }
        public string D_d_FontColor
        {
            get { return ddfontcolor; }
            set { SetValue(ref ddfontcolor, value); }
        }
        public string D_d_Icon
        {
            get { return ddicon; }
            set { SetValue(ref ddicon, value); }
        }
        // e
        public bool E_e_IsActive
        {
            get { return eeisactive; }
            set { SetValue(ref eeisactive, value); }
        }
        public string E_e_BackgroundColor
        {
            get { return eebackgroundcolor; }
            set { SetValue(ref eebackgroundcolor, value); }
        }
        public int E_e_Width
        {
            get { return eewidth; }
            set { SetValue(ref eewidth, value); }
        }
        public int E_e_ImageButtonWidth
        {
            get { return eeimagebuttonwidth; }
            set { SetValue(ref eeimagebuttonwidth, value); }
        }
        public string E_e_Text
        {
            get { return eetext; }
            set { SetValue(ref eetext, value); }
        }
        public string E_e_FontColor
        {
            get { return eefontcolor; }
            set { SetValue(ref eefontcolor, value); }
        }
        public string E_e_Icon
        {
            get { return eeicon; }
            set { SetValue(ref eeicon, value); }
        }
        #endregion
        #region Properties Modules A B C D
        // A
        public string A_Code
        {
            get { return acode; }
            set { SetValue(ref acode, value); }
        }
        public bool A_Critical
        {
            get { return acritical; }
            set { SetValue(ref acritical, value); }
        }
        public string A_Name
        {
            get { return aname; }
            set { SetValue(ref aname, value); }
        }
        public string A_Description
        {
            get { return adescription; }
            set { SetValue(ref adescription, value); }
        }
        public string A_Group
        {
            get { return agroup; }
            set { SetValue(ref agroup, value); }
        }
        public string A_Type
        {
            get { return atype; }
            set { SetValue(ref atype, value); }
        }
        public bool A_ReExecution
        {
            get { return areexecution; }
            set { SetValue(ref areexecution, value); }
        }
        // B
        // C
        public ObservableCollection<CommandDataInterfaceItem> InterfaceItems
        {
            get { return interfaceitems; }
            set { SetValue(ref interfaceitems, value); }
        }
        // D
        //public bool D_Option_a_IsChecked
        //{
        //    get { return doptionaischecked; }
        //    set { SetValue(ref doptionaischecked, value); }
        //}
        //public bool D_Option_b_IsChecked
        //{
        //    get { return doptionbischecked; }
        //    set { SetValue(ref doptionbischecked, value); }
        //}
        //public bool D_Option_c_IsChecked
        //{
        //    get { return doptioncischecked; }
        //    set { SetValue(ref doptioncischecked, value); }
        //}
        public ObservableCollection<CommandDataOptionItem> OptionItems
        {
            get { return optionitems; }
            set { SetValue(ref optionitems, value); }
        }
        public DatePromptResult StartDateValue
        {
            get { return startdatevalue; }
            set { SetValue(ref startdatevalue, value); }
        }
        public DatePromptResult EndDateValue
        {
            get { return enddatevalue; }
            set { SetValue(ref enddatevalue, value); }
        }
        public string StartDateString
        {
            get { return startdatestring; }
            set { SetValue(ref startdatestring, value); }
        }
        public string EndDateString
        {
            get { return enddatestring; }
            set { SetValue(ref enddatestring, value); }
        }
        #endregion
        #region Properties Modules b c d e
        // b
        public string B_b_Command
        {
            get { return bbcommand; }
            set { SetValue(ref bbcommand, value); }
        }
        public string B_b_DefaultPath
        {
            get { return bbdefaultpath; }
            set { SetValue(ref bbdefaultpath, value); }
        }
        public string B_b_CurrentPath
        {
            get { return bbcurrentpath; }
            set { SetValue(ref bbcurrentpath, value); }
        }
        public bool B_b_HasSubProcess
        {
            get { return bbhassubprocess; }
            set { SetValue(ref bbhassubprocess, value); }
        }
        public string B_b_MonitoringService
        {
            get { return bbmonitoringservice; }
            set { SetValue(ref bbmonitoringservice, value); }
        }
        public bool B_b_IsUser
        {
            get { return bbisuser; }
            set { SetValue(ref bbisuser, value); }
        }
        public string B_b_User
        {
            get { return bbuser; }
            set { SetValue(ref bbuser, value); }
        }
        public bool B_b_IsSubProcess
        {
            get { return bbissubprocess; }
            set { SetValue(ref bbissubprocess, value); }
        }
        public string B_b_SubProcess
        {
            get { return bbsubprocess; }
            set { SetValue(ref bbsubprocess, value); }
        }
        public string B_b_MonitoringTime
        {
            get { return bbmonitoringtime; }
            set { SetValue(ref bbmonitoringtime, value); }
        }
        public bool B_b_WaitEndSubProcess
        {
            get { return bbwaitendsubprocess; }
            set { SetValue(ref bbwaitendsubprocess, value); }
        }
        public ObservableCollection<CommandDataProcessItem> ProcessItems
        {
            get { return processitems; }
            set { SetValue(ref processitems, value); }
        }
        // c
        public string C_c_Environment
        {
            get { return ccenvironment; }
            set { SetValue(ref ccenvironment, value); }
        }
        public string C_c_Patch
        {
            get { return ccpatch; }
            set { SetValue(ref ccpatch, value); }
        }
        public ObservableCollection<CommandDataOriginActionItem> OriginAcctionsItems
        {
            get { return originactionitems; }
            set { SetValue(ref originactionitems, value); }
        }
        public ObservableCollection<CommandDataDestinationItem> DestinationsItems
        {
            get { return destinationsitems; }
            set { SetValue(ref destinationsitems, value); }
        }
        public ObservableCollection<CommandDataDestinationActionItem> ActionsDestinationItems
        {
            get { return destinationactionitems; }
            set { SetValue(ref destinationactionitems, value); }
        }
        // d
        public string D_d_Environment
        {
            get { return ddenvironment; }
            set { SetValue(ref ddenvironment, value); }
        }
        public string D_d_CommandLine
        {
            get { return ddcommandline; }
            set { SetValue(ref ddcommandline, value); }
        }
        // e
        public ObservableCollection<CommandDataObservationItem> ObservationItems
        {
            get { return observationitems; }
            set { SetValue(ref observationitems, value); }
        }
        public ObservableCollection<CommandDataObservationDetailItem> ObservationDetailItems
        {
            get { return observationdetailitems; }
            set { SetValue(ref observationdetailitems, value); }
        }
        #endregion
        #endregion

        #region Constructors
        public CommandDataViewModel(bool IsReload)
        {
            if (IsReload)
            {
                GetToolBar();
                GetInitialData();
            }
        }
        #endregion

        #region Commands
        #region Commands ToolBar A B C D
        // A
        public ICommand Module_A_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_A_Click);
            }
        }

        private async void Module_A_Click()
        {
            await Task.Run(() => ActivatePrincipalToolBarButton("A"));
        }
        // B
        public ICommand Module_B_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_B_Click);
            }
        }

        private async void Module_B_Click()
        {
            await Task.Run(() => ActivatePrincipalToolBarButton("B"));
        }
        // C
        public ICommand Module_C_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_C_Click);
            }
        }

        private async void Module_C_Click()
        {
            await Task.Run(() => ActivatePrincipalToolBarButton("C"));
        }
        // D
        public ICommand Module_D_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_D_Click);
            }
        }

        private async void Module_D_Click()
        {
            await Task.Run(() => ActivatePrincipalToolBarButton("D"));
        }
        #endregion
        #region Command ToolBar a b c d e
        // a
        public ICommand Module_A_a_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_A_a_Click);
            }
        }

        private async void Module_A_a_Click()
        {
            await Task.Run(() => ActivateSecondaryToolBarButton("Aa"));
        }
        // b
        public ICommand Module_B_b_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_B_b_Click);
            }
        }

        private async void Module_B_b_Click()
        {
            await Task.Run(() => ActivateSecondaryToolBarButton("Bb"));
        }
        // c
        public ICommand Module_C_c_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_C_c_Click);
            }
        }

        private async void Module_C_c_Click()
        {
            await Task.Run(() => ActivateSecondaryToolBarButton("Cc"));
        }
        // d
        public ICommand Module_D_d_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_D_d_Click);
            }
        }

        private async void Module_D_d_Click()
        {
            await Task.Run(() => ActivateSecondaryToolBarButton("Dd"));
        }
        // e
        public ICommand Module_E_e_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_E_e_Click);
            }
        }

        private async void Module_E_e_Click()
        {
            await Task.Run(() => ActivateSecondaryToolBarButton("Ee"));
        }
        #endregion

        #region Module D
        public ICommand ClearCommand
        {
            get
            {
                return new RelayCommand(Clear);
            }
        }

        private async void Clear()
        {
            Alert.Show("LIMPIAR");
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private async void Search()
        {
            Alert.Show("BUSCAR");
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
        //public ICommand SaveCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(Save);
        //    }
        //}

        //private async void Save()
        //{
        //    try
        //    {
        //        if (SelectedOperator == null)
        //        {
        //            Alert.Show("Debe seleccionar un operador", "Aceptar");
        //            return;
        //        }
        //        if (string.IsNullOrWhiteSpace(OperatorPassword))
        //        {
        //            Alert.Show("Debe ingresar el password del operador", "Aceptar");
        //            return;
        //        }
        //        UserDialogs.Instance.ShowLoading("Validando operador...", MaskType.Black);
        //        ApiSrv = new Services.ApiService(ApiConsult.ApiAuth);
        //        if (!await ApiIsOnline())
        //        {
        //            UserDialogs.Instance.HideLoading();
        //            Toast.ShowError(AlertMessages.Error);
        //            return;
        //        }
        //        else
        //        {
        //            if (!await GetTokenSuccess())
        //            {
        //                UserDialogs.Instance.HideLoading();
        //                Toast.ShowError(AlertMessages.Error);
        //                return;
        //            }
        //            else
        //            {
        //                TokenPbAuth = TokenGet;
        //                LoginPb loginPb = new LoginPb { Username = this.SelectedOperator.IdUser, Password = this.OperatorPassword };
        //                Response resultValidateOperator = await ApiSrv.AuthenticateProbath(TokenPbAuth.Key, loginPb);
        //                if (!resultValidateOperator.IsSuccess)
        //                {
        //                    UserDialogs.Instance.HideLoading();
        //                    Toast.ShowError(resultValidateOperator.Message);
        //                    return;
        //                }
        //                else
        //                {
        //                    OperatorObject operatorObject = JsonConvert.DeserializeObject<OperatorObject>(Crypto.DecodeString(resultValidateOperator.Data));
        //                    if (!operatorObject.IsValid)
        //                    {
        //                        UserDialogs.Instance.HideLoading();
        //                        Alert.Show(AlertMessages.UserInvalid);
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        UserDialogs.Instance.HideLoading();
        //                        UserDialogs.Instance.ShowLoading("Validando instancias...", MaskType.Black);
        //                        ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
        //                        if (!TokenValidator.IsValid(TokenMenuB))
        //                        {
        //                            if (!await ApiIsOnline())
        //                            {
        //                                UserDialogs.Instance.HideLoading();
        //                                Toast.ShowError(AlertMessages.Error);
        //                                return;
        //                            }
        //                            else
        //                            {
        //                                if (!await GetTokenSuccess())
        //                                {
        //                                    UserDialogs.Instance.HideLoading();
        //                                    Toast.ShowError(AlertMessages.Error);
        //                                    return;
        //                                }
        //                                else
        //                                {
        //                                    TokenMenuB = TokenGet;
        //                                }
        //                            }
        //                        }
        //                        StringBuilder Instances = new StringBuilder();
        //                        StringBuilder InstancesRunning = new StringBuilder();
        //                        foreach (InstanceRunningItem instanceRunningItem in InstanceRunningItems)
        //                        {
        //                            Instances.Append(instanceRunningItem.IdInstance);
        //                            Instances.Append(",");
        //                            if (string.Compare(instanceRunningItem.RunningDisplay, "SI") == 0)
        //                            {
        //                                InstancesRunning.Append(instanceRunningItem.IdInstance);
        //                                InstancesRunning.Append(",");

        //                            }
        //                        }
        //                        if (!AppHelper.UserIsSupervisor(operatorObject))
        //                        {
        //                            OperatorChangeUserIsInAllInstancesQueryValues operatorChangeUserIsInAllInstancesQueryValues = new OperatorChangeUserIsInAllInstancesQueryValues()
        //                            {
        //                                IdLog = this.LogItem.IdLog,
        //                                IdUser = PbUser.IdUser,
        //                                Instances = Instances.ToString()
        //                            };
        //                            Response resultOperatorChangeUserIsInAllInstances = await ApiSrv.GetOperatorChangeUserIsInAllInstances(TokenMenuB.Key, operatorChangeUserIsInAllInstancesQueryValues);
        //                            if (!resultOperatorChangeUserIsInAllInstances.IsSuccess)
        //                            {
        //                                UserDialogs.Instance.HideLoading();
        //                                Toast.ShowError(AlertMessages.Error);
        //                                return;
        //                            }
        //                            else
        //                            {
        //                                UserInInstances = JsonConvert.DeserializeObject<List<OperatorChangeUserIsInAllInstances>>(Crypto.DecodeString(resultOperatorChangeUserIsInAllInstances.Data));
        //                                if (!UserInInstances[0].UserIsInAllInstances)
        //                                {
        //                                    UserDialogs.Instance.HideLoading();
        //                                    Alert.Show(string.Format("Usted no tiene permisos para ejecutar en todas las instancias que tiene asignada el operador: {0}", PbUser.IdUser), "Aceptar");
        //                                    return;
        //                                }
        //                            }
        //                        }
        //                        if (InstancesRunning.Length > 0)
        //                        {
        //                            UserDialogs.Instance.HideLoading();
        //                            UserDialogs.Instance.ShowLoading("Cambiando al operador seleccionado...", MaskType.Black);
        //                            ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
        //                            if (!TokenValidator.IsValid(TokenMenuB))
        //                            {
        //                                if (!await ApiIsOnline())
        //                                {
        //                                    UserDialogs.Instance.HideLoading();
        //                                    Toast.ShowError(AlertMessages.Error);
        //                                    return;
        //                                }
        //                                else
        //                                {
        //                                    if (!await GetTokenSuccess())
        //                                    {
        //                                        UserDialogs.Instance.HideLoading();
        //                                        Toast.ShowError(AlertMessages.Error);
        //                                        return;
        //                                    }
        //                                    else
        //                                    {
        //                                        TokenMenuB = TokenGet;
        //                                    }
        //                                }
        //                            }
        //                            //string MyIp;
        //                            //foreach (IPAddress iPAddress in Dns.GetHostAddresses(Dns.GetHostName()))
        //                            //{
        //                            //    MyIp = iPAddress.ToString();
        //                            //}
        //                            OperatorChangeQueryValues operatorChangeQueryValues = new OperatorChangeQueryValues()
        //                            {
        //                                IdLog = this.LogItem.IdLog,
        //                                NewIdUser = operatorObject.IdUser,
        //                                Instances = Instances.ToString(),
        //                                OldIdUser = PbUser.IdUser,
        //                                StartDate = ConvertStartDateToSp(this.LogItem.ExecutionDateTime),
        //                                ClientIp = "127.0.0.1"
        //                            };
        //                            Response resultOperatorChange = await ApiSrv.GetOperatorChange(TokenMenuB.Key, operatorChangeQueryValues);
        //                            if (!resultOperatorChange.IsSuccess)
        //                            {
        //                                UserDialogs.Instance.HideLoading();
        //                                Toast.ShowError(AlertMessages.Error);
        //                                return;
        //                            }
        //                            else
        //                            {
        //                                OperatorChanges = JsonConvert.DeserializeObject<List<OperatorChange>>(Crypto.DecodeString(resultOperatorChange.Data));
        //                                if (!OperatorChanges[0].IsSuccess)
        //                                {
        //                                    UserDialogs.Instance.HideLoading();
        //                                    Alert.Show(string.Format("Ocurrió un error al intentar cambiar el operador actual por: {0}", operatorObject.IdUser), "Aceptar");
        //                                    return;
        //                                }
        //                                this.SelectedOperator = null;
        //                                this.OperatorPassword = string.Empty;
        //                                UserDialogs.Instance.HideLoading();
        //                                Alert.Show(string.Format("Cambio de operador satisfactorio!"), "Aceptar");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            UserDialogs.Instance.HideLoading();
        //                            Alert.Show(string.Format("No hay instancias en ejecución!"), "Aceptar");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch //(Exception ex)
        //    {
        //        Toast.ShowError(AlertMessages.Error);
        //    }

        //}
        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            this.OptionItems = new ObservableCollection<CommandDataOptionItem>()
            {
                new CommandDataOptionItem()
                {
                    IdOption = 1,
                    OptionName = "Ejecuciones Exitosas"
                },
                new CommandDataOptionItem()
                {
                    IdOption = 2,
                    OptionName = "Detalle del Último Error"
                },
                new CommandDataOptionItem()
                {
                    IdOption = 3,
                    OptionName = "Detalle del Último Error"
                }
            };

            this.A_Code = "80051";
            this.A_Critical = false;
            this.A_Name = "MENSAJE DE HOLD DE BASE DE DATOS";
            this.A_Description = "MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS MENSAJE DE HOLD DE BASE DE DATOS";
            this.A_Group = "PROCESOS GENERALES";
            this.A_Type = "MENSAJE";
            this.A_ReExecution = true;

            this.B_b_Command = "SBMJOB";
            this.B_b_DefaultPath = "/";
            this.B_b_CurrentPath = "/";
            this.B_b_HasSubProcess = true;
            this.B_b_MonitoringService = "AS400";
            this.B_b_IsUser = false;
            this.B_b_User = string.Empty;
            this.B_b_IsSubProcess = true;
            this.B_b_SubProcess = string.Empty;
            this.B_b_MonitoringTime = "60";
            this.B_b_WaitEndSubProcess = true;

            this.ProcessItems = new ObservableCollection<CommandDataProcessItem>()
            {
                new CommandDataProcessItem()
                {
                    PID = 1010,
                    NameProcess = "DPD540CL"
                },
                new CommandDataProcessItem()
                {
                    PID = 1011,
                    NameProcess = "DPD560CL"
                },
                new CommandDataProcessItem()
                {
                    PID = 1011,
                    NameProcess = "DPD560CL"
                },
                new CommandDataProcessItem()
                {
                    PID = 1011,
                    NameProcess = "DPD560CL"
                },
                new CommandDataProcessItem()
                {
                    PID = 1011,
                    NameProcess = "DPD560CL"
                }
            };
        }
        //private async void GetInitialData()
        //{
        //    try
        //    {
        //        UserDialogs.Instance.ShowLoading("Obteniendo operadores...", MaskType.Black);
        //        ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
        //        if (!await ApiIsOnline())
        //        {
        //            UserDialogs.Instance.HideLoading();
        //            Toast.ShowError(AlertMessages.Error);
        //            return;
        //        }
        //        else
        //        {
        //            if (!await GetTokenSuccess())
        //            {
        //                UserDialogs.Instance.HideLoading();
        //                Toast.ShowError(AlertMessages.Error);
        //                return;
        //            }
        //            else
        //            {
        //                TokenMenuB = TokenGet;
        //                OperatorChangeUserQueryValues operatorChangeUserQueryValues = new OperatorChangeUserQueryValues()
        //                {
        //                    CurrentIdUser = PbUser.IdUser
        //                };
        //                Response resultOperatorChangeGetUsers = await ApiSrv.GetOperatorChangeUsers(TokenMenuB.Key, operatorChangeUserQueryValues);
        //                if (!resultOperatorChangeGetUsers.IsSuccess)
        //                {
        //                    UserDialogs.Instance.HideLoading();
        //                    Toast.ShowError(AlertMessages.Error);
        //                    return;
        //                }
        //                else
        //                {
        //                    Users = JsonConvert.DeserializeObject<List<OperatorChangeUser>>(Crypto.DecodeString(resultOperatorChangeGetUsers.Data));
        //                    OperatorsItems = new ObservableCollection<PickerOperatorItem>();
        //                    foreach (OperatorChangeUser operatorChangeUser in Users)
        //                    {
        //                        OperatorsItems.Add(new PickerOperatorItem()
        //                        {
        //                            IdUser = operatorChangeUser.IdUser,
        //                            FullNameUser = string.Format("{0}", operatorChangeUser.IdUser)
        //                            //string.Format("{0} ({1}, {2})", operatorChangeUser.IdUser, operatorChangeUser.FirstNameUser.Trim(), operatorChangeUser.LastNameUser.Trim())
        //                        });
        //                    }
        //                }
        //                UserDialogs.Instance.HideLoading();
        //                UserDialogs.Instance.ShowLoading("Obteniendo instancias...", MaskType.Black);
        //                OperatorChangeInstanceQueryValues operatorChangeInstanceQueryValues = new OperatorChangeInstanceQueryValues()
        //                {
        //                    IdLog = this.LogItem.IdLog,
        //                    IdUser = PbUser.IdUser,
        //                    IsSupervisor = AppHelper.UserIsSupervisor(PbUser)
        //                };
        //                Response resultOperatorChangeGetInstances = await ApiSrv.GetOperatorChangeInstances(TokenMenuB.Key, operatorChangeInstanceQueryValues);
        //                if (!resultOperatorChangeGetInstances.IsSuccess)
        //                {
        //                    UserDialogs.Instance.HideLoading();
        //                    Toast.ShowError(resultOperatorChangeGetInstances.Message);
        //                    return;
        //                }
        //                else
        //                {
        //                    Instances = JsonConvert.DeserializeObject<List<OperatorChangeInstance>>(Crypto.DecodeString(resultOperatorChangeGetInstances.Data));
        //                    InstanceRunningItems = new ObservableCollection<InstanceRunningItem>();
        //                    foreach (OperatorChangeInstance operatorChangeInstance in Instances)
        //                    {
        //                        InstanceRunningItems.Add(new InstanceRunningItem()
        //                        {
        //                            IdInstance = operatorChangeInstance.IdInstance,
        //                            NameInstance = operatorChangeInstance.NameInstance,
        //                            Running = operatorChangeInstance.Running,
        //                            RunningDisplay = (operatorChangeInstance.Running) ? Answers.Yes : Answers.No
        //                        });
        //                    }
        //                }
        //                UserDialogs.Instance.HideLoading();
        //            }
        //        }
        //    }
        //    catch //(Exception ex)
        //    {
        //        UserDialogs.Instance.HideLoading();
        //        Toast.ShowError(AlertMessages.Error);
        //    }
        //}

        //private async Task<bool> ApiIsOnline()
        //{
        //    bool result = false;
        //    Response resultApiIsAvailable = await ApiSrv.ApiIsAvailable();
        //    if (resultApiIsAvailable.IsSuccess)
        //    {
        //        result = true;
        //    }
        //    return result;
        //}

        //private async Task<bool> GetTokenSuccess()
        //{
        //    bool result = false;
        //    Response resultToken = await ApiSrv.GetToken();
        //    if (resultToken.IsSuccess)
        //    {
        //        TokenGet = JsonConvert.DeserializeObject<Token>(Crypto.DecodeString(resultToken.Data));
        //        result = true;
        //    }
        //    return result;
        //}

        //private string ConvertStartDateToSp(DateTime startDate)
        //{
        //    string result = string.Empty;
        //    if (startDate.Millisecond < 100)
        //    {
        //        if (startDate.Millisecond < 10)
        //        {
        //            result = String.Format("{0:yyyy/MM/dd}", startDate) + " " + startDate.ToString("HH:mm:ss") + ".00" + startDate.Millisecond;
        //        }
        //        else
        //        {
        //            result = String.Format("{0:yyyy/MM/dd}", startDate) + " " + startDate.ToString("HH:mm:ss") + ".0" + startDate.Millisecond;
        //        }
        //    }
        //    else
        //    {
        //        result = String.Format("{0:yyyy/MM/dd}", startDate) + " " + startDate.ToString("HH:mm:ss") + "." + startDate.Millisecond;
        //    }
        //    return result;
        //}

        private void GetToolBar()
        {
            #region ToolBar A B C D E
            // A
            this.A_IsActive = true;
            this.A_BackgroundColor = "#2255AA";
            this.A_Width = 200;
            this.A_ImageButtonWidth = 190;
            this.A_Text = "Información";
            this.A_FontColor = "White";
            this.A_Icon = "toolbar_selected";
            // B
            this.B_IsActive = false;
            this.B_BackgroundColor = "#D7D7D7";
            this.B_Width = 60;
            this.B_ImageButtonWidth = 35;
            this.B_Text = "Datos de la Ejecución";
            this.B_FontColor = "White";
            this.B_Icon = "toolbar_unselected";
            // C
            this.C_IsActive = false;
            this.C_BackgroundColor = "#D7D7D7";
            this.C_Width = 60;
            this.C_ImageButtonWidth = 35;
            this.C_Text = "Interfaces";
            this.C_FontColor = "White";
            this.C_Icon = "toolbar_unselected";
            // D
            this.D_IsActive = false;
            this.D_BackgroundColor = "#D7D7D7";
            this.D_Width = 60;
            this.D_ImageButtonWidth = 35;
            this.D_Text = "Historial de Ejecuciones";
            this.D_FontColor = "White";
            this.D_Icon = "toolbar_unselected";
            #endregion
            #region ToolBar a b c
            // a
            this.A_a_IsActive = true;
            this.A_a_BackgroundColor = "#2255AA";
            this.A_a_Width = 200;
            this.A_a_ImageButtonWidth = 190;
            this.A_a_Text = "General";
            this.A_a_FontColor = "White";
            this.A_a_Icon = "toolbar_selected";
            // b
            this.B_b_IsActive = false;
            this.B_b_BackgroundColor = "#D7D7D7";
            this.B_b_Width = 60;
            this.B_b_ImageButtonWidth = 35;
            this.B_b_Text = "Comando";
            this.B_b_FontColor = "White";
            this.B_b_Icon = "toolbar_unselected";
            // c
            this.C_c_IsActive = false;
            this.C_c_BackgroundColor = "#D7D7D7";
            this.C_c_Width = 60;
            this.C_c_ImageButtonWidth = 35;
            this.C_c_Text = "Transferencia";
            this.C_c_FontColor = "White";
            this.C_c_Icon = "toolbar_unselected";
            #endregion
            #region ToolBar d e
            // d
            this.D_d_IsActive = false;
            this.D_d_BackgroundColor = "#2255AA";
            this.D_d_Width = 200;
            this.D_d_ImageButtonWidth = 190;
            this.D_d_Text = "Ejecución";
            this.D_d_FontColor = "White";
            this.D_d_Icon = "toolbar_selected";
            // e
            this.E_e_IsActive = false;
            this.E_e_BackgroundColor = "#D7D7D7";
            this.E_e_Width = 60;
            this.E_e_ImageButtonWidth = 35;
            this.E_e_Text = "Observaciones del Operador";
            this.E_e_FontColor = "White";
            this.E_e_Icon = "toolbar_unselected";
            #endregion

            this.SelectedModule = this.A_Text;
            this.SelectedModule_Child = this.A_a_Text;
            this.ToolBar_A_Child = true;
        }

        private void ActivatePrincipalToolBarButton(string Module)
        {
            #region ToolBar A B C D E
            // A
            this.A_IsActive = false;
            this.A_BackgroundColor = "#D7D7D7";
            this.A_Width = 60;
            this.A_ImageButtonWidth = 35;
            this.A_FontColor = "White";
            this.A_Icon = "toolbar_unselected";
            // B
            this.B_IsActive = false;
            this.B_BackgroundColor = "#D7D7D7";
            this.B_Width = 60;
            this.B_ImageButtonWidth = 35;
            this.B_FontColor = "White";
            this.B_Icon = "toolbar_unselected";
            this.B_b_IsActive = false;
            // C
            this.C_IsActive = false;
            this.C_BackgroundColor = "#D7D7D7";
            this.C_Width = 60;
            this.C_ImageButtonWidth = 35;
            this.C_FontColor = "White";
            this.C_Icon = "toolbar_unselected";
            this.C_c_IsActive = false;
            // D
            this.D_IsActive = false;
            this.D_BackgroundColor = "#D7D7D7";
            this.D_Width = 60;
            this.D_ImageButtonWidth = 35;
            this.D_FontColor = "White";
            this.D_Icon = "toolbar_unselected";
            this.D_d_IsActive = false;
            #endregion
            #region Activate
            switch (Module)
            {
                case "A":
                    this.A_IsActive = true;
                    this.A_BackgroundColor = "#2255AA";
                    this.A_Width = 200;
                    this.A_ImageButtonWidth = 190;
                    this.A_FontColor = "White";
                    this.A_Icon = "toolbar_selected";
                    this.SelectedModule = this.A_Text;
                    this.SelectedModule_Child = string.Empty;
                    this.ToolBar_A_Child = true;
                    this.ToolBar_D_Child = false;
                    ActivateSecondaryToolBarButton("Aa");
                    break;
                case "B":
                    this.B_IsActive = true;
                    this.B_BackgroundColor = "#2255AA";
                    this.B_Width = 200;
                    this.B_ImageButtonWidth = 190;
                    this.B_FontColor = "White";
                    this.B_Icon = "toolbar_selected";
                    this.SelectedModule = this.B_Text;
                    this.SelectedModule_Child = string.Empty;
                    this.ToolBar_A_Child = false;
                    this.ToolBar_D_Child = true;
                    ActivateSecondaryToolBarButton("Dd");
                    break;
                case "C":
                    this.C_IsActive = true;
                    this.C_BackgroundColor = "#2255AA";
                    this.C_Width = 200;
                    this.C_ImageButtonWidth = 190;
                    this.C_FontColor = "White";
                    this.C_Icon = "toolbar_selected";
                    this.SelectedModule = this.C_Text;
                    this.SelectedModule_Child = string.Empty;
                    this.ToolBar_A_Child = false;
                    this.ToolBar_D_Child = false;
                    break;
                case "D":
                    this.D_IsActive = true;
                    this.D_BackgroundColor = "#2255AA";
                    this.D_Width = 200;
                    this.D_ImageButtonWidth = 190;
                    this.D_FontColor = "White";
                    this.D_Icon = "toolbar_selected";
                    this.SelectedModule = this.D_Text;
                    this.SelectedModule_Child = string.Empty;
                    this.ToolBar_A_Child = false;
                    this.ToolBar_D_Child = false;
                    break;
            }
            #endregion
        }

        private void ActivateSecondaryToolBarButton(string Module)
        {
            #region ToolBar a b c d e
            this.A_IsActive = false;
            // a
            this.A_a_IsActive = false;
            this.A_a_BackgroundColor = "#D7D7D7";
            this.A_a_Width = 60;
            this.A_a_ImageButtonWidth = 35;
            this.A_a_FontColor = "White";
            this.A_a_Icon = "toolbar_unselected";
            // b
            this.B_b_IsActive = false;
            this.B_b_BackgroundColor = "#D7D7D7";
            this.B_b_Width = 60;
            this.B_b_ImageButtonWidth = 35;
            this.B_b_FontColor = "White";
            this.B_b_Icon = "toolbar_unselected";
            // c
            this.C_c_IsActive = false;
            this.C_c_BackgroundColor = "#D7D7D7";
            this.C_c_Width = 60;
            this.C_c_ImageButtonWidth = 35;
            this.C_c_FontColor = "White";
            this.C_c_Icon = "toolbar_unselected";
            // d
            this.D_d_IsActive = false;
            this.D_d_BackgroundColor = "#D7D7D7";
            this.D_d_Width = 60;
            this.D_d_ImageButtonWidth = 35;
            this.D_d_FontColor = "White";
            this.D_d_Icon = "toolbar_unselected";
            // e
            this.E_e_IsActive = false;
            this.E_e_BackgroundColor = "#D7D7D7";
            this.E_e_Width = 60;
            this.E_e_ImageButtonWidth = 35;
            this.E_e_FontColor = "White";
            this.E_e_Icon = "toolbar_unselected";
            #endregion
            #region Activate
            switch (Module)
            {
                case "Aa":
                    this.A_IsActive = true;
                    this.A_a_IsActive = true;
                    this.A_a_BackgroundColor = "#2255AA";
                    this.A_a_Width = 200;
                    this.A_a_ImageButtonWidth = 190;
                    this.A_a_FontColor = "White";
                    this.A_a_Icon = "toolbar_selected";
                    this.SelectedModule_Child = this.A_a_Text;
                    break;
                case "Bb":
                    this.B_b_IsActive = true;
                    this.B_b_BackgroundColor = "#2255AA";
                    this.B_b_Width = 200;
                    this.B_b_ImageButtonWidth = 190;
                    this.B_b_FontColor = "White";
                    this.B_b_Icon = "toolbar_selected";
                    this.SelectedModule_Child = this.B_b_Text;
                    break;
                case "Cc":
                    this.C_c_IsActive = true;
                    this.C_c_BackgroundColor = "#2255AA";
                    this.C_c_Width = 200;
                    this.C_c_ImageButtonWidth = 190;
                    this.C_c_FontColor = "White";
                    this.C_c_Icon = "toolbar_selected";
                    this.SelectedModule_Child = this.C_c_Text;
                    break;
                case "Dd":
                    this.D_d_IsActive = true;
                    this.D_d_BackgroundColor = "#2255AA";
                    this.D_d_Width = 200;
                    this.D_d_ImageButtonWidth = 190;
                    this.D_d_FontColor = "White";
                    this.D_d_Icon = "toolbar_selected";
                    this.SelectedModule_Child = this.D_d_Text;
                    break;
                case "Ee":
                    this.E_e_IsActive = true;
                    this.E_e_BackgroundColor = "#2255AA";
                    this.E_e_Width = 200;
                    this.E_e_ImageButtonWidth = 190;
                    this.E_e_FontColor = "White";
                    this.E_e_Icon = "toolbar_selected";
                    this.SelectedModule_Child = this.E_e_Text;
                    break;
            }
            #endregion
        }
        #endregion
    }
}
