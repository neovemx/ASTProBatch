using SQLite;

namespace AST_ProBatch_Mobile.Models
{
    public class Table_Config
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(250)]
        public string UrlDomain { get; set; }
        [MaxLength(250)]
        public string UrlPrefix { get; set; }
        public bool FingerPrintAllow { get; set; }
    }
}
