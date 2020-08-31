using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class Command
    {
        public Int32 IdLot { get; set; }
        public string NameLot { get; set; }
        public Int32 IdCommand { get; set; }
        public string NameCommand { get; set; }
        public Int16 Order { get; set; }
        public string IdStatus { get; set; }
        public Int32 ExecutionTime { get; set; }
        public DateTime? ExecutionDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
