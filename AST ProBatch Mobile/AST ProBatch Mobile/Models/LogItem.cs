using System;

namespace AST_ProBatch_Mobile.Models
{
    public class LogItem
    {
        public Int32 IdLog { get; set; }
        public string NameLog { get; set; }
        public bool IsEventual { get; set; }
        public Int16 IdEnvironment { get; set; }
        public string NameEnvironment { get; set; }
        public string ExecutionDateTime { get; set; }
        public string EndingDateTime { get; set; }
        public Int32 TotalCommands { get; set; }
        public Int32 ErrorCommands { get; set; }

        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public bool NotIsEventual { get; set; }
        public string Operator { get; set; }
        public string NotificationIcon { get; set; }
        public string BarItemColor { get; set; }

        //public string Status { get; set; }
        //public string StatusColor { get; set; }
        //public string Title { get; set; }    
        //public string Environment { get; set; }
        //public string Execution { get; set; }
        //public string End { get; set; }
        //public string CommandsNumber { get; set; }
        //public string CommandsFail { get; set; }
        //public bool Eventual { get; set; }
    }
}
