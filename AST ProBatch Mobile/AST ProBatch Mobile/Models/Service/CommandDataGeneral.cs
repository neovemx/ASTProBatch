using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class CommandDataGeneral
    {
        public Int32 IdCommand { get; set; }
        public string Code { get; set; }
        public bool CriticalBusiness { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string CommandType { get; set; }
        public bool ReExecution { get; set; }
    }
}
