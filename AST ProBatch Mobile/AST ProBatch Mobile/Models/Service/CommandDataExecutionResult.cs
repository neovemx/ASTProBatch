using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class CommandDataExecutionResult
    {
        public Int32 IdInstance { get; set; }
        public string Environment { get; set; }
        public string ExecutionCommand { get; set; }
        public string XmlExecutionCommand { get; set; }
    }
}
