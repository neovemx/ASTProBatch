using System;
namespace AST_ProBatch_Mobile.Models
{
    public class CalendarItem
    {
        public int Id { get; set; }
        public bool IsDaily { get; set; }
        public bool IsWeekly { get; set; }
        public bool IsMonthly { get; set; }
        public int DailyDay { get; set; }
        public bool SpecificDays { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool SpecificDaysBusinessDay { get; set; }
        public int EveryWeeks { get; set; }
        public bool BusinessDays { get; set; }
        public int TheBusinessDay { get; set; }
        public bool DaysMonth { get; set; }
        public int DaysMonthStart { get; set; }
        public int DaysMonthEnd { get; set; }
        public bool MonthlyRange { get; set; }
        public int MonthlyRangeStart { get; set; }
        public int MonthlyRangeEnd { get; set; }
        public bool MonthlyBusinessDay { get; set; }
        public int EveryMonth { get; set; }
        public bool Holiday { get; set; }
        public int HolidayValue { get; set; }
        public bool HolidayBusinessDay { get; set; }
        public bool Start { get; set; }
        public string StartDate { get; set; }
        public bool End { get; set; }
        public string EndDate { get; set; }
    }
}
