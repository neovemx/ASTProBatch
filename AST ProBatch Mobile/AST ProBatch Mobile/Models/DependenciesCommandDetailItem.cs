using System;

namespace AST_ProBatch_Mobile.Models
{
    public class DependenciesCommandDetailItem
    {
        #region MyRegionOriginal Model From API
        public Int32 IdCommand { get; set; }
        public bool Skip { get; set; }
        public string Type { get; set; }
        public Int32 IdLot { get; set; }
        public string Lot { get; set; }
        public string Command { get; set; }
        public string NameType { get; set; }
        public Int16 Number { get; set; }
        public string IdStatus { get; set; }
        public bool CriticalBusiness { get; set; }
        #endregion

        #region Model Extended
        public string StatusColor { get; set; }
        public string Instance { get; set; }
        #endregion
    }
}
