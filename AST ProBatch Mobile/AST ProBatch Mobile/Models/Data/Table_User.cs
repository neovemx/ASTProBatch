using SQLite;

namespace AST_ProBatch_Mobile.Models
{
    public class Table_User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Username { get; set; }
        [MaxLength(250)]
        public string Password { get; set; }
    }
}
