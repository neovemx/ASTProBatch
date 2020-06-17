using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class ProcessMonitorViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<ProcessItem> processitems;
        private bool toolbarisvisible;
        private string checkicon;
        #endregion

        #region Properties
        public ObservableCollection<ProcessItem> ProcessItems
        {
            get { return processitems; }
            set { SetValue(ref processitems, value); }
        }
        public bool ToolBarIsVisible
        {
            get { return toolbarisvisible; }
            set { SetValue(ref toolbarisvisible, value); }
        }
        public string CheckIcon
        {
            get { return checkicon; }
            set { SetValue(ref checkicon, value); }
        }
        #endregion

        #region Constructors
        public ProcessMonitorViewModel()
        {
            ToolBarIsVisible = false;
            CheckIcon = "check";
            GetFakeData();
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
            foreach (ProcessItem item in ProcessItems)
            {
                if (item.IsChecked) { countItems += 1; }
            }
            if (countItems <= 1)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más procesos", "Aceptar");
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
            try
            {
                if (string.CompareOrdinal(CheckIcon, "check") == 0)
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

                    await Task.Delay(1000);

                    await Task.Run(async () =>
                    {
                        var ProcessItemsTemp = ProcessItems;
                        ProcessItems = new ObservableCollection<ProcessItem>();
                        foreach (ProcessItem item in ProcessItemsTemp)
                        {
                            item.IsChecked = true;
                            item.IsEnabled = false;
                            ProcessItems.Add(item);
                        }
                        ToolBarIsVisible = true;
                        CheckIcon = "uncheck";

                        UserDialogs.Instance.HideLoading();
                    });
                }
                else
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

                    await Task.Delay(1000);

                    await Task.Run(async () =>
                    {
                        var ProcessItemsTemp = ProcessItems;
                        ProcessItems = new ObservableCollection<ProcessItem>();
                        foreach (ProcessItem item in ProcessItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            ProcessItems.Add(item);
                        }
                        ToolBarIsVisible = false;
                        CheckIcon = "check";

                        UserDialogs.Instance.HideLoading();
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Ocurrió un error: " + "/n/r/n/r" + ex.Message, "Aceptar");
                UserDialogs.Instance.HideLoading();
            }

        }
        #endregion

        #region Helpers
        private void GetFakeData()
        {
            ProcessItems = new ObservableCollection<ProcessItem>();
            ProcessItem processItem;

            processItem = new ProcessItem();
            processItem.Id = 1;
            processItem.IsChecked = false;
            processItem.IsEnabled = true;
            processItem.PID = 2756321;
            processItem.Lot = "-";
            processItem.Command = "8-Prueba BAT";
            processItem.Environment = "WKS0396";
            processItem.Service = "Windows";
            processItem.State = "state_e";
            processItem.StateColor = StatusColor.Green;
            processItem.StartHour = "10:45";
            processItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ProcessItems.Add(processItem);

            processItem = new ProcessItem();
            processItem.Id = 2;
            processItem.IsChecked = false;
            processItem.IsEnabled = true;
            processItem.PID = 2756322;
            processItem.Lot = "-";
            processItem.Command = "8-Prueba BAT";
            processItem.Environment = "WKS0396";
            processItem.Service = "Windows";
            processItem.State = "state_pause";
            processItem.StateColor = StatusColor.White;
            processItem.StartHour = "10:45";
            processItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ProcessItems.Add(processItem);

            processItem = new ProcessItem();
            processItem.Id = 3;
            processItem.IsChecked = false;
            processItem.IsEnabled = true;
            processItem.PID = 2756323;
            processItem.Lot = "-";
            processItem.Command = "8-Prueba BAT";
            processItem.Environment = "WKS0396";
            processItem.Service = "Windows";
            processItem.State = "state_ed";
            processItem.StateColor = StatusColor.Orange;
            processItem.StartHour = "10:45";
            processItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ProcessItems.Add(processItem);

            processItem = new ProcessItem();
            processItem.Id = 4;
            processItem.IsChecked = false;
            processItem.IsEnabled = true;
            processItem.PID = 2756324;
            processItem.Lot = "-";
            processItem.Command = "8-Prueba BAT";
            processItem.Environment = "WKS0396";
            processItem.Service = "Windows";
            processItem.State = "state_om";
            processItem.StateColor = StatusColor.Green;
            processItem.StartHour = "10:45";
            processItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ProcessItems.Add(processItem);

            processItem = new ProcessItem();
            processItem.Id = 5;
            processItem.IsChecked = false;
            processItem.IsEnabled = true;
            processItem.PID = 2756325;
            processItem.Lot = "-";
            processItem.Command = "8-Prueba BAT";
            processItem.Environment = "WKS0396";
            processItem.Service = "Windows";
            processItem.State = "state_f";
            processItem.StateColor = StatusColor.Blue;
            processItem.StartHour = "10:45";
            processItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ProcessItems.Add(processItem);
        }
        #endregion
    }
}
