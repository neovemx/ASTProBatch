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
        private ObservableCollection<StateItem> stateitems;
        #endregion

        #region Properties
        public ObservableCollection<StateItem> StateItems
        {
            get { return stateitems; }
            set { SetValue(ref stateitems, value); }
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

        private void GetStates()
        {
            StateItems = new ObservableCollection<StateItem>();
            StateItem stateItem;

            stateItem = new StateItem();
            stateItem.Title = "Ejecutando";
            stateItem.State = "state_e";
            stateItem.StateColor = StatusColor.Green;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Finalizado";
            stateItem.State = "state_f";
            stateItem.StateColor = StatusColor.Blue;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Esperando Dependencia";
            stateItem.State = "state_ed";
            stateItem.StateColor = StatusColor.Yellow;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Esperando Interfaz de Entrada";
            stateItem.State = "state_ei";
            stateItem.StateColor = StatusColor.Yellow;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Esperando Recurso";
            stateItem.State = "state_er";
            stateItem.StateColor = StatusColor.Yellow;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Esperando Puerto";
            stateItem.State = "state_ep";
            stateItem.StateColor = StatusColor.Yellow;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Error de Recurso";
            stateItem.State = "state_er";
            stateItem.StateColor = StatusColor.Red;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Error";
            stateItem.State = "state_e";
            stateItem.StateColor = StatusColor.Red;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Error de Dependencia";
            stateItem.State = "state_ed";
            stateItem.StateColor = StatusColor.Orange;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Error de Interfaz de Entrada";
            stateItem.State = "state_ei";
            stateItem.StateColor = StatusColor.Red;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Error de Puerto";
            stateItem.State = "state_ep";
            stateItem.StateColor = StatusColor.Red;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Planificado";
            stateItem.State = "state_p";
            stateItem.StateColor = StatusColor.Grey;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Abortado";
            stateItem.State = "state_ab";
            stateItem.StateColor = StatusColor.Purple;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Omitido";
            stateItem.State = "state_om";
            stateItem.StateColor = StatusColor.White;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Pausado";
            stateItem.State = "state_pause";
            stateItem.StateColor = StatusColor.White;
            StateItems.Add(stateItem);
        }
    }
}
