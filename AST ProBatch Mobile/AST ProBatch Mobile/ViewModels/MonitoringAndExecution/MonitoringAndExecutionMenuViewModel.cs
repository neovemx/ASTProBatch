using AST_ProBatch_Mobile.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class MonitoringAndExecutionMenuViewModel : BaseViewModel
    {
        #region Atributes

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public MonitoringAndExecutionMenuViewModel()
        {
        }
        #endregion

        #region Commands
        public ICommand BtnsubmfCommand
        {
            get
            {
                return new RelayCommand(Btnsubmf);
            }
        }

        private async void Btnsubmf()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            MainViewModel.GetInstance().Monitoring = new MonitoringViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new MonitoringPage());
        }

        public ICommand BtnsubmgCommand
        {
            get
            {
                return new RelayCommand(Btnsubmg);
            }
        }

        private async void Btnsubmg()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            MainViewModel.GetInstance().Execution = new ExecutionViewModel(true);
            await Application.Current.MainPage.Navigation.PushAsync(new ExecutionPage());
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
