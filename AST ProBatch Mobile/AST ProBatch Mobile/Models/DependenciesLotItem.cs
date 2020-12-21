using System;
namespace AST_ProBatch_Mobile.Models
{
    public class DependenciesLotItem
    {
        #region Original Model From API
        public Int32 IdLot { get; set; }
        public Int64 Code { get; set; }
        public string NameLot { get; set; }
        public string Type { get; set; }
        public string NameType { get; set; }
        public string Status { get; set; }
        public DateTime? AddedDate { get; set; }
        public Int16 Number { get; set; }
        #endregion

        #region Model Extended
        public string Instance { get; set; }
        public string StatusColor { get; set; }
        #endregion
    }
}
