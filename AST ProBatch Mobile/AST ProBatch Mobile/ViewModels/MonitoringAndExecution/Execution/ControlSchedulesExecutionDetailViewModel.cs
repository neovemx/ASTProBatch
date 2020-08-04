using System.Windows.Input;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class ControlSchedulesExecutionDetailViewModel : BaseViewModel
    {
        #region Atributes
        private CommandsToControl commandtocontrol;
        #endregion

        #region Properties
        public CommandsToControl CommandToControl
        {
            get { return commandtocontrol; }
            set { SetValue(ref commandtocontrol, value); }
        }
        #endregion

        #region Constructors
        public ControlSchedulesExecutionDetailViewModel(CommandsToControl commandToControl)
        {
            this.CommandToControl = commandToControl;
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

        #endregion
    }
}
