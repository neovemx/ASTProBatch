using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class Command
    {
        public Int32 IdLot { get; set; }
        public string NameLot { get; set; }
        public Int32 IdCommand { get; set; }
        public string NameCommand { get; set; }
        public Int16 Order { get; set; }
        public string IdStatus { get; set; }
        public Int32 ExecutionTime { get; set; }
        public DateTime? ExecutionDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool RequestParameters { get; set; }
        public bool ActionRun { get; set; }
        public bool ActionSkip { get; set; }
        public bool ActionPause { get; set; }
        public bool ActionReRunOnlyThis { get; set; }
        public bool ActionReRunThisMoreAllNext { get; set; }
        public bool ActionReRunThisMoreNotExecutedNext { get; set; }
        public bool ActionKill { get; set; }
        public bool ActionGetItBack { get; set; }
    }
}
