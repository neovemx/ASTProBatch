using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using AST_ProBatch_Mobile.Models.Service;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Cmd = AST_ProBatch_Mobile.Models.Service.Command;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class ExecutionStageThreeViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<CommandItem> commanditems;
        private bool toolbarisvisible;
        private string actionicon;
        private string checkicon;
        private string viewicon;
        private bool isrefreshing;
        private bool compactviewisvisible;
        private bool fullviewisvisible;
        private bool isvisibleemptyview;
        private InstanceItem instanceitem;
        private bool isexecution;
        private bool iseventual;
        #endregion

        #region Properties
        public List<Cmd> Commands { get; set; }

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
        public InstanceItem InstanceItem
        {
            get { return instanceitem; }
            set { SetValue(ref instanceitem, value); }
        }
        public bool IsExecution
        {
            get { return isexecution; }
            set { SetValue(ref isexecution, value); }
        }
        public bool IsEventual
        {
            get { return iseventual; }
            set { SetValue(ref iseventual, value); }
        }
        #endregion

        #region Constructors
        public ExecutionStageThreeViewModel(bool IsReload, InstanceItem instanceitem)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                this.InstanceItem = instanceitem;
                this.IsExecution = instanceitem.LogItem.IsExecution;
                this.IsEventual = instanceitem.LogItem.IsEventual;
                this.ToolBarIsVisible = false;
                this.ActionIcon = "actions";
                this.CheckIcon = "check";
                this.ViewIcon = "view_b";
                this.FullViewIsVisible = true;
                this.CompactViewIsVisible = false;
                this.IsVisibleEmptyView = false;

                GetCommandsByInstance(this.InstanceItem.IdInstance);

                //GetFakeData();
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new Xamarin.Forms.Command(async () =>
                {
                    try
                    {
                        this.IsRefreshing = true;
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
                            CommandQueryValues commandQueryValues = new CommandQueryValues()
                            {
                                IdInstance = this.InstanceItem.IdInstance
                            };
                            Response resultGetCommandsByInstance = await ApiSrv.GetCommandsByInstance(token.Key, commandQueryValues);
                            if (!resultGetCommandsByInstance.IsSuccess)
                            {
                                UserDialogs.Instance.HideLoading();
                                Toast.ShowError(resultGetCommandsByInstance.Message);
                                return;
                            }
                            else
                            {
                                Commands = JsonConvert.DeserializeObject<List<Cmd>>(Crypto.DecodeString(resultGetCommandsByInstance.Data));
                                CommandItems.Clear();
                                foreach (Cmd command in Commands)
                                {
                                    TimeSpan execTime = TimeSpan.FromMinutes(command.ExecutionTime);
                                    CommandItems.Add(new CommandItem()
                                    {
                                        IsExecution = this.IsExecution,
                                        IdLot = command.IdLot,
                                        NameLot = command.NameLot.Trim(),
                                        IdCommand = command.IdCommand,
                                        NameCommand = command.NameCommand.Trim(),
                                        Order = command.Order,
                                        IdStatus = command.IdStatus,
                                        Duration = string.Format("{0:00} hora(s) {1:00} minuto(s)", (int)execTime.TotalHours, execTime.Minutes),
                                        ExecutionStart = (command.ExecutionDateTime != null) ? ((DateTime)command.ExecutionDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                        ExecutionEnd = (command.EndDateTime != null) ? ((DateTime)command.EndDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                        IsChecked = false,
                                        IsEnabled = true,
                                        StatusColor = GetStatusColor.ByIdStatus(command.IdStatus.Trim()),
                                        InstanceItem = this.InstanceItem,
                                        BarItemColor = (this.InstanceItem.LogItem.IsEventual) ? BarItemColor.HighLight : BarItemColor.Base
                                    });
                                }
                                if (CommandItems.Count == 0)
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
                        this.IsRefreshing = false;
                    }
                    catch //(Exception ex)
                    {
                        Alert.Show("Ocurrió un error", "Aceptar");
                        this.IsRefreshing = false;
                    }
                });
            }
        }

        public ICommand ActionsCommand
        {
            get
            {
                return new RelayCommand(Actions);
            }
        }

        private async void Actions()
        {
            if (this.IsEventual)
            {
                Alert.Show("La bitácora es eventual!");
                return;
            }
            if (!this.IsExecution)
            {
                Alert.Show("Modo Monitoreo, ingrese a través de Ejecución!");
                return;
            }
            if (this.IsVisibleEmptyView)
            {
                Alert.Show("No hay datos para realizar operaciones!");
                return;
            }
            if (this.CommandItems.Count == 1)
            {
                Alert.Show("Sólo hay un comando en la vista!");
                return;
            }

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
            try
            {
                if (this.IsEventual)
                {
                    Alert.Show("La bitácora es eventual!");
                    return;
                }
                if (!this.IsExecution)
                {
                    Alert.Show("Modo Monitoreo, ingrese a través de Ejecución!");
                    return;
                }
                if (this.IsVisibleEmptyView)
                {
                    Alert.Show("No hay datos para realizar operaciones!");
                    return;
                }
                if (this.CommandItems.Count == 1)
                {
                    Alert.Show("Sólo hay un comando en la vista!");
                    return;
                }

                if (string.CompareOrdinal(CheckIcon, "check") == 0)
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

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
                        var CommandItemsTemp = CommandItems;
                        CommandItems = new ObservableCollection<CommandItem>();
                        foreach (CommandItem item in CommandItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            CommandItems.Add(item);
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
                        var CommandItemsTemp = CommandItems;
                        CommandItems = new ObservableCollection<CommandItem>();
                        foreach (CommandItem item in CommandItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            CommandItems.Add(item);
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
                        var CommandItemsTemp = CommandItems;
                        CommandItems = new ObservableCollection<CommandItem>();
                        foreach (CommandItem item in CommandItemsTemp)
                        {
                            item.IsChecked = false;
                            item.IsEnabled = true;
                            CommandItems.Add(item);
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
                Alert.Show("Ocurrió un error", "Aceptar");
                UserDialogs.Instance.HideLoading();
            }

        }
        #endregion

        #region Helpers
        private async void GetCommandsByInstance(int IdInstance)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo lote/comandos...", MaskType.Black);

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
                    CommandQueryValues commandQueryValues = new CommandQueryValues()
                    {
                        IdInstance = this.InstanceItem.IdInstance
                    };
                    Response resultGetCommandsByInstance = await ApiSrv.GetCommandsByInstance(token.Key, commandQueryValues);
                    if (!resultGetCommandsByInstance.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(resultGetCommandsByInstance.Message);
                        return;
                    }
                    else
                    {
                        Commands = JsonConvert.DeserializeObject<List<Cmd>>(Crypto.DecodeString(resultGetCommandsByInstance.Data));
                        CommandItems = new ObservableCollection<CommandItem>();
                        foreach (Cmd command in Commands)
                        {
                            TimeSpan execTime = TimeSpan.FromMinutes(command.ExecutionTime);
                            CommandItems.Add(new CommandItem()
                            {
                                IsExecution = this.IsExecution,
                                IdLot = command.IdLot,
                                NameLot = command.NameLot.Trim(),
                                IdCommand = command.IdCommand,
                                NameCommand = command.NameCommand.Trim(),
                                Order = command.Order,
                                IdStatus = command.IdStatus,
                                Duration = string.Format("{0:00} hora(s) {1:00} minuto(s)", (int)execTime.TotalHours, execTime.Minutes),
                                ExecutionStart = (command.ExecutionDateTime != null) ? ((DateTime)command.ExecutionDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                ExecutionEnd = (command.EndDateTime != null) ? ((DateTime)command.EndDateTime).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                IsChecked = false,
                                IsEnabled = true,
                                StatusColor = GetStatusColor.ByIdStatus(command.IdStatus.Trim()),
                                InstanceItem = this.InstanceItem,
                                BarItemColor = (this.InstanceItem.LogItem.IsEventual) ? BarItemColor.HighLight : BarItemColor.Base
                            });
                        }
                        if (CommandItems.Count == 0)
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
        //    CommandItems = new ObservableCollection<CommandItem>();
        //    CommandItem commandItem;

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
        //    commandItem.IsEventual = this.IsEventual;
        //    commandItem.IsNotEventual = this.IsNotEventual;
        //    CommandItems.Add(commandItem);

        //    commandItem = new CommandItem();
        //    commandItem.Id = 2;
        //    commandItem.IdLot = 1;
        //    commandItem.TitleLot = "LOTE - 1";
        //    commandItem.IdEnvironment = 1;
        //    commandItem.TitleEnvironment = "Windows";
        //    commandItem.IsChecked = false;
        //    commandItem.IsEnabled = true;
        //    commandItem.Title = "COMANDO 2";
        //    commandItem.Notifications = "notification";
        //    commandItem.Status = "";
        //    commandItem.StatusColor = StatusColor.Orange;
        //    commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.Duration = "1 hora 45 minutos";
        //    commandItem.IsEventual = this.IsEventual;
        //    commandItem.IsNotEventual = this.IsNotEventual;
        //    CommandItems.Add(commandItem);

        //    commandItem = new CommandItem();
        //    commandItem.Id = 3;
        //    commandItem.IdLot = 1;
        //    commandItem.TitleLot = "LOTE - 1";
        //    commandItem.IdEnvironment = 1;
        //    commandItem.TitleEnvironment = "Windows";
        //    commandItem.IsChecked = false;
        //    commandItem.IsEnabled = true;
        //    commandItem.Title = "COMANDO 3";
        //    commandItem.Notifications = "notification_n";
        //    commandItem.Status = "";
        //    commandItem.StatusColor = StatusColor.Green;
        //    commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.Duration = "1 hora 45 minutos";
        //    commandItem.IsEventual = this.IsEventual;
        //    commandItem.IsNotEventual = this.IsNotEventual;
        //    CommandItems.Add(commandItem);

        //    commandItem = new CommandItem();
        //    commandItem.Id = 4;
        //    commandItem.IdLot = 1;
        //    commandItem.TitleLot = "LOTE - 1";
        //    commandItem.IdEnvironment = 1;
        //    commandItem.TitleEnvironment = "Windows";
        //    commandItem.IsChecked = false;
        //    commandItem.IsEnabled = true;
        //    commandItem.Title = "COMANDO 4";
        //    commandItem.Notifications = "notification_n";
        //    commandItem.Status = "";
        //    commandItem.StatusColor = StatusColor.White;
        //    commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.Duration = "1 hora 45 minutos";
        //    commandItem.IsEventual = this.IsEventual;
        //    commandItem.IsNotEventual = this.IsNotEventual;
        //    CommandItems.Add(commandItem);

        //    commandItem = new CommandItem();
        //    commandItem.Id = 5;
        //    commandItem.IdLot = 1;
        //    commandItem.TitleLot = "LOTE - 1";
        //    commandItem.IdEnvironment = 1;
        //    commandItem.TitleEnvironment = "Windows";
        //    commandItem.IsChecked = false;
        //    commandItem.IsEnabled = true;
        //    commandItem.Title = "COMANDO 5";
        //    commandItem.Notifications = "notification";
        //    commandItem.Status = "";
        //    commandItem.StatusColor = StatusColor.White;
        //    commandItem.ExecutionStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.ExecutionEnd = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //    commandItem.Duration = "1 hora 45 minutos";
        //    commandItem.IsEventual = this.IsEventual;
        //    commandItem.IsNotEventual = this.IsNotEventual;
        //    CommandItems.Add(commandItem);
        //}
        #endregion
    }
}
