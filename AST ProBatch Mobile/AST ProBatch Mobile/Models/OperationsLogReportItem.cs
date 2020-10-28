using System;

namespace AST_ProBatch_Mobile.Models
{
    public class OperationsLogReportItem
    {
        #region Original Model From API
        public Int64 IdLog { get; set; }
        public Int32 IdInstance { get; set; }
        public Int32 IdLot { get; set; }
        public Int32 IdCommand { get; set; }
        public string IdUser { get; set; }
        public string Environment { get; set; }
        public string Service { get; set; }
        public string CodeCommand { get; set; }
        public string NameCommand { get; set; }
        public string Product { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
        public string IPClient { get; set; }
        public string NameLog { get; set; }
        public Int16 InstanceNumber { get; set; }
        public Int32 CodeLot { get; set; }
        public string NameLot { get; set; }
        public string Source { get; set; }
        public bool Critical { get; set; }
        #endregion

        #region Model Extended
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        #endregion
    }
}
