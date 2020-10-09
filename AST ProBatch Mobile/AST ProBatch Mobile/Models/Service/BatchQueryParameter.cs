using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class BatchQueryParameter
    {
        public Int32 IdParameter { get; set; }
        public string NameParameter { get; set; }
        public string Value { get; set; }
        public string IdTypeParameter { get; set; }
        public string IdTypeData { get; set; }
        public bool Edit { get; set; }
        public bool Hidden { get; set; }
        public string IdStatus { get; set; }
    }
}
