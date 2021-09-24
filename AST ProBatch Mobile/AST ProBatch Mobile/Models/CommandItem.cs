using System;

namespace AST_ProBatch_Mobile.Models
{
    public class CommandItem
    {
        #region Original Model From API
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
        #endregion

        #region Model Extended
        public bool IsExecution { get; set; }
        public string Duration { get; set; }
        public string ExecutionStart { get; set; }
        public string ExecutionEnd { get; set; }
        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public string StatusColor { get; set; }
        public InstanceItem InstanceItem { get; set; }
        public bool Critical { get; set; }
        public string BarItemColor { get; set; }
        //public int Id { get; set; }
        //public int IdLot { get; set; }
        //public string TitleLot { get; set; }
        //public int IdEnvironment { get; set; }
        //public string TitleEnvironment { get; set; }
        //public string Title { get; set; }
        //public string Notifications { get; set; }
        //public string Status { get; set; }
        //public string ExecutionStart { get; set; }
        //public string ExecutionEnd { get; set; }
        //public string Duration { get; set; }
        //public bool IsEventual { get; set; }
        //public bool IsNotEventual { get; set; }
        //public bool Critical { get; set; }
        #endregion
    }
}
