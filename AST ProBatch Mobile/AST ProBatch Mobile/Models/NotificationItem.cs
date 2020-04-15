using System;
using System.Collections.Generic;
using System.Text;

namespace AST_ProBatch_Mobile.Models
{
    public class NotificationItem
    {
        #region Constructor
        //public NotificationItem()
        //{
        //    TargetType = typeof(NotificationItem);
        //}
        #endregion

        #region Atributes
        public int Id { get; set; }
        public bool IsReaded { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationText { get; set; }
        public string State { get; set; }
        public string StatusColor { get; set; }
        public string DateTime { get; set; }
        //public Type TargetType { get; set; }
        #endregion
    }
}
