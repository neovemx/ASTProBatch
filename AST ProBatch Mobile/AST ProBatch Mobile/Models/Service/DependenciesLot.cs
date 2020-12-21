using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class DependenciesLot
    {
        public Int32 IdLot { get; set; }
        public Int64 Code { get; set; }
        public string NameLot { get; set; }
        public string Type { get; set; }
        public string NameType { get; set; }
        public string IdStatus { get; set; }
        public string Status { get; set; }
        public DateTime? AddedDate { get; set; }
        public Int16 Number { get; set; }
    }
}
