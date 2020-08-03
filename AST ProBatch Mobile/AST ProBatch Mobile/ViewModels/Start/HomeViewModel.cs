using System.Diagnostics;
using System.Windows.Input;
using AST_ProBatch_Mobile.Views;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Atributes

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public HomeViewModel()
        {

        }
        #endregion

        #region Commands
        public ICommand AboutCommand
        {
            get
            {
                return new RelayCommand(About);
            }
        }

        private async void About()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new AboutPage());
        }

        public ICommand LogoutCommand
        {
            get
            {
                return new RelayCommand(Logout);
            }
        }

        private async void Logout()
        {
            if (await Confirm.Show("Deseas salir?"))
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        public ICommand BtnmppalaCommand
        {
            get
            {
                return new RelayCommand(Btnmppala);
            }
        }

        private async void Btnmppala()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new StatisticalReportsMenuPage());
        }

        public ICommand BtnmppalbCommand
        {
            get
            {
                return new RelayCommand(Btnmppalb);
            }
        }

        private async void Btnmppalb()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new MonitoringAndExecutionMenuPage());
        }

        public ICommand BtnmppalcCommand
        {
            get
            {
                return new RelayCommand(Btnmppalc);
            }
        }

        private async void Btnmppalc()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new OperationsLogPage());
        }

        public ICommand BtnmppaldCommand
        {
            get
            {
                return new RelayCommand(Btnmppald);
            }
        }

        private async void Btnmppald()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new PlannerMenuPage());
        }
        #endregion
    }
}
