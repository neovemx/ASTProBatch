using System;
using System.Collections.Generic;

namespace AST_ProBatch_Mobile.Models
{
    public class LogItem
    {
        #region Original Model From API
        public Int32 IdLog { get; set; }
        public string NameLog { get; set; }
        public bool IsEventual { get; set; }
        public Int16 IdEnvironment { get; set; }
        public string NameEnvironment { get; set; }
        public DateTime ExecutionDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        public Int32 TotalCommands { get; set; }
        public Int32 ErrorCommands { get; set; }
        public Int32 TotalPending { get; set; }
        public Int32 TotalOk { get; set; }
        public Int32 TotalOmitt { get; set; }
        public Int32 TotalExecuted { get; set; }
        public bool ActionBlock { get; set; }
        public bool ActionExecute { get; set; }
        public bool ActionRelease { get; set; }
        public bool AccionOpen { get; set; }
        public bool AccionSelection { get; set; }
        #endregion

        #region Model Extended
        public bool IsExecution { get; set; }
        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public bool NotIsEventual { get; set; }
        public string IdUser { get; set; }
        public string Operator { get; set; }
        public string NotificationIcon { get; set; }
        public string BarItemColor { get; set; }
        public string ExecutionDateTimeString { get; set; }
        public string EndingDateTimeString { get; set; }
        #endregion

        //public string Status { get; set; }
        //public string StatusColor { get; set; }
    }
}
