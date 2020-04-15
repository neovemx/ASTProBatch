using AST_ProBatch_Mobile.Interfaces;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Atributes
        private string apiurl;
        #endregion

        #region Properties
        public string Apiurl
        {
            get { return apiurl; }
            set { SetValue(ref apiurl, value); }
        }
        #endregion

        #region Constructors
        public SettingsViewModel()
        {
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Apiurl))
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe ingresar un url", "Aceptar");
                return;
            }
            DependencyService.Get<Toast>().Show("Configuración guardada!");
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(Close);
            }
        }

        private async void Close()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        #endregion
    }
}
