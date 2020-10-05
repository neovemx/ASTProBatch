using System.Collections.ObjectModel;
using System.Windows.Input;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class StatusInfoDependenciesViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<StatusItem> statusitemsprocess;
        #endregion

        #region Properties
        public ObservableCollection<StatusItem> StatusItemsProcess
        {
            get { return statusitemsprocess; }
            set { SetValue(ref statusitemsprocess, value); }
        }
        #endregion

        #region Constructors
        public StatusInfoDependenciesViewModel()
        {
            GetStates();
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

        #region Helpers
        private void GetStates()
        {
            StatusItemsProcess = new ObservableCollection<StatusItem>();
            StatusItem statusItemProcess;

            statusItemProcess = new StatusItem();
            statusItemProcess.Title = "Comandos Omitidos";
            statusItemProcess.State = "";
            statusItemProcess.StateColor = StatusColor.DarkBlue;
            StatusItemsProcess.Add(statusItemProcess);
        }
        #endregion
    }
}
