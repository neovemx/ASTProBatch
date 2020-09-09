using Acr.UserDialogs;
//using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Models.Service;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
using ASTProBatchMobile.Models.Service;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LogObservationsViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<ObservationItem> observationitems;
        private LogItem logitem;
        private string name;
        private string details;
        private ObservationItem selectedobservation;
        private bool isloadingdata;
        #endregion

        #region Properties
        private List<ObservationGet> Observations { get; set; }
        private List<ObservationAdd> ObservationAdd { get; set; }
        private List<ObservationMod> ObservationMod { get; set; }
        private List<ObservationDel> ObservationDel { get; set; }
        private Token TokenGet { get; set; }

        public ObservationItem SelectedObservation
        {
            get { return selectedobservation; }
            set
            {
                SetValue(ref selectedobservation, value);
                HandleSelectedObservation();
            }
        }
        public ObservableCollection<ObservationItem> ObservationItems
        {
            get { return observationitems; }
            set { SetValue(ref observationitems, value); }
        }
        public LogItem LogItem
        {
            get { return logitem; }
            set { SetValue(ref logitem, value); }
        }
        public string Name
        {
            get { return name; }
            set { SetValue(ref name, value); }
        }
        public string Details
        {
            get { return details; }
            set { SetValue(ref details, value); }
        }
        public bool IsLoadingData
        {
            get { return isloadingdata; }
            set { SetValue(ref isloadingdata, value); }
        }
        #endregion

        #region Constructors
        public LogObservationsViewModel(bool IsReload, LogItem logitem)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                this.LogItem = logitem;
                SelectedObservation = new ObservationItem();
                GetInitialData();
                //GetFakeData();
            }
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
            this.Name = string.Empty;
            this.Details = string.Empty;
            this.SelectedObservation = new ObservationItem();
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
                if (IsLoadingData)
                {
                    return;
                }
                if (String.IsNullOrWhiteSpace(this.Name))
                {
                    Alert.Show("Debe ingresar un nombre para la observación");
                    return;
                }

                if (String.IsNullOrWhiteSpace(this.Details))
                {
                    Alert.Show("Debe ingresar un detalle para la observación");
                    return;
                }

                bool result = await Confirm.Show("Desea guardar los cambios de la observación");
                if (result)
                {
                    IsLoadingData = true;
                    if (this.SelectedObservation.IdObsv != 0)
                    {
                        UserDialogs.Instance.ShowLoading(AlertMessages.Updating, MaskType.Black);
                        if (!TokenValidator.IsValid(TokenGet))
                        {
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
                            }
                        }
                        ObservationModQueryValues QueryValuesMod = new ObservationModQueryValues()
                        {
                            IdObsv = this.SelectedObservation.IdObsv,
                            IdLog = this.LogItem.IdLog,
                            IdUser = PbUser.IdUser,
                            NameObsv = this.Name,
                            DetailObsv = this.Details
                        };
                        Response resultObservationsMod = await ApiSrv.ModObservationsByLogAndUser(TokenGet.Key, QueryValuesMod);
                        if (!resultObservationsMod.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            ObservationMod = JsonConvert.DeserializeObject<List<ObservationMod>>(Crypto.DecodeString(resultObservationsMod.Data));
                            switch (ObservationMod[0].ResultModObservation)
                            {
                                case AddOrModResult.OK:
                                    UserDialogs.Instance.HideLoading();
                                    UserDialogs.Instance.ShowLoading(AlertMessages.UpdatingList, MaskType.Black);
                                    ObservationGetQueryValues QueryValuesGet = new ObservationGetQueryValues()
                                    {
                                        IdLog = this.LogItem.IdLog,
                                        IdUser = PbUser.IdUser
                                    };
                                    Response resultObservationsGet = await ApiSrv.GetObservationsByLogAndUser(TokenGet.Key, QueryValuesGet);
                                    if (!resultObservationsGet.IsSuccess)
                                    {
                                        UserDialogs.Instance.HideLoading();
                                        Toast.ShowError(AlertMessages.Error);
                                        return;
                                    }
                                    else
                                    {
                                        Observations = JsonConvert.DeserializeObject<List<ObservationGet>>(Crypto.DecodeString(resultObservationsGet.Data));
                                        ObservationItems.Clear();
                                        foreach (ObservationGet observationGet in Observations)
                                        {
                                            ObservationItems.Add(new ObservationItem()
                                            {
                                                IdObsv = observationGet.IdObsv,
                                                DateObsv = observationGet.DateObsv,
                                                NameObsv = observationGet.NameObsv,
                                                IdUser = observationGet.IdUser,
                                                DetailObsv = observationGet.DetailObsv,
                                                DateObsvString = observationGet.DateObsv.ToString(DateTimeFormatString.LatinDate)
                                            });
                                        }
                                        UserDialogs.Instance.HideLoading();
                                    }

                                    Toast.ShowSuccess(AlertMessages.Success);
                                    this.SelectedObservation = new ObservationItem();
                                    this.Name = string.Empty;
                                    this.Details = string.Empty;
                                    break;
                                case AddOrModResult.ERROR:
                                    UserDialogs.Instance.HideLoading();
                                    Toast.ShowError(AlertMessages.NoSuccess);
                                    break;
                                case AddOrModResult.EXISTING_DATA:
                                    UserDialogs.Instance.HideLoading();
                                    Alert.Show(AlertMessages.Existing);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        UserDialogs.Instance.ShowLoading(AlertMessages.Adding, MaskType.Black);
                        if (!TokenValidator.IsValid(TokenGet))
                        {
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
                            }
                        }
                        ObservationAddQueryValues QueryValuesAdd = new ObservationAddQueryValues()
                        {
                            IdLog = this.LogItem.IdLog,
                            IdUser = PbUser.IdUser,
                            NameObsv = this.Name,
                            DetailObsv = this.Details
                        };
                        Response resultObservationsAdd = await ApiSrv.AddObservationsByLogAndUser(TokenGet.Key, QueryValuesAdd);
                        if (!resultObservationsAdd.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            ObservationAdd = JsonConvert.DeserializeObject<List<ObservationAdd>>(Crypto.DecodeString(resultObservationsAdd.Data));
                            switch (ObservationAdd[0].ResultAddObservation)
                            {
                                case AddOrModResult.OK:
                                    UserDialogs.Instance.HideLoading();
                                    UserDialogs.Instance.ShowLoading(AlertMessages.UpdatingList, MaskType.Black);
                                    ObservationGetQueryValues QueryValuesGet = new ObservationGetQueryValues()
                                    {
                                        IdLog = this.LogItem.IdLog,
                                        IdUser = PbUser.IdUser
                                    };
                                    Response resultObservationsGet = await ApiSrv.GetObservationsByLogAndUser(TokenGet.Key, QueryValuesGet);
                                    if (!resultObservationsGet.IsSuccess)
                                    {
                                        UserDialogs.Instance.HideLoading();
                                        Toast.ShowError(AlertMessages.Error);
                                        return;
                                    }
                                    else
                                    {
                                        Observations = JsonConvert.DeserializeObject<List<ObservationGet>>(Crypto.DecodeString(resultObservationsGet.Data));
                                        ObservationItems.Clear();
                                        foreach (ObservationGet observationGet in Observations)
                                        {
                                            ObservationItems.Add(new ObservationItem()
                                            {
                                                IdObsv = observationGet.IdObsv,
                                                DateObsv = observationGet.DateObsv,
                                                NameObsv = observationGet.NameObsv,
                                                IdUser = observationGet.IdUser,
                                                DetailObsv = observationGet.DetailObsv,
                                                DateObsvString = observationGet.DateObsv.ToString(DateTimeFormatString.LatinDate)
                                            });
                                        }
                                        UserDialogs.Instance.HideLoading();
                                    }

                                    Toast.ShowSuccess(AlertMessages.Success);
                                    this.Name = string.Empty;
                                    this.Details = string.Empty;
                                    break;
                                case AddOrModResult.ERROR:
                                    UserDialogs.Instance.HideLoading();
                                    Toast.ShowError(AlertMessages.NoSuccess);
                                    break;
                                case AddOrModResult.EXISTING_DATA:
                                    UserDialogs.Instance.HideLoading();
                                    Alert.Show(AlertMessages.Existing);
                                    break;
                            }
                        }
                    }
                    IsLoadingData = false;
                }
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }

        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        private async void Delete()
        {
            try
            {
                if (IsLoadingData)
                {
                    return;
                }
                if (this.SelectedObservation.IdObsv == 0)
                {
                    Alert.Show(AlertMessages.Delete);
                    return;
                }
                bool result = await Confirm.Show("Desea eliminar la observación");
                if (result)
                {
                    IsLoadingData = true;
                    UserDialogs.Instance.ShowLoading(AlertMessages.Deleting, MaskType.Black);
                    if (!TokenValidator.IsValid(TokenGet))
                    {
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
                        }
                    }
                    ObservationDelQueryValues QueryValuesDel = new ObservationDelQueryValues()
                    {
                        IdObsv = this.SelectedObservation.IdObsv
                    };
                    Response resultObservationsDel = await ApiSrv.DelObservationsByLogAndUser(TokenGet.Key, QueryValuesDel);
                    if (!resultObservationsDel.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        Toast.ShowError(AlertMessages.Error);
                        return;
                    }
                    else
                    {
                        ObservationDel = JsonConvert.DeserializeObject<List<ObservationDel>>(Crypto.DecodeString(resultObservationsDel.Data));
                        if (!ObservationDel[0].Result)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.NoSuccess);
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            UserDialogs.Instance.ShowLoading(AlertMessages.UpdatingList, MaskType.Black);
                            ObservationGetQueryValues QueryValuesGet = new ObservationGetQueryValues()
                            {
                                IdLog = this.LogItem.IdLog,
                                IdUser = PbUser.IdUser
                            };
                            Response resultObservationsGet = await ApiSrv.GetObservationsByLogAndUser(TokenGet.Key, QueryValuesGet);
                            if (!resultObservationsGet.IsSuccess)
                            {
                                UserDialogs.Instance.HideLoading();
                                Toast.ShowError(AlertMessages.Error);
                                return;
                            }
                            else
                            {
                                Observations = JsonConvert.DeserializeObject<List<ObservationGet>>(Crypto.DecodeString(resultObservationsGet.Data));
                                ObservationItems.Clear();
                                foreach (ObservationGet observationGet in Observations)
                                {
                                    ObservationItems.Add(new ObservationItem()
                                    {
                                        IdObsv = observationGet.IdObsv,
                                        DateObsv = observationGet.DateObsv,
                                        NameObsv = observationGet.NameObsv,
                                        IdUser = observationGet.IdUser,
                                        DetailObsv = observationGet.DetailObsv,
                                        DateObsvString = observationGet.DateObsv.ToString(DateTimeFormatString.LatinDate)
                                    });
                                }
                                UserDialogs.Instance.HideLoading();
                            }

                            Toast.ShowSuccess(AlertMessages.Success);
                            this.Name = string.Empty;
                            this.Details = string.Empty;
                            this.SelectedObservation = new ObservationItem();
                        }
                    }
                    IsLoadingData = false;
                }
            }
            catch //(Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Toast.ShowError(AlertMessages.Error);
            }

        }
        #endregion

        #region Helpers
        private void HandleSelectedObservation()
        {
            this.Name = this.SelectedObservation.NameObsv;
            this.Details = this.SelectedObservation.DetailObsv;
        }

        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo observaciones...", MaskType.Black);
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
                        ObservationGetQueryValues QueryValues = new ObservationGetQueryValues()
                        {
                            IdLog = this.LogItem.IdLog,
                            IdUser = PbUser.IdUser
                        };
                        Response resultObservationsGet = await ApiSrv.GetObservationsByLogAndUser(TokenGet.Key, QueryValues);
                        if (!resultObservationsGet.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            Observations = JsonConvert.DeserializeObject<List<ObservationGet>>(Crypto.DecodeString(resultObservationsGet.Data));
                            ObservationItems = new ObservableCollection<ObservationItem>();
                            foreach (ObservationGet observationGet in Observations)
                            {
                                ObservationItems.Add(new ObservationItem()
                                {
                                    IdObsv = observationGet.IdObsv,
                                    DateObsv = observationGet.DateObsv,
                                    NameObsv = observationGet.NameObsv,
                                    IdUser = observationGet.IdUser,
                                    DetailObsv = observationGet.DetailObsv,
                                    DateObsvString = observationGet.DateObsv.ToString(DateTimeFormatString.LatinDate)
                                });
                            }
                            UserDialogs.Instance.HideLoading();
                        }
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

        #region Fake Data
        //private void GetFakeData()
        //{
        //    ObservationItems = new ObservableCollection<ObservationItem>();
        //    ObservationItem observationItem;

        //    observationItem = new ObservationItem();
        //    observationItem.Id = 1;
        //    observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 1";
        //    observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
        //    observationItem.Operator = "ADMINISTRADOR";
        //    observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
        //    ObservationItems.Add(observationItem);

        //    observationItem = new ObservationItem();
        //    observationItem.Id = 2;
        //    observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 2";
        //    observationItem.Detail = "Conteni.do de la observación a detalle, texto texto datos datos valores valores.";
        //    observationItem.Operator = "WEB APLLICATION";
        //    observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
        //    ObservationItems.Add(observationItem);

        //    observationItem = new ObservationItem();
        //    observationItem.Id = 3;
        //    observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 3";
        //    observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
        //    observationItem.Operator = "TEST AND BUILD";
        //    observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
        //    ObservationItems.Add(observationItem);

        //    observationItem = new ObservationItem();
        //    observationItem.Id = 4;
        //    observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 4";
        //    observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
        //    observationItem.Operator = "DEVELOPER";
        //    observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
        //    ObservationItems.Add(observationItem);

        //    observationItem = new ObservationItem();
        //    observationItem.Id = 5;
        //    observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 5";
        //    observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
        //    observationItem.Operator = "ADMINISTRADOR";
        //    observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
        //    ObservationItems.Add(observationItem);

        //    observationItem = new ObservationItem();
        //    observationItem.Id = 6;
        //    observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 6";
        //    observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
        //    observationItem.Operator = "ADMINISTRADOR";
        //    observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
        //    ObservationItems.Add(observationItem);

        //    observationItem = new ObservationItem();
        //    observationItem.Id = 7;
        //    observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 7";
        //    observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
        //    observationItem.Operator = "ADMINISTRADOR";
        //    observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
        //    ObservationItems.Add(observationItem);
        //}
        #endregion
    }
}
