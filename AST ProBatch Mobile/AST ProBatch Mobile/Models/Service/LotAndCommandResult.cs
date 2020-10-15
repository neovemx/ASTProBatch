using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class LotAndCommandResult
    {
        public Int32 IdTemplate { get; set; }
        public string Template { get; set; }
        public Int32 IdLog { get; set; }
        public string Log { get; set; }
        public string Instance { get; set; }
        public Int32 IdLot { get; set; }
        public string Lot { get; set; }
        public Int32 IdCommand { get; set; }
        public string Command { get; set; }
        public Int16 IdCommandGroup { get; set; }
        public string CommandGroup { get; set; }
        public Int16 IdEnvironment { get; set; }
        public string Environment { get; set; }
        public string ExecutionTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public string IdStatus { get; set; }
        public string Status { get; set; }
    }
}
