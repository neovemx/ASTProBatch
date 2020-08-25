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
        public string ExecutionDateTime { get; set; }
        public string EndingDateTime { get; set; }
        public Int32 TotalCommands { get; set; }
        public Int32 ErrorCommands { get; set; }
        #endregion

        #region Model Extended
        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public bool NotIsEventual { get; set; }
        public string IdUser { get; set; }
        public string Operator { get; set; }
        public string NotificationIcon { get; set; }
        public string BarItemColor { get; set; }
        #endregion

        //public string Status { get; set; }
        //public string StatusColor { get; set; }
    }
}
