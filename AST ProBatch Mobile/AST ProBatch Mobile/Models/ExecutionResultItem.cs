using System;

namespace AST_ProBatch_Mobile.Models
{
    public class ExecutionResultItem
    {
        #region Original Model From API
        public Int32 Id { get; set; }
        public string Result { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion

        #region Model Extended
        public string ResultName { get; set; }
        #endregion
    }
}
