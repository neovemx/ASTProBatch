using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using AST_ProBatch_Mobile.Models.Service;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

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
        private bool compactviewisvisible;
        private bool fullviewisvisible;
        private bool isvisibleemptyview;
        private LogItem logitem;
        private bool isexecution;
        #endregion

        #region Properties
        public List<Instance> Instances { get; set; }

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
        public bool IsVisibleEmptyView
        {
            get { return isvisibleemptyview; }
            set { SetValue(ref isvisibleemptyview, value); }
        }
        public LogItem LogItem
        {
            get { return logitem; }
            set { SetValue(ref logitem, value); }
        }
        public bool IsExecution
        {
            get { return isexecution; }
            set { SetValue(ref isexecution, value); }
        }
        #endregion

        #region Constructors
        public ExecutionStageTwoViewModel(bool IsReload, LogItem logitem)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                this.LogItem = logitem;
                this.IsExecution = logitem.IsExecution;
                this.ToolBarIsVisible = false;
                this.ActionIcon = "actions";
                this.CheckIcon = "check";
                this.ViewIcon = "view_b";
                this.FullViewIsVisible = true;
                this.CompactViewIsVisible = false;
                this.IsVisibleEmptyView = false;

                GetInstancesByLogAndUser(this.LogItem.IdLog, this.LogItem.IdUser, this.LogItem.IsEventual);

                //GetFakeData();
            }
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
            if (this.IsVisibleEmptyView)
            {
                Alert.Show("No hay datos para realizar operaciones!");
                return;
            }
            if (this.InstanceItems.Count == 1)
            {
                Alert.Show("Sólo hay una instancia en la vista!");
                return;
            }

            int countItems = 0;
            foreach (InstanceItem item in InstanceItems)
            {
                if (item.IsChecked) { countItems += 1; }
            }
            if (countItems <= 1)
            {
                Alert.Show("Debe seleccionar dos o más instancias");
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
                if (this.IsVisibleEmptyView)
                {
                    Alert.Show("No hay datos para realizar operaciones!");
                    return;
                }

                if (this.InstanceItems.Count == 1)
                {
                    Alert.Show("Sólo hay una instancia en la vista!");
                    return;
                }

                if (string.CompareOrdinal(CheckIcon, "check") == 0)
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

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
                        var InstanceItemsTemp = InstanceItems;
                        InstanceItems = new ObservableCollection<InstanceItem>();
                        foreach (InstanceItem item in InstanceItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            InstanceItems.Add(item);
                        }
                        ToolBarIsVisible = false;
                        CheckIcon = "check";

                        UserDialogs.Instance.HideLoading();
                    });
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Ocurrió un error", "Aceptar");
                UserDialogs.Instance.HideLoading();
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
            try
            {
                if (this.IsVisibleEmptyView)
                {
                    Alert.Show("No hay datos para realizar operaciones!");
                    return;
                }

                if (FullViewIsVisible)
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

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
                        ToolBarIsVisible = false;
                        CheckIcon = "check";

                        UserDialogs.Instance.HideLoading();
                    });
                }
                else
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

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
                        ToolBarIsVisible = false;
                        CheckIcon = "check";

                        UserDialogs.Instance.HideLoading();
                    });
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Ocurrió un error: " + "/n/r/n/r" + ex.Message, "Aceptar");
                UserDialogs.Instance.HideLoading();
            }

        }
        #endregion

        #region Helpers
        private async void GetInstancesByLogAndUser(int IdLog, string IdUser, bool IsEventual)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo instancias...", MaskType.Black);

                Response resultApiIsAvailable = await ApiSrv.ApiIsAvailable();

                if (!resultApiIsAvailable.IsSuccess)
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(resultApiIsAvailable.Message);
                    return;
                }

                Response resultToken = await ApiSrv.GetToken();

                if (!resultToken.IsSuccess)
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(resultToken.Message);
                    return;
                }
                else
                {
                    Token token = JsonConvert.DeserializeObject<Token>(Crypto.DecodeString(resultToken.Data));
                    InstanceQueryValues instanceQueryValues = new InstanceQueryValues()
                    {
                        IdLog = IdLog,
                        IdUser = IdUser,
                        IsEventual = IsEventual
                    };
                    Response resultGetInstancesByLogAndUser = await ApiSrv.GetInstancesByLogAndUser(token.Key, instanceQueryValues);
                    if (!resultGetInstancesByLogAndUser.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(resultGetInstancesByLogAndUser.Message);
                        return;
                    }
                    else
                    {
                        Instances = JsonConvert.DeserializeObject<List<Instance>>(Crypto.DecodeString(resultGetInstancesByLogAndUser.Data));
                        InstanceItems = new ObservableCollection<InstanceItem>();
                        foreach (Instance instance in Instances)
                        {
                            InstanceItems.Add(new InstanceItem()
                            {
                                IsExecution = this.IsExecution,
                                IdInstance = instance.IdInstance,
                                InstanceNumber = instance.InstanceNumber,
                                NameInstance = instance.NameInstance.Trim(),
                                IdStatusInstance = instance.IdStatusInstance,
                                IdStatusLastCommand = instance.IdStatusLastCommand,
                                TotalCommands = instance.TotalCommands,
                                PendingCommands = instance.PendingCommands,
                                OkCommands = instance.OkCommands,
                                ErrorCommands = instance.ErrorCommands,
                                OmittedCommands = instance.OmittedCommands,
                                IsChecked = false,
                                IsEnabled = true,
                                NotificationIcon = IconSet.Notification,
                                StatusInstanceColor = GetStatusColor.ByIdStatus(instance.IdStatusInstance.Trim()),
                                StatusLastProcessColor = GetStatusColor.ByIdStatus(instance.IdStatusLastCommand.Trim()),
                                LogItem = this.LogItem
                            });
                        }
                        if (InstanceItems.Count == 0)
                        {
                            this.FullViewIsVisible = false;
                            this.CompactViewIsVisible = false;
                            this.IsVisibleEmptyView = true;
                        }
                        else
                        {
                            this.FullViewIsVisible = true;
                            this.CompactViewIsVisible = false;
                            this.IsVisibleEmptyView = false;
                        }
                    }
                }
                UserDialogs.Instance.HideLoading();
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError("Ocurrió un error.");
            }
        }
        #endregion

        #region FakeData
        //private void GetFakeData()
        //{
        //    InstanceItems = new ObservableCollection<InstanceItem>();
        //    InstanceItem instanceItem;
        //    LotItem lotItem;
        //    CommandItem commandItem;

        //    instanceItem = new InstanceItem();
        //    instanceItem.Id = 1;
        //    instanceItem.IsChecked = false;
        //    instanceItem.IsEnabled = true;
        //    instanceItem.Title = "INSTANCIA 1";
        //    instanceItem.Notifications = "notification";
        //    instanceItem.Status = "";
        //    instanceItem.StatusColor = StatusColor.Green;
        //    instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    instanceItem.StatusLastProcess = "F";
        //    instanceItem.StatusLastProcessColor = StatusColor.Green;
        //    instanceItem.CommandsTotal = 500;
        //    instanceItem.CommandsPending = 100;
        //    instanceItem.CommandsOk = 200;
        //    instanceItem.CommandsError = 50;
        //    instanceItem.CommandsOmitted = 150;
        //    //instanceItem.IsEventual = this.IsEventual;
        //    //instanceItem.IsNotEventual = this.IsNotEventual;
        //    instanceItem.LotItems = new ObservableCollection<LotItem>();
        //    lotItem = new LotItem();
        //    lotItem.Id = 1;
        //    lotItem.Code = 51;
        //    lotItem.Title = "51 - Prueba";
        //    lotItem.Description = "Prueba";
        //    lotItem.DateCreation = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    lotItem.InPath = "";
        //    lotItem.OutPath = "";
        //    lotItem.StatusSelected = new PickerStatusItem() { Id = 1, StatusName = "A - ACTIVO", StatusIndex = 0 };
        //    lotItem.CommandItems = new ObservableCollection<CommandItem>();
        //    commandItem = new CommandItem();
        //    commandItem.Id = 1;
        //    commandItem.IdLot = 1;
        //    commandItem.TitleLot = "LOTE - 1";
        //    commandItem.IdEnvironment = 1;
        //    commandItem.TitleEnvironment = "Windows";
        //    commandItem.IsChecked = false;
        //    commandItem.IsEnabled = true;
        //    commandItem.Title = "COMANDO 1";
        //    commandItem.Notifications = "notification";
        //    commandItem.Status = "";
        //    commandItem.StatusColor = StatusColor.Green;
        //    commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.Duration = "1 hora 45 minutos";
        //    //commandItem.IsEventual = this.IsEventual;
        //    //commandItem.IsNotEventual = this.IsNotEventual;
        //    lotItem.CommandItems.Add(commandItem);
        //    instanceItem.LotItems.Add(lotItem);
        //    InstanceItems.Add(instanceItem);

        //    instanceItem = new InstanceItem();
        //    instanceItem.Id = 2;
        //    instanceItem.IsChecked = false;
        //    instanceItem.IsEnabled = true;
        //    instanceItem.Title = "INSTANCIA 2";
        //    instanceItem.Notifications = "notification";
        //    instanceItem.Status = "";
        //    instanceItem.StatusColor = StatusColor.Orange;
        //    instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    instanceItem.StatusLastProcess = "E";
        //    instanceItem.StatusLastProcessColor = StatusColor.Red;
        //    instanceItem.CommandsTotal = 500;
        //    instanceItem.CommandsPending = 100;
        //    instanceItem.CommandsOk = 200;
        //    instanceItem.CommandsError = 50;
        //    instanceItem.CommandsOmitted = 150;
        //    //instanceItem.IsEventual = this.IsEventual;
        //    //instanceItem.IsNotEventual = this.IsNotEventual;
        //    instanceItem.LotItems = new ObservableCollection<LotItem>();
        //    lotItem = new LotItem();
        //    lotItem.Id = 1;
        //    lotItem.Code = 51;
        //    lotItem.Title = "51 - Prueba";
        //    lotItem.Description = "Prueba";
        //    lotItem.DateCreation = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    lotItem.InPath = "";
        //    lotItem.OutPath = "";
        //    lotItem.StatusSelected = new PickerStatusItem() { Id = 1, StatusName = "A - ACTIVO", StatusIndex = 0 };
        //    lotItem.CommandItems = new ObservableCollection<CommandItem>();
        //    commandItem = new CommandItem();
        //    commandItem.Id = 1;
        //    commandItem.IdLot = 1;
        //    commandItem.TitleLot = "LOTE - 1";
        //    commandItem.IdEnvironment = 1;
        //    commandItem.TitleEnvironment = "Windows";
        //    commandItem.IsChecked = false;
        //    commandItem.IsEnabled = true;
        //    commandItem.Title = "COMANDO 1";
        //    commandItem.Notifications = "notification";
        //    commandItem.Status = "";
        //    commandItem.StatusColor = StatusColor.Green;
        //    commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.Duration = "1 hora 45 minutos";
        //    //commandItem.IsEventual = this.IsEventual;
        //    //commandItem.IsNotEventual = this.IsNotEventual;
        //    lotItem.CommandItems.Add(commandItem);
        //    instanceItem.LotItems.Add(lotItem);
        //    InstanceItems.Add(instanceItem);

        //    instanceItem = new InstanceItem();
        //    instanceItem.Id = 3;
        //    instanceItem.IsChecked = false;
        //    instanceItem.IsEnabled = true;
        //    instanceItem.Title = "INSTANCIA 3";
        //    instanceItem.Notifications = "notification_n";
        //    instanceItem.Status = "";
        //    instanceItem.StatusColor = StatusColor.Green;
        //    instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    instanceItem.StatusLastProcess = "P";
        //    instanceItem.StatusLastProcessColor = StatusColor.White;
        //    instanceItem.CommandsTotal = 500;
        //    instanceItem.CommandsPending = 100;
        //    instanceItem.CommandsOk = 200;
        //    instanceItem.CommandsError = 50;
        //    instanceItem.CommandsOmitted = 150;
        //    //instanceItem.IsEventual = this.IsEventual;
        //    //instanceItem.IsNotEventual = this.IsNotEventual;
        //    instanceItem.LotItems = new ObservableCollection<LotItem>();
        //    lotItem = new LotItem();
        //    lotItem.Id = 1;
        //    lotItem.Code = 51;
        //    lotItem.Title = "51 - Prueba";
        //    lotItem.Description = "Prueba";
        //    lotItem.DateCreation = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    lotItem.InPath = "";
        //    lotItem.OutPath = "";
        //    lotItem.StatusSelected = new PickerStatusItem() { Id = 1, StatusName = "A - ACTIVO", StatusIndex = 0 };
        //    lotItem.CommandItems = new ObservableCollection<CommandItem>();
        //    commandItem = new CommandItem();
        //    commandItem.Id = 1;
        //    commandItem.IdLot = 1;
        //    commandItem.TitleLot = "LOTE - 1";
        //    commandItem.IdEnvironment = 1;
        //    commandItem.TitleEnvironment = "Windows";
        //    commandItem.IsChecked = false;
        //    commandItem.IsEnabled = true;
        //    commandItem.Title = "COMANDO 1";
        //    commandItem.Notifications = "notification";
        //    commandItem.Status = "";
        //    commandItem.StatusColor = StatusColor.Green;
        //    commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.Duration = "1 hora 45 minutos";
        //    //commandItem.IsEventual = this.IsEventual;
        //    //commandItem.IsNotEventual = this.IsNotEventual;
        //    lotItem.CommandItems.Add(commandItem);
        //    instanceItem.LotItems.Add(lotItem);
        //    InstanceItems.Add(instanceItem);

        //    instanceItem = new InstanceItem();
        //    instanceItem.Id = 4;
        //    instanceItem.IsChecked = false;
        //    instanceItem.IsEnabled = true;
        //    instanceItem.Title = "INSTANCIA 4";
        //    instanceItem.Notifications = "notification_n";
        //    instanceItem.Status = "";
        //    instanceItem.StatusColor = StatusColor.White;
        //    instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    instanceItem.StatusLastProcess = "D";
        //    instanceItem.StatusLastProcessColor = StatusColor.Orange;
        //    instanceItem.CommandsTotal = 500;
        //    instanceItem.CommandsPending = 100;
        //    instanceItem.CommandsOk = 200;
        //    instanceItem.CommandsError = 50;
        //    instanceItem.CommandsOmitted = 150;
        //    //instanceItem.IsEventual = this.IsEventual;
        //    //instanceItem.IsNotEventual = this.IsNotEventual;
        //    instanceItem.LotItems = new ObservableCollection<LotItem>();
        //    lotItem = new LotItem();
        //    lotItem.Id = 1;
        //    lotItem.Code = 51;
        //    lotItem.Title = "51 - Prueba";
        //    lotItem.Description = "Prueba";
        //    lotItem.DateCreation = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    lotItem.InPath = "";
        //    lotItem.OutPath = "";
        //    lotItem.StatusSelected = new PickerStatusItem() { Id = 1, StatusName = "A - ACTIVO", StatusIndex = 0 };
        //    lotItem.CommandItems = new ObservableCollection<CommandItem>();
        //    commandItem = new CommandItem();
        //    commandItem.Id = 1;
        //    commandItem.IdLot = 1;
        //    commandItem.TitleLot = "LOTE - 1";
        //    commandItem.IdEnvironment = 1;
        //    commandItem.TitleEnvironment = "Windows";
        //    commandItem.IsChecked = false;
        //    commandItem.IsEnabled = true;
        //    commandItem.Title = "COMANDO 1";
        //    commandItem.Notifications = "notification";
        //    commandItem.Status = "";
        //    commandItem.StatusColor = StatusColor.Green;
        //    commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.Duration = "1 hora 45 minutos";
        //    //commandItem.IsEventual = this.IsEventual;
        //    //commandItem.IsNotEventual = this.IsNotEventual;
        //    lotItem.CommandItems.Add(commandItem);
        //    instanceItem.LotItems.Add(lotItem);
        //    InstanceItems.Add(instanceItem);

        //    instanceItem = new InstanceItem();
        //    instanceItem.Id = 5;
        //    instanceItem.IsChecked = false;
        //    instanceItem.IsEnabled = true;
        //    instanceItem.Title = "INSTANCIA 5";
        //    instanceItem.Notifications = "notification";
        //    instanceItem.Status = "";
        //    instanceItem.StatusColor = StatusColor.White;
        //    instanceItem.Execution = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    instanceItem.StatusLastProcess = "F";
        //    instanceItem.StatusLastProcessColor = StatusColor.Green;
        //    instanceItem.CommandsTotal = 500;
        //    instanceItem.CommandsPending = 100;
        //    instanceItem.CommandsOk = 200;
        //    instanceItem.CommandsError = 50;
        //    instanceItem.CommandsOmitted = 150;
        //    //instanceItem.IsEventual = this.IsEventual;
        //    //instanceItem.IsNotEventual = this.IsNotEventual;
        //    instanceItem.LotItems = new ObservableCollection<LotItem>();
        //    lotItem = new LotItem();
        //    lotItem.Id = 1;
        //    lotItem.Code = 51;
        //    lotItem.Title = "51 - Prueba";
        //    lotItem.Description = "Prueba";
        //    lotItem.DateCreation = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    lotItem.InPath = "";
        //    lotItem.OutPath = "";
        //    lotItem.StatusSelected = new PickerStatusItem() { Id = 1, StatusName = "A - ACTIVO", StatusIndex = 0 };
        //    lotItem.CommandItems = new ObservableCollection<CommandItem>();
        //    commandItem = new CommandItem();
        //    commandItem.Id = 1;
        //    commandItem.IdLot = 1;
        //    commandItem.TitleLot = "LOTE - 1";
        //    commandItem.IdEnvironment = 1;
        //    commandItem.TitleEnvironment = "Windows";
        //    commandItem.IsChecked = false;
        //    commandItem.IsEnabled = true;
        //    commandItem.Title = "COMANDO 1";
        //    commandItem.Notifications = "notification";
        //    commandItem.Status = "";
        //    commandItem.StatusColor = StatusColor.Green;
        //    commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.Duration = "1 hora 45 minutos";
        //    //commandItem.IsEventual = this.IsEventual;
        //    //commandItem.IsNotEventual = this.IsNotEventual;
        //    lotItem.CommandItems.Add(commandItem);
        //    instanceItem.LotItems.Add(lotItem);
        //    InstanceItems.Add(instanceItem);
        //}
        #endregion
    }
}
