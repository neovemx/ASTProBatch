using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class RecurrenceMonitorViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<RecurrenceItem> recurrenceitems;
        private bool toolbarisvisible;
        private string checkicon;
        #endregion

        #region Properties
        public ObservableCollection<RecurrenceItem> RecurrenceItems
        {
            get { return recurrenceitems; }
            set { SetValue(ref recurrenceitems, value); }
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
        public RecurrenceMonitorViewModel()
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
            foreach (RecurrenceItem item in RecurrenceItems)
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
                        var RecurrenceItemsTemp = RecurrenceItems;
                        RecurrenceItems = new ObservableCollection<RecurrenceItem>();
                        foreach (RecurrenceItem item in RecurrenceItemsTemp)
                        {
                            item.IsChecked = true;
                            item.IsEnabled = false;
                            RecurrenceItems.Add(item);
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
                        var RecurrenceItemsTemp = RecurrenceItems;
                        RecurrenceItems = new ObservableCollection<RecurrenceItem>();
                        foreach (RecurrenceItem item in RecurrenceItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            RecurrenceItems.Add(item);
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
            RecurrenceItems = new ObservableCollection<RecurrenceItem>();
            RecurrenceItem recurrenceItem;

            recurrenceItem = new RecurrenceItem();
            recurrenceItem.Id = 1;
            recurrenceItem.IsChecked = false;
            recurrenceItem.IsEnabled = true;
            recurrenceItem.PID = 2756321;
            recurrenceItem.Lot = "-";
            recurrenceItem.Command = "8-Prueba BAT";
            recurrenceItem.Environment = "WKS0396";
            recurrenceItem.Service = "Windows";
            recurrenceItem.Recurrence = "00:02:00";
            recurrenceItem.State = "state_e_green";
            recurrenceItem.StartTime = "10:45:00";
            recurrenceItem.EndTime = "11:25:00";
            recurrenceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            RecurrenceItems.Add(recurrenceItem);

            recurrenceItem = new RecurrenceItem();
            recurrenceItem.Id = 2;
            recurrenceItem.IsChecked = false;
            recurrenceItem.IsEnabled = true;
            recurrenceItem.PID = 2756322;
            recurrenceItem.Lot = "-";
            recurrenceItem.Command = "8-Prueba BAT";
            recurrenceItem.Environment = "WKS0396";
            recurrenceItem.Service = "Windows";
            recurrenceItem.Recurrence = "00:02:00";
            recurrenceItem.State = "state_e_green";
            recurrenceItem.StartTime = "10:45:00";
            recurrenceItem.EndTime = "11:25:00";
            recurrenceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            RecurrenceItems.Add(recurrenceItem);

            recurrenceItem = new RecurrenceItem();
            recurrenceItem.Id = 3;
            recurrenceItem.IsChecked = false;
            recurrenceItem.IsEnabled = true;
            recurrenceItem.PID = 2756323;
            recurrenceItem.Lot = "-";
            recurrenceItem.Command = "8-Prueba BAT";
            recurrenceItem.Environment = "WKS0396";
            recurrenceItem.Service = "Windows";
            recurrenceItem.Recurrence = "00:02:00";
            recurrenceItem.State = "state_e_green";
            recurrenceItem.StartTime = "10:45:00";
            recurrenceItem.EndTime = "11:25:00";
            recurrenceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            RecurrenceItems.Add(recurrenceItem);

            recurrenceItem = new RecurrenceItem();
            recurrenceItem.Id = 4;
            recurrenceItem.IsChecked = false;
            recurrenceItem.IsEnabled = true;
            recurrenceItem.PID = 2756324;
            recurrenceItem.Lot = "-";
            recurrenceItem.Command = "8-Prueba BAT";
            recurrenceItem.Environment = "WKS0396";
            recurrenceItem.Service = "Windows";
            recurrenceItem.Recurrence = "00:02:00";
            recurrenceItem.State = "state_e_green";
            recurrenceItem.StartTime = "10:45:00";
            recurrenceItem.EndTime = "11:25:00";
            recurrenceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            RecurrenceItems.Add(recurrenceItem);

            recurrenceItem = new RecurrenceItem();
            recurrenceItem.Id = 5;
            recurrenceItem.IsChecked = false;
            recurrenceItem.IsEnabled = true;
            recurrenceItem.PID = 2756325;
            recurrenceItem.Lot = "-";
            recurrenceItem.Command = "8-Prueba BAT";
            recurrenceItem.Environment = "WKS0396";
            recurrenceItem.Service = "Windows";
            recurrenceItem.Recurrence = "00:02:00";
            recurrenceItem.State = "state_e_green";
            recurrenceItem.StartTime = "10:45:00";
            recurrenceItem.EndTime = "11:25:00";
            recurrenceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            RecurrenceItems.Add(recurrenceItem);
        }
        #endregion
    }
}
