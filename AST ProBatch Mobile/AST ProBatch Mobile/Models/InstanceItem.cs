using System;
using System.Collections.Generic;
using System.Text;

namespace AST_ProBatch_Mobile.Models
{
    public class InstanceItem
    {
        #region Atributes
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public string Title { get; set; }
        public string Notifications { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public string Execution { get; set; }
        public string StatusLastProcess { get; set; }
        public string StatusLastProcessColor { get; set; }
        public int CommandsTotal { get; set; }
        public int CommandsPending { get; set; }
        public int CommandsOk { get; set; }
        public int CommandsError { get; set; }
        public int CommandsOmitted { get; set; }
        public bool IsEventual { get; set; }
        public bool IsNotEventual { get; set; }
        #endregion
    }
}
