using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class ProcessMonitorThreeViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<CommandItem> commanditems;
        private bool toolbarisvisible;
        private string actionicon;
        private string checkicon;
        private bool isrefreshing;
        private bool isloadingdata;
        #endregion

        #region Properties
        public ObservableCollection<CommandItem> CommandItems
        {
            get { return commanditems; }
            set { SetValue(ref commanditems, value); }
        }

        public bool ToolBarIsVisible
        {
            get { return toolbarisvisible; }
            set { SetValue(ref toolbarisvisible, value); }
        }

        public string ActionIcon
        {
            get { return actionicon; }
            set { SetValue(ref actionicon, value); }
        }

        public string CheckIcon
        {
            get { return checkicon; }
            set { SetValue(ref checkicon, value); }
        }

        public bool IsRefreshing
        {
            get { return isrefreshing; }
            set { SetValue(ref isrefreshing, value); }
        }

        public bool IsLoadingData
        {
            get { return isloadingdata; }
            set { SetValue(ref isloadingdata, value); }
        }
        #endregion

        #region Constructors
        public ProcessMonitorThreeViewModel(InstanceItem instanceitem)
        {
            ToolBarIsVisible = false;
            ActionIcon = "actions";
            CheckIcon = "check";

            if (instanceitem == null) { return; }

            CommandItems = new ObservableCollection<CommandItem>();
            CommandItem commandItem;

            commandItem = new CommandItem();
            commandItem.Id = 1;
            commandItem.IsChecked = false;
            commandItem.IsEnabled = true;
            commandItem.Title = "LOTE 1 - COMANDO 1";
            commandItem.Notifications = "notification";
            commandItem.State = "state_e_green";
            commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            CommandItems.Add(commandItem);

            commandItem = new CommandItem();
            commandItem.Id = 2;
            commandItem.IsChecked = false;
            commandItem.IsEnabled = true;
            commandItem.Title = "LOTE 1 - COMANDO 2";
            commandItem.Notifications = "notification";
            commandItem.State = "state_ed_orange";
            commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            CommandItems.Add(commandItem);

            commandItem = new CommandItem();
            commandItem.Id = 3;
            commandItem.IsChecked = false;
            commandItem.IsEnabled = true;
            commandItem.Title = "LOTE 1 - COMANDO 3";
            commandItem.Notifications = "notification_n";
            commandItem.State = "state_f";
            commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            CommandItems.Add(commandItem);

            commandItem = new CommandItem();
            commandItem.Id = 4;
            commandItem.IsChecked = false;
            commandItem.IsEnabled = true;
            commandItem.Title = "LOTE 1 - COMANDO 4";
            commandItem.Notifications = "notification_n";
            commandItem.State = "state_pause";
            commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            CommandItems.Add(commandItem);

            commandItem = new CommandItem();
            commandItem.Id = 5;
            commandItem.IsChecked = false;
            commandItem.IsEnabled = true;
            commandItem.Title = "LOTE 1 - COMANDO 5";
            commandItem.Notifications = "notification";
            commandItem.State = "state_om";
            commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            CommandItems.Add(commandItem);
        }
        #endregion

        #region Commands
        public ICommand ActionsCommand
        {
            get
            {
                return new RelayCommand(Actions);
            }
        }

        private async void Actions()
        {
            int countItems = 0;
            foreach (CommandItem item in CommandItems)
            {
                if (item.IsChecked) { countItems += 1; }
            }
            if (countItems <= 1)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más comandos", "Aceptar");
                return;
            }
            if (ToolBarIsVisible)
            {
                ToolBarIsVisible = false;
            }
            else
            {
                ToolBarIsVisible = true;
            }
        }

        public ICommand CheckCommand
        {
            get
            {
                return new RelayCommand(Check);
            }
        }

        private async void Check()
        {
            if (string.CompareOrdinal(CheckIcon, "check") == 0)
            {
                IsLoadingData = true;
                await Task.Delay(1000);
                await Task.Run(async () =>
                {
                    var CommandItemsTemp = CommandItems;
                    CommandItems = new ObservableCollection<CommandItem>();
                    foreach (CommandItem item in CommandItemsTemp)
                    {
                        item.IsChecked = true;
                        item.IsEnabled = false;
                        CommandItems.Add(item);
                    }
                    IsLoadingData = false;
                    ToolBarIsVisible = true;
                    CheckIcon = "uncheck";
                });
            }
            else
            {
                IsLoadingData = true;
                await Task.Delay(1000);
                await Task.Run(async () =>
                {
                    var CommandItemsTemp = CommandItems;
                    CommandItems = new ObservableCollection<CommandItem>();
                    foreach (CommandItem item in CommandItemsTemp)
                    {
                        item.IsChecked = false;
                        item.IsEnabled = true;
                        CommandItems.Add(item);
                    }
                    IsLoadingData = false;
                    ToolBarIsVisible = false;
                    CheckIcon = "check";
                });
            }
        }
        #endregion
    }
}
