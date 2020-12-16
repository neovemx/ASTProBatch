using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        #region Atributes
        private string appversion;
        private string appbuild;
        #endregion

        #region Properties
        public string AppVersion
        {
            get { return appversion; }
            set { SetValue(ref appversion, value); }
        }
        public string AppBuild
        {
            get { return appbuild; }
            set { SetValue(ref appbuild, value); }
        }
        #endregion

        #region Constructors
        public AboutViewModel()
        {
            this.AppVersion = $"{VersionTracking.CurrentVersion}";
            this.AppBuild = $"build({VersionTracking.CurrentBuild})";
        }
        #endregion

        #region Commands
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
