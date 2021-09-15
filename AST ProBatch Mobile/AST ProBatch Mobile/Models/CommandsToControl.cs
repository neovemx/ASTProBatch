using System;

namespace AST_ProBatch_Mobile.Models
{
    public class CommandsToControl
    {
        #region Original Model From API
        public string IdStatus { get; set; }
        public Int32 InstanceNumber { get; set; }
        public string Lot { get; set; }
        public string Command { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeUntil { get; set; }
        public bool OutOfSchedule { get; set; }
        public bool CriticalBusiness { get; set; }
        #endregion

        #region Model Extended
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public string TimeFromString { get; set; }
        public string TimeUntilString { get; set; }
        public string StatusColorEE { get; set; }
        public string StatusColorEC { get; set; }
        #endregion
    }
}
