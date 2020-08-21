using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LogInquiriesViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<PickerLotItem> lotitems;
        private ObservableCollection<PickerCommandItem> commanditems;
        private ObservableCollection<ResultLogInquirieItem> resultloginquirieitems;
        private ObservableCollection<LogInquirieOperatorItem> loginquirieoperatoritems;
        private string logtitle;
        private int logid;
        private PickerLotItem lotselected;
        private PickerCommandItem commandselected;
        private bool resultisvisible;
        private bool formisvisible;
        #endregion

        #region Properties
        public ObservableCollection<PickerLotItem> LotItems
        {
            get { return lotitems; }
            set { SetValue(ref lotitems, value); }
        }
        public PickerLotItem LotSelected
        {
            get { return lotselected; }
            set
            {
                SetValue(ref lotselected, value);
            }
        }
        public ObservableCollection<PickerCommandItem> CommandItems
        {
            get { return commanditems; }
            set { SetValue(ref commanditems, value); }
        }
        public PickerCommandItem CommandSelected
        {
            get { return commandselected; }
            set
            {
                SetValue(ref commandselected, value);
            }
        }
        public ObservableCollection<ResultLogInquirieItem> ResultLogInquirieItems
        {
            get { return resultloginquirieitems; }
            set { SetValue(ref resultloginquirieitems, value); }
        }
        public string LogTitle
        {
            get { return logtitle; }
            set { SetValue(ref logtitle, value); }
        }
        public int LogId
        {
            get { return logid; }
            set { SetValue(ref logid, value); }
        }
        public bool ResultIsVisible
        {
            get { return resultisvisible; }
            set { SetValue(ref resultisvisible, value); }
        }
        public bool FormIsVisible
        {
            get { return formisvisible; }
            set { SetValue(ref formisvisible, value); }
        }
        public ObservableCollection<LogInquirieOperatorItem> LogInquirieOperatorItems
        {
            get { return loginquirieoperatoritems; }
            set { SetValue(ref loginquirieoperatoritems, value); }
        }
        #endregion

        #region Constructors
        public LogInquiriesViewModel(bool IsReload, LogItem logitem)
        {
            if (IsReload)
            {
                this.LogTitle = logitem.NameLog;
                this.LogId = (int)logitem.IdLog;
                this.ResultIsVisible = false;
                this.FormIsVisible = true;
                GetFakeData();
            }
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
            if (this.LotSelected == null)
            {
                Alert.Show("Debe seleccionar un lote.");
                return;
            }
            UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);
            await Task.Delay(2000);
            this.FormIsVisible = false;
            this.ResultIsVisible = true;
            UserDialogs.Instance.HideLoading();
        }
        #endregion

        #region Helpers

        #endregion

        #region Fake Data
        private void GetFakeData()
        {
            ResultLogInquirieItems = new ObservableCollection<ResultLogInquirieItem>();
            ResultLogInquirieItem resultLogInquirieItem;

            resultLogInquirieItem = new ResultLogInquirieItem();
            resultLogInquirieItem.LogTitle = this.LogTitle;
            resultLogInquirieItem.IdLog = this.LogId;
            resultLogInquirieItem.IdInstance = 1;
            resultLogInquirieItem.Pause = "";
            resultLogInquirieItem.NameInstance = "Inst - 1";
            resultLogInquirieItem.IdLog = 1;
            resultLogInquirieItem.StartHour = "10:13";
            resultLogInquirieItem.NameLot = "Lote 14";
            resultLogInquirieItem.IdCommand = 1;
            resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
            resultLogInquirieItem.Status = "FIN";
            resultLogInquirieItem.StatusColor = StatusColor.Blue;
            resultLogInquirieItem.StatusResult = "OK";
            resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
            resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
            ResultLogInquirieItems.Add(resultLogInquirieItem);

            resultLogInquirieItem = new ResultLogInquirieItem();
            resultLogInquirieItem.LogTitle = this.LogTitle;
            resultLogInquirieItem.IdLog = this.LogId;
            resultLogInquirieItem.IdInstance = 1;
            resultLogInquirieItem.Pause = "";
            resultLogInquirieItem.NameInstance = "Inst - 1";
            resultLogInquirieItem.IdLog = 1;
            resultLogInquirieItem.StartHour = "10:13";
            resultLogInquirieItem.NameLot = "Lote 14";
            resultLogInquirieItem.IdCommand = 1;
            resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
            resultLogInquirieItem.Status = "FIN";
            resultLogInquirieItem.StatusColor = StatusColor.Red;
            resultLogInquirieItem.StatusResult = "ERROR";
            resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
            resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
            ResultLogInquirieItems.Add(resultLogInquirieItem);

            resultLogInquirieItem = new ResultLogInquirieItem();
            resultLogInquirieItem.LogTitle = this.LogTitle;
            resultLogInquirieItem.IdLog = this.LogId;
            resultLogInquirieItem.IdInstance = 1;
            resultLogInquirieItem.Pause = "";
            resultLogInquirieItem.NameInstance = "Inst - 1";
            resultLogInquirieItem.IdLog = 1;
            resultLogInquirieItem.StartHour = "10:13";
            resultLogInquirieItem.NameLot = "Lote 14";
            resultLogInquirieItem.IdCommand = 1;
            resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
            resultLogInquirieItem.Status = "FIN";
            resultLogInquirieItem.StatusColor = StatusColor.Blue;
            resultLogInquirieItem.StatusResult = "OK";
            resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
            resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
            ResultLogInquirieItems.Add(resultLogInquirieItem);

            resultLogInquirieItem = new ResultLogInquirieItem();
            resultLogInquirieItem.LogTitle = this.LogTitle;
            resultLogInquirieItem.IdLog = this.LogId;
            resultLogInquirieItem.IdInstance = 1;
            resultLogInquirieItem.Pause = "";
            resultLogInquirieItem.NameInstance = "Inst - 1";
            resultLogInquirieItem.IdLog = 1;
            resultLogInquirieItem.StartHour = "10:13";
            resultLogInquirieItem.NameLot = "Lote 14";
            resultLogInquirieItem.IdCommand = 1;
            resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
            resultLogInquirieItem.Status = "FIN";
            resultLogInquirieItem.StatusColor = StatusColor.Blue;
            resultLogInquirieItem.StatusResult = "OK";
            resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
            resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
            ResultLogInquirieItems.Add(resultLogInquirieItem);

            resultLogInquirieItem = new ResultLogInquirieItem();
            resultLogInquirieItem.LogTitle = this.LogTitle;
            resultLogInquirieItem.IdLog = this.LogId;
            resultLogInquirieItem.IdInstance = 1;
            resultLogInquirieItem.Pause = "";
            resultLogInquirieItem.NameInstance = "Inst - 1";
            resultLogInquirieItem.IdLog = 1;
            resultLogInquirieItem.StartHour = "10:13";
            resultLogInquirieItem.NameLot = "Lote 14";
            resultLogInquirieItem.IdCommand = 1;
            resultLogInquirieItem.NameCommand = "8014 - Prueba Grande 14";
            resultLogInquirieItem.Status = "EJECUTANDO";
            resultLogInquirieItem.StatusColor = StatusColor.Green;
            resultLogInquirieItem.StatusResult = "";
            resultLogInquirieItem.StartDateTime = "24/05 15:45:27";
            resultLogInquirieItem.EndDateTime = "24/05 15:45:27";
            ResultLogInquirieItems.Add(resultLogInquirieItem);

            LotItems = new ObservableCollection<PickerLotItem>();
            PickerLotItem lotItem;

            lotItem = new PickerLotItem();
            lotItem.Id = 1;
            lotItem.LotName = "LOTE / COMANDO 1";
            LotItems.Add(lotItem);

            lotItem = new PickerLotItem();
            lotItem.Id = 2;
            lotItem.LotName = "LOTE / COMANDO 2";
            LotItems.Add(lotItem);

            lotItem = new PickerLotItem();
            lotItem.Id = 3;
            lotItem.LotName = "LOTE / COMANDO 3";
            LotItems.Add(lotItem);

            CommandItems = new ObservableCollection<PickerCommandItem>();
            PickerCommandItem commandItem;

            commandItem = new PickerCommandItem();
            commandItem.Id = 1;
            commandItem.CommandName = "COMANDO 1";
            CommandItems.Add(commandItem);

            commandItem = new PickerCommandItem();
            commandItem.Id = 2;
            commandItem.CommandName = "COMANDO 2";
            CommandItems.Add(commandItem);

            commandItem = new PickerCommandItem();
            commandItem.Id = 3;
            commandItem.CommandName = "COMANDO 3";
            CommandItems.Add(commandItem);

            LogInquirieOperatorItems = new ObservableCollection<LogInquirieOperatorItem>();
            LogInquirieOperatorItem logInquirieOperatorItem;

            logInquirieOperatorItem = new LogInquirieOperatorItem();
            logInquirieOperatorItem.IdInstance = 1;
            logInquirieOperatorItem.NameInstance = "Inst - 1";
            logInquirieOperatorItem.IdOperator = 1;
            logInquirieOperatorItem.NameOperator = "ADMINISTRADOR";
            LogInquirieOperatorItems.Add(logInquirieOperatorItem);

            logInquirieOperatorItem = new LogInquirieOperatorItem();
            logInquirieOperatorItem.IdInstance = 1;
            logInquirieOperatorItem.NameInstance = "Inst - 1";
            logInquirieOperatorItem.IdOperator = 2;
            logInquirieOperatorItem.NameOperator = "ADMINISTRADOR WEB";
            LogInquirieOperatorItems.Add(logInquirieOperatorItem);

            logInquirieOperatorItem = new LogInquirieOperatorItem();
            logInquirieOperatorItem.IdInstance = 1;
            logInquirieOperatorItem.NameInstance = "Inst - 1";
            logInquirieOperatorItem.IdOperator = 3;
            logInquirieOperatorItem.NameOperator = "PLANIFICADOR";
            LogInquirieOperatorItems.Add(logInquirieOperatorItem);

            logInquirieOperatorItem = new LogInquirieOperatorItem();
            logInquirieOperatorItem.IdInstance = 1;
            logInquirieOperatorItem.NameInstance = "Inst - 1";
            logInquirieOperatorItem.IdOperator = 4;
            logInquirieOperatorItem.NameOperator = "SUPERVISOR";
            LogInquirieOperatorItems.Add(logInquirieOperatorItem);

            logInquirieOperatorItem = new LogInquirieOperatorItem();
            logInquirieOperatorItem.IdInstance = 1;
            logInquirieOperatorItem.NameInstance = "Inst - 1";
            logInquirieOperatorItem.IdOperator = 5;
            logInquirieOperatorItem.NameOperator = "TESTER";
            LogInquirieOperatorItems.Add(logInquirieOperatorItem);
        }
        #endregion
    }
}
