using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class BatchQueryCommand
    {
        public Int32 IdCommand { get; set; }
        public string Command { get; set; }
        public bool Critical { get; set; }
        public string IdStatus { get; set; }
        public Int16 Order { get; set; }
        public bool Rejection { get; set; }
        public bool ItDepends { get; set; }
        public bool Depended { get; set; }
        public bool CriticalBusiness { get; set; }
        public string Omission { get; set; }
        public string NameOmission { get; set; }
    }
}
