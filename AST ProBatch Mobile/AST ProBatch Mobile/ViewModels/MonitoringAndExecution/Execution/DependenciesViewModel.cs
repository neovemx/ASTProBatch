using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Models.Service;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using AST_ProBatch_Mobile.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class DependenciesViewModel : BaseViewModel
    {
        #region Atributes
        private CommandItem commanditem;
        private string listviewtitlelot;
        private string listviewtitlelotdetail;
        private string listviewtitlecommand;
        private string listviewtitlecommanddetail;
        private ObservableCollection<DependenciesLotItem> dependencieslotitems;
        private ObservableCollection<DependenciesLotItem> dependencieslotdetailitems;
        private ObservableCollection<DependenciesCommandItem> dependenciescommanditems;
        private ObservableCollection<DependenciesCommandDetailItem> dependenciescommanddetailitems;
        private ObservableCollection<DependenciesResourceItem> dependenciesresourceitems;
        #endregion

        #region Properties
        private Token TokenGet { get; set; }
        private List<DependenciesLot> DependenciesLots { get; set; }
        private List<DependenciesLot> DependenciesDetailLots { get; set; }
        private List<DependenciesCommand> DependenciesCommands { get; set; }
        private List<DependenciesCommandDetail> DependenciesDetailCommands { get; set; }
        private List<DependenciesResource> DependenciesResources { get; set; }

        public CommandItem CommandItem
        {
            get { return commanditem; }
            set { SetValue(ref commanditem, value); }
        }
        public string ListViewTitleLot
        {
            get { return listviewtitlelot; }
            set { SetValue(ref listviewtitlelot, value); }
        }
        public string ListViewTitleLotDetail
        {
            get { return listviewtitlelotdetail; }
            set { SetValue(ref listviewtitlelotdetail, value); }
        }
        public string ListViewTitleCommand
        {
            get { return listviewtitlecommand; }
            set { SetValue(ref listviewtitlecommand, value); }
        }
        public string ListViewTitleCommandDetail
        {
            get { return listviewtitlecommanddetail; }
            set { SetValue(ref listviewtitlecommanddetail, value); }
        }
        public ObservableCollection<DependenciesLotItem> DependenciesLotItems
        {
            get { return dependencieslotitems; }
            set { SetValue(ref dependencieslotitems, value); }
        }
        public ObservableCollection<DependenciesLotItem> DependenciesLotDetailItems
        {
            get { return dependencieslotdetailitems; }
            set { SetValue(ref dependencieslotdetailitems, value); }
        }
        public ObservableCollection<DependenciesCommandItem> DependenciesCommandItems
        {
            get { return dependenciescommanditems; }
            set { SetValue(ref dependenciescommanditems, value); }
        }
        public ObservableCollection<DependenciesCommandDetailItem> DependenciesCommandDetailItems
        {
            get { return dependenciescommanddetailitems; }
            set { SetValue(ref dependenciescommanddetailitems, value); }
        }
        public ObservableCollection<DependenciesResourceItem> DependenciesResourceItems
        {
            get { return dependenciesresourceitems; }
            set { SetValue(ref dependenciesresourceitems, value); }
        }
        #endregion

        #region Constructors
        public DependenciesViewModel(bool IsReload, CommandItem commandItem)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);

                this.CommandItem = commandItem;
                this.ListViewTitleLot = string.Format("Lotes de los que depende ({0})", commandItem.NameLot.Split('-')[0].Trim());
                this.ListViewTitleLotDetail = string.Format("Lotes que dependen de éste ({0})", commandItem.NameLot.Split('-')[0].Trim());
                this.ListViewTitleCommand = string.Format("Lotes-Comandos de los que depende ({0}-{1})", commandItem.NameLot.Split('-')[0].Trim(), commandItem.NameCommand.Split('-')[0].Trim());
                this.ListViewTitleCommandDetail = string.Format("Lotes-Comandos que dependen de éste ({0}-{1})", commandItem.NameLot.Split('-')[0].Trim(), commandItem.NameCommand.Split('-')[0].Trim());

                GetInitialData();
            }
        }
        #endregion

        #region Commands
        public ICommand StatusCommand
        {
            get
            {
                return new RelayCommand(Status);
            }
        }

        private async void Status()
        {

            MainViewModel.GetInstance().StatusInfoDependencies = new StatusInfoDependenciesViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new StatusInfoDependenciesPage());
        }
        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo lotes de los que depende...", MaskType.Black);
                if (!await ApiIsOnline())
                {
                    UserDialogs.Instance.HideLoading();
                    Toast.ShowError(AlertMessages.Error);
                    return;
                }
                else
                {
                    if (!await GetTokenSuccess())
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        DependenciesGetLotsThatDependsQueryValues dependenciesGetLotsThatDependsQueryValues = new DependenciesGetLotsThatDependsQueryValues()
                        {
                            IdLog = this.CommandItem.InstanceItem.LogItem.IdLog,
                            IdLot = this.commanditem.IdLot
                        };
                        Response resultGetDependenciesLots = await ApiSrv.DependenciesGetLotsThatDepends(TokenGet.Key, dependenciesGetLotsThatDependsQueryValues);
                        if (!resultGetDependenciesLots.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            DependenciesLots = JsonConvert.DeserializeObject<List<DependenciesLot>>(Crypto.DecodeString(resultGetDependenciesLots.Data));
                            DependenciesLotItems = new ObservableCollection<DependenciesLotItem>();
                            foreach (DependenciesLot dependenciesLot in DependenciesLots)
                            {
                                DependenciesLotItems.Add(new DependenciesLotItem()
                                {
                                    IdLot = dependenciesLot.IdLot,
                                    Code = dependenciesLot.Code,
                                    NameLot = dependenciesLot.NameLot,
                                    Type = dependenciesLot.Type,
                                    NameType = dependenciesLot.NameType,
                                    Status = dependenciesLot.Status,
                                    AddedDate = (dependenciesLot.AddedDate != null) ? (DateTime)dependenciesLot.AddedDate : new DateTime(),
                                    Number = dependenciesLot.Number,
                                    Instance = this.CommandItem.InstanceItem.NameInstance,
                                    StatusColor = GetStatusColor.ByIdStatus(dependenciesLot.IdStatus.Trim())
                                });
                            }
                        }
                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.ShowLoading("Obteniendo lotes que dependen de este...", MaskType.Black);
                        DependenciesGetDependentLotDetailQueryValues dependenciesGetDependentLotDetailQuery = new DependenciesGetDependentLotDetailQueryValues()
                        {
                            IdLog = this.CommandItem.InstanceItem.LogItem.IdLog,
                            IdLot = this.commanditem.IdLot
                        };
                        Response resultGetDependenciesLotsDetail = await ApiSrv.DependenciesGetDependentLotDetail(TokenGet.Key, dependenciesGetDependentLotDetailQuery);
                        if (!resultGetDependenciesLotsDetail.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            DependenciesDetailLots = JsonConvert.DeserializeObject<List<DependenciesLot>>(Crypto.DecodeString(resultGetDependenciesLotsDetail.Data));
                            DependenciesLotDetailItems = new ObservableCollection<DependenciesLotItem>();
                            foreach (DependenciesLot dependenciesLot in DependenciesDetailLots)
                            {
                                DependenciesLotDetailItems.Add(new DependenciesLotItem()
                                {
                                    IdLot = dependenciesLot.IdLot,
                                    Code = dependenciesLot.Code,
                                    NameLot = dependenciesLot.NameLot,
                                    Type = dependenciesLot.Type,
                                    NameType = dependenciesLot.NameType,
                                    Status = dependenciesLot.Status,
                                    AddedDate = (dependenciesLot.AddedDate != null) ? (DateTime)dependenciesLot.AddedDate : new DateTime(),
                                    Number = dependenciesLot.Number,
                                    Instance = this.CommandItem.InstanceItem.NameInstance,
                                    StatusColor = GetStatusColor.ByIdStatus(dependenciesLot.IdStatus.Trim())
                                });
                            }
                        }
                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.ShowLoading("Obteniendo lotes-comandos de los que depende...", MaskType.Black);
                        DependenciesGetCommandsThatDependsQueryValues dependenciesGetCommandsThatDependsQueryValues = new DependenciesGetCommandsThatDependsQueryValues()
                        {
                            IdLog = this.CommandItem.InstanceItem.LogItem.IdLog,
                            IdLot = this.commanditem.IdLot
                        };
                        Response resultGetDependenciesCommands = await ApiSrv.DependenciesGetCommandsThatDepends(TokenGet.Key, dependenciesGetCommandsThatDependsQueryValues);
                        if (!resultGetDependenciesCommands.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            DependenciesCommands = JsonConvert.DeserializeObject<List<DependenciesCommand>>(Crypto.DecodeString(resultGetDependenciesCommands.Data));
                            DependenciesCommandItems = new ObservableCollection<DependenciesCommandItem>();
                            foreach (DependenciesCommand dependenciesCommand in DependenciesCommands)
                            {
                                DependenciesCommandItems.Add(new DependenciesCommandItem()
                                {
                                    IdCommand = dependenciesCommand.IdCommand,
                                    Skip = dependenciesCommand.Skip,
                                    Type = dependenciesCommand.Type,
                                    IdLot = dependenciesCommand.IdLot,
                                    Lot = dependenciesCommand.Lot,
                                    Command = dependenciesCommand.Command,
                                    LotOrder = dependenciesCommand.LotOrder,
                                    NameType = dependenciesCommand.NameType,
                                    Status = dependenciesCommand.Status,
                                    AddedDate = (dependenciesCommand.AddedDate != null) ? (DateTime)dependenciesCommand.AddedDate : new DateTime(),
                                    Number = dependenciesCommand.Number,
                                    IdStatus = dependenciesCommand.IdStatus,
                                    CriticalBusiness = dependenciesCommand.CriticalBusiness,
                                    StatusColor = GetStatusColor.ByIdStatus(dependenciesCommand.IdStatus.Trim()),
                                    Instance = this.CommandItem.InstanceItem.NameInstance
                                });
                            }
                        }
                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.ShowLoading("Obteniendo lotes-comandos que dependen de este...", MaskType.Black);
                        DependenciesGetDependentCommandDetailQueryValues dependenciesGetDependentCommandDetailQueryValues = new DependenciesGetDependentCommandDetailQueryValues()
                        {
                            IdLog = this.CommandItem.InstanceItem.LogItem.IdLog,
                            IdLot = this.commanditem.IdLot
                        };
                        Response resultGetDependenciesCommandsDetail = await ApiSrv.DependenciesGetDependentCommandDetail(TokenGet.Key, dependenciesGetDependentCommandDetailQueryValues);
                        if (!resultGetDependenciesCommandsDetail.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            DependenciesDetailCommands = JsonConvert.DeserializeObject<List<DependenciesCommandDetail>>(Crypto.DecodeString(resultGetDependenciesCommandsDetail.Data));
                            DependenciesCommandDetailItems = new ObservableCollection<DependenciesCommandDetailItem>();
                            foreach (DependenciesCommandDetail dependenciesCommandDetail in DependenciesDetailCommands)
                            {
                                DependenciesCommandDetailItems.Add(new DependenciesCommandDetailItem()
                                {
                                    IdCommand = dependenciesCommandDetail.IdCommand,
                                    Skip = dependenciesCommandDetail.Skip,
                                    Type = dependenciesCommandDetail.Type,
                                    IdLot = dependenciesCommandDetail.IdLot,
                                    Lot = dependenciesCommandDetail.Lot,
                                    Command = dependenciesCommandDetail.Command,
                                    NameType = dependenciesCommandDetail.NameType,
                                    Number = dependenciesCommandDetail.Number,
                                    IdStatus = dependenciesCommandDetail.IdStatus,
                                    CriticalBusiness = dependenciesCommandDetail.CriticalBusiness,
                                    StatusColor = GetStatusColor.ByIdStatus(dependenciesCommandDetail.IdStatus.Trim()),
                                    Instance = this.CommandItem.InstanceItem.NameInstance
                                });
                            }
                        }
                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.ShowLoading("Obteniendo recursos de dependencias...", MaskType.Black);
                        DependenciesGetResourceQueryValues dependenciesGetResourceQueryValues = new DependenciesGetResourceQueryValues()
                        {
                            IdCommand = this.CommandItem.IdCommand
                        };
                        Response resultGetDependenciesResources = await ApiSrv.DependenciesGetResource(TokenGet.Key, dependenciesGetResourceQueryValues);
                        if (!resultGetDependenciesResources.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            DependenciesResources = JsonConvert.DeserializeObject<List<DependenciesResource>>(Crypto.DecodeString(resultGetDependenciesResources.Data));
                            DependenciesResourceItems = new ObservableCollection<DependenciesResourceItem>();
                            foreach (DependenciesResource dependenciesResource in DependenciesResources)
                            {
                                DependenciesResourceItems.Add(new DependenciesResourceItem()
                                {
                                    IdResource = dependenciesResource.IdResource,
                                    ResourceName = dependenciesResource.ResourceName
                                });
                            }
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }
        }

        private async Task<bool> ApiIsOnline()
        {
            bool result = false;
            Response resultApiIsAvailable = await ApiSrv.ApiIsAvailable();
            if (resultApiIsAvailable.IsSuccess)
            {
                result = true;
            }
            return result;
        }

        private async Task<bool> GetTokenSuccess()
        {
            bool result = false;
            Response resultToken = await ApiSrv.GetToken();
            if (resultToken.IsSuccess)
            {
                TokenGet = JsonConvert.DeserializeObject<Token>(Crypto.DecodeString(resultToken.Data));
                result = true;
            }
            return result;
        }
        #endregion
    }
}
