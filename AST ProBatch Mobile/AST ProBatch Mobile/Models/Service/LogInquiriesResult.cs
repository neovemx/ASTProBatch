using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class LogInquiriesResult
    {
        public Int16 Number { get; set; }
        public string NameInstance { get; set; }
        public bool Selected { get; set; }
        public Int16 Order { get; set; }
        public DateTime? StartTime { get; set; }
        public bool Pause { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
        public string Lot { get; set; }
        public string Command { get; set; }
        public bool IgnoreResult { get; set; }
        public Int16 IdTypeCommand { get; set; }
        public Int32 ExecutionTime { get; set; }
        public Int32 GraceTime { get; set; }
        public bool Critical { get; set; }
        public bool FixedTime { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeUntil { get; set; }
        public bool RequestParameters { get; set; }
        public string IdStatus { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public Int16 IdEnvironment { get; set; }
        public string NameEnvironment { get; set; }
        public string IpEnvironment { get; set; }
        public string UserEnvironment { get; set; }
        public bool IsWindowsOS { get; set; }
        public Int16 IdService { get; set; }
        public bool IsDBMS { get; set; }
        public Int64 PID { get; set; }
        public string ExecutionCommand { get; set; }
        public bool CriticalCusiness { get; set; }
    }
}
