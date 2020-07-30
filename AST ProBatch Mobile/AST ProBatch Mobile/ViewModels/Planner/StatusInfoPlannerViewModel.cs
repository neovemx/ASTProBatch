using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class StatusInfoPlannerViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<StatusItem> statusitems;
        #endregion

        #region Properties
        public ObservableCollection<StatusItem> StatusItems
        {
            get { return statusitems; }
            set { SetValue(ref statusitems, value); }
        }
        #endregion

        #region Constructors
        public StatusInfoPlannerViewModel()
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
            statusItem.Title = "Ejecutando";
            statusItem.State = "state_e";
            statusItem.StateColor = StatusColor.Green;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Finalizado";
            statusItem.State = "state_f";
            statusItem.StateColor = StatusColor.Blue;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Esperando Dependencia";
            statusItem.State = "state_ed";
            statusItem.StateColor = StatusColor.Yellow;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Esperando Interfaz de Entrada";
            statusItem.State = "state_ei";
            statusItem.StateColor = StatusColor.Yellow;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Esperando Recurso";
            statusItem.State = "state_er";
            statusItem.StateColor = StatusColor.Yellow;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Esperando Puerto";
            statusItem.State = "state_ep";
            statusItem.StateColor = StatusColor.Yellow;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Error de Recurso";
            statusItem.State = "state_er";
            statusItem.StateColor = StatusColor.Red;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Error";
            statusItem.State = "state_e";
            statusItem.StateColor = StatusColor.Red;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Error de Dependencia";
            statusItem.State = "state_ed";
            statusItem.StateColor = StatusColor.Orange;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Error de Interfaz de Entrada";
            statusItem.State = "state_ei";
            statusItem.StateColor = StatusColor.Red;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Error de Puerto";
            statusItem.State = "state_ep";
            statusItem.StateColor = StatusColor.Red;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Planificado";
            statusItem.State = "state_p";
            statusItem.StateColor = StatusColor.Grey;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Abortado";
            statusItem.State = "state_ab";
            statusItem.StateColor = StatusColor.Purple;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Omitido";
            statusItem.State = "state_om";
            statusItem.StateColor = StatusColor.White;
            StatusItems.Add(statusItem);

            statusItem = new StatusItem();
            statusItem.Title = "Pausado";
            statusItem.State = "state_pause";
            statusItem.StateColor = StatusColor.White;
            StatusItems.Add(statusItem);
        } 
        #endregion
    }
}
