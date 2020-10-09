using System;
    
namespace AST_ProBatch_Mobile.Models.Service
{
    public class BatchQueryLot
    {
        public Int32 IdLot { get; set; }
        public string Code { get; set; }
        public string NameLot { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public string PathIn { get; set; }
        public string PathOut { get; set; }
        public string IdStatus { get; set; }
        public string Status { get; set; }
        public bool IsTransfer { get; set; }
    }
}
