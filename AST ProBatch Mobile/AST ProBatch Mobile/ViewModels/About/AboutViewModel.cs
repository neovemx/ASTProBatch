using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class AboutViewModel
    {
        #region Atributes

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public AboutViewModel()
        {
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
