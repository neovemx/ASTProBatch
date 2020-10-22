using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class LogExecutionDelayResult
    {
        public Int32 Id { get; set; }
        public Int32 IdTemplate { get; set; }
        public string Template { get; set; }
        public string Log { get; set; }
        public Int32 IdLot { get; set; }
        public string Lot { get; set; }
        public Int32 IdCommand { get; set; }
        public string Command { get; set; }
        public string Event { get; set; }
        public Int32 IdEvent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DelayTime { get; set; }
        public string User { get; set; }
        public string Supervisor { get; set; }
    }
}
