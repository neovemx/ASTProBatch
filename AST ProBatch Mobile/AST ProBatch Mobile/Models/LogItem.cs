﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AST_ProBatch_Mobile.Models
{
    public class LogItem
    {
        #region Constructor
        //public LogItem()
        //{
        //    TargetType = typeof(LogItem);
        //}
        #endregion

        #region Atributes
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public string Title { get; set; }
        public string Notifications { get; set; }
        public string State { get; set; }
        public string Environment { get; set; }
        public string Execution { get; set; }
        //public Type TargetType { get; set; } 
        #endregion
    }
}
