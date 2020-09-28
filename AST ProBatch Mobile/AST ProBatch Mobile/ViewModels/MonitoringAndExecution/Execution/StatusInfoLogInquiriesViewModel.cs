using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class StatusInfoLogInquiriesViewModel : BaseViewModel
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
        public StatusInfoLogInquiriesViewModel()
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
            statusItemProcess.Title = "No Ejecutado";
            statusItemProcess.State = "";
            statusItemProcess.StateColor = StatusColor.White;
            StatusItemsProcess.Add(statusItemProcess);

            statusItemProcess = new StatusItem();
            statusItemProcess.Title = "Ejecutando";
            statusItemProcess.State = "";
            statusItemProcess.StateColor = StatusColor.Green;
            StatusItemsProcess.Add(statusItemProcess);

            statusItemProcess = new StatusItem();
            statusItemProcess.Title = "Finalizado correctamente";
            statusItemProcess.State = "";
            statusItemProcess.StateColor = StatusColor.Blue;
            StatusItemsProcess.Add(statusItemProcess);

            statusItemProcess = new StatusItem();
            statusItemProcess.Title = "Esperando Dependencia";
            statusItemProcess.State = "";
            statusItemProcess.StateColor = StatusColor.Yellow;
            StatusItemsProcess.Add(statusItemProcess);

            statusItemProcess = new StatusItem();
            statusItemProcess.Title = "Error de Dependencia";
            statusItemProcess.State = "";
            statusItemProcess.StateColor = StatusColor.Orange;
            StatusItemsProcess.Add(statusItemProcess);

            statusItemProcess = new StatusItem();
            statusItemProcess.Title = "Finalizado con Errores";
            statusItemProcess.State = "";
            statusItemProcess.StateColor = StatusColor.Red;
            StatusItemsProcess.Add(statusItemProcess);

            statusItemProcess = new StatusItem();
            statusItemProcess.Title = "Abortado por el Usuario";
            statusItemProcess.State = "";
            statusItemProcess.StateColor = StatusColor.Purple;
            StatusItemsProcess.Add(statusItemProcess);
        }
        #endregion
    }
}
