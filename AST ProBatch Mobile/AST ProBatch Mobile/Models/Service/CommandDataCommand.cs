using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class CommandDataCommand
    {
        public Int32 IdCommand { get; set; }
        public string Command { get; set; }
        public string DefaultPath { get; set; }
        public string CurrentPath { get; set; }
        public bool HasSubProcess { get; set; }
        public string ServiceMonitoring { get; set; }
        public bool IsUser { get; set; }
        public bool IsProcess { get; set; }
        public Int16 MonitoringTime { get; set; }
        public bool EndSubProcess { get; set; }
    }
}
