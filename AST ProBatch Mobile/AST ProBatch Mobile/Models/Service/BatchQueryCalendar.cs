using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class BatchQueryCalendar
    {
        public Int32 IdLot { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Frecuency { get; set; }
        public bool Daily { get; set; }
        public bool Weekly { get; set; }
        public bool Monthly { get; set; }
        public byte RangeFrecuency { get; set; }
        public byte Day { get; set; }
        public string WeeklyBusinessDay { get; set; }
        public string MonthlyRangeDay { get; set; }
        public string MonthlyDayWeek { get; set; }
        public byte Subsequent { get; set; }
        public bool BusinessDay { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool MonthlyDaily { get; set; }
        public bool UsaHoliday { get; set; }
        public Int16 HolidayType { get; set; }
        public bool HolidayBusinessDay { get; set; }
    }
}
