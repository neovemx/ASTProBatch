using System;
using System.Collections.ObjectModel;

namespace AST_ProBatch_Mobile.Models
{
    public class InstanceItem
    {
        #region Original Model From API
        public Int32 IdInstance { get; set; }
        public Int16 InstanceNumber { get; set; }
        public string NameInstance { get; set; }
        public string IdStatusInstance { get; set; }
        public string IdStatusLastCommand { get; set; }
        public string NameLastCommand { get; set; }
        public Int32 TotalCommands { get; set; }
        public Int32 PendingCommands { get; set; }
        public Int32 OkCommands { get; set; }
        public Int32 ErrorCommands { get; set; }
        public Int32 OmittedCommands { get; set; }
        public bool ActionExecute { get; set; }
        public bool ActionKill { get; set; }
        public bool ActionStop { get; set; }
        public bool ActionOpen { get; set; }
        #endregion

        #region Model Extended
        public bool IsExecution { get; set; }
        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public string NotificationIcon { get; set; }      
        public string StatusInstanceColor { get; set; }      
        public string StatusLastProcessColor { get; set; }
        public LogItem LogItem { get; set; }
        public string BarItemColor { get; set; }
        //public string StatusInstance { get; set; }
        //public string StatusLastProcess { get; set; }
        //public int Id { get; set; }
        //public string Title { get; set; }
        //public string Execution { get; set; }
        //public int CommandsTotal { get; set; }
        //public int CommandsPending { get; set; }
        //public int CommandsOk { get; set; }
        //public int CommandsError { get; set; }
        //public int CommandsOmitted { get; set; }
        //public bool IsEventual { get; set; }
        //public bool IsNotEventual { get; set; }
        //public ObservableCollection<LotItem> LotItems;
        #endregion
    }
}
