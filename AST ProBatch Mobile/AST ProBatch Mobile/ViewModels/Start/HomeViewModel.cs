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
            MainViewModel.GetInstance().About = new AboutViewModel();
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
            MainViewModel.GetInstance().StatisticalReportsMenu = new StatisticalReportsMenuViewModel();
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
            MainViewModel.GetInstance().MonitoringAndExecutionMenu = new MonitoringAndExecutionMenuViewModel();
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
            MainViewModel.GetInstance().OperationsLog = new OperationsLogViewModel();
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
            MainViewModel.GetInstance().PlannerMenu = new PlannerMenuViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new PlannerMenuPage());
        }
        #endregion
    }
}
