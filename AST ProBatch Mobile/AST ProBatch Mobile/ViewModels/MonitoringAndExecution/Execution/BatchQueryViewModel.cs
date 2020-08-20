using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
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
        #region Lot
        private ObservableCollection<PickerStatusItem> statusitems;
        private ObservableCollection<ParameterItem> parameteritems;
        private PickerStatusItem statusselected;
        private LotItem lotitem;
        private int statusindex;
        private bool resultisvisible;
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
        private bool isvisiblecalendar;
        private bool isdaily;
        private bool isweekly;
        private bool ismonthly;
        private int dailyday;
        private bool specificdays;
        private bool monday;
        private bool tuesday;
        private bool wednesday;
        private bool thursday;
        private bool friday;
        private bool saturday;
        private bool sunday;
        private bool specificdaysbusinessday;
        private int everyWeeks;
        private bool businessdays;
        private int thebusinessday;
        private bool daysmonth;
        private int daysmonthstart;
        private int daysmonthend;
        private bool monthlyrange;
        private int monthlyrangestart;
        private int monthlyrangeend;
        private bool monthlybusinessday;
        private int everymonth;
        private bool holiday;
        private int holidayvalue;
        private bool holidaybusinessday;
        private bool start;
        private string startdate;
        private bool end;
        private string enddate;
        #endregion
        #endregion

        #region Properties
        public InstanceItem InstanceItem
        {
            get { return instanceitem; }
            set { SetValue(ref instanceitem, value); }
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
        public bool ResultIsVisible
        {
            get { return resultisvisible; }
            set { SetValue(ref resultisvisible, value); }
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
        public bool IsVisibleCalendar
        {
            get { return isvisiblecalendar; }
            set { SetValue(ref isvisiblecalendar, value); }
        }
        public bool IsDaily
        {
            get { return isdaily; }
            set { SetValue(ref isdaily, value); }
        }
        public bool IsWeekly
        {
            get { return isweekly; }
            set { SetValue(ref isweekly, value); }
        }
        public bool IsMonthly
        {
            get { return ismonthly; }
            set { SetValue(ref ismonthly, value); }
        }
        public int DailyDay
        {
            get { return dailyday; }
            set { SetValue(ref dailyday, value); }
        }
        public bool SpecificDays
        {
            get { return specificdays; }
            set { SetValue(ref specificdays, value); }
        }
        public bool Monday
        {
            get { return monday; }
            set { SetValue(ref monday, value); }
        }
        public bool Tuesday
        {
            get { return tuesday; }
            set { SetValue(ref tuesday, value); }
        }
        public bool Wednesday
        {
            get { return wednesday; }
            set { SetValue(ref wednesday, value); }
        }
        public bool Thursday
        {
            get { return thursday; }
            set { SetValue(ref thursday, value); }
        }
        public bool Friday
        {
            get { return friday; }
            set { SetValue(ref friday, value); }
        }
        public bool Saturday
        {
            get { return saturday; }
            set { SetValue(ref saturday, value); }
        }
        public bool Sunday
        {
            get { return sunday; }
            set { SetValue(ref sunday, value); }
        }
        public bool SpecificDaysBusinessDay
        {
            get { return specificdaysbusinessday; }
            set { SetValue(ref specificdaysbusinessday, value); }
        }
        public int EveryWeeks
        {
            get { return everyWeeks; }
            set { SetValue(ref everyWeeks, value); }
        }
        public bool BusinessDays
        {
            get { return businessdays; }
            set { SetValue(ref businessdays, value); }
        }
        public int TheBusinessDay
        {
            get { return thebusinessday; }
            set { SetValue(ref thebusinessday, value); }
        }
        public bool DaysMonth
        {
            get { return daysmonth; }
            set { SetValue(ref daysmonth, value); }
        }
        public int DaysMonthStart
        {
            get { return daysmonthstart; }
            set { SetValue(ref daysmonthstart, value); }
        }
        public int DaysMonthEnd
        {
            get { return daysmonthend; }
            set { SetValue(ref daysmonthend, value); }
        }
        public bool MonthlyRange
        {
            get { return monthlyrange; }
            set { SetValue(ref monthlyrange, value); }
        }
        public int MonthlyRangeStart
        {
            get { return monthlyrangestart; }
            set { SetValue(ref monthlyrangestart, value); }
        }
        public int MonthlyRangeEnd
        {
            get { return monthlyrangeend; }
            set { SetValue(ref monthlyrangeend, value); }
        }
        public bool MonthlyBusinessDay
        {
            get { return monthlybusinessday; }
            set { SetValue(ref monthlybusinessday, value); }
        }
        public int EveryMonth
        {
            get { return everymonth; }
            set { SetValue(ref everymonth, value); }
        }
        public bool Holiday
        {
            get { return holiday; }
            set { SetValue(ref holiday, value); }
        }
        public int HolidayValue
        {
            get { return holidayvalue; }
            set { SetValue(ref holidayvalue, value); }
        }
        public bool HolidayBusinessDay
        {
            get { return holidaybusinessday; }
            set { SetValue(ref holidaybusinessday, value); }
        }
        public bool Start
        {
            get { return start; }
            set { SetValue(ref start, value); }
        }
        public string StartDate
        {
            get { return startdate; }
            set { SetValue(ref startdate, value); }
        }
        public bool End
        {
            get { return end; }
            set { SetValue(ref end, value); }
        }
        public string EndDate
        {
            get { return enddate; }
            set { SetValue(ref enddate, value); }
        }
        #endregion
        #endregion

        #region Constructors
        public BatchQueryViewModel(InstanceItem instanceItem)
        {
            this.ResultIsVisible = false;
            this.InstanceItem = instanceItem;
            this.ParameterItems = new ObservableCollection<ParameterItem>();
            this.commanditems = new ObservableCollection<CommandItem>();
            GetFakeData();
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
                commandItem.Id = 1;
                commandItem.Title = "8999 - Prueba grande";
                commandItem.Critical = true;
                commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                commandItem.Id = 2;
                commandItem.Title = "8999 - Prueba grande";
                commandItem.Critical = false;
                commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                commandItem.Id = 3;
                commandItem.Title = "8999 - Prueba grande";
                commandItem.Critical = true;
                commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                commandItem.Id = 4;
                commandItem.Title = "8999 - Prueba grande";
                commandItem.Critical = true;
                commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                commandItem.Id = 5;
                commandItem.Title = "8999 - Prueba grande";
                commandItem.Critical = true;
                commandItem.Status = "A";
                this.CommandItems.Add(commandItem);

                commandItem = new CommandItem();
                commandItem.Id = 6;
                commandItem.Title = "8999 - Prueba grande";
                commandItem.Critical = false;
                commandItem.Status = "A";
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
                this.IsVisibleCalendar = true;

                this.IsDaily = true;
                this.IsWeekly = false;
                this.IsMonthly = false;
                this.DailyDay = 1;
                this.SpecificDays = true;
                this.Monday = true;
                this.Tuesday = true;
                this.Wednesday = true;
                this.Thursday = true;
                this.Friday = true;
                this.Saturday = true;
                this.Sunday = true;
                this.SpecificDaysBusinessDay = false;
                this.BusinessDays = false;
                this.TheBusinessDay = -1;
                this.EveryWeeks = 1;
                this.DaysMonth = true;
                this.DaysMonthStart = 0;
                this.DaysMonthEnd = 0;
                this.MonthlyRange = false;
                this.MonthlyRangeStart = -1;
                this.MonthlyRangeEnd = -1;
                this.MonthlyBusinessDay = false;
                this.EveryMonth = 0;
                this.Holiday = false;
                this.HolidayValue = 0;
                this.HolidayBusinessDay = false;
                this.Start = true;
                this.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
                this.End = true;
                this.EndDate = DateTime.Now.ToString("dd/MM/yyyy");

                UserDialogs.Instance.HideLoading();
            });
        }
        #endregion

        #region Helpers
        private void GetFakeData()
        {
            #region PickerStatus
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
            #endregion

            #region PickerDay
            this.DayItems = new ObservableCollection<PickerDayItem>();
            PickerDayItem dayItem;

            dayItem = new PickerDayItem();
            dayItem.Id = 1;
            dayItem.DayName = "1";
            this.DayItems.Add(dayItem);

            dayItem = new PickerDayItem();
            dayItem.Id = 2;
            dayItem.DayName = "2";
            this.DayItems.Add(dayItem);

            dayItem = new PickerDayItem();
            dayItem.Id = 3;
            dayItem.DayName = "3";
            this.DayItems.Add(dayItem);

            dayItem = new PickerDayItem();
            dayItem.Id = 4;
            dayItem.DayName = "4";
            this.DayItems.Add(dayItem);

            dayItem = new PickerDayItem();
            dayItem.Id = 5;
            dayItem.DayName = "5";
            this.DayItems.Add(dayItem);

            dayItem = new PickerDayItem();
            dayItem.Id = 6;
            dayItem.DayName = "6";
            this.DayItems.Add(dayItem);
            #endregion

            #region PickerTheBusinessDay
            this.TheBusinessDayItems = new ObservableCollection<PickerTheBusinessDayItem>();
            PickerTheBusinessDayItem theBusinessDayItem;

            theBusinessDayItem = new PickerTheBusinessDayItem();
            theBusinessDayItem.Id = 1;
            theBusinessDayItem.TheBusinessDayName = "1º";
            this.TheBusinessDayItems.Add(theBusinessDayItem);

            theBusinessDayItem = new PickerTheBusinessDayItem();
            theBusinessDayItem.Id = 2;
            theBusinessDayItem.TheBusinessDayName = "Último";
            this.TheBusinessDayItems.Add(theBusinessDayItem);
            #endregion

            #region PickerEveryWeeks
            this.EveryWeeksItems = new ObservableCollection<PickerEveryWeeksItem>();
            PickerEveryWeeksItem everyWeeksItem;

            everyWeeksItem = new PickerEveryWeeksItem();
            everyWeeksItem.Id = 1;
            everyWeeksItem.EveryWeeksName = "1";
            this.EveryWeeksItems.Add(everyWeeksItem);

            everyWeeksItem = new PickerEveryWeeksItem();
            everyWeeksItem.Id = 2;
            everyWeeksItem.EveryWeeksName = "2";
            this.EveryWeeksItems.Add(everyWeeksItem);

            everyWeeksItem = new PickerEveryWeeksItem();
            everyWeeksItem.Id = 3;
            everyWeeksItem.EveryWeeksName = "3";
            this.EveryWeeksItems.Add(everyWeeksItem);
            #endregion

            #region PickerDaysMonthStart
            this.DaysMonthStartItems = new ObservableCollection<PickerDaysMonthStartItem>();
            PickerDaysMonthStartItem pickerDaysMonthStartItem;

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 1;
            pickerDaysMonthStartItem.DaysMonthStartName = "1";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 2;
            pickerDaysMonthStartItem.DaysMonthStartName = "2";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 3;
            pickerDaysMonthStartItem.DaysMonthStartName = "3";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 4;
            pickerDaysMonthStartItem.DaysMonthStartName = "4";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 5;
            pickerDaysMonthStartItem.DaysMonthStartName = "5";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 6;
            pickerDaysMonthStartItem.DaysMonthStartName = "6";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 7;
            pickerDaysMonthStartItem.DaysMonthStartName = "7";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 8;
            pickerDaysMonthStartItem.DaysMonthStartName = "8";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 9;
            pickerDaysMonthStartItem.DaysMonthStartName = "9";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 10;
            pickerDaysMonthStartItem.DaysMonthStartName = "10";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 11;
            pickerDaysMonthStartItem.DaysMonthStartName = "11";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 12;
            pickerDaysMonthStartItem.DaysMonthStartName = "12";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 13;
            pickerDaysMonthStartItem.DaysMonthStartName = "13";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 14;
            pickerDaysMonthStartItem.DaysMonthStartName = "14";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 15;
            pickerDaysMonthStartItem.DaysMonthStartName = "15";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 16;
            pickerDaysMonthStartItem.DaysMonthStartName = "16";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 17;
            pickerDaysMonthStartItem.DaysMonthStartName = "17";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 18;
            pickerDaysMonthStartItem.DaysMonthStartName = "18";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 19;
            pickerDaysMonthStartItem.DaysMonthStartName = "19";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 20;
            pickerDaysMonthStartItem.DaysMonthStartName = "20";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 21;
            pickerDaysMonthStartItem.DaysMonthStartName = "21";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 22;
            pickerDaysMonthStartItem.DaysMonthStartName = "22";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 23;
            pickerDaysMonthStartItem.DaysMonthStartName = "23";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 24;
            pickerDaysMonthStartItem.DaysMonthStartName = "24";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 25;
            pickerDaysMonthStartItem.DaysMonthStartName = "25";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 26;
            pickerDaysMonthStartItem.DaysMonthStartName = "26";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 27;
            pickerDaysMonthStartItem.DaysMonthStartName = "27";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 28;
            pickerDaysMonthStartItem.DaysMonthStartName = "28";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 29;
            pickerDaysMonthStartItem.DaysMonthStartName = "29";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 30;
            pickerDaysMonthStartItem.DaysMonthStartName = "30";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);

            pickerDaysMonthStartItem = new PickerDaysMonthStartItem();
            pickerDaysMonthStartItem.Id = 31;
            pickerDaysMonthStartItem.DaysMonthStartName = "31";
            this.DaysMonthStartItems.Add(pickerDaysMonthStartItem);
            #endregion

            #region PickerDaysMonthEnd
            this.DaysMonthEndItems = new ObservableCollection<PickerDaysMonthEndItem>();
            PickerDaysMonthEndItem pickerDaysMonthEndItem;

            pickerDaysMonthEndItem = new PickerDaysMonthEndItem();
            pickerDaysMonthEndItem.Id = 1;
            pickerDaysMonthEndItem.DaysMonthEndName = "1";
            this.DaysMonthEndItems.Add(pickerDaysMonthEndItem);

            pickerDaysMonthEndItem = new PickerDaysMonthEndItem();
            pickerDaysMonthEndItem.Id = 2;
            pickerDaysMonthEndItem.DaysMonthEndName = "2";
            this.DaysMonthEndItems.Add(pickerDaysMonthEndItem);

            pickerDaysMonthEndItem = new PickerDaysMonthEndItem();
            pickerDaysMonthEndItem.Id = 3;
            pickerDaysMonthEndItem.DaysMonthEndName = "3";
            this.DaysMonthEndItems.Add(pickerDaysMonthEndItem);
            #endregion

            #region PickerMonthlyRangeStart
            this.MonthlyRangeStartItems = new ObservableCollection<PickerMonthlyRangeStartItem>();
            PickerMonthlyRangeStartItem pickerMonthlyRangeStartItem;

            pickerMonthlyRangeStartItem = new PickerMonthlyRangeStartItem();
            pickerMonthlyRangeStartItem.Id = 1;
            pickerMonthlyRangeStartItem.MonthlyRangeStartName = "1º";
            this.MonthlyRangeStartItems.Add(pickerMonthlyRangeStartItem);

            pickerMonthlyRangeStartItem = new PickerMonthlyRangeStartItem();
            pickerMonthlyRangeStartItem.Id = 1;
            pickerMonthlyRangeStartItem.MonthlyRangeStartName = "2º";
            this.MonthlyRangeStartItems.Add(pickerMonthlyRangeStartItem);

            pickerMonthlyRangeStartItem = new PickerMonthlyRangeStartItem();
            pickerMonthlyRangeStartItem.Id = 1;
            pickerMonthlyRangeStartItem.MonthlyRangeStartName = "3º";
            this.MonthlyRangeStartItems.Add(pickerMonthlyRangeStartItem);

            pickerMonthlyRangeStartItem = new PickerMonthlyRangeStartItem();
            pickerMonthlyRangeStartItem.Id = 1;
            pickerMonthlyRangeStartItem.MonthlyRangeStartName = "4º";
            this.MonthlyRangeStartItems.Add(pickerMonthlyRangeStartItem);

            pickerMonthlyRangeStartItem = new PickerMonthlyRangeStartItem();
            pickerMonthlyRangeStartItem.Id = 1;
            pickerMonthlyRangeStartItem.MonthlyRangeStartName = "5º";
            this.MonthlyRangeStartItems.Add(pickerMonthlyRangeStartItem);

            pickerMonthlyRangeStartItem = new PickerMonthlyRangeStartItem();
            pickerMonthlyRangeStartItem.Id = 1;
            pickerMonthlyRangeStartItem.MonthlyRangeStartName = "Antepenúltimo";
            this.MonthlyRangeStartItems.Add(pickerMonthlyRangeStartItem);

            pickerMonthlyRangeStartItem = new PickerMonthlyRangeStartItem();
            pickerMonthlyRangeStartItem.Id = 1;
            pickerMonthlyRangeStartItem.MonthlyRangeStartName = "Anteúltimo";
            this.MonthlyRangeStartItems.Add(pickerMonthlyRangeStartItem);

            pickerMonthlyRangeStartItem = new PickerMonthlyRangeStartItem();
            pickerMonthlyRangeStartItem.Id = 1;
            pickerMonthlyRangeStartItem.MonthlyRangeStartName = "Último";
            this.MonthlyRangeStartItems.Add(pickerMonthlyRangeStartItem);
            #endregion

            #region PickerMonthlyRangeEnd
            this.MonthlyRangeEndItems = new ObservableCollection<PickerMonthlyRangeEndItem>();
            PickerMonthlyRangeEndItem pickerMonthlyRangeEndItem;

            pickerMonthlyRangeEndItem = new PickerMonthlyRangeEndItem();
            pickerMonthlyRangeEndItem.Id = 1;
            pickerMonthlyRangeEndItem.MonthlyRangeEndName = "Dia";
            this.MonthlyRangeEndItems.Add(pickerMonthlyRangeEndItem);

            pickerMonthlyRangeEndItem = new PickerMonthlyRangeEndItem();
            pickerMonthlyRangeEndItem.Id = 1;
            pickerMonthlyRangeEndItem.MonthlyRangeEndName = "Lunes";
            this.MonthlyRangeEndItems.Add(pickerMonthlyRangeEndItem);

            pickerMonthlyRangeEndItem = new PickerMonthlyRangeEndItem();
            pickerMonthlyRangeEndItem.Id = 1;
            pickerMonthlyRangeEndItem.MonthlyRangeEndName = "Martes";
            this.MonthlyRangeEndItems.Add(pickerMonthlyRangeEndItem);

            pickerMonthlyRangeEndItem = new PickerMonthlyRangeEndItem();
            pickerMonthlyRangeEndItem.Id = 1;
            pickerMonthlyRangeEndItem.MonthlyRangeEndName = "Miércoles";
            this.MonthlyRangeEndItems.Add(pickerMonthlyRangeEndItem);

            pickerMonthlyRangeEndItem = new PickerMonthlyRangeEndItem();
            pickerMonthlyRangeEndItem.Id = 1;
            pickerMonthlyRangeEndItem.MonthlyRangeEndName = "Jueves";
            this.MonthlyRangeEndItems.Add(pickerMonthlyRangeEndItem);

            pickerMonthlyRangeEndItem = new PickerMonthlyRangeEndItem();
            pickerMonthlyRangeEndItem.Id = 1;
            pickerMonthlyRangeEndItem.MonthlyRangeEndName = "Viernes";
            this.MonthlyRangeEndItems.Add(pickerMonthlyRangeEndItem);

            pickerMonthlyRangeEndItem = new PickerMonthlyRangeEndItem();
            pickerMonthlyRangeEndItem.Id = 1;
            pickerMonthlyRangeEndItem.MonthlyRangeEndName = "Sábado";
            this.MonthlyRangeEndItems.Add(pickerMonthlyRangeEndItem);

            pickerMonthlyRangeEndItem = new PickerMonthlyRangeEndItem();
            pickerMonthlyRangeEndItem.Id = 1;
            pickerMonthlyRangeEndItem.MonthlyRangeEndName = "Domingo";
            this.MonthlyRangeEndItems.Add(pickerMonthlyRangeEndItem);
            #endregion

            #region PickerEveryMonth
            this.EveryMonthItems = new ObservableCollection<PickerEveryMonthItem>();
            PickerEveryMonthItem pickerEveryMonthItem;

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "1";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "2";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "3";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "4";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "5";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "6";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "7";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "8";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "9";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "10";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "11";
            this.EveryMonthItems.Add(pickerEveryMonthItem);

            pickerEveryMonthItem = new PickerEveryMonthItem();
            pickerEveryMonthItem.Id = 1;
            pickerEveryMonthItem.EveryMonthName = "12";
            this.EveryMonthItems.Add(pickerEveryMonthItem);
            #endregion

            #region Lot
            this.LotItem = new LotItem();
            this.LotItem.Id = 1;
            this.LotItem.Code = 51;
            this.LotItem.Title = "51 - Prueba";
            this.LotItem.Description = "Prueba";
            this.LotItem.DateCreation = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            this.LotItem.InPath = "";
            this.LotItem.OutPath = "";
            this.LotItem.StatusSelected = new PickerStatusItem() { Id = 1, StatusName = "A - ACTIVO", StatusIndex = 0 };
            this.statusindex = this.LotItem.StatusSelected.StatusIndex;
            #endregion
        }
        #endregion
    }
}
