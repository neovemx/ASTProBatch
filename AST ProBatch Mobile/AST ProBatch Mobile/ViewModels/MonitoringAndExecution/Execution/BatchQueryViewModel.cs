using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Models.Service;
using AST_ProBatch_Mobile.Security;
using AST_ProBatch_Mobile.Utilities;
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
    public class BatchQueryViewModel : BaseViewModel
    {
        #region Atributes
        private CommandItem commanditem;
        #region Lot
        private ObservableCollection<PickerStatusItem> statusitems;
        private ObservableCollection<ParameterItem> parameteritems;
        private PickerStatusItem statusselected;
        private LotItem lotitem;
        private int statusindex;
        #endregion
        #region Command
        private ObservableCollection<CommandItem> commanditems;
        #endregion
        #region Calendar
        private ObservableCollection<PickerDayItem> dayitems;
        private ObservableCollection<PickerTheBusinessDayItem> thebusinessdayitems;
        private ObservableCollection<PickerEveryWeeksItem> everyweeksitems;
        private ObservableCollection<PickerDaysMonthStartItem> daysmonthstartitems;
        private ObservableCollection<PickerDaysMonthEndItem> daysmonthenditems;
        private ObservableCollection<PickerMonthlyRangeStartItem> monthlyrangestartitems;
        private ObservableCollection<PickerMonthlyRangeEndItem> monthlyrangeenditems;
        private ObservableCollection<PickerEveryMonthItem> everymonthitems;
        private CalendarItem batchcalendaritem;
        private bool isvisiblecalendar;
        #endregion
        #endregion

        #region Properties
        private Token TokenGet { get; set; }
        private List<BatchQueryLot> BatchQueryLotData { get; set; }
        private List<BatchQueryParameter> BatchQueryParametersData { get; set; }
        private List<BatchQueryCommand> BatchQueryCommandsData { get; set; }
        private List<BatchQueryCalendar> BatchQueryCalendarData { get; set; }

        public CommandItem CommandItem
        {
            get { return commanditem; }
            set { SetValue(ref commanditem, value); }
        }
        #region Lot
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
        public PickerStatusItem StatusSelected
        {
            get { return statusselected; }
            set { SetValue(ref statusselected, value); }
        }
        public LotItem LotItem
        {
            get { return lotitem; }
            set { SetValue(ref lotitem, value); }
        }
        public int StatusIndex
        {
            get { return statusindex; }
            set { SetValue(ref statusindex, value); }
        }
        #endregion
        #region Command
        public ObservableCollection<CommandItem> CommandItems
        {
            get { return commanditems; }
            set { SetValue(ref commanditems, value); }
        }
        #endregion
        #region Calendar
        public ObservableCollection<PickerDayItem> DayItems
        {
            get { return dayitems; }
            set { SetValue(ref dayitems, value); }
        }
        public ObservableCollection<PickerTheBusinessDayItem> TheBusinessDayItems
        {
            get { return thebusinessdayitems; }
            set { SetValue(ref thebusinessdayitems, value); }
        }
        public ObservableCollection<PickerEveryWeeksItem> EveryWeeksItems
        {
            get { return everyweeksitems; }
            set { SetValue(ref everyweeksitems, value); }
        }
        public ObservableCollection<PickerDaysMonthStartItem> DaysMonthStartItems
        {
            get { return daysmonthstartitems; }
            set { SetValue(ref daysmonthstartitems, value); }
        }
        public ObservableCollection<PickerDaysMonthEndItem> DaysMonthEndItems
        {
            get { return daysmonthenditems; }
            set { SetValue(ref daysmonthenditems, value); }
        }
        public ObservableCollection<PickerMonthlyRangeStartItem> MonthlyRangeStartItems
        {
            get { return monthlyrangestartitems; }
            set { SetValue(ref monthlyrangestartitems, value); }
        }
        public ObservableCollection<PickerMonthlyRangeEndItem> MonthlyRangeEndItems
        {
            get { return monthlyrangeenditems; }
            set { SetValue(ref monthlyrangeenditems, value); }
        }
        public ObservableCollection<PickerEveryMonthItem> EveryMonthItems
        {
            get { return everymonthitems; }
            set { SetValue(ref everymonthitems, value); }
        }
        public CalendarItem BatchCalendarItem
        {
            get { return batchcalendaritem; }
            set { SetValue(ref batchcalendaritem, value); }
        }
        public bool IsVisibleCalendar
        {
            get { return isvisiblecalendar; }
            set { SetValue(ref isvisiblecalendar, value); }
        }
        #endregion
        #endregion

        #region Constructors
        public BatchQueryViewModel(bool IsReload, CommandItem commandItem)
        {
            if (IsReload)
            {
                ApiSrv = new Services.ApiService(ApiConsult.ApiMenuB);
                this.CommandItem = commandItem;

                GetInitialData();
            }
        }
        #endregion

        #region Commands
        public ICommand SearchParametersCommand
        {
            get
            {
                return new RelayCommand(SearchParameters);
            }
        }

        private async void SearchParameters()
        {
            UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

            await Task.Delay(1000);

            await Task.Run(async () =>
            {
                ParameterItem parameterItem;

                //parameterItem = new ParameterItem();
                //parameterItem.Id = 1;
                //parameterItem.Title = "PARAMETRO PRUEBA";
                //parameterItem.Value = "valor prueba";
                //this.ParameterItems.Add(parameterItem);

                //parameterItem = new ParameterItem();
                //parameterItem.Id = 2;
                //parameterItem.Title = "PARAMETRO PRUEBA";
                //parameterItem.Value = "valor prueba";
                //this.ParameterItems.Add(parameterItem);

                //parameterItem = new ParameterItem();
                //parameterItem.Id = 3;
                //parameterItem.Title = "PARAMETRO PRUEBA";
                //parameterItem.Value = "valor prueba";
                //this.ParameterItems.Add(parameterItem);

                //parameterItem = new ParameterItem();
                //parameterItem.Id = 4;
                //parameterItem.Title = "PARAMETRO PRUEBA";
                //parameterItem.Value = "valor prueba";
                //this.ParameterItems.Add(parameterItem);

                //parameterItem = new ParameterItem();
                //parameterItem.Id = 5;
                //parameterItem.Title = "PARAMETRO PRUEBA";
                //parameterItem.Value = "valor prueba";
                //this.ParameterItems.Add(parameterItem);

                //parameterItem = new ParameterItem();
                //parameterItem.Id = 6;
                //parameterItem.Title = "PARAMETRO PRUEBA";
                //parameterItem.Value = "valor prueba";
                //this.ParameterItems.Add(parameterItem);

                //parameterItem = new ParameterItem();
                //parameterItem.Id = 7;
                //parameterItem.Title = "PARAMETRO PRUEBA";
                //parameterItem.Value = "valor prueba";
                //this.ParameterItems.Add(parameterItem);

                //this.ResultIsVisible = true;

                UserDialogs.Instance.HideLoading();
            });
        }

        public ICommand SearchCommandsCommand
        {
            get
            {
                return new RelayCommand(SearchCommands);
            }
        }

        private async void SearchCommands()
        {
            UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

            await Task.Delay(1000);

            await Task.Run(async () =>
            {
                CommandItem commandItem;

                commandItem = new CommandItem();
                //commandItem.Id = 1;
                //commandItem.Title = "8999 - Prueba grande";
                //commandItem.Critical = true;
                //commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                //commandItem.Id = 2;
                //commandItem.Title = "8999 - Prueba grande";
                //commandItem.Critical = false;
                //commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                //commandItem.Id = 3;
                //commandItem.Title = "8999 - Prueba grande";
                //commandItem.Critical = true;
                //commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                //commandItem.Id = 4;
                //commandItem.Title = "8999 - Prueba grande";
                //commandItem.Critical = true;
                //commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                //commandItem.Id = 5;
                //commandItem.Title = "8999 - Prueba grande";
                //commandItem.Critical = true;
                //commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                //commandItem.Id = 6;
                //commandItem.Title = "8999 - Prueba grande";
                //commandItem.Critical = false;
                //commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                UserDialogs.Instance.HideLoading();
            });
        }

        public ICommand SearchCalendarCommand
        {
            get
            {
                return new RelayCommand(SearchCalendar);
            }
        }

        private async void SearchCalendar()
        {
            UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);

            await Task.Delay(1000);

            await Task.Run(async () =>
            {
                UserDialogs.Instance.HideLoading();
            });
        }
        #endregion

        #region Helpers
        private async void GetInitialData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Obteniendo datos del lote...", MaskType.Black);
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
                        BatchAllQueryValues batchAllQueryValues = new BatchAllQueryValues()
                        {
                            IdLot = this.CommandItem.IdLot
                        };

                        #region GET LOT DATA
                        Response batchGetLot = await ApiSrv.BatchQueryGetLots(TokenGet.Key, batchAllQueryValues);
                        if (!batchGetLot.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            BatchQueryLotData = JsonConvert.DeserializeObject<List<BatchQueryLot>>(Crypto.DecodeString(batchGetLot.Data));
                            BatchQueryLot batchQueryLot = BatchQueryLotData[0];
                            this.LotItem = new LotItem()
                            {
                                IdLot = batchQueryLot.IdLot,
                                Code = batchQueryLot.Code,
                                NameLot = batchQueryLot.NameLot,
                                Description = batchQueryLot.Description,
                                CreationDate = (batchQueryLot.CreationDate != null) ? (DateTime)batchQueryLot.CreationDate : new DateTime(),
                                CreationDateString = (batchQueryLot.CreationDate != null) ? ((DateTime)batchQueryLot.CreationDate).ToString(DateTimeFormatString.LatinDate24Hours) : "",
                                PathIn = batchQueryLot.PathIn,
                                PathOut = batchQueryLot.PathOut,
                                IdStatus = batchQueryLot.IdStatus,
                                Status = batchQueryLot.Status,
                                IsTransfer = batchQueryLot.IsTransfer
                            };
                        }
                        #endregion

                        #region PARAMETERS ON LOT
                        Response batchGetParameters = await ApiSrv.BatchQueryGetParameters(TokenGet.Key, batchAllQueryValues);
                        if (!batchGetParameters.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            UserDialogs.Instance.ShowLoading("Obteniendo parámetros...", MaskType.Black);
                            BatchQueryParametersData = JsonConvert.DeserializeObject<List<BatchQueryParameter>>(Crypto.DecodeString(batchGetParameters.Data));
                            ParameterItems = new ObservableCollection<ParameterItem>();
                            foreach (BatchQueryParameter batchQueryParameter in BatchQueryParametersData)
                            {
                                ParameterItems.Add(new ParameterItem()
                                {
                                    IdParameter = batchQueryParameter.IdParameter,
                                    NameParameter = batchQueryParameter.NameParameter,
                                    Value = batchQueryParameter.Value,
                                    IdTypeParameter = batchQueryParameter.IdTypeParameter,
                                    IdTypeData = batchQueryParameter.IdTypeData,
                                    Edit = batchQueryParameter.Edit,
                                    Hidden = batchQueryParameter.Hidden,
                                    IdStatus = batchQueryParameter.IdStatus
                                });
                            }
                        }
                        #endregion

                        #region COMMANDS ON LOT
                        Response batchGetCommands = await ApiSrv.BatchQueryGetCommands(TokenGet.Key, batchAllQueryValues);
                        if (!batchGetCommands.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            UserDialogs.Instance.ShowLoading("Obteniendo comandos...", MaskType.Black);
                            BatchQueryCommandsData = JsonConvert.DeserializeObject<List<BatchQueryCommand>>(Crypto.DecodeString(batchGetCommands.Data));
                            CommandItems = new ObservableCollection<CommandItem>();
                            foreach (BatchQueryCommand batchQueryCommand in BatchQueryCommandsData)
                            {
                                CommandItems.Add(new CommandItem()
                                {
                                    NameCommand = batchQueryCommand.Command,
                                    Critical = batchQueryCommand.CriticalBusiness,
                                    IdStatus = batchQueryCommand.IdStatus
                                });
                            }
                        }
                        #endregion

                        #region CALENDAR ON LOT
                        Response batchGetCalendars = await ApiSrv.BatchQueryGetCalendars(TokenGet.Key, batchAllQueryValues);
                        if (!batchGetCalendars.IsSuccess)
                        {
                            UserDialogs.Instance.HideLoading();
                            Toast.ShowError(AlertMessages.Error);
                            return;
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            UserDialogs.Instance.ShowLoading("Obteniendo calendario...", MaskType.Black);
                            BatchQueryCalendarData = JsonConvert.DeserializeObject<List<BatchQueryCalendar>>(Crypto.DecodeString(batchGetCalendars.Data));
                            BatchQueryCalendar batchQueryCalendar;
                            if (BatchQueryCalendarData.Count != 0)
                            {
                                this.IsVisibleCalendar = true;
                                batchQueryCalendar = BatchQueryCalendarData[0];
                                BatchCalendarItem = new CalendarItem()
                                {
                                    IdLot = batchQueryCalendar.IdLot,
                                    Start = (batchQueryCalendar.Start != null ? (DateTime)batchQueryCalendar.Start : new DateTime()),
                                    End = (batchQueryCalendar.End != null ? (DateTime)batchQueryCalendar.End : new DateTime()),
                                    Frecuency = batchQueryCalendar.Frecuency,
                                    Daily = batchQueryCalendar.Daily,
                                    Weekly = batchQueryCalendar.Weekly,
                                    Monthly = batchQueryCalendar.Monthly,
                                    RangeFrecuency = batchQueryCalendar.RangeFrecuency,
                                    Day = batchQueryCalendar.Day,
                                    WeeklyBusinessDay = batchQueryCalendar.WeeklyBusinessDay,
                                    MonthlyRangeDay = batchQueryCalendar.MonthlyRangeDay,
                                    MonthlyDayWeek = batchQueryCalendar.MonthlyDayWeek,
                                    Subsequent = batchQueryCalendar.Subsequent,
                                    BusinessDay = batchQueryCalendar.BusinessDay,
                                    ContraryBusinessDay = !batchQueryCalendar.BusinessDay,
                                    Monday = batchQueryCalendar.Monday,
                                    Tuesday = batchQueryCalendar.Tuesday,
                                    Wednesday = batchQueryCalendar.Wednesday,
                                    Thursday = batchQueryCalendar.Thursday,
                                    Friday = batchQueryCalendar.Friday,
                                    Saturday = batchQueryCalendar.Saturday,
                                    Sunday = batchQueryCalendar.Sunday,
                                    MonthlyDaily = batchQueryCalendar.MonthlyDaily,
                                    UsaHoliday = batchQueryCalendar.UsaHoliday,
                                    HolidayType = batchQueryCalendar.HolidayType,
                                    HolidayBusinessDay = batchQueryCalendar.HolidayBusinessDay,
                                    StartDateString = (batchQueryCalendar.Start != null ? ((DateTime)batchQueryCalendar.Start).ToString(DateTimeFormatString.LatinDate) : ""),
                                    EndDateString = (batchQueryCalendar.End != null ? ((DateTime)batchQueryCalendar.End).ToString(DateTimeFormatString.LatinDate) : ""),
                                    StartChecked = (batchQueryCalendar.Start != null ? true : false),
                                    EndChecked = (batchQueryCalendar.End != null ? true : false)
                                };
                            }
                            else
                            {
                                this.IsVisibleCalendar = false;
                            }
                        }
                        #endregion
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