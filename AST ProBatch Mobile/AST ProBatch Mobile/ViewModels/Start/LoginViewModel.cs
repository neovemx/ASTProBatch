using System.Windows.Input;
using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using AST_ProBatch_Mobile.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Atributes
        private string username;
        private string password;
        //private PbUser pbuser;
        private string urldomain;
        private string urlprefix;
        private bool ischecked;
        private bool isvisiblelogin;
        private bool isvisiblefingerprint;
        private bool isfingerprintavailable;
        public bool uiisvisible;
        public bool uierrorisvisible;
        public bool isenabled;
        #endregion

        #region Properties
        public string Username
        {
            get { return username; }
            set { SetValue(ref username, value); }
        }
        public string Password
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }
        //public PbUser PbUser
        //{
        //    get { return pbuser; }
        //    set { SetValue(ref pbuser, value); }
        //}
        public bool UIIsVisible
        {
            get { return uiisvisible; }
            set { SetValue(ref uiisvisible, value); }
        }
        public bool UIErrorIsVisible
        {
            get { return uierrorisvisible; }
            set { SetValue(ref uierrorisvisible, value); }
        }
        public string UrlDomain
        {
            get { return urldomain; }
            set { SetValue(ref urldomain, value); }
        }
        public string UrlPrefix
        {
            get { return urlprefix; }
            set { SetValue(ref urlprefix, value); }
        }
        public bool IsChecked
        {
            get { return ischecked; }
            set { SetValue(ref ischecked, value); }
        }

        public bool IsVisibleLogin
        {
            get { return isvisiblelogin; }
            set { SetValue(ref isvisiblelogin, value); }
        }
        public bool IsVisibleFingerPrint
        {
            get { return isvisiblefingerprint; }
            set { SetValue(ref isvisiblefingerprint, value); }
        }
        public bool IsFingerPrintAvailable
        {
            get { return isfingerprintavailable; }
            set { SetValue(ref isfingerprintavailable, value); }
        }

        public bool IsEnabled
        {
            get { return isenabled; }
            set { SetValue(ref isenabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel(InitialLoad initialLoad, Table_Config table_Config)
        {
            DBHelper = new Services.DataHelper();

            GetFingerPrintAvailable();

            if (initialLoad.IsSuccess)
            {
                if (initialLoad.HasConfigData)
                {
                    #region Set UI & Global Data
                    this.UIIsVisible = true;
                    this.UIErrorIsVisible = false;
                    this.IsChecked = table_Config.FingerPrintAllow;
                    this.UrlDomain = table_Config.UrlDomain;
                    this.UrlPrefix = table_Config.UrlPrefix;
                    this.IsEnabled = true;
                    #endregion

                    if (IsChecked)
                    {
                        this.IsVisibleFingerPrint = true;
                        this.IsVisibleLogin = false;
                    }
                    else
                    {
                        this.IsVisibleFingerPrint = false;
                        this.IsVisibleLogin = true;
                    }
                }
                else
                {
                    Alert.Show("Aplicación sin configuración!");
                }
            }
            else
            {
                this.UIIsVisible = false;
                this.UIErrorIsVisible = true;
            }
        }
        #endregion

        #region Commands
        public ICommand SettingsCommand
        {
            get
            {
                return new RelayCommand(Settings);
            }
        }

        private async void Settings()
        {
            MainViewModel.GetInstance().Settings = new SettingsViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new SettingsPage());
        }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Username))
            {
                Alert.Show("Debe ingresar un usuario");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                Alert.Show("Debe ingresar una contraseña");
                return;
            }

            if (IsChecked)
            {
                if (!IsFingerPrintAvailable)
                {
                    Alert.Show("Sensor de huella no disponible o no configurado.");
                    IsChecked = false;
                    return;
                }
                else
                {
                    var result = await CrossFingerprint.Current.AuthenticateAsync("Toque el sensor");
                    if (!result.Authenticated)
                    {
                        this.Username = string.Empty;
                        this.Password = string.Empty;
                        IsChecked = false;
                        return;
                    }
                }
            }

            ApiSrv = new Services.ApiService(ApiConsult.ApiAuth);
            RefreshAppConfig();

            try
            {
                UserDialogs.Instance.ShowLoading("Iniciando sesión...", MaskType.Black);

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
                    LoginPb loginPb = new LoginPb { Username = this.Username, Password = this.Password };
                    Response resultLoginProbatch = await ApiSrv.AuthenticateProbath(token.Key, loginPb);
                    if (!resultLoginProbatch.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(resultLoginProbatch.Message);
                        return;
                    }
                    else
                    {
                        PbUser = JsonConvert.DeserializeObject<PbUser>(Crypto.DecodeString(resultLoginProbatch.Data));
                        if (!PbUser.IsValid)
                        {
                            UserDialogs.Instance.HideLoading();
                            //Alert.Show("Usuario y/o Password incorrectos");
                            this.IsEnabled = false;
                            this.IsEnabled = true;
                            return;
                        }
                        else
                        {
                            if (IsChecked)
                            {
                                Table_Config table_Config = new Table_Config { Id = 1, UrlDomain = this.UrlDomain, UrlPrefix = this.UrlPrefix, FingerPrintAllow = this.IsChecked };
                                if (!await DBHelper.PullAsyncAppConfig(table_Config))
                                {
                                    UserDialogs.Instance.HideLoading();
                                    Toast.ShowError("No se pudo actualizar la configuración.");
                                    return;
                                }
                                else
                                {
                                    Table_User table_User = new Table_User { Id = 1, Username = Crypto.EncryptString(this.Username), Password = Crypto.EncryptString(this.Password) };
                                    if (!await DBHelper.PullAsyncProbatchCredentials(table_User))
                                    {
                                        UserDialogs.Instance.HideLoading();
                                        Toast.ShowError("No se pudo actualizar la configuración.");
                                        return;
                                    }
                                    else
                                    {
                                        IsVisibleLogin = false;
                                        IsVisibleFingerPrint = true;
                                    }
                                }
                            }

                            UserDialogs.Instance.HideLoading();
                            this.Username = string.Empty;
                            this.Password = string.Empty;
                            MainViewModel.GetInstance().Home = new HomeViewModel();
                            Application.Current.MainPage = new NavigationPage(new HomePage());
                            Alert.Show("Bienvenido: " + PbUser.FisrtName.Trim() + ", " + PbUser.LastName.Trim() + "!", "Continuar");
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError("Ocurrió un error.");
                return;
            }
        }

        public ICommand FingerPrintCommand
        {
            get
            {
                return new RelayCommand(FingerPrint);
            }
        }

        private async void FingerPrint()
        {
            try
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiAuth);
                RefreshAppConfig();

                var result = await CrossFingerprint.Current.AuthenticateAsync("Toque el sensor");
                if (!result.Authenticated)
                {
                    return;
                }

                UserDialogs.Instance.ShowLoading("Iniciando sesión...", MaskType.Black);

                Table_User tableUserFingerPrint = await DBHelper.GetAsyncProbatchCredentials();
                if (tableUserFingerPrint == null)
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError("No se pudo obtener las credenciales de ProBatch");
                    return;
                }

                try
                {
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
                        LoginPb loginPb = new LoginPb { Username = Crypto.DecodeString(tableUserFingerPrint.Username), Password = Crypto.DecodeString(tableUserFingerPrint.Password) };
                        Response resultLoginProbatch = await ApiSrv.AuthenticateProbath(token.Key, loginPb);
                        if (!resultLoginProbatch.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(resultLoginProbatch.Message);
                            return;
                        }
                        else
                        {
                            PbUser = JsonConvert.DeserializeObject<PbUser>(Crypto.DecodeString(resultLoginProbatch.Data));
                            if (!PbUser.IsValid)
                            {
                                UserDialogs.Instance.HideLoading();
                                bool deleteFingerPrintRegister = await UserDialogs.Instance.ConfirmAsync("Credenciales inválidas, deseas eliminar el registro biométrico?", "AST●ProBatch®", "Si", "No");
                                if (deleteFingerPrintRegister)
                                {
                                    IsChecked = false;
                                    Table_Config table_Config = new Table_Config { Id = 1, UrlDomain = this.UrlDomain, UrlPrefix = this.UrlPrefix, FingerPrintAllow = IsChecked };
                                    if (!await DBHelper.PullAsyncAppConfig(table_Config))
                                    {
                                        Toast.ShowError("No se pudo actualizar la configuración.");
                                        return;
                                    }
                                    else
                                    {
                                        Table_User table_User = new Table_User { Id = 1, Username = string.Empty, Password = string.Empty };
                                        if (!await DBHelper.PullAsyncProbatchCredentials(table_User))
                                        {
                                            Toast.ShowError("No se pudo actualizar la configuración.");
                                            return;
                                        }
                                        else
                                        {
                                            IsVisibleLogin = true;
                                            IsVisibleFingerPrint = false;
                                        }
                                    }
                                    return;
                                }
                            }
                            else
                            {
                                if (!IsChecked)
                                {
                                    Table_Config table_Config = new Table_Config { Id = 1, UrlDomain = this.UrlDomain, UrlPrefix = this.UrlPrefix, FingerPrintAllow = this.IsChecked };
                                    if (!await DBHelper.PullAsyncAppConfig(table_Config))
                                    {
                                        Toast.ShowError("No se pudo actualizar la configuración.");
                                        return;
                                    }
                                    else
                                    {
                                        Table_User table_User = new Table_User { Id = 1, Username = string.Empty, Password = string.Empty };
                                        if (!await DBHelper.PullAsyncProbatchCredentials(table_User))
                                        {
                                            Toast.ShowError("No se pudo actualizar la configuración.");
                                            return;
                                        }
                                        else
                                        {
                                            IsVisibleLogin = true;
                                            IsVisibleFingerPrint = false;
                                        }
                                    }
                                }

                                UserDialogs.Instance.HideLoading();
                                this.Username = string.Empty;
                                this.Password = string.Empty;
                                MainViewModel.GetInstance().Home = new HomeViewModel();
                                Application.Current.MainPage = new NavigationPage(new HomePage());
                                Alert.Show("Bienvenido: " + PbUser.FisrtName.Trim() + ", " + PbUser.LastName.Trim() + "!", "Continuar");
                            }
                        }
                    }
                }
                catch //(Exception ex)
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError("Ocurrió un error.");
                    return;
                }
            }
            catch //(Exception ex)
            {
                Toast.ShowError("Ocurrió un error datos locales.");
                return;
            }
        }
        #endregion

        #region Helpers
        private async void RefreshAppConfig()
        {
            Table_Config table_Config = await DBHelper.GetAsyncAppConfig();
            if (table_Config != null)
            {
                this.UrlDomain = table_Config.UrlDomain;
                this.UrlPrefix = table_Config.UrlPrefix;
                //this.IsChecked = table_Config.FingerPrintAllow;
            }
        }

        private async void GetFingerPrintAvailable()
        {
            IsFingerPrintAvailable = await CrossFingerprint.Current.IsAvailableAsync();
        }
        #endregion
    }
}
