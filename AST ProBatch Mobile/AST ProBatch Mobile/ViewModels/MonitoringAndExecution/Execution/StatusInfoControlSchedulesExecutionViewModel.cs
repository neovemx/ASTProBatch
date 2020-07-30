using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class StatusInfoControlSchedulesExecutionViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<StatusItem> stateitems;
        #endregion

        #region Properties
        public ObservableCollection<StatusItem> StatusItems
        {
            get { return stateitems; }
            set { SetValue(ref stateitems, value); }
        }
        #endregion

        #region Constructors
        public StatusInfoControlSchedulesExecutionViewModel()
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
            StatusItems = new ObservableCollection<StatusItem>();
            StatusItem statusItem;

            statusItem = new StatusItem();
            statusItem.Title = "No iniciado";
            statusItem.State = "EE";
            statusItem.StateColor = StatusColor.White;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "En ejecución";
            statusItem.State = "EE";
            statusItem.StateColor = StatusColor.Green;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Finalizado";
            statusItem.State = "EE";
            statusItem.StateColor = StatusColor.Blue;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "En horario";
            statusItem.State = "EC";
            statusItem.StateColor = StatusColor.White;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Fuera de horario";
            statusItem.State = "EC";
            statusItem.StateColor = StatusColor.Red;
            StatusItems.Add(statusItem);
        }
        #endregion
    }
}
