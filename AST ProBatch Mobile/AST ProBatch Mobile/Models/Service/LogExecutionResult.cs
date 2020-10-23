using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class LogExecutionResult
    {
        public Int32 IdTemplate { get; set; }
        public string Template { get; set; }
        public Int32 IdLog { get; set; }
        public string Log { get; set; }
        public Int16 IdEnvironment { get; set; }
        public string Environment { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ExecutionTime { get; set; }
    }
}
