using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
        //private bool iseventual;
        //private bool isnoteventual;
        private InstanceItem instanceitem;
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

        //public bool IsEventual
        //{
        //    get { return iseventual; }
        //    set { SetValue(ref iseventual, value); }
        //}

        //public bool IsNotEventual
        //{
        //    get { return isnoteventual; }
        //    set { SetValue(ref isnoteventual, value); }
        //}

        public InstanceItem InstanceItem
        {
            get { return instanceitem; }
            set { SetValue(ref instanceitem, value); }
        }
        #endregion

        #region Constructors
        public ExecutionStageThreeViewModel(bool IsReload, InstanceItem instanceitem)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                this.InstanceItem = instanceitem;

                this.ToolBarIsVisible = false;
                this.ActionIcon = "actions";
                this.CheckIcon = "check";
                this.ViewIcon = "view_b";
                this.FullViewIsVisible = true;
                this.CompactViewIsVisible = false;

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
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Ocurrió un error: " + "/n/r/n/r" + ex.Message, "Aceptar");
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
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Ocurrió un error: " + "/n/r/n/r" + ex.Message, "Aceptar");
                UserDialogs.Instance.HideLoading();
            }

        }
        #endregion

        #region Helpers
        private async void GetCommandsByInstance(int IdInstance)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

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

                    }
                }
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
