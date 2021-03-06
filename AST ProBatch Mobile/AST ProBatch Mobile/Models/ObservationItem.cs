﻿using System;

namespace AST_ProBatch_Mobile.Models
{
    public class ObservationItem
    {
        #region Original Model From Api
        public Int32 IdObsv { get; set; }
        public DateTime DateObsv { get; set; }
        public string NameObsv { get; set; }
        public string IdUser { get; set; }
        public string DetailObsv { get; set; }
        #endregion

        #region Model Extended
        public string DateObsvString { get; set; }
        #endregion
    }
}
