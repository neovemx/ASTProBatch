using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class CommandDataViewModel : BaseViewModel
    {
        #region Atributes
        //private ObservableCollection<InstanceRunningItem> instancerunningitems;
        //private ObservableCollection<PickerOperatorItem> operatorsitems;
        //private LogItem logitem;
        //private PickerOperatorItem selectedoperator;
        //private bool isloadingdata;
        public string selectedmodule;

        public bool aisactive;
        public string abackgroundcolor;
        public int awidth;
        public int aimagebuttonwidth;
        public string atext;
        public string afontcolor;
        public string aicon;

        public bool bisactive;
        public string bbackgroundcolor;
        public int bwidth;
        public int bimagebuttonwidth;
        public string btext;
        public string bfontcolor;
        public string bicon;

        public bool cisactive;
        public string cbackgroundcolor;
        public int cwidth;
        public int cimagebuttonwidth;
        public string ctext;
        public string cfontcolor;
        public string cicon;

        public bool disactive;
        public string dbackgroundcolor;
        public int dwidth;
        public int dimagebuttonwidth;
        public string dtext;
        public string dfontcolor;
        public string dicon;

        public bool eisactive;
        public string ebackgroundcolor;
        public int ewidth;
        public int eimagebuttonwidth;
        public string etext;
        public string efontcolor;
        public string eicon;
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
        #region Button Module A
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
        #endregion
        #region Button Module B
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
        #endregion
        #region Button Module C
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
        #endregion
        #region Button Module D
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
        #region Button Module E
        public bool E_IsActive
        {
            get { return eisactive; }
            set { SetValue(ref eisactive, value); }
        }
        public string E_BackgroundColor
        {
            get { return ebackgroundcolor; }
            set { SetValue(ref ebackgroundcolor, value); }
        }
        public int E_Width
        {
            get { return ewidth; }
            set { SetValue(ref ewidth, value); }
        }
        public int E_ImageButtonWidth
        {
            get { return eimagebuttonwidth; }
            set { SetValue(ref eimagebuttonwidth, value); }
        }
        public string E_Text
        {
            get { return etext; }
            set { SetValue(ref etext, value); }
        }
        public string E_FontColor
        {
            get { return efontcolor; }
            set { SetValue(ref efontcolor, value); }
        }
        public string E_Icon
        {
            get { return eicon; }
            set { SetValue(ref eicon, value); }
        }
        #endregion

        #region Constructors
        public CommandDataViewModel(bool IsReload)
        {
            if (IsReload)
            {
                GetToolBar();
                //this.LogItem = logItem;
                //GetInitialData();
                //GetFakeData();
            }
        }
        #endregion

        #region Commands
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

        public ICommand Module_E_ClickCommand
        {
            get
            {
                return new RelayCommand(Module_E_Click);
            }
        }

        private async void Module_E_Click()
        {
            await Task.Run(() => ActivatePrincipalToolBarButton("E"));
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

        private void GetToolBar()
        {
            this.A_IsActive = true;
            this.A_BackgroundColor = "#2255AA";
            this.A_Width = 200;
            this.A_ImageButtonWidth = 190;
            this.A_Text = "Información";
            this.A_FontColor = "White";
            this.A_Icon = "check";

            this.B_IsActive = false;
            this.B_BackgroundColor = "#D7D7D7";
            this.B_Width = 60;
            this.B_ImageButtonWidth = 35;
            this.B_Text = "Datos de la Ejecución";
            this.B_FontColor = "White";
            this.B_Icon = "uncheck";

            this.C_IsActive = false;
            this.C_BackgroundColor = "#D7D7D7";
            this.C_Width = 60;
            this.C_ImageButtonWidth = 35;
            this.C_Text = "Interfaces";
            this.C_FontColor = "White";
            this.C_Icon = "uncheck";

            this.D_IsActive = false;
            this.D_BackgroundColor = "#D7D7D7";
            this.D_Width = 60;
            this.D_ImageButtonWidth = 35;
            this.D_Text = "Acciones sobre Resultados";
            this.D_FontColor = "White";
            this.D_Icon = "uncheck";

            this.E_IsActive = false;
            this.E_BackgroundColor = "#D7D7D7";
            this.E_Width = 60;
            this.E_ImageButtonWidth = 35;
            this.E_Text = "Historial de Ejecuciones";
            this.E_FontColor = "White";
            this.E_Icon = "uncheck";

            this.SelectedModule = this.A_Text;
        }

        private void ActivatePrincipalToolBarButton(string Module)
        {
            this.A_IsActive = false;
            this.A_BackgroundColor = "#D7D7D7";
            this.A_Width = 60;
            this.A_ImageButtonWidth = 35;
            this.A_FontColor = "White";
            this.A_Icon = "uncheck";

            this.B_IsActive = false;
            this.B_BackgroundColor = "#D7D7D7";
            this.B_Width = 60;
            this.B_ImageButtonWidth = 35;
            this.B_FontColor = "White";
            this.B_Icon = "uncheck";

            this.C_IsActive = false;
            this.C_BackgroundColor = "#D7D7D7";
            this.C_Width = 60;
            this.C_ImageButtonWidth = 35;
            this.C_FontColor = "White";
            this.C_Icon = "uncheck";

            this.D_IsActive = false;
            this.D_BackgroundColor = "#D7D7D7";
            this.D_Width = 60;
            this.D_ImageButtonWidth = 35;
            this.D_FontColor = "White";
            this.D_Icon = "uncheck";

            this.E_IsActive = false;
            this.E_BackgroundColor = "#D7D7D7";
            this.E_Width = 60;
            this.E_ImageButtonWidth = 35;
            this.E_FontColor = "White";
            this.E_Icon = "uncheck";

            switch (Module)
            {
                case "A":
                    this.A_IsActive = true;
                    this.A_BackgroundColor = "#2255AA";
                    this.A_Width = 200;
                    this.A_ImageButtonWidth = 190;
                    this.A_FontColor = "White";
                    this.A_Icon = "check";
                    this.SelectedModule = this.A_Text;
                    break;
                case "B":
                    this.B_IsActive = true;
                    this.B_BackgroundColor = "#2255AA";
                    this.B_Width = 200;
                    this.B_ImageButtonWidth = 190;
                    this.B_FontColor = "White";
                    this.B_Icon = "check";
                    this.SelectedModule = this.B_Text;
                    break;
                case "C":
                    this.C_IsActive = true;
                    this.C_BackgroundColor = "#2255AA";
                    this.C_Width = 200;
                    this.C_ImageButtonWidth = 190;
                    this.C_FontColor = "White";
                    this.C_Icon = "check";
                    this.SelectedModule = this.C_Text;
                    break;
                case "D":
                    this.D_IsActive = true;
                    this.D_BackgroundColor = "#2255AA";
                    this.D_Width = 200;
                    this.D_ImageButtonWidth = 190;
                    this.D_FontColor = "White";
                    this.D_Icon = "check";
                    this.SelectedModule = this.D_Text;
                    break;
                case "E":
                    this.E_IsActive = true;
                    this.E_BackgroundColor = "#2255AA";
                    this.E_Width = 200;
                    this.E_ImageButtonWidth = 190;
                    this.E_FontColor = "White";
                    this.E_Icon = "check";
                    this.SelectedModule = this.E_Text;
                    break;
            }
        }
        #endregion

        #region Helpers
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
        #endregion
    }
}
