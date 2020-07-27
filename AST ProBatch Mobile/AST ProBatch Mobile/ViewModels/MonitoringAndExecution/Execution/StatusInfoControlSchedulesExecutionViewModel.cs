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
            StateItems = new ObservableCollection<StateItem>();
            StateItem stateItem;

            stateItem = new StateItem();
            stateItem.Title = "No iniciado";
            stateItem.State = "EE";
            stateItem.StateColor = StatusColor.White;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "En ejecución";
            stateItem.State = "EE";
            stateItem.StateColor = StatusColor.Green;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Finalizado";
            stateItem.State = "EE";
            stateItem.StateColor = StatusColor.Blue;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "En horario";
            stateItem.State = "EC";
            stateItem.StateColor = StatusColor.White;
            StateItems.Add(stateItem);

            stateItem = new StateItem();
            stateItem.Title = "Fuera de horario";
            stateItem.State = "EC";
            stateItem.StateColor = StatusColor.Red;
            StateItems.Add(stateItem);
        }
        #endregion
    }
}
