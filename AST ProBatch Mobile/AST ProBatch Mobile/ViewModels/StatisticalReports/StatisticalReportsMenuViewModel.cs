using AST_ProBatch_Mobile.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class StatisticalReportsMenuViewModel : BaseViewModel
    {
        #region Atributes

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public StatisticalReportsMenuViewModel()
        {
        }
        #endregion

        #region Commands
        public ICommand BtnsubmaCommand
        {
            get
            {
                return new RelayCommand(Btnsubma);
            }
        }

        private async void Btnsubma()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            MainViewModel.GetInstance().LotAndCommand = new LotAndCommandViewModel(true);
            await Application.Current.MainPage.Navigation.PushAsync(new LotAndCommandPage());
        }

        public ICommand BtnsubmbCommand
        {
            get
            {
                return new RelayCommand(Btnsubmb);
            }
        }

        private async void Btnsubmb()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            MainViewModel.GetInstance().LogExecutionDelay = new LogExecutionDelayViewModel(true);
            await Application.Current.MainPage.Navigation.PushAsync(new LogExecutionDelayPage());
        }

        public ICommand BtnsubmcCommand
        {
            get
            {
                return new RelayCommand(Btnsubmc);
            }
        }

        private async void Btnsubmc()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            MainViewModel.GetInstance().Home = new HomeViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LogExecutionPage());
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
