using System;
using System.Collections.Generic;
using System.Text;

namespace AST_ProBatch_Mobile.Models
{
    public class CommandsToControl
    {
        #region Atributes
        public string LogTitle { get; set; }
        public int Id { get; set; }
        public int Instance { get; set; }
        public string InstanceName { get; set; }
        public int Lot { get; set; }
        public string LotName { get; set; }
        public int Command { get; set; }
        public string CommandName { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string ControlStart { get; set; }
        public string ControlEnd { get; set; }
        public string StatusColorEE { get; set; }
        public string StatusColorEC { get; set; }
        #endregion
    }
}
