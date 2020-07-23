using System;
using System.Collections.Generic;
using System.Text;

namespace AST_ProBatch_Mobile.Models
{
    public class RecurrenceItem
    {
        #region Atributes
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public int PID { get; set; }
        public string Title { get; set; }
        public string Lot { get; set; }
        public string Command { get; set; }
        public string Environment { get; set; }
        public string Service { get; set; }
        public string Recurrence { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string State { get; set; }
        public string StateColor { get; set; }
        public string Execution { get; set; }
        #endregion
    }
}
