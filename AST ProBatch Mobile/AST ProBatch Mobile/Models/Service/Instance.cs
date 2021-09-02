using System;

namespace AST_ProBatch_Mobile.Models.Service
{
    public class Instance
    {
        public Int32 IdInstance { get; set; }
        public Int16 InstanceNumber { get; set; }
        public string NameInstance { get; set; }
        public string IdStatusInstance { get; set; }
        public string IdStatusLastCommand { get; set; }
        public Int32 TotalCommands { get; set; }
        public Int32 PendingCommands { get; set; }
        public Int32 OkCommands { get; set; }
        public Int32 ErrorCommands { get; set; }
        public Int32 OmittedCommands { get; set; }
        public bool ActionExecute { get; set; }
        public bool ActionKill { get; set; }
        public bool ActionStop { get; set; }
        public bool ActionOpen { get; set; }
    }
}
