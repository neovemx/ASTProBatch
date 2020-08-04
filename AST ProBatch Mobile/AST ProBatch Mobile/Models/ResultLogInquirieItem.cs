using System;
using System.Collections.Generic;
using System.Text;

namespace AST_ProBatch_Mobile.Models
{
    public class ResultLogInquirieItem
    {
        public string LogTitle { get; set; }
        public int IdLog { get; set; }
        public int IdInstance { get; set; }
        public string NameInstance { get; set; }
        public string StartHour { get; set; }
        public string Pause { get; set; }
        public int IdLot { get; set; }
        public string NameLot { get; set; }
        public int IdCommand { get; set; }
        public string NameCommand { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public string StatusResult { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
    }
}
