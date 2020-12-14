using AST_ProBatch_Mobile.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class PlannerMenuViewModel : BaseViewModel
    {
        #region Atributes

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public PlannerMenuViewModel()
        {
        }
        #endregion

        #region Commands
        public ICommand BtnsubmdCommand
        {
            get
            {
                return new RelayCommand(Btnsubmd);
            }
        }

        private async void Btnsubmd()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            MainViewModel.GetInstance().ProcessMonitor = new ProcessMonitorViewModel(true);
            await Application.Current.MainPage.Navigation.PushAsync(new ProcessMonitorPage());
        }

        public ICommand BtnsubmeCommand
        {
            get
            {
                return new RelayCommand(Btnsubme);
            }
        }

        private async void Btnsubme()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            MainViewModel.GetInstance().RecurrenceMonitor = new RecurrenceMonitorViewModel(true);
            await Application.Current.MainPage.Navigation.PushAsync(new RecurrenceMonitorPage());
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
