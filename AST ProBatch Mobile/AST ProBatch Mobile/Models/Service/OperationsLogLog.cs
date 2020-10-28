using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class OperationsLogLog
    {
        public Int32 IdLog { get; set; }
        public string NameLog { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string Environment { get; set; }
        public bool Eventual { get; set; }
    }
}
