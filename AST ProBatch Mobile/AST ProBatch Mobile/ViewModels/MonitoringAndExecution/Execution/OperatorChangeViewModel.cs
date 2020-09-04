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
using ASTProBatchMobile.Models.Service;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using OperatorObject = AST_ProBatch_Mobile.Models.PbUser;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class OperatorChangeViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<InstanceRunningItem> instancerunningitems;
        private ObservableCollection<PickerOperatorItem> operatorsitems;
        private LogItem logitem;
        private PickerOperatorItem selectedoperator;
        private bool isloadingdata;
        public string operatorpassword;
        #endregion

        #region Properties
        private List<OperatorChangeUser> Users { get; set; }
        private List<OperatorChangeInstance> Instances { get; set; }
        private List<OperatorChangeUserIsInAllInstances> UserInInstances { get; set; }
        private Token TokenGet { get; set; }
        private Token TokenPbAuth { get; set; }
        private Token TokenMenuB { get; set; }
        private Response ApiResponse { get; set; }

        public PickerOperatorItem SelectedOperator
        {
            get { return selectedoperator; }
            set
            {
                SetValue(ref selectedoperator, value);
            }
        }
        public ObservableCollection<InstanceRunningItem> InstanceRunningItems
        {
            get { return instancerunningitems; }
            set { SetValue(ref instancerunningitems, value); }
        }
        public ObservableCollection<PickerOperatorItem> OperatorsItems
        {
            get { return operatorsitems; }
            set { SetValue(ref operatorsitems, value); }
        }
        public LogItem LogItem
        {
            get { return logitem; }
            set { SetValue(ref logitem, value); }
        }
        public string OperatorPassword
        {
            get { return operatorpassword; }
            set { SetValue(ref operatorpassword, value); }
        }
        public bool IsLoadingData
        {
            get { return isloadingdata; }
            set { SetValue(ref isloadingdata, value); }
        }
        #endregion

        #region Constructors
        public OperatorChangeViewModel(bool IsReload, LogItem logItem)
        {
            if (IsReload)
            {
                this.LogItem = logItem;
                GetInitialData();
                //GetFakeData();
            }
        }
        #endregion

        #region Commands
        public ICommand NewCommand
        {
            get
            {
                return new RelayCommand(New);
            }
        }

        private async void New()
        {
            this.SelectedOperator = null;
            this.OperatorPassword = string.Empty;
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            try
            {
                if (SelectedOperator == null)
                {
                    Alert.Show("Debe seleccionar un operador", "Aceptar");
                    return;
                }
                if (string.IsNullOrWhiteSpace(OperatorPassword))
                {
                    Alert.Show("Debe ingresar el password del operador", "Aceptar");
                    return;
                }
                UserDialogs.Instance.ShowLoading("Validando operador...", MaskType.Black);
                ApiSrv = new Services.ApiService(ApiConsult.ApiAuth);
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
                        TokenPbAuth = TokenGet;
                        LoginPb loginPb = new LoginPb { Username = this.SelectedOperator.IdUser, Password = this.OperatorPassword };
                        Response resultValidateOperator = await ApiSrv.AuthenticateProbath(TokenPbAuth.Key, loginPb);
                        if (!resultValidateOperator.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(resultValidateOperator.Message);
                            return;
                        }
                        else
                        {
                            OperatorObject operatorObject = JsonConvert.DeserializeObject<OperatorObject>(Crypto.DecodeString(resultValidateOperator.Data));
                            if (!operatorObject.IsValid)
                            {
                                UserDialogs.Instance.HideLoading();
                                Alert.Show(AlertMessages.UserInvalid);
                                return;
                            }
                            else
                            {
                                UserDialogs.Instance.HideLoading();
                                UserDialogs.Instance.ShowLoading("Validando instancias...", MaskType.Black);
                                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                                if (!TokenValidator.IsValid(TokenMenuB))
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
                                        else
                                        {
                                            TokenMenuB = TokenGet;
                                        }
                                    }
                                }
                                StringBuilder Instances = new StringBuilder();
                                StringBuilder InstancesRunning = new StringBuilder();
                                foreach (InstanceRunningItem instanceRunningItem in InstanceRunningItems)
                                {
                                    Instances.Append(instanceRunningItem.IdInstance);
                                    Instances.Append(",");
                                    if (string.Compare(instanceRunningItem.RunningDisplay, "SI") == 0)
                                    {
                                        InstancesRunning.Append(instanceRunningItem.IdInstance);
                                        InstancesRunning.Append(",");
                                        
                                    }
                                }
                                if (!AppHelper.UserIsSupervisor(operatorObject))
                                {
                                    OperatorChangeUserIsInAllInstancesQueryValues operatorChangeUserIsInAllInstancesQueryValues = new OperatorChangeUserIsInAllInstancesQueryValues()
                                    {
                                        IdLog = this.LogItem.IdLog,
                                        IdUser = PbUser.IdUser,
                                        Instances = Instances.ToString()
                                    };
                                    Response resultOperatorChangeUserIsInAllInstances = await ApiSrv.GetOperatorChangeUserIsInAllInstances(TokenMenuB.Key, operatorChangeUserIsInAllInstancesQueryValues);
                                    if (!resultOperatorChangeUserIsInAllInstances.IsSuccess)
                                    {
                                        UserDialogs.Instance.HideLoading();
                                        Toast.ShowError(AlertMessages.Error);
                                        return;
                                    }
                                    else
                                    {
                                        UserInInstances = JsonConvert.DeserializeObject<List<OperatorChangeUserIsInAllInstances>>(Crypto.DecodeString(resultOperatorChangeUserIsInAllInstances.Data));
                                        if (!UserInInstances[0].UserIsInAllInstances)
                                        {
                                            UserDialogs.Instance.HideLoading();
                                            Alert.Show(string.Format("Usted no tiene permisos para ejecutar en todas las instancias que tiene asignada el operador: {0}", PbUser.IdUser), "Aceptar");
                                            return;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                Toast.ShowError(AlertMessages.Error);
            }

        }
        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo operadores...", MaskType.Black);
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
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
                        TokenMenuB = TokenGet;
                        OperatorChangeUserQueryValues operatorChangeUserQueryValues = new OperatorChangeUserQueryValues()
                        {
                            CurrentIdUser = PbUser.IdUser
                        };
                        Response resultOperatorChangeGetUsers = await ApiSrv.GetOperatorChangeUsers(TokenMenuB.Key, operatorChangeUserQueryValues);
                        if (!resultOperatorChangeGetUsers.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            Users = JsonConvert.DeserializeObject<List<OperatorChangeUser>>(Crypto.DecodeString(resultOperatorChangeGetUsers.Data));
                            OperatorsItems = new ObservableCollection<PickerOperatorItem>();
                            foreach (OperatorChangeUser operatorChangeUser in Users)
                            {
                                OperatorsItems.Add(new PickerOperatorItem()
                                {
                                    IdUser = operatorChangeUser.IdUser,
                                    FullNameUser = string.Format("{0}", operatorChangeUser.IdUser)
                                    //string.Format("{0} ({1}, {2})", operatorChangeUser.IdUser, operatorChangeUser.FirstNameUser.Trim(), operatorChangeUser.LastNameUser.Trim())
                                });
                            }
                        }
                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.ShowLoading("Obteniendo instancias...", MaskType.Black);
                        OperatorChangeInstanceQueryValues operatorChangeInstanceQueryValues = new OperatorChangeInstanceQueryValues()
                        {
                            IdLog = this.LogItem.IdLog,
                            IdUser = PbUser.IdUser,
                            IsSupervisor = AppHelper.UserIsSupervisor(PbUser)
                        };
                        Response resultOperatorChangeGetInstances = await ApiSrv.GetOperatorChangeInstances(TokenMenuB.Key, operatorChangeInstanceQueryValues);
                        if (!resultOperatorChangeGetInstances.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(resultOperatorChangeGetInstances.Message);
                            return;
                        }
                        else
                        {
                            Instances = JsonConvert.DeserializeObject<List<OperatorChangeInstance>>(Crypto.DecodeString(resultOperatorChangeGetInstances.Data));
                            InstanceRunningItems = new ObservableCollection<InstanceRunningItem>();
                            foreach (OperatorChangeInstance operatorChangeInstance in Instances)
                            {
                                InstanceRunningItems.Add(new InstanceRunningItem()
                                {
                                    IdInstance = operatorChangeInstance.IdInstance,
                                    NameInstance = operatorChangeInstance.NameInstance,
                                    Running = operatorChangeInstance.Running,
                                    RunningDisplay = (operatorChangeInstance.Running) ? Answers.Yes : Answers.No
                                });
                            }
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

        #region Fake Data
        //private void GetFakeData()
        //{
        //    InstanceRunningItems = new ObservableCollection<InstanceRunningItem>();
        //    InstanceRunningItem instanceRunningItem;

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 1;
        //    instanceRunningItem.Title = "INSTANCIA 1";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 2;
        //    instanceRunningItem.Title = "INSTANCIA 2";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 3;
        //    instanceRunningItem.Title = "INSTANCIA 3";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 4;
        //    instanceRunningItem.Title = "INSTANCIA 4";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 5;
        //    instanceRunningItem.Title = "INSTANCIA 5";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 6;
        //    instanceRunningItem.Title = "INSTANCIA 6";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 7;
        //    instanceRunningItem.Title = "INSTANCIA 7";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 8;
        //    instanceRunningItem.Title = "INSTANCIA 8";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 9;
        //    instanceRunningItem.Title = "INSTANCIA 9";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    instanceRunningItem = new InstanceRunningItem();
        //    instanceRunningItem.Id = 10;
        //    instanceRunningItem.Title = "INSTANCIA 10";
        //    instanceRunningItem.IsRunning = "SI";
        //    InstanceRunningItems.Add(instanceRunningItem);

        //    OperatorsItems = new ObservableCollection<PickerOperatorItem>();
        //    PickerOperatorItem operatorItem;

        //    operatorItem = new PickerOperatorItem();
        //    operatorItem.Id = 1;
        //    operatorItem.OperatorName = "ADMINISTRADOR WEB";
        //    OperatorsItems.Add(operatorItem);

        //    operatorItem = new PickerOperatorItem();
        //    operatorItem.Id = 2;
        //    operatorItem.OperatorName = "PLANIFICADOR";
        //    OperatorsItems.Add(operatorItem);

        //    operatorItem = new PickerOperatorItem();
        //    operatorItem.Id = 3;
        //    operatorItem.OperatorName = "SUPERVISOR";
        //    OperatorsItems.Add(operatorItem);

        //    operatorItem = new PickerOperatorItem();
        //    operatorItem.Id = 4;
        //    operatorItem.OperatorName = "TESTER";
        //    OperatorsItems.Add(operatorItem);

        //    operatorItem = new PickerOperatorItem();
        //    operatorItem.Id = 5;
        //    operatorItem.OperatorName = "ADMINISTRADOR";
        //    OperatorsItems.Add(operatorItem);

        //    operatorItem = new PickerOperatorItem();
        //    operatorItem.Id = 6;
        //    operatorItem.OperatorName = "USUARIO DE SEGURIDAD";
        //    OperatorsItems.Add(operatorItem);

        //    operatorItem = new PickerOperatorItem();
        //    operatorItem.Id = 7;
        //    operatorItem.OperatorName = "USUARIO ANONIMO";
        //    OperatorsItems.Add(operatorItem);
        //}
        #endregion
    }
}
