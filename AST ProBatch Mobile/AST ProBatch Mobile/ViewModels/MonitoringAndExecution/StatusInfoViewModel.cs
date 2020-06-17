using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class StatusInfoViewModel : BaseViewModel
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
        public StatusInfoViewModel()
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
            stateItem.Title = "No Ejecutado";
            stateItem.State = "state_ne";
            stateItem.StateColor = StatusColor.White;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Esperando Dependencia";
            stateItem.State = "state_ed";
            stateItem.StateColor = StatusColor.Yellow;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Esperando Conexión";
            stateItem.State = "state_ec";
            stateItem.StateColor = StatusColor.Orange;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Error Dependencia";
            stateItem.State = "state_ed";
            stateItem.StateColor = StatusColor.Orange;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Finalizado con Errores";
            stateItem.State = "state_fe";
            stateItem.StateColor = StatusColor.Red;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Abortado por el Usuario";
            stateItem.State = "state_a";
            stateItem.StateColor = StatusColor.Purple;
            StateItems.Add(stateItem);
        }
    }
}
