using System;

namespace AST_ProBatch_Mobile.Models
{
    public class ExecutionResultErrorLogItem
    {
        #region Original Model From API
        public Int32 Id { get; set; }
        public string Error { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Model Extended
        public string ErrorName { get; set; }
        #endregion
    }
}
