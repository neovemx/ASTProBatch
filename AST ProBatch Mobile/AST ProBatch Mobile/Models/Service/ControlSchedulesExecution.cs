using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class ControlSchedulesExecution
    {
        public string IdStatus { get; set; }
        public Int32 InstanceNumber { get; set; }
        public string Lot { get; set; }
        public string Command { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeUntil { get; set; }
        public DateTime? OutOfSchedule { get; set; }
        public bool CriticalBusiness { get; set; }
    }
}
