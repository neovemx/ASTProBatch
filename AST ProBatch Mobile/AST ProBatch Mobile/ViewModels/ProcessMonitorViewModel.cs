using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class ProcessMonitorViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<LogItem> logitems;
        private bool toolbarisvisible;
        private string actionicon;
        private string checkicon;
        private bool isrefreshing;
        private bool isloadingdata;
        #endregion

        #region Properties
        public ObservableCollection<LogItem> LogItems
        {
            get { return logitems; }
            set { SetValue(ref logitems, value); }
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
        public ProcessMonitorViewModel()
        {
            ToolBarIsVisible = false;
            ActionIcon = "actions";
            CheckIcon = "check";

            LogItems = new ObservableCollection<LogItem>();
            LogItem logItem;

            logItem = new LogItem();
            logItem.Id = 1;
            logItem.IsChecked = false;
            logItem.IsEnabled = true;
            logItem.Title = "BITACORA 1";
            logItem.Notifications = "notification";
            logItem.State = "state_e_green";
            logItem.Environment = "WINDOWS";
            logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            LogItems.Add(logItem);

            logItem = new LogItem();
            logItem.Id = 2;
            logItem.IsChecked = false;
            logItem.IsEnabled = true;
            logItem.Title = "BITACORA 2";
            logItem.Notifications = "notification";
            logItem.State = "state_ed_orange";
            logItem.Environment = "WINDOWS";
            logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            LogItems.Add(logItem);

            logItem = new LogItem();
            logItem.Id = 3;
            logItem.IsChecked = false;
            logItem.IsEnabled = true;
            logItem.Title = "BITACORA 3";
            logItem.Notifications = "notification_n";
            logItem.State = "state_f";
            logItem.Environment = "WINDOWS";
            logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            LogItems.Add(logItem);

            logItem = new LogItem();
            logItem.Id = 4;
            logItem.IsChecked = false;
            logItem.IsEnabled = true;
            logItem.Title = "BITACORA 4";
            logItem.Notifications = "notification_n";
            logItem.State = "state_pause";
            logItem.Environment = "WINDOWS";
            logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            LogItems.Add(logItem);

            logItem = new LogItem();
            logItem.Id = 5;
            logItem.IsChecked = false;
            logItem.IsEnabled = true;
            logItem.Title = "BITACORA 5";
            logItem.Notifications = "notification";
            logItem.State = "state_om";
            logItem.Environment = "WINDOWS";
            logItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            LogItems.Add(logItem);
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
            foreach (LogItem item in LogItems)
            {
                if (item.IsChecked) { countItems += 1; }
            }
            if (countItems <= 1)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
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
                    var LogItemsTemp = LogItems;
                    LogItems = new ObservableCollection<LogItem>();
                    foreach (LogItem item in LogItemsTemp)
                    {
                        item.IsChecked = true;
                        item.IsEnabled = false;
                        LogItems.Add(item);
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
                    var LogItemsTemp = LogItems;
                    LogItems = new ObservableCollection<LogItem>();
                    foreach (LogItem item in LogItemsTemp)
                    {
                        item.IsChecked = false;
                        item.IsEnabled = true;
                        LogItems.Add(item);
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
