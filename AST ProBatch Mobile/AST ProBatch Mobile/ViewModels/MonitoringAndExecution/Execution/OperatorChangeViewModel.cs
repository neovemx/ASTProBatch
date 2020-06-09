using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class OperatorChangeViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<InstanceRunningItem> instancerunningitems;
        private ObservableCollection<OperatorItem> operatorstems;
        private string logtitle;
        private string logoperator;
        private OperatorItem selectedoperator;
        private bool isloadingdata;
        public string operatorpassword;
        #endregion

        #region Properties
        public OperatorItem SelectedOperator
        {
            get { return selectedoperator; }
            set
            {
                SetValue(ref selectedoperator, value);
            }
        }
        public ObservableCollection<InstanceRunningItem> InstanceRunningItems
        {
            get { return instancerunningitems; }
            set { SetValue(ref instancerunningitems, value); }
        }
        public ObservableCollection<OperatorItem> OperatorsItems
        {
            get { return operatorstems; }
            set { SetValue(ref operatorstems, value); }
        }

        public string LogTitle
        {
            get { return logtitle; }
            set { SetValue(ref logtitle, value); }
        }

        public string LogOperator
        {
            get { return logoperator; }
            set { SetValue(ref logoperator, value); }
        }

        public string OperatorPassword
        {
            get { return operatorpassword; }
            set { SetValue(ref operatorpassword, value); }
        }

        public bool IsLoadingData
        {
            get { return isloadingdata; }
            set { SetValue(ref isloadingdata, value); }
        }
        #endregion

        #region Constructors
        public OperatorChangeViewModel(LogItem logitem)
        {
            this.LogTitle = logitem.Title;
            this.LogOperator = logitem.Operator;

            GetFakeData();
        }
        #endregion

        #region Commands
        public ICommand NewCommand
        {
            get
            {
                return new RelayCommand(New);
            }
        }

        private async void New()
        {
            this.SelectedOperator = null;
            this.OperatorPassword = string.Empty;
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            try
            {
                if (SelectedOperator == null)
                {
                    await UserDialogs.Instance.AlertAsync("Debe seleccionar un operador", "AST●ProBatch®", "Aceptar");
                    return;
                }
                if (string.IsNullOrWhiteSpace(OperatorPassword))
                {
                    await UserDialogs.Instance.AlertAsync("Debe ingresar el password del operador", "AST●ProBatch®", "Aceptar");
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Ocurrió un error.", "Aceptar");
            }

        }
        #endregion

        #region Helpers

        #endregion

        #region Fake Data
        private void GetFakeData()
        {
            InstanceRunningItems = new ObservableCollection<InstanceRunningItem>();
            InstanceRunningItem instanceRunningItem;

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 1;
            instanceRunningItem.Title = "INSTANCIA 1";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 2;
            instanceRunningItem.Title = "INSTANCIA 2";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 3;
            instanceRunningItem.Title = "INSTANCIA 3";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 4;
            instanceRunningItem.Title = "INSTANCIA 4";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 5;
            instanceRunningItem.Title = "INSTANCIA 5";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 6;
            instanceRunningItem.Title = "INSTANCIA 6";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 7;
            instanceRunningItem.Title = "INSTANCIA 7";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 8;
            instanceRunningItem.Title = "INSTANCIA 8";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 9;
            instanceRunningItem.Title = "INSTANCIA 9";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            instanceRunningItem = new InstanceRunningItem();
            instanceRunningItem.Id = 10;
            instanceRunningItem.Title = "INSTANCIA 10";
            instanceRunningItem.IsRunning = "SI";
            InstanceRunningItems.Add(instanceRunningItem);

            OperatorsItems = new ObservableCollection<OperatorItem>();
            OperatorItem operatorItem;

            operatorItem = new OperatorItem();
            operatorItem.Id = 1;
            operatorItem.OperatorName = "ADMINISTRADOR WEB";
            OperatorsItems.Add(operatorItem);

            operatorItem = new OperatorItem();
            operatorItem.Id = 2;
            operatorItem.OperatorName = "PLANIFICADOR";
            OperatorsItems.Add(operatorItem);

            operatorItem = new OperatorItem();
            operatorItem.Id = 3;
            operatorItem.OperatorName = "SUPERVISOR";
            OperatorsItems.Add(operatorItem);

            operatorItem = new OperatorItem();
            operatorItem.Id = 4;
            operatorItem.OperatorName = "TESTER";
            OperatorsItems.Add(operatorItem);

            operatorItem = new OperatorItem();
            operatorItem.Id = 5;
            operatorItem.OperatorName = "ADMINISTRADOR";
            OperatorsItems.Add(operatorItem);

            operatorItem = new OperatorItem();
            operatorItem.Id = 6;
            operatorItem.OperatorName = "USUARIO DE SEGURIDAD";
            OperatorsItems.Add(operatorItem);

            operatorItem = new OperatorItem();
            operatorItem.Id = 7;
            operatorItem.OperatorName = "USUARIO ANONIMO";
            OperatorsItems.Add(operatorItem);
        }
        #endregion
    }
}
