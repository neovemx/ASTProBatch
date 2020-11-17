using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class Monitor
    {
        public Int64 IdCalendar { get; set; }
        public Int64 PID { get; set; }
        public Int32? IdLot { get; set; }
        public string NameLot { get; set; }
        public Int32? IdCommand { get; set; }
        public string NameCommand { get; set; }
        public Int16? IdEnvironment { get; set; }
        public string NameEnvironment { get; set; }
        public string IPAddress { get; set; }
        public Int16? IdService { get; set; }
        public string NameService { get; set; }
        public bool? IsServicePD { get; set; }
        public TimeSpan? StartHour { get; set; }
        public string IdStatus { get; set; }
        public string Ommited { get; set; }
        public DateTime? ExecutionStart { get; set; }
        public DateTime? ExecutionEnd { get; set; }
        public bool ReExecution { get; set; }
        public TimeSpan? RecurrenceTime { get; set; }
        public TimeSpan? EndHour { get; set; }
        public Int16? Order { get; set; }
    }
}
