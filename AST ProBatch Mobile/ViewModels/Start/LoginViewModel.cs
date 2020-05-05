using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Atributes
        private string username;
        private string password;
        private bool isrunning;
        private bool isenabled;
        private string _passwordTemp;
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
        public bool IsRunning
        {
            get { return isrunning; }
            set { SetValue(ref isrunning, value); }
        }
        public bool IsEnabled
        {
            get { return isenabled; }
            set { SetValue(ref isenabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsEnabled = true;
            this.Username = "ADMINISTRADOR";
            this.Password = "accusys123*";
            this._passwordTemp = "accusys123*";
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
            MainViewModel.GetInstance().Login = new LoginViewModel();
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
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe ingresar un usuario", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe ingresar una contraseña", "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            //TODO: Validacion de credenciales al web services va aquí
            if (string.CompareOrdinal(this.Password, this._passwordTemp) != 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Usuario y/o Password incorrectos", "Aceptar");
                this.Username = string.Empty;
                this.Password = string.Empty;
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            DependencyService.Get<Toast>().Show("Bienvenido: " + this.Username + "!");
            this.Username = string.Empty;
            this.Password = string.Empty;
            MainViewModel.GetInstance().Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
        #endregion
    }
}
