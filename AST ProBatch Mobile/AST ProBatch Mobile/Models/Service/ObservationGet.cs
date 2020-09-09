using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class ObservationGet
    {
        public Int32 IdObsv { get; set; }
        public DateTime DateObsv { get; set; }
        public string NameObsv { get; set; }
        public string IdUser { get; set; }
        public string DetailObsv { get; set; }
    }
}
