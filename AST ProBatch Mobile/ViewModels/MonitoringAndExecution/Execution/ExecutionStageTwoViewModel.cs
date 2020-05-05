using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class ExecutionStageTwoViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<InstanceItem> instanceitems;
        private bool toolbarisvisible;
        private string actionicon;
        private string checkicon;
        private string viewicon;
        private bool isrefreshing;
        private bool isloadingdata;
        private bool compactviewisvisible;
        private bool fullviewisvisible;
        #endregion

        #region Properties
        public ObservableCollection<InstanceItem> InstanceItems
        {
            get { return instanceitems; }
            set { SetValue(ref instanceitems, value); }
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

        public string ViewIcon
        {
            get { return viewicon; }
            set { SetValue(ref viewicon, value); }
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

        public bool FullViewIsVisible
        {
            get { return fullviewisvisible; }
            set { SetValue(ref fullviewisvisible, value); }
        }

        public bool CompactViewIsVisible
        {
            get { return compactviewisvisible; }
            set { SetValue(ref compactviewisvisible, value); }
        }
        #endregion

        #region Constructors
        public ExecutionStageTwoViewModel(LogItem logitem)
        {
            ToolBarIsVisible = false;
            ActionIcon = "actions";
            CheckIcon = "check";
            ViewIcon = "view_b";
            FullViewIsVisible = true;
            CompactViewIsVisible = false;

            InstanceItems = new ObservableCollection<InstanceItem>();
            InstanceItem instanceItem;

            instanceItem = new InstanceItem();
            instanceItem.Id = 1;
            instanceItem.IsChecked = false;
            instanceItem.IsEnabled = true;
            instanceItem.Title = "INSTANCIA 1";
            instanceItem.Notifications = "notification";
            instanceItem.State = "state_e_green";
            instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            InstanceItems.Add(instanceItem);

            instanceItem = new InstanceItem();
            instanceItem.Id = 2;
            instanceItem.IsChecked = false;
            instanceItem.IsEnabled = true;
            instanceItem.Title = "INSTANCIA 2";
            instanceItem.Notifications = "notification";
            instanceItem.State = "state_ed_orange";
            instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            InstanceItems.Add(instanceItem);

            instanceItem = new InstanceItem();
            instanceItem.Id = 3;
            instanceItem.IsChecked = false;
            instanceItem.IsEnabled = true;
            instanceItem.Title = "INSTANCIA 3";
            instanceItem.Notifications = "notification_n";
            instanceItem.State = "state_f";
            instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            InstanceItems.Add(instanceItem);

            instanceItem = new InstanceItem();
            instanceItem.Id = 4;
            instanceItem.IsChecked = false;
            instanceItem.IsEnabled = true;
            instanceItem.Title = "INSTANCIA 4";
            instanceItem.Notifications = "notification_n";
            instanceItem.State = "state_pause";
            instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            InstanceItems.Add(instanceItem);

            instanceItem = new InstanceItem();
            instanceItem.Id = 5;
            instanceItem.IsChecked = false;
            instanceItem.IsEnabled = true;
            instanceItem.Title = "INSTANCIA 5";
            instanceItem.Notifications = "notification";
            instanceItem.State = "state_om";
            instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            InstanceItems.Add(instanceItem);
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
            foreach (InstanceItem item in InstanceItems)
            {
                if (item.IsChecked) { countItems += 1; }
            }
            if (countItems <= 1)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más instancias", "Aceptar");
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
                    var InstanceItemsTemp = InstanceItems;
                    InstanceItems = new ObservableCollection<InstanceItem>();
                    foreach (InstanceItem item in InstanceItemsTemp)
                    {
                        item.IsChecked = true;
                        item.IsEnabled = false;
                        InstanceItems.Add(item);
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
                    var InstanceItemsTemp = InstanceItems;
                    InstanceItems = new ObservableCollection<InstanceItem>();
                    foreach (InstanceItem item in InstanceItemsTemp)
                    {
                        item.IsChecked = false;
                        item.IsEnabled = true;
                        InstanceItems.Add(item);
                    }
                    IsLoadingData = false;
                    ToolBarIsVisible = false;
                    CheckIcon = "check";
                });
            }
        }

        public ICommand ViewCommand
        {
            get
            {
                return new RelayCommand(View);
            }
        }

        private async void View()
        {
            if (FullViewIsVisible)
            {
                IsLoadingData = true;
                //await Task.Delay(1000);
                await Task.Run(async () =>
                {
                    var InstanceItemsTemp = InstanceItems;
                    InstanceItems = new ObservableCollection<InstanceItem>();
                    foreach (InstanceItem item in InstanceItemsTemp)
                    {
                        item.IsChecked = false;
                        item.IsEnabled = true;
                        InstanceItems.Add(item);
                    }
                    FullViewIsVisible = false;
                    CompactViewIsVisible = true;
                    ViewIcon = "view_a";
                    IsLoadingData = false;
                    ToolBarIsVisible = false;
                    CheckIcon = "check";
                });
            }
            else
            {
                IsLoadingData = true;
                //await Task.Delay(1000);
                await Task.Run(async () =>
                {
                    var InstanceItemsTemp = InstanceItems;
                    InstanceItems = new ObservableCollection<InstanceItem>();
                    foreach (InstanceItem item in InstanceItemsTemp)
                    {
                        item.IsChecked = false;
                        item.IsEnabled = true;
                        InstanceItems.Add(item);
                    }
                    FullViewIsVisible = true;
                    CompactViewIsVisible = false;
                    ViewIcon = "view_b";
                    IsLoadingData = false;
                    ToolBarIsVisible = false;
                    CheckIcon = "check";
                });
            }
        }
        #endregion
    }
}
