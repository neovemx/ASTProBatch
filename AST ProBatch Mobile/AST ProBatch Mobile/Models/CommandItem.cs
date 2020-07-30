using System;
using System.Collections.Generic;
using System.Text;

namespace AST_ProBatch_Mobile.Models
{
    public class CommandItem
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public bool IsEnabled { get; set; }
        public string Title { get; set; }
        public string Notifications { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public string ExecutionStart { get; set; }
        public string ExecutionEnd { get; set; }
        public string Duration { get; set; }
        public bool IsEventual { get; set; }
        public bool IsNotEventual { get; set; }
    }
}
