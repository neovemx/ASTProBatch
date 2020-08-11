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
    public class BatchQueryViewModel : BaseViewModel
    {
        #region Atributes
        private InstanceItem instanceitem;
        private ObservableCollection<PickerStatusItem> statusitems;
        private LotItem lotitem;
        private PickerStatusItem statusselected;
        private int statusindex;
        private ObservableCollection<ParameterItem> parameteritems;
        private bool resultisvisible;
        #endregion

        #region Properties
        public InstanceItem InstanceItem
        {
            get { return instanceitem; }
            set { SetValue(ref instanceitem, value); }
        }

        public ObservableCollection<PickerStatusItem> StatusItems
        {
            get { return statusitems; }
            set { SetValue(ref statusitems, value); }
        }

        public ObservableCollection<ParameterItem> ParameterItems
        {
            get { return parameteritems; }
            set { SetValue(ref parameteritems, value); }
        }

        public LotItem LotItem
        {
            get { return lotitem; }
            set { SetValue(ref lotitem, value); }
        }

        public PickerStatusItem StatusSelected
        {
            get { return statusselected; }
            set { SetValue(ref statusselected, value); }
        }

        public int StatusIndex
        {
            get { return statusindex; }
            set { SetValue(ref statusindex, value); }
        }

        public bool ResultIsVisible
        {
            get { return resultisvisible; }
            set { SetValue(ref resultisvisible, value); }
        }
        #endregion

        #region Constructors
        public BatchQueryViewModel(InstanceItem instanceItem)
        {
            this.ResultIsVisible = false;
            this.InstanceItem = instanceItem;
            this.ParameterItems = new ObservableCollection<ParameterItem>();
            GetFakeData();
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private async void Search()
        {
            UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

            await Task.Delay(1000);

            await Task.Run(async () =>
            {
                ParameterItem parameterItem;

                parameterItem = new ParameterItem();
                parameterItem.Id = 1;
                parameterItem.Title = "PARAMETRO PRUEBA";
                parameterItem.Value = "valor prueba";
                this.ParameterItems.Add(parameterItem);

                parameterItem = new ParameterItem();
                parameterItem.Id = 2;
                parameterItem.Title = "PARAMETRO PRUEBA";
                parameterItem.Value = "valor prueba";
                this.ParameterItems.Add(parameterItem);

                parameterItem = new ParameterItem();
                parameterItem.Id = 3;
                parameterItem.Title = "PARAMETRO PRUEBA";
                parameterItem.Value = "valor prueba";
                this.ParameterItems.Add(parameterItem);

                parameterItem = new ParameterItem();
                parameterItem.Id = 4;
                parameterItem.Title = "PARAMETRO PRUEBA";
                parameterItem.Value = "valor prueba";
                this.ParameterItems.Add(parameterItem);

                parameterItem = new ParameterItem();
                parameterItem.Id = 5;
                parameterItem.Title = "PARAMETRO PRUEBA";
                parameterItem.Value = "valor prueba";
                this.ParameterItems.Add(parameterItem);

                parameterItem = new ParameterItem();
                parameterItem.Id = 6;
                parameterItem.Title = "PARAMETRO PRUEBA";
                parameterItem.Value = "valor prueba";
                this.ParameterItems.Add(parameterItem);

                parameterItem = new ParameterItem();
                parameterItem.Id = 7;
                parameterItem.Title = "PARAMETRO PRUEBA";
                parameterItem.Value = "valor prueba";
                this.ParameterItems.Add(parameterItem);

                this.ResultIsVisible = true;

                UserDialogs.Instance.HideLoading();
            });
        }
        #endregion

        #region Helpers
        private void GetFakeData()
        {
            this.StatusItems = new ObservableCollection<PickerStatusItem>();
            PickerStatusItem statusItem;

            statusItem = new PickerStatusItem();
            statusItem.Id = 1;
            statusItem.StatusName = "A - ACTIVO";
            statusItem.StatusIndex = 0;
            this.StatusItems.Add(statusItem);

            statusItem = new PickerStatusItem();
            statusItem.Id = 2;
            statusItem.StatusName = "I - INACTIVO";
            statusItem.StatusIndex = 1;
            this.StatusItems.Add(statusItem);

            this.LotItem = new LotItem();
            this.LotItem.Code = 51;
            this.LotItem.Title = "51 - Prueba";
            this.LotItem.Description = "Prueba";
            this.LotItem.DateCreation = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            this.LotItem.InPath = "";
            this.LotItem.OutPath = "";
            this.LotItem.StatusSelected = new PickerStatusItem() { Id = 1, StatusName = "A - ACTIVO", StatusIndex = 0 };

            this.statusindex = this.LotItem.StatusSelected.StatusIndex;
        }
        #endregion
    }
}
