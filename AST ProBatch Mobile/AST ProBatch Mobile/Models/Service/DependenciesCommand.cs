using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class DependenciesCommand
    {
        public Int32 IdCommand { get; set; }
        public bool Skip { get; set; }
        public string Type { get; set; }
        public Int32 IdLot { get; set; }
        public string Lot { get; set; }
        public string Command { get; set; }
        public Int16 LotOrder { get; set; }
        public string NameType { get; set; }
        public string Status { get; set; }
        public DateTime? AddedDate { get; set; }
        public Int16 Number { get; set; }
        public string IdStatus { get; set; }
        public bool CriticalBusiness { get; set; }
    }
}
