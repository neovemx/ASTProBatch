using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class Log
    {
        public Int32? IdLog { get; set; }
        public string NameLog { get; set; }
        public bool? IsEventual { get; set; }
        public Int16? IdEnvironment { get; set; }
        public string NameEnvironment { get; set; }
        public DateTime? ExecutionDateTime { get; set; }
        public DateTime? EndingDateTime { get; set; }
        public Int32? TotalCommands { get; set; }
        public Int32? ErrorCommands { get; set; }
        public Int32 TotalExecuted { get; set; }
        public bool ActionBlock { get; set; }
        public bool ActionExecute { get; set; }
        public bool ActionRelease { get; set; }
        public bool AccionOpen { get; set; }
        public bool AccionSelection { get; set; }
    }
}
