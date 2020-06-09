using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Atributes
        private string urldomain;
        private string urlprefix;
        private bool isfingerprint;
        #endregion

        #region Properties
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
        public bool IsFingerPrint
        {
            get { return isfingerprint; }
            set { SetValue(ref isfingerprint, value); }
        }
        #endregion

        #region Constructors
        public SettingsViewModel(string urlDomain, string urlPrefix, bool isFingerPrint)
        {
            this.UrlDomain = urlDomain;
            this.UrlPrefix = urlPrefix;
            this.IsFingerPrint = isFingerPrint;
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
            if (string.IsNullOrEmpty(this.UrlDomain) && string.IsNullOrEmpty(this.UrlPrefix))
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe ingresar un dominio y un prefijo", "Aceptar");
                return;
            }
            Table_Config table_Config = new Table_Config { Id = 1, UrlDomain = this.UrlDomain, UrlPrefix = this.UrlPrefix, FingerPrintAllow = this.IsFingerPrint };
            if (!await dbHelper.PullAsyncAppConfig(table_Config))
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "No se pudo actualizar la configuración.", "Aceptar");
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
