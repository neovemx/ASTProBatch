using System.Threading.Tasks;
using System.Windows.Input;
using AST_ProBatch_Mobile.Models;
using ASTProBatchMobile.Utilities;
using GalaSoft.MvvmLight.Command;
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
            if (string.IsNullOrEmpty(urlDomain) && string.IsNullOrEmpty(urlPrefix))
            {
                _ = RefreshAppConfig();
            }
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
            if (string.IsNullOrWhiteSpace(this.UrlDomain) || string.IsNullOrWhiteSpace(this.UrlPrefix))
            {
                Alert.Show("Debe ingresar un dominio y un prefijo");
                return;
            }
            Table_Config table_Config = new Table_Config { Id = 1, UrlDomain = this.UrlDomain, UrlPrefix = this.UrlPrefix, FingerPrintAllow = this.IsFingerPrint };
            if (!await dbHelper.PullAsyncAppConfig(table_Config))
            {
                Alert.Show("No se pudo actualizar la configuración.");
                return;
            }
            Toast.ShowSuccess("Configuración guardada!");
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

        #region Helpers
        private async Task<bool> RefreshAppConfig()
        {
            Table_Config table_Config = await dbHelper.GetAsyncAppConfig();
            if (table_Config != null)
            {
                this.UrlDomain = table_Config.UrlDomain;
                this.UrlPrefix = table_Config.UrlPrefix;
                this.IsFingerPrint = table_Config.FingerPrintAllow;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
