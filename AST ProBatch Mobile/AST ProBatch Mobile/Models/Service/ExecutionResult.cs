using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class ExecutionResult
    {
        public Int32 Id { get; set; }
        public string Result { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
