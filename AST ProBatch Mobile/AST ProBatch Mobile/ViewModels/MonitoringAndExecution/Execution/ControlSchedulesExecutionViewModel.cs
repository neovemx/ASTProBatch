using System.Collections.ObjectModel;
using System.Windows.Input;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using AST_ProBatch_Mobile.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class ControlSchedulesExecutionViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<CommandsToControl> commandstocontrolitems;
        private string logtitle;
        #endregion

        #region Properties
        public ObservableCollection<CommandsToControl> CommandsToControlItems
        {
            get { return commandstocontrolitems; }
            set { SetValue(ref commandstocontrolitems, value); }
        }

        public string LogTitle
        {
            get { return logtitle; }
            set { SetValue(ref logtitle, value); }
        }
        #endregion

        #region Constructors
        public ControlSchedulesExecutionViewModel(LogItem logItem)
        {
            this.LogTitle = logItem.NameLog;
            GetFakeData();
        }
        #endregion

        #region Commands
        public ICommand StatusInfoCommand
        {
            get
            {
                return new RelayCommand(StatusInfo);
            }
        }

        private async void StatusInfo()
        {
            MainViewModel.GetInstance().StatusInfoControlSchedulesExecution = new StatusInfoControlSchedulesExecutionViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new StatusInfoControlSchedulesExecutionPage());
        }
        #endregion

        #region Helpers
        private void GetFakeData()
        {
            CommandsToControlItems = new ObservableCollection<CommandsToControl>();
            CommandsToControl commandsToControl;

            commandsToControl = new CommandsToControl();
            commandsToControl.LogTitle = this.LogTitle;
            commandsToControl.Id = 1;
            commandsToControl.Instance = 1;
            commandsToControl.InstanceName = "Instancia 1";
            commandsToControl.Lot = 1;
            commandsToControl.LotName = "99991 - Prueba SQR 1";
            commandsToControl.Command = 1;
            commandsToControl.CommandName = "9990 - Prueba";
            commandsToControl.Start = "10:40";
            commandsToControl.End = "10:50";
            commandsToControl.ControlStart = "11:04";
            commandsToControl.ControlEnd = "11:30";
            commandsToControl.StatusColorEE = StatusColor.Blue;
            commandsToControl.StatusColorEC = StatusColor.Red;
            CommandsToControlItems.Add(commandsToControl);

            commandsToControl = new CommandsToControl();
            commandsToControl.LogTitle = this.LogTitle;
            commandsToControl.Id = 2;
            commandsToControl.Instance = 2;
            commandsToControl.InstanceName = "Instancia 2";
            commandsToControl.Lot = 2;
            commandsToControl.LotName = "99991 - Prueba SQR 1";
            commandsToControl.Command = 2;
            commandsToControl.CommandName = "9990 - Prueba";
            commandsToControl.Start = "10:40";
            commandsToControl.End = "10:50";
            commandsToControl.ControlStart = "11:04";
            commandsToControl.ControlEnd = "11:30";
            commandsToControl.StatusColorEE = StatusColor.Blue;
            commandsToControl.StatusColorEC = StatusColor.Red;
            CommandsToControlItems.Add(commandsToControl);

            commandsToControl = new CommandsToControl();
            commandsToControl.LogTitle = this.LogTitle;
            commandsToControl.Id = 3;
            commandsToControl.Instance = 3;
            commandsToControl.InstanceName = "Instancia 2";
            commandsToControl.Lot = 3;
            commandsToControl.LotName = "99991 - Prueba SQR 1";
            commandsToControl.Command = 2;
            commandsToControl.CommandName = "9990 - Prueba";
            commandsToControl.Start = "10:40";
            commandsToControl.End = "10:50";
            commandsToControl.ControlStart = "11:04";
            commandsToControl.ControlEnd = "11:30";
            commandsToControl.StatusColorEE = StatusColor.Green;
            commandsToControl.StatusColorEC = StatusColor.White;
            CommandsToControlItems.Add(commandsToControl);
        }
        #endregion
    }
}
