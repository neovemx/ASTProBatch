using System;

namespace AST_ProBatch_Mobile.Models
{
    public class CommandDataExecutionHistoryItem
    {
        #region Original Model From API
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ExecutionTime { get; set; }
        public string Name { get; set; }
        public bool IsEventual { get; set; }
        public bool IsAutomatic { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }
        #endregion

        #region Model Extended
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        #endregion
    }
}
